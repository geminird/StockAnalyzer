﻿using System.Collections.Generic;
using System.Linq;

namespace StockAnalyzer.StockMath
{
   public class BoolSerie
   {
      public string Name { get; set; }
      public bool[] Values { get; set; }

      public BoolSerie(int size, string name)
      {
         this.Values = new bool[size];
         this.Name = name;
      }

      public BoolSerie(int size, string name, bool value)
      {
         this.Values = new bool[size];
         for(int i = 0; i < this.Count; i++)
         {
            this[i] = value;
         }
         this.Name = name;
      }

      public BoolSerie(string name, IEnumerable<bool> enumerable)
      {
          this.Name = name;
          this.Values = enumerable.ToArray();
      }

      public bool this[int index]
      {
         get { return this.Values[index]; }
         set { this.Values[index] = value; }
      }

      public int Count
      {
         get { return this.Values.Count(); }
      }
   }
}
