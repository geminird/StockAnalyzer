﻿using System;
using System.Drawing;
using StockAnalyzer.StockMath;

namespace StockAnalyzer.StockClasses.StockViewableItems.StockIndicators
{
    public class StockIndicator_EMA3Lines : StockIndicatorBase
    {
        public StockIndicator_EMA3Lines()
        {
        }
        public override IndicatorDisplayTarget DisplayTarget
        {
            get { return IndicatorDisplayTarget.PriceIndicator; }
        }
        public override string[] ParameterNames
        {
            get { return new string[] { "FastPeriod", "MediumPeriod", "SlowPeriod", "FastShift", "MediumShift", "SlowShift" }; }
        }

        public override Object[] ParameterDefaultValues
        {
            get { return new Object[] { 5, 8, 13, 3, 5, 8 }; }
        }
        public override ParamRange[] ParameterRanges
        {
            get
            {
                return new ParamRange[] { 
             new ParamRangeInt(1, 500), new ParamRangeInt(1, 500), new ParamRangeInt(1, 500), 
             new ParamRangeInt(0, 500), new ParamRangeInt(0, 500), new ParamRangeInt(0, 500)   };
            }
        }
        public override string[] SerieNames { get { return new string[] { "EMA(" + this.Parameters[0].ToString() + ")", "EMA(" + this.Parameters[1].ToString() + ")", "EMA(" + this.Parameters[2].ToString() + ")" }; } }

        public override System.Drawing.Pen[] SeriePens
        {
            get
            {
                if (seriePens == null)
                {
                    seriePens = new Pen[] { new Pen(Color.Green), new Pen(Color.Blue), new Pen(Color.Red) };
                }
                return seriePens;
            }
        }
        public override void ApplyTo(StockSerie stockSerie)
        {
            FloatSerie fastSerie = stockSerie.GetIndicator(this.SerieNames[0]).Series[0].ShiftForward((int)this.parameters[3]);
            FloatSerie mediumSerie = stockSerie.GetIndicator(this.SerieNames[1]).Series[0].ShiftForward((int)this.parameters[4]);
            FloatSerie slowSerie = stockSerie.GetIndicator(this.SerieNames[2]).Series[0].ShiftForward((int)this.parameters[5]);
            
            this.Series[0] = fastSerie;
            this.Series[1] = mediumSerie;
            this.Series[2] = slowSerie;

            this.SetSerieNames();

            // Detecting events
            this.CreateEventSeries(stockSerie.Count);

            for (int i = 2; i < stockSerie.Count; i++)
            {
                if (fastSerie[i] > mediumSerie[i] && mediumSerie[i] > slowSerie[i])
                {
                    this.Events[0][i] = true;
                }
                else if (fastSerie[i] < mediumSerie[i] && mediumSerie[i] < slowSerie[i])
                {
                    this.Events[1][i] = true;
                }
            }
        }

        static string[] eventNames = new string[] { "UpTrend", "DownTrend" };
        public override string[] EventNames
        {
            get { return eventNames; }
        }
        static readonly bool[] isEvent = new bool[] { false, false };
        public override bool[] IsEvent
        {
            get { return isEvent; }
        }
    }
}
