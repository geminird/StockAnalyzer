﻿using StockAnalyzer.StockClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace StockAnalyzer.StockAgent
{
    public enum TradeAction
    {
        Nothing,
        Buy,
        Sell
    }

    public interface IStockAgent
    {
        string Description { get; }
        void Randomize();

        TradeAction Decide();

        IList<IStockAgent> Reproduce(IStockAgent partner, int nbChildren);

        bool AreSameParams(IStockAgent other);

        string ToLog();
        void Initialize(StockSerie stockSerie, StockBarDuration duration);
        string GetParameterValues();
        void SetParam(PropertyInfo property, StockAgentParamAttribute attribute, float newValue);
    }
}
