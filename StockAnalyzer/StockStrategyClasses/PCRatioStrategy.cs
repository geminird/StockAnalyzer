﻿using StockAnalyzer.StockClasses;
using StockAnalyzer.Portofolio;
using StockAnalyzer.StockMath;

namespace StockAnalyzer.StockStrategyClasses
{
    public class PCRatioStrategy : StockStrategyBase
    {
        #region StockStrategy Properties
        override public string Description
        {
            get { return "This strategy buys and sells using trailing orders, there is no real strategy, just create a buy or sell order according to the parameters"; }
        }
        #endregion
        #region StockStrategy Methods

        override public StockOrder TryToBuy(StockDailyValue dailyValue, int index, float amount, ref float benchmark)
        {
            benchmark = dailyValue.CLOSE;

            #region Create Buy Order
            // Get PCR indicators
            StockOrder stockOrder = null;
            // Review buy limit according to indicators
            if (StockDictionary.StockDictionarySingleton.ContainsKey("PCR.EQUITY"))
            {
                FloatSerie pcRatioEMA6 = this.Serie.GetIndicator("EMA(6)").Series[0];
                FloatSerie pcRatioEMA12 = this.Serie.GetIndicator("EMA(12)").Series[0];
                if (pcRatioEMA12[index] < pcRatioEMA6[index])
                {
                    stockOrder = StockOrder.CreateBuyAtMarketOpenStockOrder(dailyValue.NAME, dailyValue.DATE, dailyValue.DATE.AddDays(5), amount, false);
                }
            }
            #endregion
            return stockOrder;
        }
        override public StockOrder TryToSell(StockDailyValue dailyValue, int index, int number, ref float benchmark)
        {
            benchmark = dailyValue.CLOSE;

            #region Create Sell Order
            StockOrder stockOrder = null;
            if (StockDictionary.StockDictionarySingleton.ContainsKey("PCR.EQUITY"))
            {
                FloatSerie pcRatioEMA6 = this.Serie.GetIndicator("EMA(6)").Series[0];
                FloatSerie pcRatioEMA12 = this.Serie.GetIndicator("EMA(12)").Series[0];

                // Review sell limit according to indicators
                if (pcRatioEMA12[index] > pcRatioEMA6[index])
                {
                    stockOrder = StockOrder.CreateSellAtMarketOpenStockOrder(dailyValue.NAME, dailyValue.DATE, dailyValue.DATE.AddDays(5), number, false);
                }
            }
            #endregion
            return stockOrder;
        }
        #endregion
    }
}
