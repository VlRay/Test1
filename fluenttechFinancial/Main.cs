using System.ComponentModel;
using System.Data;

namespace fluenttechFinancial
{
    class MarketLine
    {
        public string Time;
        public long Quantity;
        public double Price;

        public MarketLine(string time, long quantity, double price) {
            Time = time;
            Quantity = quantity;
            Price = price;
        }

    }

    

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
    }
   
    public partial class Main : Form
    {
        
        public Main()
        {
           

            InitializeComponent();

            // read csv
            IEnumerable<MarketLine> marketsList = File.ReadAllLines("MarketDataTest.csv")
                .Skip(1)
                .Select(csvLine =>
                {
                    string[] tmp = csvLine.Split(',');
                    MarketLine marketLine = new MarketLine(tmp[0], long.Parse(tmp[1]), double.Parse(tmp[2]));
                    return marketLine;
                });

            int formatTimeLenght = 16;

            // calculation
            IEnumerable<MarketCandle> marketsFinal = marketsList.GroupBy(marketLine => marketLine.Time.Substring(0, formatTimeLenght))
                                                        .Select(marketLine => 
            {
                   string timeFormat = marketLine.First().Time.Substring(0, formatTimeLenght);
                   IEnumerable<MarketLine> marketsListCond = marketsList.Where(x => x.Time.Substring(0, formatTimeLenght) == timeFormat).OrderBy(x => x.Time);
               
                   MarketCandle marketCandle = new MarketCandle(timeFormat, marketsListCond.First().Price, marketsListCond.Last().Price,
                                                     marketsListCond.Max(x => x.Price), marketsListCond.Min(x => x.Price), marketsListCond.Sum(x => x.Quantity))
                       ;

                       return marketCandle;
              })
                                                        ;

            DataTable dt = new DataTable();

            // header
            dt.Columns.Add("Date");
            dt.Columns.Add("Open");
            dt.Columns.Add("Close");
            dt.Columns.Add("High");
            dt.Columns.Add("Low");
            dt.Columns.Add("Sum Volume");


            marketsFinal
                .ToList()
                .ForEach(x => {
               dt.Rows.Add(x.TimeFormat, x.Open, x.Close, x.High, x.Low, x.SumVolum);
            });
            
            dataGridView1.DataSource = dt;

            // sort default
            dataGridView1.Sort(dataGridView1.Columns["Date"], ListSortDirection.Ascending);

        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            double closingPrice = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            double openingPrice = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[1].Value);

            if (closingPrice > openingPrice)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
            }
            else
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}