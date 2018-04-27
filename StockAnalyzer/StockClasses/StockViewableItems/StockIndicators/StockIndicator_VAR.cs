﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using StockAnalyzer.StockMath;

namespace StockAnalyzer.StockClasses.StockViewableItems.StockIndicators
{
    public class StockIndicator_VAR : StockIndicatorBase
    {
        public StockIndicator_VAR()
        {
        }
        public override IndicatorDisplayTarget DisplayTarget
        {
            get { return IndicatorDisplayTarget.NonRangedIndicator; }
        }

        public override string Name
        {
            get { return "VAR(" + this.Parameters[0].ToString() + ")"; }
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

        public override string[] SerieNames { get { return new string[] { "VAR(" + this.Parameters[0].ToString() + ")" }; } }

        public override System.Drawing.Pen[] SeriePens
        {
            get
            {
                if (seriePens == null)
                {
                    seriePens = new Pen[] { new Pen(Color.Black) { DashStyle = DashStyle.Custom } };
                }
                return seriePens;
            }
        }

        public override HLine[] HorizontalLines
        {
            get
            {
                HLine[] lines = new HLine[] { new HLine(0, new Pen(Color.Black)) };
                return lines;
            }
        }

        public override void ApplyTo(StockSerie stockSerie)
        {
            FloatSerie varSerie = stockSerie.GetSerie(StockDataType.VARIATION) * (100f * (int)this.parameters[0]);
            FloatSerie emaSerie = varSerie.CalculateEMA((int)this.parameters[0]);
            this.series[0] = emaSerie;
            this.Series[0].Name = this.SerieNames[0];

            // Detecting events
            this.CreateEventSeries(stockSerie.Count);

            bool bull = true, previousBull = true;
            for (int i = 2; i < stockSerie.Count; i++)
            {
                bull = emaSerie[i] > 0;
                this.eventSeries[0][i] = bull && ! previousBull;
                this.eventSeries[1][i] = !bull & previousBull;
                this.eventSeries[2][i] = bull;
                this.eventSeries[3][i] = !bull;

                previousBull = bull;
            }
        }

        static readonly string[] eventNames = new string[] { "CrossUp", "CrossDown", "Bullish", "Bearish" };
        public override string[] EventNames => eventNames;

        static readonly bool[] isEvent = new bool[] { true, true, false, false };
        public override bool[] IsEvent => isEvent;
    }
}
