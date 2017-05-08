﻿using System;
using System.Linq;
using System.Drawing;
using StockAnalyzer.StockMath;

namespace StockAnalyzer.StockClasses.StockViewableItems.StockIndicators
{
    public class StockIndicator_CROSSING : StockIndicatorBase
    {
        public StockIndicator_CROSSING()
        {
        }
        public override IndicatorDisplayTarget DisplayTarget
        {
            get { return IndicatorDisplayTarget.PriceIndicator; }
        }

        public override object[] ParameterDefaultValues
        {
            get { return new Object[] { "EMAL(20)", "EMA(200)" }; }
        }
        public override ParamRange[] ParameterRanges
        {
            get { return new ParamRange[] { new ParamRangeIndicator(), new ParamRangeIndicator() }; }
        }
        public override string[] ParameterNames
        {
            get { return new string[] { "FastIndicator", "SlowIndicator" }; }
        }

        public override string[] SerieNames { get { return new string[] { this.Parameters[0].ToString(), this.Parameters[1].ToString() }; } }

        public override System.Drawing.Pen[] SeriePens
        {
            get
            {
                if (seriePens == null)
                {
                    seriePens = new Pen[] { new Pen(Color.DarkGreen), new Pen(Color.DarkRed) };
                }
                return seriePens;
            }
        }

        public override void ApplyTo(StockSerie stockSerie)
        {
            FloatSerie fastIndicatorSerie = stockSerie.GetIndicator(this.parameters[0].ToString().Replace("_", ",")).Series[0];
            FloatSerie slowIndicatorSerie = stockSerie.GetIndicator(this.parameters[1].ToString().Replace("_", ",")).Series[0];

            this.series[0] = fastIndicatorSerie;
            this.series[1] = slowIndicatorSerie;
            this.SetSerieNames();

            CreateEventSeries(stockSerie.Count);

            bool previousBulish = true;
            for (int i = 1; i < stockSerie.Count; i++)
            {
                bool bullish = fastIndicatorSerie[i] >= slowIndicatorSerie[i];
                this.eventSeries[0][i] = bullish && !previousBulish;
                this.eventSeries[1][i] = !bullish && previousBulish;
                this.eventSeries[2][i] = bullish;
                this.eventSeries[3][i] = !bullish;
                previousBulish = bullish;
            }
        }

        static string[] eventNames = new string[] { "BullishCrossing", "BearishCrossing", "Bullish", "Bearish" };
        public override string[] EventNames
        {
            get { return eventNames; }
        }

        static readonly bool[] isEvent = new bool[] { true, true, false, false };
        public override bool[] IsEvent
        {
            get { return isEvent; }
        }
    }
}
