using fluenttechFinancial.Models;
using System.ComponentModel;
using System.Globalization;

namespace fluenttechFinancial
{
    public partial class Main : Form
    {
        private readonly List<MarketRecord> _marketData = new();
        private readonly List<CandleStick> _candles = new();

        public Main()
        {
            InitializeComponent();
            ConfigureGrid();
        }
        private void ConfigureGrid()
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.RowPrePaint += HighlightCandleRow;
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Select Market Data CSV"
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                var records = LoadMarketData(openFileDialog.FileName);
                var candles = GenerateCandles(records);

                DisplayCandles(candles);

                _marketData.Clear();
                _marketData.AddRange(records);

                _candles.Clear();
                _candles.AddRange(candles);

                MessageBox.Show($"Loaded {records.Count} records.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static List<MarketRecord> LoadMarketData(string path)
        {
            return File.ReadLines(path)
                       .Skip(1) // Skip header
                       .Select(ParseMarketRecord)
                       .Where(record => record != null)
                       .ToList()!;
        }

        private static MarketRecord? ParseMarketRecord(string line)
        {
            var parts = line.Split(',');
            if (parts.Length < 3)
                return null;

            if (!DateTime.TryParse(parts[0], out var time) ||
                !double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out var quantity) ||
                !double.TryParse(parts[2], NumberStyles.Float, CultureInfo.InvariantCulture, out var price))
            {
                return null;
            }

            return new MarketRecord
            {
                Time = time,
                Quantity = quantity,
                Price = price
            };
        }

        private static List<CandleStick> GenerateCandles(IEnumerable<MarketRecord> records)
        {
            return records
                .GroupBy(r => new DateTime(r.Time.Year, r.Time.Month, r.Time.Day, r.Time.Hour, r.Time.Minute, 0))
                .Select(g => new CandleStick
                {
                    Minute = g.Key,
                    Open = g.OrderBy(r => r.Time).First().Price,
                    Close = g.OrderBy(r => r.Time).Last().Price,
                    High = g.Max(r => r.Price),
                    Low = g.Min(r => r.Price),
                    Volume = g.Sum(r => r.Quantity)
                })
                .OrderBy(c => c.Minute)
                .ToList();
        }

        private void DisplayCandles(IEnumerable<CandleStick> candles)
        {
            var bindingList = new BindingList<CandleStick>(candles.ToList());
            var bindingSource = new BindingSource(bindingList, null);
            dataGridView1.DataSource = bindingSource;
        }

        private void HighlightCandleRow(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = dataGridView1.Rows[e.RowIndex];
            if (row.DataBoundItem is not CandleStick candle)
                return;

            row.DefaultCellStyle.BackColor = candle.Close > candle.Open
                ? Color.LightGreen
                : Color.LightCoral;
        }
    }
}
