using System.ComponentModel;
using System.Data;

namespace fluenttechFinancial
{
 public partial class Main : Form
    {
        
        public static string nameFileCsv = "MarketDataTest.csv";

        public static int formatTimeLenght = 16;

        public Main()
        {
           
            InitializeComponent();

            // read csv
            IEnumerable<MarketLine> marketList = MarketLine.GetMarketListFromCsv(nameFileCsv);

            // calculation
            IEnumerable<MarketCandle> marketCandle = MarketCandle.GetMarketCandle(marketList, formatTimeLenght);

            DataTable dt = new DataTable();

            // header
            dt.Columns.Add("Date");
            dt.Columns.Add("Open");
            dt.Columns.Add("Close");
            dt.Columns.Add("High");
            dt.Columns.Add("Low");
            dt.Columns.Add("Sum Volume");


            marketCandle
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