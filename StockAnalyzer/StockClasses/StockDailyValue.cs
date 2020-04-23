﻿using System;
using System.Globalization;
using System.IO;

namespace StockAnalyzer.StockClasses
{
    public enum StockDataType
    {
        NONE = -1,
        CLOSE = 0,
        OPEN,
        HIGH,
        LOW,
        AVG,
        ATR,
        VARIATION,
        VOLUME
    };

    public class StockDailyValue
    {
        public string NAME { get; set; }
        public DateTime DATE { get; set; }
        public float OPEN { get; set; }
        public float HIGH { get; set; }
        public float LOW { get; set; }
        public float CLOSE { get; set; }
        public float PreviousClose { get; set; }
        public float AVG { get; set; }
        public float ATR { get; set; }
        public long VOLUME { get; set; }
        public long UPVOLUME { get; set; }
        public int TICK { get; set; }
        public int UPTICK { get; set; }
        public float VARIATION { get; set; }
        public float AMPLITUDE { get; set; }
        public float Range { get { return this.HIGH - this.LOW; } }
        public void CalculateUpVolume()
        {
            if (this.UPVOLUME == 0)
            {
                float range = this.Range;
                if (this.CLOSE > this.OPEN && range != 0.0f) this.UPVOLUME = (long)(range / (2 * range + this.OPEN - this.CLOSE) * this.VOLUME);
                if (this.CLOSE < this.OPEN && range != 0.0f) this.UPVOLUME = (long)((range + this.CLOSE - this.OPEN) / (2 * range + this.CLOSE - this.OPEN) * this.VOLUME);
                if (this.CLOSE == this.OPEN) this.UPVOLUME = (long)(0.5f * this.VOLUME);
            }
        }

        private static CultureInfo frenchCulture = CultureInfo.GetCultureInfo("fr-FR");
        private static CultureInfo usCulture = CultureInfo.GetCultureInfo("en-US");

        public StockSerie Serie { get; set; }

        public float GetStockData(StockDataType dataType)
        {
            if (dataType == StockDataType.VOLUME)
            {
                return (float)this.VOLUME;
            }
            Type type = this.GetType();
            System.Reflection.PropertyInfo propInfo = type.GetProperty(dataType.ToString());
            if (propInfo == null)
            {
                return Serie.GetSerie(dataType).Values[Serie.IndexOf(this.DATE)];
            }
            else
            {
                return (float)propInfo.GetValue(this, null);
            }
        }

