namespace StockAnalyzer.StockClasses.StockStatistic.MatchPatterns
{
    public class StockMatchPattern_BarUp : IStockMatchPattern
    {
        public bool MatchPattern(StockSerie stockSerie, int index)
        {
            if (index < stockSerie.Count) return stockSerie.GetValue(StockDataType.VARIATION, index) > 0.0f;
            return false;
        }

        public string Suffix
        {
            get { return "BarUp"; }
        }
    }
}