
class MarketLine
{
    public string Time;
    public long Quantity;
    public double Price;


    public MarketLine(string time, long quantity, double price)
    {
        Time = time;
        Quantity = quantity;
        Price = price;
    }

    public static IEnumerable<MarketLine> GetMarketListFromCsv(string nameFileCsv)
    {
        IEnumerable<MarketLine> marketList = File.ReadAllLines(nameFileCsv)
               .Skip(1)
               .Select(csvLine =>
               {
                   string[] tmp = csvLine.Split(',');
                   MarketLine marketLine = new MarketLine(tmp[0], long.Parse(tmp[1]), double.Parse(tmp[2]));
                   return marketLine;
               });

        return marketList;
    }

}
