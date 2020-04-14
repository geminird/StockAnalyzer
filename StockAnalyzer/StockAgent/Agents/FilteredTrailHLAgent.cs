﻿using StockAnalyzer.StockClasses;
using StockAnalyzer.StockClasses.StockViewableItems.StockTrailStops;
using StockAnalyzer.StockMath;
using System;

namespace StockAnalyzer.StockAgent.Agents
{
    public class FilteredTrailHLAgent : StockAgentBase
    {
        public FilteredTrailHLAgent(StockContext context)
            : base(context)
        {
            Period = 13;
        }

        [StockAgentParam(2, 20)]
        public int Period { get; set; }

        [StockAgentParam(20, 80)]
        public int FilterPeriod { get; set; }

        public override string Description => "Buy with TRAILHL Stop";

        IStockTrailStop trailStop;
        FloatSerie emaFilterSerie;
        BoolSerie bullEvents;
        BoolSerie bearEvents;
        protected override void Init(StockSerie stockSerie)
        {
            emaFilterSerie = stockSerie.GetIndicator($"EMA({FilterPeriod})").Series[0];
            trailStop = stockSerie.GetTrailStop($"TRAILHL({Period})");
            bullEvents = trailStop.Events[Array.IndexOf<string>(trailStop.EventNames, "BrokenUp")];
            bearEvents = trailStop.Events[Array.IndexOf<string>(trailStop.EventNames, "BrokenDown")];
        }

        protected override TradeAction TryToOpenPosition()
        {
            int i = context.CurrentIndex;

            if (bullEvents[i] && closeSerie[i] >= emaFilterSerie[i])
            {
                return TradeAction.Buy;
            }
            return TradeAction.Nothing;
        }

        protected override TradeAction TryToClosePosition()
        {
            int i = context.CurrentIndex;

            if (bearEvents[i]) // bar fast below slow EMA
            {
                return TradeAction.Sell;
            }
            return TradeAction.Nothing;
        }
    }
}