        public bool Equals(StockDataType dataType, StockDailyValue dailyValue, float accuracyPercent)
        {
            float thisValue = this.GetStockData(dataType);
            float otherValue = dailyValue.GetStockData(dataType);
            float accuracy = thisValue * accuracyPercent;
            if (((thisValue - accuracy) <= otherValue) && ((thisValue + accuracy) >= otherValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Lower(StockDataType dataType, StockDailyValue dailyValue, float accuracyPercent)
        {
            float thisValue = this.GetStockData(dataType);
            float otherValue = dailyValue.GetStockData(dataType);
            float accuracy = thisValue * accuracyPercent;
            if (((thisValue + accuracy) < otherValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool LowerOrEquals(StockDataType dataType, StockDailyValue dailyValue, float accuracyPercent)
        {
            float thisValue = this.GetStockData(dataType);
            float otherValue = dailyValue.GetStockData(dataType);
            float accuracy = thisValue * accuracyPercent;
            if (((thisValue - accuracy) <= otherValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Greater(StockDataType dataType, StockDailyValue dailyValue, float accuracyPercent)
        {
            float thisValue = this.GetStockData(dataType);
            float otherValue = dailyValue.GetStockData(dataType);
            float accuracy = thisValue * accuracyPercent;
            if (((thisValue - accuracy) > otherValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool GreaterOrEquals(StockDataType dataType, StockDailyValue dailyValue, float accuracyPercent)
        {
            float thisValue = this.GetStockData(dataType);
            float otherValue = dailyValue.GetStockData(dataType);
            float accuracy = thisValue * accuracyPercent;
            if (((thisValue + accuracy) >= otherValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public StockDailyValue()
        {
        }

        public StockDailyValue(string name, float open, float high, float low, float close, long volume, DateTime date)
        {
            this.NAME = name;
            this.DATE = date;
            if (open == 0.0f)
            {
                this.OPEN = close;
            }
            else
            {
                this.OPEN = open;
            }
            this.HIGH = Math.Max(Math.Max(Math.Max(high, this.OPEN), close), low);
            if (low == 0.0f)
            {
                this.LOW = Math.Min(this.OPEN, close);
            }
            else
            {
                this.LOW = Math.Min(Math.Min(Math.Min(low, this.OPEN), close), this.HIGH);
            }
            this.CLOSE = close;
            this.VOLUME = volume;
            this.AVG = (open + high + low + 2.0f * close) / 5.0f;
        }
        public StockDailyValue(string name, float open, float high, float low, float close, long volume, long upVolume, int ticks, int upTicks, DateTime date)
        {
            this.NAME = name;
            this.DATE = date;
            if (open == 0.0f)
            {
                this.OPEN = close;
            }
            else
            {
                this.OPEN = open;
            }
            this.HIGH = Math.Max(Math.Max(high, this.OPEN), close);
            if (low == 0.0f)
            {
                this.LOW = Math.Min(this.OPEN, close);
            }
            else
            {
                this.LOW = Math.Min(Math.Min(low, this.OPEN), close);
            }
            this.CLOSE = close;
            this.VOLUME = volume;
            this.UPVOLUME = upVolume;
            this.TICK = ticks;
            this.UPTICK = upTicks;
            this.AVG = (open + high + low + 2.0f * close) / 5.0f;
        }

        #region CSV file IO
        public static StockDailyValue ReadMarketDataFromCSVStream(StreamReader sr, string stockName, bool useAdjusted)
        {
            StockDailyValue stockValue = null;
            try
            {
                // File format
                // Date,Open,High,Low,Close,Volume,Adj Close (UpVolume, Tick, Uptick)
                // 2010-06-18,10435.00,10513.75,10379.60,10450.64,4555360000,10450.64
                string[] row = sr.ReadLine().Split(',');
                if (row.Length == 7)
                {
                    if (useAdjusted || row[4] != row[6])
                    {
                        float close = float.Parse(row[4], usCulture);
                        float adjClose = float.Parse(row[6], usCulture);
                        float adjRatio = adjClose / close;
                        stockValue = new StockDailyValue(
                            stockName,
                            float.Parse(row[1], usCulture) * adjRatio,
                            float.Parse(row[2], usCulture) * adjRatio,
                            float.Parse(row[3], usCulture) * adjRatio,
                            adjClose,
                            long.Parse(row[5], usCulture),
                            DateTime.Parse(row[0], usCulture));
                    }
                    else
                    {
                        stockValue = new StockDailyValue(
                                stockName,
                                float.Parse(row[1], usCulture),
                                float.Parse(row[2], usCulture),
                                float.Parse(row[3], usCulture),
                                float.Parse(row[4], usCulture),
                                long.Parse(row[5], usCulture),
                                DateTime.Parse(row[0], usCulture));
                    }
                }
                if (row.Length == 6)
                {
                    stockValue = new StockDailyValue(
                            stockName,
                            float.Parse(row[1], usCulture),
                            float.Parse(row[2], usCulture),
                            float.Parse(row[3], usCulture),
                            float.Parse(row[4], usCulture),
                            long.Parse(row[5], usCulture),
                            DateTime.Parse(row[0], usCulture));
                }
                else if (row.Length == 10)
                {
                    stockValue = new StockDailyValue(
                                stockName,
                                float.Parse(row[1], usCulture),
                                float.Parse(row[2], usCulture),
                                float.Parse(row[3], usCulture),
                                float.Parse(row[4], usCulture),
                                long.Parse(row[5], usCulture),
                                long.Parse(row[7], usCulture),
                                int.Parse(row[8], usCulture),
                                int.Parse(row[9], usCulture),
                                DateTime.Parse(row[0], usCulture));
                }
            }
            catch (System.Exception)
            {
                // Assume input is right, Ignore invalid lines
            }
            return stockValue;
        }

        static public string StringFormat()
        {
            return "Date,Open,High,Low,Close,Volume,Adj Close,UpVolume,Tick,UpTick";
        }
        public override string ToString()
        {
            return DATE.ToString("s") + "," + OPEN.ToString(usCulture) + "," + HIGH.ToString(usCulture) + "," + LOW.ToString(usCulture) + "," + CLOSE.ToString(usCulture)
                + "," + VOLUME.ToString(usCulture) + "," + CLOSE.ToString(usCulture) + "," + UPVOLUME.ToString(usCulture) + "," + TICK.ToString(usCulture) + "," + UPTICK.ToString(usCulture);
        }
        #endregion

        private bool isComplete = true;
        public bool IsComplete { get { return isComplete; } set { isComplete = value; } }
    }
}
