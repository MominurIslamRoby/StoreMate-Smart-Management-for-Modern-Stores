
namespace IMS
{
    partial class DashboardHomeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblSubText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTotalProducts = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTodaysSales = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotalCustomers = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotalSales = new System.Windows.Forms.Label();
            this.chartSales = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartTopProducts = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cmbTimeFilter = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTopProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSubText
            // 
            this.lblSubText.AutoSize = true;
            this.lblSubText.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblSubText.Location = new System.Drawing.Point(462, 35);
            this.lblSubText.Name = "lblSubText";
            this.lblSubText.Size = new System.Drawing.Size(598, 38);
            this.lblSubText.TabIndex = 1;
            this.lblSubText.Text = "Here’s what’s happening in your store today";
            this.lblSubText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblTotalProducts);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(23, 139);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 120);
            this.panel1.TabIndex = 2;
            // 
            // lblTotalProducts
            // 
            this.lblTotalProducts.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTotalProducts.AutoSize = true;
            this.lblTotalProducts.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalProducts.Location = new System.Drawing.Point(63, 24);
            this.lblTotalProducts.Name = "lblTotalProducts";
            this.lblTotalProducts.Size = new System.Drawing.Size(77, 45);
            this.lblTotalProducts.TabIndex = 1;
            this.lblTotalProducts.Text = "120";
            this.lblTotalProducts.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(41, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total Products";
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.AutoSize = true;
            this.panel2.BackColor = System.Drawing.Color.AliceBlue;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lblTodaysSales);
            this.panel2.Location = new System.Drawing.Point(485, 139);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 120);
            this.panel2.TabIndex = 3;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(45, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 23);
            this.label3.TabIndex = 1;
            this.label3.Text = "Today’s Sales";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblTodaysSales
            // 
            this.lblTodaysSales.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTodaysSales.AutoSize = true;
            this.lblTodaysSales.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTodaysSales.Location = new System.Drawing.Point(13, 13);
            this.lblTodaysSales.Name = "lblTodaysSales";
            this.lblTodaysSales.Size = new System.Drawing.Size(165, 45);
            this.lblTodaysSales.TabIndex = 0;
            this.lblTodaysSales.Text = "৳ 45,000";
            this.lblTodaysSales.Click += new System.EventHandler(this.label4_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel3.AutoSize = true;
            this.panel3.BackColor = System.Drawing.Color.AliceBlue;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.lblTotalCustomers);
            this.panel3.Location = new System.Drawing.Point(1320, 139);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 120);
            this.panel3.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(36, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 23);
            this.label5.TabIndex = 1;
            this.label5.Text = "Total Customers";
            // 
            // lblTotalCustomers
            // 
            this.lblTotalCustomers.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTotalCustomers.AutoSize = true;
            this.lblTotalCustomers.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCustomers.Location = new System.Drawing.Point(72, 13);
            this.lblTotalCustomers.Name = "lblTotalCustomers";
            this.lblTotalCustomers.Size = new System.Drawing.Size(58, 45);
            this.lblTotalCustomers.TabIndex = 0;
            this.lblTotalCustomers.Text = "38";
            // 
            // panel4
            // 
            this.panel4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel4.AutoSize = true;
            this.panel4.BackColor = System.Drawing.Color.AliceBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.lblTotalSales);
            this.panel4.Location = new System.Drawing.Point(905, 139);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 120);
            this.panel4.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(45, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Total Sales";
            // 
            // lblTotalSales
            // 
            this.lblTotalSales.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTotalSales.AutoSize = true;
            this.lblTotalSales.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSales.Location = new System.Drawing.Point(13, 13);
            this.lblTotalSales.Name = "lblTotalSales";
            this.lblTotalSales.Size = new System.Drawing.Size(165, 45);
            this.lblTotalSales.TabIndex = 0;
            this.lblTotalSales.Text = "৳ 45,000";
            // 
            // chartSales
            // 
            chartArea3.Name = "ChartArea1";
            this.chartSales.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartSales.Legends.Add(legend3);
            this.chartSales.Location = new System.Drawing.Point(95, 406);
            this.chartSales.Name = "chartSales";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartSales.Series.Add(series3);
            this.chartSales.Size = new System.Drawing.Size(569, 300);
            this.chartSales.TabIndex = 5;
            this.chartSales.Text = "chart1";
            // 
            // chartTopProducts
            // 
            chartArea4.Name = "ChartArea1";
            this.chartTopProducts.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chartTopProducts.Legends.Add(legend4);
            this.chartTopProducts.Location = new System.Drawing.Point(912, 406);
            this.chartTopProducts.Name = "chartTopProducts";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chartTopProducts.Series.Add(series4);
            this.chartTopProducts.Size = new System.Drawing.Size(583, 300);
            this.chartTopProducts.TabIndex = 6;
            this.chartTopProducts.Text = "chart2";
            // 
            // cmbTimeFilter
            // 
            this.cmbTimeFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbTimeFilter.FormattingEnabled = true;
            this.cmbTimeFilter.Items.AddRange(new object[] {
            "Last 7 Days",
            "Last 30 Days",
            "This Year"});
            this.cmbTimeFilter.Location = new System.Drawing.Point(166, 344);
            this.cmbTimeFilter.Name = "cmbTimeFilter";
            this.cmbTimeFilter.Size = new System.Drawing.Size(235, 24);
            this.cmbTimeFilter.TabIndex = 7;
            this.cmbTimeFilter.SelectedIndexChanged += new System.EventHandler(this.cmbTimeFilter_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 342);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 23);
            this.label4.TabIndex = 2;
            this.label4.Text = "View chart for :";
            // 
            // DashboardHomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1596, 779);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbTimeFilter);
            this.Controls.Add(this.chartTopProducts);
            this.Controls.Add(this.chartSales);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblSubText);
            this.Name = "DashboardHomeForm";
            this.Text = "DashboardHomeForm";
            this.Load += new System.EventHandler(this.DashboardHomeForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTopProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblSubText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTotalProducts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTodaysSales;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotalCustomers;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotalSales;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSales;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTopProducts;
        private System.Windows.Forms.ComboBox cmbTimeFilter;
        private System.Windows.Forms.Label label4;
    }
}