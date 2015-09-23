﻿using StockAnalyzer.StockClasses;
using StockAnalyzer.Portofolio;
using StockAnalyzer.StockClasses.StockViewableItems.StockTrailStops;

namespace StockAnalyzer.StockStrategyClasses
{
    public class TrailVolStrategy : StockStrategyBase
    {
        #region StockStrategy Properties
        override public string Description
        {
            get { return "This strategy buys and sells using trail volume indicator"; }
        }
        #endregion
        #region StockStrategy Methods

        private IStockTrailStop trailStop;
        public TrailVolStrategy()
        {
            this.TriggerIndicator = StockTrailStopManager.CreateTrailStop("TRAILVOL()");
            trailStop = (IStockTrailStop) this.TriggerIndicator;
        }

        override public StockOrder TryToBuy(StockDailyValue dailyValue, int index, float amount, ref float benchmark)
        {
            benchmark = dailyValue.CLOSE;

            if (!this.Serie.HasVolume) // This is a volume based strategy
            { return null; }
            if (this.trailStop == null)
            { return null; }

            #region Create Buy Order
            if (this.SupportShortSelling)
            {
                if (float.IsNaN(trailStop.Series[1][index - 1]) && !float.IsNaN(trailStop.Series[1][index]))
                {
                    return StockOrder.CreateBuyAtMarketOpenStockOrder(dailyValue.NAME, dailyValue.DATE, dailyValue.DATE.AddDays(30), amount, true);
                }
            }
            if (float.IsNaN(trailStop.Series[0][index - 1] ) && !float.IsNaN(trailStop.Series[0][index]))
            {
                return StockOrder.CreateBuyAtMarketOpenStockOrder(dailyValue.NAME, dailyValue.DATE, dailyValue.DATE.AddDays(30), amount, false);
            }
            return null;
            #endregion
        }
        override public StockOrder TryToSell(StockDailyValue dailyValue, int index, int number, ref float benchmark)
        {
            benchmark = dailyValue.CLOSE;

            #region Create Sell Order
            StockOrder stockOrder = null;
            // Review buy limit according to indicators
            if (LastBuyOrder.IsShortOrder)
            {
                if (float.IsNaN(trailStop.Series[0][index - 1])&& !float.IsNaN(trailStop.Series[0][index]))
                {
                    stockOrder = StockOrder.CreateSellAtMarketOpenStockOrder(dailyValue.NAME, dailyValue.DATE, dailyValue.DATE.AddDays(30), number, true);
                }
            }
            else
            {
                if (float.IsNaN(trailStop.Series[1][index - 1]) && !float.IsNaN(trailStop.Series[1][index]))
                {
                    stockOrder = StockOrder.CreateSellAtMarketOpenStockOrder(dailyValue.NAME, dailyValue.DATE, dailyValue.DATE.AddDays(30), number, false);
                }
            }
            #endregion
            return stockOrder;
        }
        #endregion
    }
}
