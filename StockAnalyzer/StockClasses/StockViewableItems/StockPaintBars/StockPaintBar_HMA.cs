﻿using System.Drawing;

namespace StockAnalyzer.StockClasses.StockViewableItems.StockPaintBars
{
   public class StockPaintBar_HMA : StockPaintBarIndicatorEventBase
   {
      public override System.Drawing.Pen[] SeriePens
      {
         get
         {
            if (seriePens == null)
            {
                    //  "Bottom", "Top", "CloseAbove", "CloseBelow", "FirstBarAbove", "FirstBarBelow", "Bullish", "Bearish" , "BullRun", "BearRun", "Rising", "Falling" 
                    seriePens = new Pen[] { new Pen(Color.Green), new Pen(Color.Red), new Pen(Color.Green), new Pen(Color.Red), new Pen(Color.Green), new Pen(Color.Red),
                        new Pen(Color.Green), new Pen(Color.Red), new Pen(Color.Green), new Pen(Color.Red), new Pen(Color.Green), new Pen(Color.Red) };
            }
            return seriePens;
         }
      }
   }
}
