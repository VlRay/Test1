
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

class MarketCandle
{
    public string TimeFormat;
    public double Open;
    public double Close;
    public double High;
    public double Low;
    public long SumVolum;


    public MarketCandle(string timeFormat, double open, double close, double high, double low, long sumVolum)
    {
        TimeFormat = timeFormat;
        Open = open;
        Close = close;
        High = high;
        Low = low;
        SumVolum = sumVolum;
    }

    public static IEnumerable<MarketCandle> GetMarketCandle(IEnumerable<MarketLine> marketList, int formatTimeLenght)
    {
        IEnumerable<MarketCandle> marketCandle = marketList.GroupBy(marketLine => marketLine.Time.Substring(0, formatTimeLenght))
                            .Select(marketLine =>
                            {
                                string timeFormat = marketLine.First().Time.Substring(0, formatTimeLenght);
                                IEnumerable<MarketLine> marketsListCond = marketList.Where(x => x.Time.Substring(0, formatTimeLenght) == timeFormat).OrderBy(x => x.Time);

                                MarketCandle marketCandle = new MarketCandle(timeFormat, marketsListCond.First().Price, marketsListCond.Last().Price,
                                                                    marketsListCond.Max(x => x.Price), marketsListCond.Min(x => x.Price), marketsListCond.Sum(x => x.Quantity))
                                    ;

                                return marketCandle;
                            });

        return marketCandle;
    }


}

