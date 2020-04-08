﻿using StockAnalyzer.StockClasses;
using StockAnalyzer.StockClasses.StockViewableItems.StockClouds;
using StockAnalyzer.StockMath;
using System;

namespace StockAnalyzer.StockAgent.Agents
{
    public class EMACloudAgent : StockAgentBase
    {
        public EMACloudAgent(StockContext context)
            : base(context)
        {
            FastPeriod = 13;
        }

        protected override IStockAgent CreateInstance(StockContext context)
        {
            return new EMACloudAgent(context);
        }

        [StockAgentParam(15, 25)]
        public int FastPeriod { get; set; }

        [StockAgentParam(40, 60)]
        public int SlowPeriod { get; set; }

        [StockAgentParam(6, 12)]
        public int SignalPeriod { get; set; }

        public override string Description => "Buy when Open and close are above EMA";

        IStockCloud cloud;
        BoolSerie bullEvents;
        BoolSerie bearEvents;
        public override void Initialize(StockSerie stockSerie)
        {
            cloud = stockSerie.GetCloud($"EMA2Lines({FastPeriod},{SlowPeriod},{SignalPeriod})");
            bullEvents = cloud.Events[Array.IndexOf<string>(cloud.EventNames, "BrokenUp")];
            bearEvents = cloud.Events[Array.IndexOf<string>(cloud.EventNames, "BrokenDown")];
        }

        protected override TradeAction TryToOpenPosition()
        {
            int i = context.CurrentIndex;

            if (bullEvents[i])
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
