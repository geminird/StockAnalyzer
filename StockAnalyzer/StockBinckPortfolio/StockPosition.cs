﻿using System;

namespace StockAnalyzer.StockBinckPortfolio
{
    public class StockPosition
    {
        public StockPosition()
        {
            EndDate = DateTime.MaxValue;
            this.Leverage = 1;
        }
        public string StockName { get; set; }
        public int Qty { get; set; }
        public float OpenValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsClosed => EndDate != DateTime.MaxValue;
        public bool IsShort { get; set; }
        public float Leverage { get; set; }

        internal void Dump()
        {
            if (this.IsClosed)
            {
                Console.WriteLine($"Name: {StockName} Qty:{Qty} StartDate:{StartDate.ToShortDateString()} EndDate:{EndDate.ToShortDateString()}");
            }
            else
            {
                Console.WriteLine($"Name: {StockName} Qty:{Qty} StartDate:{StartDate.ToShortDateString()} Opened");
            }
        }

        public override string ToString()
        {
            return this.IsClosed ? $"Name: {StockName} Qty:{Qty} StartDate:{StartDate.ToShortDateString()} EndDate:{EndDate.ToShortDateString()}"
                : $"Name: {StockName} Qty:{Qty} StartDate:{StartDate.ToShortDateString()} Opened";
        }
    }
}