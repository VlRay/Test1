using System.Data;

namespace fluenttechFinancial
{
    partial class Main
    {
        private TableLayoutPanel tableLayoutPanel;
        private DataGridView dataGridView1;
        private Button btnLoadData;

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region WinForm Designer generated code

        private void InitializeComponent()
        {
            tableLayoutPanel = new TableLayoutPanel();
            dataGridView1 = new DataGridView();
            btnLoadData = new Button();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 1;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel.Controls.Add(dataGridView1, 0, 0);
            tableLayoutPanel.Controls.Add(btnLoadData, 0, 1);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.Size = new Size(1059, 634);
            tableLayoutPanel.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(1053, 588);
            dataGridView1.TabIndex = 0;
            // 
            // btnLoadData
            // 
            btnLoadData.Dock = DockStyle.Fill;
            btnLoadData.Location = new Point(3, 597);
            btnLoadData.Name = "btnLoadData";
            btnLoadData.Padding = new Padding(5);
            btnLoadData.Size = new Size(1053, 34);
            btnLoadData.TabIndex = 1;
            btnLoadData.Text = "Load Data";
            btnLoadData.Click += btnLoadData_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1059, 634);
            Controls.Add(tableLayoutPanel);
            Name = "Main";
            Text = "Fluent Candle Stick";
            tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}