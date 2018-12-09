﻿using StockAnalyzer.StockClasses.StockViewableItems.StockIndicators;
using StockAnalyzer.StockLogging;
using StockAnalyzer.StockMath;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace StockAnalyzer.StockClasses.StockViewableItems.StockDecorators
{
    public class StockDecorator_SIGNAL : StockDecoratorBase, IStockDecorator
    {
        public override string Definition
        {
            get { return "Plots signal line and plots crossings"; }
        }

        public StockDecorator_SIGNAL()
        {
        }

        public override IndicatorDisplayTarget DisplayTarget
        {
            get { return IndicatorDisplayTarget.NonRangedIndicator; }
        }

        public override IndicatorDisplayStyle DisplayStyle
        {
            get { return IndicatorDisplayStyle.DecoratorPlot; }
        }

        public override string[] ParameterNames
        {
            get { return new string[] { "Smoothing" }; }
        }
        public override Object[] ParameterDefaultValues
        {
            get { return new Object[] { 6 }; }
        }
        public override ParamRange[] ParameterRanges
        {
            get { return new ParamRange[] { new ParamRangeInt(1, 500) }; }
        }

        public override string[] SerieNames { get { return new string[] { $"Signal({(int)this.parameters[0]})" }; } }

        public override System.Drawing.Pen[] SeriePens
        {
            get
            {
                if (seriePens == null)
                {
                    seriePens = new Pen[] { new Pen(Color.DarkRed) };
                }
                return seriePens;
            }
        }

        public override void ApplyTo(StockSerie stockSerie)
        {
            using (MethodLogger ml = new MethodLogger(this))
            {
                List<string> eventNames = this.EventNames.ToList();

                CreateEventSeries(stockSerie.Count);

                int smoothing = (int)this.parameters[0];

                IStockIndicator indicator = stockSerie.GetIndicator(this.DecoratedItem);
                if (indicator == null || indicator.Series[0].Count == 0) return;


                FloatSerie dataSerie = indicator.Series[0];
                FloatSerie signalSerie = dataSerie.CalculateEMA(smoothing);

                this.Series[0] = signalSerie;
                this.Series[0].Name = this.SerieNames[0];

                int bullishIndex = eventNames.IndexOf("Bullish");
                int bearishIndex = eventNames.IndexOf("Bearish");
                int crossAboveIndex = eventNames.IndexOf("CrossAbove");
                int crossBelowIndex = eventNames.IndexOf("CrossBelow");

                bool bullish = dataSerie[1] >= dataSerie[0];
                bool previousBullish = bullish;
                for (int i = 1; i < dataSerie.Count - 1; i++)
                {
                    bullish = dataSerie[i] >= signalSerie[i];
                    this.Events[bullishIndex][i] = bullish;
                    this.Events[bearishIndex][i] = !bullish;

                    this.Events[crossAboveIndex][i] = bullish && !previousBullish;
                    this.Events[crossBelowIndex][i] = !bullish && previousBullish;

                    previousBullish = bullish;
                }
            }
        }

        public override System.Drawing.Pen[] EventPens
        {
            get
            {
                if (eventPens == null)
                {
                    eventPens = new Pen[] {
                        new Pen(Color.Transparent), new Pen(Color.Transparent),
                        new Pen(Color.Red), new Pen(Color.Green) };
                    eventPens[0].Width = 2;
                    eventPens[1].Width = 2;
                    eventPens[2].Width = 2;
                    eventPens[3].Width = 2;
                }
                return eventPens;
            }
        }

        private static string[] eventNames = new string[]
        {
            "Bullish",
            "Bearish",
            "CrossBelow",
            "CrossAbove"
        };

        public override string[] EventNames
        {
            get { return eventNames; }
        }
        static readonly bool[] isEvent = new bool[]
        {
            false, false,
            true, true,
        };
        public override bool[] IsEvent
        {
            get { return isEvent; }
        }
    }
}
