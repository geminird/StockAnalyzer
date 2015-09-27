﻿using System;
using System.Drawing;
using StockAnalyzer.StockMath;

namespace StockAnalyzer.StockClasses.StockViewableItems.StockIndicators
{
   public class StockIndicator_TRUE : StockIndicatorBase, IRange
   {
      public StockIndicator_TRUE()
      {
      }
      public override IndicatorDisplayTarget DisplayTarget
      {
         get { return IndicatorDisplayTarget.RangedIndicator; }
      }

      public override string Name
      {
         get { return "TRUE()"; }
      }

      public override string Definition
      {
         get { return "TRUE()"; }
      }
      public override object[] ParameterDefaultValues
      {
         get { return new Object[] { }; }
      }
      public override ParamRange[] ParameterRanges
      {
         get { return new ParamRange[] { }; }
      }
      public override string[] ParameterNames
      {
         get { return new string[] { }; }
      }

      public override string[] SerieNames { get { return new string[] { "TRUE()" }; } }

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
         FloatSerie TRUESerie = new FloatSerie(stockSerie.Count);
         this.series[0] = TRUESerie;
         this.series[0].Name = this.Name;

         // Detecting events
         this.CreateEventSeries(stockSerie.Count);
         int i = 0;
         foreach(StockDailyValue value in stockSerie.Values)
         {
            this.eventSeries[0][i] = true;
            this.eventSeries[1][i] = false;

            this.eventSeries[2][i] = value.VARIATION>0f;
            this.eventSeries[3][i] = value.VARIATION<0f;
            i++;
         }
      }

      static string[] eventNames = new string[] { "True", "False", "HigherClose", "LowerClose" };
      public override string[] EventNames
      {
         get { return eventNames; }
      }
      static readonly bool[] isEvent = new bool[] { false, false, true, true, };
      public override bool[] IsEvent
      {
         get { return isEvent; }
      }

      public float Max
      {
         get { return 1.0f; }
      }

      public float Min
      {
         get { return -1.0f; }
      }
   }
}
