﻿using System;
using System.Drawing;
using StockAnalyzer.StockMath;

namespace StockAnalyzer.StockClasses.StockViewableItems.StockIndicators
{
   public class StockIndicator_SARHL : StockIndicatorBase
   {
      public StockIndicator_SARHL()
      {
      }
      public override IndicatorDisplayTarget DisplayTarget
      {
         get { return IndicatorDisplayTarget.PriceIndicator; }
      }
      public override IndicatorDisplayStyle DisplayStyle
      {
         get
         {
            return IndicatorDisplayStyle.SupportResistance;
         }
      }

      public override string[] ParameterNames
      {
         get { return new string[] { "HLPeriod", "Step", "Max", "ErrorMargin" }; }
      }

      public override Object[] ParameterDefaultValues
      {
         get { return new Object[] { 3, 0.002f, 0.02f, 0.05f }; }
      }
      public override ParamRange[] ParameterRanges
      {
          get { return new ParamRange[] { new ParamRangeInt(1, 500), new ParamRangeFloat(0.00001f, 10.0f), new ParamRangeFloat(0.0001f, 100.0f), new ParamRangeFloat(0.0f, 1.0f) }; }
      }

      public override string[] SerieNames { get { return new string[] { "SARHL.S", "SARHL.R" }; } }

      public override System.Drawing.Pen[] SeriePens
      {
         get
         {
            if (seriePens == null)
            {
               seriePens = new Pen[] { new Pen(Color.Green, 2), new Pen(Color.Red, 2) };
            }
            return seriePens;
         }
      }

      public override void ApplyTo(StockSerie stockSerie)
      {
         int period = (int)this.parameters[0];
         float accelerationFactorStep = (float)this.parameters[1];
         float accelerationFactorMax = (float)this.parameters[2];
         float margin = (float)this.parameters[3];

         FloatSerie sarSupport;
         FloatSerie sarResistance;

         stockSerie.CalculateSARHL(period, accelerationFactorStep, accelerationFactorStep, accelerationFactorMax, margin, out sarSupport, out sarResistance);

         this.Series[0] = sarSupport;
         this.Series[1] = sarResistance;

         // Detecting events
         this.CreateEventSeries(stockSerie.Count);

         for (int i = 5; i < stockSerie.Count; i++)
         {
            this.Events[0][i] = float.IsNaN(sarResistance[i - 1]) && !float.IsNaN(sarResistance[i]);
            this.Events[1][i] = float.IsNaN(sarSupport[i - 1]) && !float.IsNaN(sarSupport[i]);
            this.Events[2][i] = this.Events[1][i] && !this.Events[1][i - 1];
            this.Events[3][i] = this.Events[0][i] && !this.Events[0][i - 1];
         }
      }

      static string[] eventNames = new string[] { "UpTrend", "DownTrend", "BrokenUp", "BrokenDown", "UpTrend", "DownTrend", "BrokenUp", "BrokenDown" };
      public override string[] EventNames
      {
         get { return eventNames; }
      }
      static readonly bool[] isEvent = new bool[] { false, false, true, true, false, false, true, true };
      public override bool[] IsEvent
      {
         get { return isEvent; }
      }
   }
}
