﻿using StockAnalyzer.StockMath;
using System;
using System.Drawing;

namespace StockAnalyzer.StockClasses.StockViewableItems.StockClouds
{
    public class StockCloud_TREND : StockCloudBase
    {
        public override IndicatorDisplayTarget DisplayTarget
        {
            get { return IndicatorDisplayTarget.PriceIndicator; }
        }
        public override string[] ParameterNames
        {
            get { return new string[] { "FastPeriod", "SlowPeriod" }; }
        }
        public override string Definition => "Paint cloud based on HL";

        public override Object[] ParameterDefaultValues
        {
            get { return new Object[] { 20, 50 }; }
        }
        public override ParamRange[] ParameterRanges
        {
            get { return new ParamRange[] { new ParamRangeInt(1, 500), new ParamRangeInt(1, 500) }; }
        }
        public override Pen[] SeriePens
        {
            get
            {
                if (seriePens == null)
                {
                    seriePens = new Pen[] { new Pen(Color.Green, 1), new Pen(Color.DarkRed, 1) };
                }
                return seriePens;
            }
        }
        public override void ApplyTo(StockSerie stockSerie)
        {
            var fastPeriod = (int)this.parameters[0];
            var lowSerie = stockSerie.GetSerie(StockDataType.LOW);
            var highSerie = stockSerie.GetSerie(StockDataType.HIGH);
            var closeSerie = stockSerie.GetSerie(StockDataType.CLOSE);

            var bullSerie = new FloatSerie(stockSerie.Count);
            var bearSerie = new FloatSerie(stockSerie.Count);

            var high = bullSerie[0] = highSerie[0];
            var low = bearSerie[0] = lowSerie[0];
            int i = 1;
            bool broken = false;
            bool bullish = false;
            while (!broken) // Prepare trend
            {
                if (closeSerie[i] > high) // Broken up
                {
                    bullish = true;
                    broken = true;
                    high = bullSerie[i] = highSerie[i - 1];
                    low = bearSerie[i] = lowSerie[i - 1];
                }
                else if (closeSerie[i] < low) // Broken Down
                {
                    bullish = false;
                    broken = true;
                    high = bullSerie[i] = highSerie[i - 1];
                    low = bearSerie[i] = lowSerie[i - 1];
                }
                else
                {
                    bullSerie[i] = bullSerie[i - 1];
                    bearSerie[i] = bearSerie[i - 1];
                    i++;
                }
            }

            for (i = 1; i < stockSerie.Count; i++)
            {
                if (bullish)
                {
                    if (closeSerie[i] < low) // Bull run broken
                    {
                        bullish = false;
                        low = lowSerie[i];
                        bullSerie[i] = low;
                        bearSerie[i] = high;
                    }
                    else // Bull run continues
                    {
                        low = Math.Max(low, lowSerie.GetMin(Math.Max(0, i - fastPeriod), i));
                        high = Math.Max(high, highSerie[i]);
                        bullSerie[i] = high;
                        bearSerie[i] = low;
                    }
                }
                else
                {
                    if (closeSerie[i] > high) // Bear run broken
                    {
                        bullish = true;
                        high = highSerie[i];
                        bullSerie[i] = high;
                        bearSerie[i] = low;
                    }
                    else // Bear run continues
                    {
                        low = Math.Min(low, lowSerie[i]);
                        high = Math.Min(high, highSerie.GetMax(Math.Max(0, i - fastPeriod), i));
                        bullSerie[i] = low;
                        bearSerie[i] = high;
                    }
                }
            }

            this.Series[0] = bullSerie;
            this.Series[1] = bearSerie;

            // Detecting events
            this.GenerateEvents(stockSerie);
        }
    }
}
