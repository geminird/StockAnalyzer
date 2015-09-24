﻿using System.Linq;
using StockAnalyzer.Portofolio;
using StockAnalyzer.StockClasses;
using StockAnalyzer.StockClasses.StockViewableItems.StockIndicators;

namespace StockAnalyzer.StockStrategyClasses
{
   public class BBStrategy : StockStrategyBase
   {
      #region StockStrategy Properties
      override public string Description
      {
         get { return "This strategy buys and sells when TRAILHLSR signals happen out of bollinger band"; }
      }
      #endregion
      #region StockStrategy Methods

      private IStockIndicator SRIndicator;
      private IStockIndicator bbBand;

      private int resistanceEventIndex;
      private int supportEventIndex;

      public BBStrategy()
      {
         this.TriggerIndicator = StockIndicatorManager.CreateIndicator("TRAILHLSR(9)");
         SRIndicator = (IStockIndicator)this.TriggerIndicator;

         bbBand = StockIndicatorManager.CreateIndicator("BB(100,1.75,-1.75)");

         supportEventIndex = SRIndicator.EventNames.ToList().IndexOf("SupportDetected");
         resistanceEventIndex = SRIndicator.EventNames.ToList().IndexOf("ResistanceDetected");
      }

      public override void Initialise(StockSerie stockSerie, StockOrder lastBuyOrder, bool supportShortSelling)
      {
         base.Initialise(stockSerie, lastBuyOrder, supportShortSelling);
         this.bbBand.ApplyTo(stockSerie);
      }

      override public StockOrder TryToBuy(StockDailyValue dailyValue, int index, float amount, ref float benchmark)
      {
         benchmark = dailyValue.CLOSE;

         #region Create Buy Order
         if (this.SupportShortSelling)
         {
            //if (float.IsNaN(trailStop.Series[1][index - 1]) && !float.IsNaN(trailStop.Series[1][index]))
            //{
            //    return StockOrder.CreateBuyAtMarketOpenStockOrder(dailyValue.NAME, dailyValue.DATE, dailyValue.DATE.AddDays(30), amount, true);
            //}
         }
         if (this.SRIndicator.Events[supportEventIndex][index] && this.SRIndicator.Series[0][index] < bbBand.Series[1][index])
         {
            return StockOrder.CreateBuyAtMarketOpenStockOrder(dailyValue.NAME, dailyValue.DATE, dailyValue.DATE.AddDays(30), amount, false);
         }

         #endregion
         return null;
      }
      override public StockOrder TryToSell(StockDailyValue dailyValue, int index, int number, ref float benchmark)
      {
         benchmark = dailyValue.CLOSE;

         #region Create Sell Order
         // Review buy limit according to indicators
         if (LastBuyOrder.IsShortOrder)
         {
            //if (float.IsNaN(trailStop.Series[0][index - 1])&& !float.IsNaN(trailStop.Series[0][index]))
            //{
            //    return StockOrder.CreateSellAtMarketOpenStockOrder(dailyValue.NAME, dailyValue.DATE, dailyValue.DATE.AddDays(30), number, true);
            //}
         }
         else
         {
            // Sell in case of Resistance detected
            if (this.SRIndicator.Events[resistanceEventIndex][index])
            {
               return StockOrder.CreateSellAtMarketOpenStockOrder(dailyValue.NAME, dailyValue.DATE,
                   dailyValue.DATE.AddDays(30), number, false);
            }
         }
         #endregion
         return null;
      }
      #endregion
   }
}
