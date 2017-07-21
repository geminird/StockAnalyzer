using System.Linq;

namespace StockAnalyzer.StockClasses.StockStatistic.MatchPatterns
{
    public class StockMatchPattern_ROR : IStockMatchPattern
    {
        public StockMatchPattern_ROR(float trigger)
        {
            this.Trigger = trigger;
        }

        public float Trigger { get; set; }

        public bool MatchPattern(StockSerie stockSerie, int index)
        {
            var ror = stockSerie.GetIndicator("ROR(50,1,1)").Series[1];
            return ror[index]>Trigger;
        }
    }
}