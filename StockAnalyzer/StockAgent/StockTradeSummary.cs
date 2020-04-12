﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockAnalyzer.StockAgent
{
    public class StockTradeSummary
    {
        public IList<StockTrade> Trades { get; private set; }

        public float MaxDrawdown { get { return this.Trades.Count > 0 ? this.Trades.Min(t => t.DrawDown) : 0f; } }

        public float AvgGain { get { return this.Trades.Count > 0 ? this.Trades.Average(t => t.Gain) : 0f; } }
        public float CumulGain { get { return this.Trades.Count > 0 ? this.Trades.Sum(t => t.Gain) : 0f; } }
        public float CompoundGain { get { return this.Trades.Count > 0 ? this.Trades.Select(t => t.Gain + 1).Aggregate(1f, (i, j) => i * j) - 1f : 0f; } }

        public int NbWinTrade { get { return Trades.Count(t => t.Gain >= 0); } }
        public int NbLostTrade { get { return Trades.Count(t => t.Gain < 0); } }
        public float WinRatio { get { return NbLostTrade != 0 ? NbWinTrade / (float)NbLostTrade : 0f; } }

        public StockTradeSummary(StockContext context)
        {
            Trades = context.TradeLog.Where(t => t.IsClosed).ToList();
        }

        public string ToLog()
        {
            string res = "Max Drawdown=" + MaxDrawdown + Environment.NewLine;
            res += "Average Gain=" + AvgGain + Environment.NewLine;
            res += "Cumul Gain=" + CumulGain + Environment.NewLine;
            res += "Compound Gain=" + CompoundGain + Environment.NewLine;
            res += "Nb Trade=" + Trades.Count() + Environment.NewLine;
            res += "Nb Win Trade=" + NbWinTrade + Environment.NewLine;
            res += "Nb Lost Trade=" + NbLostTrade + Environment.NewLine;
            res += "Win Ratio=" + WinRatio + Environment.NewLine;
            return res;
        }

        public string ToStats()
        {
            string res = MaxDrawdown + "\t";
            res += AvgGain + "\t";
            res += CumulGain + "\t";
            res += CompoundGain + "\t";
            res += Trades.Count() + "\t";
            res += NbWinTrade + "\t";
            res += NbLostTrade + "\t";
            res += WinRatio + "\t";
            return res;
        }
    }
}
