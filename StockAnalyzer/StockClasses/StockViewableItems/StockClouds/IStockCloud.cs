﻿using StockAnalyzer.StockMath;

namespace StockAnalyzer.StockClasses.StockViewableItems.StockClouds
{
    public interface IStockCloud : IStockViewableSeries, IStockEvent
    {
        FloatSerie[] Series { get; }

        FloatSerie BullSerie { get; }
        FloatSerie BearSerie { get; }
    }
}
