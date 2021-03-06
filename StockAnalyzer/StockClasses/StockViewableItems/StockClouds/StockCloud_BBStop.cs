﻿using StockAnalyzer.StockMath;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace StockAnalyzer.StockClasses.StockViewableItems.StockClouds
{
    public class StockCloud_BBStop : StockCloudBase
    {
        public override IndicatorDisplayTarget DisplayTarget
        {
            get { return IndicatorDisplayTarget.PriceIndicator; }
        }
        public override string Definition => "Paint a cloud base on two EMA lines";

        public override string[] ParameterNames
        {
            get { return new string[] { "Period", "NbUpDev", "NbDownDev", "MAType" }; }
        }

        public override Object[] ParameterDefaultValues
        {
            get { return new Object[] { 20, 2.0f, -2.0f, "MA" }; }
        }
        static List<string> emaTypes = new List<string>() { "EMA", "HMA", "MA", "EA", "MID" };
        public override ParamRange[] ParameterRanges
        {
            get
            {
                return new ParamRange[]
                {
                new ParamRangeInt(1, 500),
                new ParamRangeFloat(0f, 20.0f),
                new ParamRangeFloat(-20.0f, 0.0f),
                new ParamRangeStringList( emaTypes)
                };
            }
        }
        public override Pen[] SeriePens
        {
            get
            {
                if (seriePens == null)
                {
                    seriePens = new Pen[] { new Pen(Color.Green, 1), new Pen(Color.DarkRed, 1), new Pen(Color.DarkBlue, 2) };
                }
                return seriePens;
            }
        }
        public override string[] SerieNames { get { return new string[] { "Bull", "Bear", "MA" }; } }
        public override void ApplyTo(StockSerie stockSerie)
        {
            var bbIndicator = stockSerie.GetIndicator($"BB({(int)this.parameters[0]},{(float)this.parameters[1]},{(float)this.parameters[2]},{this.parameters[3]})");
            var bbUp = bbIndicator.Series[0];
            var bbDown = bbIndicator.Series[1];
            var maSerie = bbIndicator.Series[2];
            FloatSerie closeSerie = stockSerie.GetSerie(StockDataType.CLOSE);

            var bullSerie = new FloatSerie(stockSerie.Count);
            var bearSerie = new FloatSerie(stockSerie.Count);

            bool isBull = true;
            bullSerie[0] = bbUp[0];
            bearSerie[0] = bbDown[0];
            for (int i = 1; i < stockSerie.Count; i++)
            {
                if (isBull)
                {
                    if (closeSerie[i] < bearSerie[i - 1]) // Reverse
                    {
                        isBull = false;
                        bullSerie[i] = bbDown[i];
                        bearSerie[i] = bbUp[i];
                    }
                    else // Continue;
                    {
                        bullSerie[i] = bbUp[i];
                        bearSerie[i] = Math.Max(bbDown[i], bearSerie[i - 1]);
                    }
                }
                else
                {
                    if (closeSerie[i] > bearSerie[i - 1]) // Reverse
                    {
                        isBull = true;
                        bullSerie[i] = bbUp[i];
                        bearSerie[i] = bbDown[i];
                    }
                    else // Continue;
                    {
                        bullSerie[i] = bbDown[i];
                        bearSerie[i] = Math.Min(bbUp[i], bearSerie[i - 1]);
                    }
                }
            }

            this.Series[0] = bullSerie;
            this.Series[1] = bearSerie;
            this.Series[2] = maSerie;
            this.Series[2].Name = maSerie.Name;

            // Detecting events
            base.GenerateEvents(stockSerie);
            for (int i = 5; i < stockSerie.Count; i++)
            {
                var bullVal = bullSerie[i];
                var bearVal = bearSerie[i];
                var signalVal = maSerie[i];
                var upBand = Math.Max(bullVal, bearVal);
                var lowBand = Math.Min(bullVal, bearVal);

                if (signalVal > upBand)
                {
                    this.Events[7][i] = true;
                    this.Events[10][i] = !this.Events[7][i - 1];
                }
                else if (signalVal < lowBand)
                {
                    this.Events[8][i] = true;
                    this.Events[11][i] = !this.Events[8][i - 1];
                }
                else
                {
                    this.Events[9][i] = true;
                }
            }

        }
        public override string[] EventNames => eventNames;

        protected static new string[] eventNames = new string[]
  {
             "AboveCloud", "BelowCloud", "InCloud",     // 0,1,2
             "BullishCloud", "BearishCloud",            // 3, 4
             "CloudUp", "CloudDown",                    // 5, 6
             "SignalUp", "SignalDown" , "SignalInCloud",// 7,8,9
             "BrokenUp", "BrokenDown"                   // 10,11
  };
        public override bool[] IsEvent => isEvent;

        protected static new readonly bool[] isEvent = new bool[] { false, false, false, false, false, true, true, false, false, false, true, true };

    }
}
