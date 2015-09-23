﻿using System;
using System.Drawing;

namespace StockAnalyzer.StockClasses.StockViewableItems.StockIndicators
{
    public class StockIndicator_VLTY2 : StockIndicatorBase
    {
        public StockIndicator_VLTY2()
        {
        }
        public override IndicatorDisplayTarget DisplayTarget
        {
            get { return IndicatorDisplayTarget.NonRangedIndicator; }
        }
        
        public override string Definition
        {
            get { return "VLTY2(int Period)"; }
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
            get { return new string[]{"Period"}; }
        }

        public override string[] SerieNames { get { return new string[] { "VLTY2(" + this.Parameters[0].ToString() + ")" }; } }
        
        public override System.Drawing.Pen[] SeriePens
        {
            get
            {
                if (seriePens == null)
                {
                    seriePens = new Pen[] { new Pen(Color.Blue)};
                }
                return seriePens;
            }
        }
        public override HLine[] HorizontalLines
        {
            get { return null; }
        }

        public override void ApplyTo(StockSerie stockSerie)
        {
            this.series[0] = stockSerie.CalculateVolatility2((int)this.parameters[0]);
            this.Series[0].Name = this.Name;
        }

        static string[] eventNames = new string[] { };
        public override string[] EventNames
        {
            get { return eventNames; }
        }
        static readonly bool[] isEvent = new bool[] {  };
        public override bool[] IsEvent
        {
            get { return isEvent; }
        }
    }
}
