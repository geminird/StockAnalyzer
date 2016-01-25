﻿using System;
using System.Drawing;
using StockAnalyzer.StockMath;

namespace StockAnalyzer.StockClasses.StockViewableItems.StockIndicators
{
   public class StockIndicator_EMA : StockIndicatorBase
   {
      public StockIndicator_EMA()
      {
      }
      public override IndicatorDisplayTarget DisplayTarget
      {
         get { return IndicatorDisplayTarget.PriceIndicator; }
      }

      public override string Name
      {
         get { return "EMA(" + this.Parameters[0].ToString() + ")"; }
      }

      public override string Definition
      {
         get { return "EMA(int Period)"; }
      }
      public override object[] ParameterDefaultValues
      {
         get { return new Object[] { 20 }; }
      }
      public override ParamRange[] ParameterRanges
      {
         get { return new ParamRange[] { new ParamRangeInt(1, 500) }; }
      }
      public override string[] ParameterNames
      {
         get { return new string[] { "Period" }; }
      }
      public override string[] SerieNames { get { return new string[] { "EMA(" + this.Parameters[0].ToString() + ")" }; } }

      public override System.Drawing.Pen[] SeriePens
      {
         get
         {
            if (seriePens == null)
            {
               seriePens = new Pen[] { new Pen(Color.Blue) };
            }
            return seriePens;
         }
      }

      public override void ApplyTo(StockSerie stockSerie)
      {
         FloatSerie closeSerie = stockSerie.GetSerie(StockDataType.CLOSE);
         FloatSerie highSerie = stockSerie.GetSerie(StockDataType.HIGH);
         FloatSerie lowSerie = stockSerie.GetSerie(StockDataType.LOW);
         FloatSerie emaSerie = closeSerie.CalculateEMA((int)this.parameters[0]);
         this.series[0] = emaSerie;
         this.series[0].Name = this.Name;

         // Detecting events
         this.CreateEventSeries(stockSerie.Count);
         for (int i = 2; i < emaSerie.Count; i++)
         {
            this.eventSeries[0][i] = (emaSerie[i - 2] > emaSerie[i - 1] && emaSerie[i - 1] < emaSerie[i]);
            this.eventSeries[1][i] = (emaSerie[i - 2] < emaSerie[i - 1] && emaSerie[i - 1] > emaSerie[i]);
            this.eventSeries[2][i] = closeSerie[i] > emaSerie[i];
            this.eventSeries[3][i] = closeSerie[i] < emaSerie[i];
            this.eventSeries[4][i] = lowSerie[i] > emaSerie[i] && lowSerie[i-1] < emaSerie[i-1];
            this.eventSeries[5][i] = highSerie[i] < emaSerie[i] && highSerie[i - 1] > emaSerie[i - 1];
            this.eventSeries[6][i] = lowSerie[i] > emaSerie[i] && closeSerie[i - 1] < closeSerie[i];
            this.eventSeries[7][i] = highSerie[i] < emaSerie[i] && closeSerie[i - 1] > closeSerie[i];
         }
      }

      static string[] eventNames = new string[] { "Bottom", "Top", "CloseAbove", "CloseBelow", "FirstBarAbove", "FirstBarBelow", "Bullish", "Bearish" };
      public override string[] EventNames
      {
         get { return eventNames; }
      }
      static readonly bool[] isEvent = new bool[] { true, true, false, false, true, true };
      public override bool[] IsEvent
      {
         get { return isEvent; }
      }
   }
}
