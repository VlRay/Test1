using System.Data;

namespace fluenttechFinancial
{
    class MarketLine
    {
        public string Time;
        public int Quantity;
        public double Price;
        public string TimeWithMinute;

        public MarketLine(string time, int quantity, double price) {
            Time = time;
            Quantity = quantity;
            Price = price;
            TimeWithMinute = time.Substring(0,16);
        }

    }

    class MarketCandle
    {
        public string TimeWithMinute;
        public double Open;
        public double Close;
        public double High;
        public double Low;
        public int SumVolum;

        public MarketCandle(string timeWithMinute, double open, double close, double high, double low, int sumVolum)
        {
            TimeWithMinute = timeWithMinute;
            Open = open;
            Close = close;
            High = high;
            Low = low;
            SumVolum = sumVolum;
        }
    }

    public partial class Main : Form
    {
       

        public Main()
        {
            InitializeComponent();

            IEnumerable<MarketLine> marketsList = File.ReadAllLines("MarketDataTestLittle.csv")
                .Skip(1)
                .Select(csvLine =>
                {
                    string[] tmp = csvLine.Split(',');
                    MarketLine marketLine = new MarketLine(tmp[0], int.Parse(tmp[1]), double.Parse(tmp[2]));
                    return marketLine;
                })
                ;

            IEnumerable<string> marketsGroup = marketsList.GroupBy(x => x.TimeWithMinute)
                                                        .Select(x =>
                                                        {
                                                            return x.First().TimeWithMinute;
                                                        });


            IEnumerable<MarketCandle> marketsFinal = marketsGroup.Select(timeWithMinute => 
            {
                   IEnumerable<MarketLine> marketsListCond = marketsList.Where(x => x.TimeWithMinute == timeWithMinute).OrderBy(x => x.Time);
               
                   MarketCandle marketCandle = new MarketCandle(timeWithMinute, marketsListCond.First().Price, marketsListCond.Last().Price,
                marketsListCond.Max(x => x.Price), marketsListCond.Min(x => x.Price), marketsListCond.Sum(x => x.Quantity))
                       ;
                       return marketCandle;
              })
                                                        ;

            DataTable dt = new DataTable();

            dt.Columns.Add("Date");
            dt.Columns.Add("Open");
            dt.Columns.Add("Close");
            dt.Columns.Add("High");
            dt.Columns.Add("Low");
            dt.Columns.Add("Sum Volume");


            marketsFinal
                .ToList()
                .ForEach(x => {
               dt.Rows.Add(x.TimeWithMinute, x.Open, x.Close, x.High, x.Low, x.SumVolum);
            });
            
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}