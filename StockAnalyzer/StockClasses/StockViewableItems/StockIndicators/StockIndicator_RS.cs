﻿using StockAnalyzer.StockMath;
using System;
using System.Drawing;

namespace StockAnalyzer.StockClasses.StockViewableItems.StockIndicators
{
    public class StockIndicator_RS : StockIndicatorBase
    {
        public override string Definition => base.Definition + Environment.NewLine + "Show relative strength compare to another serie or indice";
        public override IndicatorDisplayTarget DisplayTarget => IndicatorDisplayTarget.NonRangedIndicator;
        public override string[] ParameterNames => new string[] { "StockName", "FastSmoothing", "SlowSmoothing" };
        public override Object[] ParameterDefaultValues => new Object[] { "CAC40", 1, 9 };
        public override ParamRange[] ParameterRanges => new ParamRange[] { new ParamRangeStockName(), new ParamRangeInt(1, 500), new ParamRangeInt(1, 500) };
        public override string[] SerieNames => new string[] {
            this.Parameters[0].ToString(),
            this.Parameters[0].ToString() + $"({(int)this.parameters[1]})",
            this.Parameters[0].ToString() + $"({(int)this.parameters[2]})" };
        public override System.Drawing.Pen[] SeriePens
        {
            get
            {
                if (seriePens == null)
                {
                    seriePens = new Pen[] { new Pen(Color.Black, 2), new Pen(Color.Green, 2), new Pen(Color.Red, 2) };
                }
                return seriePens;
            }
        }
        public override void ApplyTo(StockSerie stockSerie)
        {
            var stockName = this.parameters[0].ToString();
            var fastSmoothing = (int)this.parameters[1];
            var slowSmoothing = (int)this.parameters[2];

            if (!StockDictionary.StockDictionarySingleton.ContainsKey(stockName))
            {
                throw new ArgumentException("Stock name not found: " + stockName);
            }

            var otherSerie = StockDictionary.StockDictionarySingleton[stockName];
            if (!otherSerie.Initialise())
            {
                throw new ArgumentException("Stock cannot be initialized: " + stockName);
            }

            var refSerie = stockSerie.GenerateRelativeStrenthStockSerie(StockDictionary.StockDictionarySingleton[this.parameters[0].ToString()]).GetSerie(StockDataType.CLOSE);

            this.Series[0] = refSerie;
            this.Series[1] = refSerie.CalculateEMA(fastSmoothing);
            this.Series[2] = refSerie.CalculateEMA(slowSmoothing);

            // Detecting events
            this.CreateEventSeries(stockSerie.Count);

            for (int i = 5; i < stockSerie.Count; i++)
            {
                this.Events[0][i] = this.Series[1][i - 1] < this.Series[2][i - 1] && this.Series[1][i] > this.Series[2][i];
                this.Events[1][i] = this.Series[1][i - 1] > this.Series[2][i - 1] && this.Series[1][i] < this.Series[2][i];
                this.Events[0][i] = this.Series[1][i] > this.Series[2][i];
                this.Events[1][i] = this.Series[1][i] < this.Series[2][i];
            }
        }

        private static string[] eventNames = new string[]
         {
             "BrokenUp", "BrokenDown",    // 0, 1
             "Bullish", "Bearish"         // 2, 3
         };

        public override string[] EventNames => eventNames;

        private static readonly bool[] isEvent = new bool[] { true, true, false, false };
        public override bool[] IsEvent => isEvent;
    }
}
