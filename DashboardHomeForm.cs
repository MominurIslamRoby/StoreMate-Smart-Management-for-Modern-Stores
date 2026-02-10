using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace IMS
{
    public partial class DashboardHomeForm : Form
    {
        private string loggedUser;
        // 🔹 UPDATED: Now uses central config
        private SqlConnection con = DatabaseConfig.GetConnection();

        public DashboardHomeForm()
        {
            InitializeComponent();
            InitializeTimeFilter();
        }

        public DashboardHomeForm(string username)
        {
            InitializeComponent();
            loggedUser = username;
            InitializeTimeFilter();
        }

        private void InitializeTimeFilter()
        {
            if (cmbTimeFilter.Items.Count == 0)
            {
                cmbTimeFilter.Items.Add("Last 7 Days");
                cmbTimeFilter.Items.Add("Last 30 Days");
                cmbTimeFilter.Items.Add("This Year");
            }
            cmbTimeFilter.SelectedIndex = 0;
        }

        private void DashboardHomeForm_Load(object sender, EventArgs e)
        {
            LoadDashboardStats();
            LoadTopProductsChart();
        }

        private void LoadDashboardStats()
        {
            try
            {
                // 🔹 UPDATED: Using 'using' block for clean aggregate queries
                using (SqlConnection localCon = DatabaseConfig.GetConnection())
                {
                    localCon.Open();

                    lblTotalProducts.Text = new SqlCommand("SELECT COUNT(*) FROM Products", localCon).ExecuteScalar().ToString();

                    lblTodaysSales.Text = "৳ " + Convert.ToDecimal(new SqlCommand("SELECT ISNULL(SUM(TotalAmount), 0) FROM Sales WHERE CAST(SaleDate AS DATE) = CAST(GETDATE() AS DATE)", localCon).ExecuteScalar()).ToString("N0");

                    lblTotalSales.Text = "৳ " + Convert.ToDecimal(new SqlCommand("SELECT ISNULL(SUM(TotalAmount), 0) FROM Sales", localCon).ExecuteScalar()).ToString("N0");

                    object custCount = new SqlCommand("SELECT COUNT(DISTINCT CustomerName) FROM Sales WHERE CustomerName IS NOT NULL AND CustomerName <> ''", localCon).ExecuteScalar();
                    lblTotalCustomers.Text = (custCount != DBNull.Value) ? custCount.ToString() : "0";
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading stats: " + ex.Message); }
        }

        private void cmbTimeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            int days = 7;
            if (cmbTimeFilter.Text == "Last 30 Days") days = 30;
            else if (cmbTimeFilter.Text == "This Year") days = 365;

            LoadSalesChart(days);
        }

        private void LoadSalesChart(int days = 7)
        {
            try
            {
                using (SqlConnection localCon = DatabaseConfig.GetConnection())
                {
                    // 🔹 UPDATED: Parameterized query for safer filtering
                    string query = @"SELECT CAST(SaleDate AS DATE) as Day, SUM(TotalAmount) as Total 
                                     FROM Sales 
                                     WHERE SaleDate >= DATEADD(day, -@days, GETDATE())
                                     GROUP BY CAST(SaleDate AS DATE) 
                                     ORDER BY Day ASC";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, localCon))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@days", days);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        chartSales.Series.Clear();
                        Series series = chartSales.Series.Add("Daily Revenue");

                        series.ChartType = (days <= 7) ? SeriesChartType.Column : SeriesChartType.SplineArea;
                        series.Color = System.Drawing.Color.RoyalBlue;
                        series.BorderWidth = 3;

                        foreach (DataRow row in dt.Rows)
                        {
                            series.Points.AddXY(Convert.ToDateTime(row["Day"]).ToString("dd MMM"), row["Total"]);
                        }

                        chartSales.Titles.Clear();
                        chartSales.Titles.Add($"Revenue - Last {days} Days");
                        chartSales.Invalidate();
                    }
                }
            }
            catch { /* Handle error silently */ }
        }

        private void LoadTopProductsChart()
        {
            try
            {
                using (SqlConnection localCon = DatabaseConfig.GetConnection())
                {
                    string query = @"SELECT TOP 5 ProductName, SUM(SubTotal) as Revenue 
                                     FROM SaleItems GROUP BY ProductName ORDER BY Revenue DESC";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, localCon))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        chartTopProducts.Series.Clear();
                        Series series = chartTopProducts.Series.Add("TopProducts");
                        series.ChartType = SeriesChartType.Pie;

                        foreach (DataRow row in dt.Rows)
                        {
                            series.Points.AddXY(row["ProductName"].ToString(), row["Revenue"]);
                        }
                        chartTopProducts.Titles.Clear();
                        chartTopProducts.Titles.Add("Top 5 Products by Revenue");
                        series["PieLabelStyle"] = "Outside";
                    }
                }
            }
            catch { /* Handle error silently */ }
        }

        // Empty event handlers kept for designer compatibility
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
    }
}