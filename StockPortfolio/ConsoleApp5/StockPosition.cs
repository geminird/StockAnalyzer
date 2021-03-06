﻿using System;

namespace ConsoleApp5
{
    public class StockPosition
    {
        public StockPosition()
        {
            EndDate = DateTime.MaxValue;
        }
        public string StockName { get; set; }
        public int Qty { get; set; }
        public float OpenValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsClosed => EndDate != DateTime.MaxValue;

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
    }
}