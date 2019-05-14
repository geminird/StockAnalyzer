﻿using StockAnalyzer.StockMath;
using System;
using System.Drawing;

namespace StockAnalyzer.StockClasses.StockViewableItems.StockTrailStops
{
    public class StockTrailStop_TRAILSAR : StockTrailStopBase
    {
        public override IndicatorDisplayTarget DisplayTarget
        {
            get { return IndicatorDisplayTarget.PriceIndicator; }
        }
        public override bool RequiresVolumeData { get { return false; } }
        public override string[] ParameterNames
        {
            get { return new string[] { "Step" }; }
        }

        public override Object[] ParameterDefaultValues
        {
            get { return new Object[] { 0.1f }; }
        }
        public override ParamRange[] ParameterRanges
        {
            get { return new ParamRange[] { new ParamRangeFloat(0.001f, 20f) }; }
        }
        public override string[] SerieNames { get { return new string[] { "TRAILSAR.LS", "TRAILSAR.SS" }; } }

        public override void ApplyTo(StockSerie stockSerie)
        {
            FloatSerie longStopSerie;
            FloatSerie shortStopSerie;

            float step = (float)this.parameters[0] / 100f;

            stockSerie.CalculateSAR(step, 0, float.MaxValue, out longStopSerie, out shortStopSerie, 1);

            this.Series[0] = longStopSerie;
            this.Series[1] = shortStopSerie;

            // Generate events
            this.GenerateEvents(stockSerie, longStopSerie, shortStopSerie);
        }
    }
}
