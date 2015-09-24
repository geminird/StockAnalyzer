﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using StockAnalyzer.Portofolio;
using StockAnalyzer.StockClasses;

namespace StockAnalyzer.StockPortfolioStrategy
{
   abstract public class StockPortfolioStrategyBase : IStockPortfolioStrategy
   {
      abstract public string Description { get; }

      public List<StockSerie> Series { get; set; }

      public StockPortofolio Portfolio { get; set; }

      protected List<StockPosition> Positions { get; set; }

      public StockDictionary StockDictionary { get; private set; }

      protected float availableLiquidity;

      public void Initialise(System.Collections.Generic.List<StockClasses.StockSerie> stockSeries, Portofolio.StockPortofolio portfolio, StockDictionary stockDictionary)
      {
         this.Series = stockSeries;
         this.Portfolio = portfolio;
         this.StockDictionary = stockDictionary;

         this.availableLiquidity = portfolio.AvailableLiquitidity;

         this.Positions = new List<StockPosition>();
      }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="date"></param>
      /// <returns></returns>
      abstract protected DateTime? InitialiseAllocation(DateTime startDate);

      public void Apply(DateTime startDate, DateTime endDate, UpdatePeriod updatePeriod)
      {
         // Initiliase allocation before simulation starts
         DateTime? actualStartDate = this.InitialiseAllocation(startDate);
         if (actualStartDate == null)
         {
            MessageBox.Show("Impossible to start simulation at this date: " + startDate, "Invalid input data",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
         }

         var calendar = CultureInfo.CurrentCulture.Calendar;
         DateTime currentDate = actualStartDate.Value.AddDays(1);
         DateTime previousDate = currentDate;

         StockSerie serie = Series.First();

         while (currentDate < endDate)
         {
            switch (updatePeriod)
            {
               case UpdatePeriod.Daily:
                  //Console.WriteLine("Updating " + updatePeriod.ToString() + " :" + currentDate.ToShortDateString());
                  this.ApplyAtDate(currentDate);
                  break;
               case UpdatePeriod.Weekly:
                  if (calendar.GetWeekOfYear(currentDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday) !=
                      calendar.GetWeekOfYear(previousDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                  {
                     //Console.WriteLine("Updating " + updatePeriod.ToString() + " :" + currentDate.ToShortDateString());
                     this.ApplyAtDate(currentDate);
                  }
                  break;
               case UpdatePeriod.Monthly:
                  if (currentDate.Month > previousDate.Month)
                  {
                     //Console.WriteLine("Updating " + updatePeriod.ToString() + " :" + currentDate.ToShortDateString());
                     this.ApplyAtDate(currentDate);
                  }
                  break;
            }
            previousDate = currentDate;
            do
            {
               currentDate = currentDate.AddDays(1);
            } while (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday ||
                     serie.IndexOf(currentDate) == -1);
         }
      }

      protected abstract void ApplyAtDate(DateTime applyDate);

      protected void Dump(DateTime applyDate)
      {
#if true
         Console.WriteLine("Position Dump");
         float value = 0;
         foreach (var position in this.Positions)
         {
            StockSerie serie = this.Series.Find(s => s.StockName == position.StockName);
            float open = serie[applyDate].OPEN;
            value += position.Value(open);
            Console.WriteLine(position.StockName + " " + position.Number + "==> Open: " + open + " ==> TotalCost: " + position.TotalCost + " CurrentValue: " + position.Value(open));
         }
#endif
         Console.WriteLine("PortFolio cash: " + this.availableLiquidity + " Value: " + value + "Total: " + (this.availableLiquidity + value));

      }
   }
}