﻿using StockAnalyzer.StockClasses;
using StockAnalyzer.Portofolio;
using StockAnalyzer.StockClasses.StockViewableItems.StockIndicators;
using StockAnalyzer.StockClasses.StockViewableItems.StockTrailStops;

namespace StockAnalyzer.StockStrategyClasses
{
    public class HilbertStopStrategy : StockStrategyBase
    {
        #region StockStrategy Properties
        override public string Description
        {
            get { return "This strategy buys and sells Hilbert support/resistance signals"; }
        }
        #endregion
        #region StockStrategy Methods

        IStockTrailStop trailStop = null;
        
        IStockIndicator hilbertSR;
        public HilbertStopStrategy()
        {
            this.TriggerIndicator = StockIndicatorManager.CreateIndicator("HILBERTSR(3,15)");
            hilbertSR = (IStockIndicator)this.TriggerIndicator;
        }

        public override void Initialise(StockSerie stockSerie, StockOrder lastBuyOrder, bool supportShortSelling)
        {
            base.Initialise(stockSerie, lastBuyOrder, supportShortSelling);

            this.trailStop = StockTrailStopManager.CreateTrailStop("TRAILHL(3)");
            this.trailStop.ApplyTo(stockSerie);
        }

        override public StockOrder TryToBuy(StockDailyValue dailyValue, int index, float amount, ref float benchmark)
        {
            benchmark = dailyValue.CLOSE;

            if (this.TriggerIndicator == null) { return null; }

            if (this.SupportShortSelling)
            {
                if (!float.IsNaN(hilbertSR.Series[1][index]) && this.trailStop.Events[1][index])
                {
                    return StockOrder.CreateBuyAtMarketOpenStockOrder(dailyValue.NAME, dailyValue.DATE, dailyValue.DATE.AddDays(30), amount, true);
                }
            }
            else if (!float.IsNaN(hilbertSR.Series[0][index]) && this.trailStop.Events[0][index])
            {
                return StockOrder.CreateBuyAtMarketOpenStockOrder(dailyValue.NAME, dailyValue.DATE, dailyValue.DATE.AddDays(30), amount, false);
            }
            return null;
        }
        override public StockOrder TryToSell(StockDailyValue dailyValue, int index, int number, ref float benchmark)
        {
            benchmark = dailyValue.CLOSE;

            if (this.TriggerIndicator == null) { return null; }

            if (LastBuyOrder.IsShortOrder)
            {
                if (this.trailStop.Events[0][index])
                {
                    return StockOrder.CreateSellAtMarketOpenStockOrder(dailyValue.NAME, dailyValue.DATE, dailyValue.DATE.AddDays(30), number, true);
                }
            }
            else
            {
                if (this.trailStop.Events[1][index])
                {
                    return StockOrder.CreateSellAtMarketOpenStockOrder(dailyValue.NAME, dailyValue.DATE, dailyValue.DATE.AddDays(30), number, false);
                }
            }
            return null;
        }
        #endregion
    }
}
