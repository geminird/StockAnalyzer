﻿using System;
using System.Collections.Generic;
using System.Linq;
using StockAnalyzer.StockClasses.StockViewableItems.StockIndicators;
using StockAnalyzer.StockClasses.StockViewableItems.StockTrails;

namespace StockAnalyzer.StockClasses.StockViewableItems
{
   public abstract class ParamRange
   {
      public Object MinValue { get; protected set; }
      public Object MaxValue { get; protected set; }

      public ParamRange()
      {
      }

      public ParamRange(Object minValue, Object maxValue)
      {
         this.MinValue = minValue;
         this.MaxValue = maxValue;
      }

      public abstract bool isValidString(string value);
      public abstract bool isInRange(Object valueString);

      virtual public Type GetParamType()
      {
         return MinValue == null ? null : MinValue.GetType();
      }
   }

   public class ParamRangeInt : ParamRange
   {
      public ParamRangeInt(Object minValue, Object maxValue)
         : base(minValue, maxValue)
      {
      }

      public override bool isInRange(Object value)
      {
         return (int) value >= (int) this.MinValue && (int) value <= (int) this.MaxValue;
      }

      public override bool isValidString(string valueString)
      {
         int intValue;
         if (!int.TryParse(valueString, out intValue))
         {
            return false;
         }
         return this.isInRange(intValue);
      }
   }

   public class ParamRangeFloat : ParamRange
   {
      public ParamRangeFloat()
         : base(float.MinValue, float.MaxValue)
      {
      }
      public ParamRangeFloat(Object minValue, Object maxValue)
         : base(minValue, maxValue)
      {
      }

      public override bool isInRange(Object value)
      {
         return (float) value >= (float) this.MinValue && (float) value <= (float) this.MaxValue;
      }

      public override bool isValidString(string valueString)
      {
         float floatValue;
         if (!float.TryParse(valueString, out floatValue))
         {
            return false;
         }
         return this.isInRange(floatValue);
      }
   }

   public class ParamRangeBool : ParamRange
   {
      public ParamRangeBool()
      {
         this.MinValue = false;
         this.MaxValue = true;
      }

      public override bool isInRange(Object value)
      {
         return true;
      }

      public override bool isValidString(string valueString)
      {
         bool boolValue;
         return bool.TryParse(valueString, out boolValue);
      }
   }

   public class ParamRangeStringList : ParamRange
   {
      private List<string> stringList;

      public ParamRangeStringList(List<string> list)
      {
         this.MinValue = String.Empty;
         this.MaxValue = String.Empty;
         this.stringList = list;
      }

      public override bool isInRange(Object value)
      {
         return true;
      }

      public override bool isValidString(string valueString)
      {
         return stringList.Contains(valueString.ToUpper());
      }

   }
   public class ParamRangeIndicator : ParamRange
   {
      public ParamRangeIndicator()
      {
         this.MinValue = String.Empty;
         this.MaxValue = String.Empty;
      }

      public override bool isInRange(Object value)
      {
         return StockIndicatorManager.Supports(value.ToString());
      }

      public override bool isValidString(string valueString)
      {
         return StockIndicatorManager.Supports(valueString);
      }

      override public Type GetParamType()
      {
         return typeof(string);
      }
   }
   public class ParamRangeTrail : ParamRange
   {
      public ParamRangeTrail()
      {
         this.MinValue = String.Empty;
         this.MaxValue = String.Empty;
      }

      public override bool isInRange(Object value)
      {
         return StockTrailManager.Supports(value.ToString());
      }

      public override bool isValidString(string valueString)
      {
         return StockTrailManager.Supports(valueString);
      }

      override public Type GetParamType()
      {
         return typeof(string);
      }
   }

   public class ParamRangeStockName : ParamRange
   {
      public ParamRangeStockName()
      {
         this.MinValue = StockDictionary.StockDictionarySingleton.Keys.First();
         this.MaxValue = StockDictionary.StockDictionarySingleton.Keys.Last();
      }

      public override bool isInRange(Object value)
      {
         return StockDictionary.StockDictionarySingleton.ContainsKey(value.ToString());
      }

      public override bool isValidString(string valueString)
      {
         return StockDictionary.StockDictionarySingleton.ContainsKey(valueString);
      }

      override public Type GetParamType()
      {
         return typeof(string);
      }
   }
}
