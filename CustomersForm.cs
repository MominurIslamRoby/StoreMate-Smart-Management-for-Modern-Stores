using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.IO;

namespace IMS
{
    public partial class CustomersForm : Form
    {
        // 🔹 UPDATED: Now uses the central DatabaseConfig class
        private SqlConnection con = DatabaseConfig.GetConnection();

        public CustomersForm()
        {
            InitializeComponent();
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
        }

        private void CustomersForm_Load(object sender, EventArgs e)
        {
            cmbTimeFilter.SelectedIndex = 1;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.ReadOnly = true;
            dgvCustomers.AutoGenerateColumns = true;

            LoadCustomerList();
        }

        // Forces a clean reload when coming from Dashboard
        public void ShowAndRefresh()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(ShowAndRefresh));
                return;
            }
            this.BringToFront();
            txtSearch.Clear();
            LoadCustomerList();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadCustomerList();
            MessageBox.Show("Customer list synchronized with database.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadCustomerList(string searchTerm = "")
        {
            try
            {
                string query = "SELECT ID, Name, Phone, JoinDate FROM Customers";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query += " WHERE Name LIKE @search OR Phone LIKE @search";
                }

                // 🔹 UPDATED: Using a fresh connection instance and 'using' block for reliability
                using (SqlConnection localCon = DatabaseConfig.GetConnection())
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(query, localCon))
                    {
                        if (!string.IsNullOrEmpty(searchTerm))
                            da.SelectCommand.Parameters.AddWithValue("@search", "%" + searchTerm + "%");

                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvCustomers.DataSource = null;
                        dgvCustomers.DataSource = dt;

                        if (dt.Rows.Count > 0)
                        {
                            string firstName = dgvCustomers.Rows[0].Cells[1].Value.ToString();
                            LoadPurchaseHistory(firstName);
                        }
                        else
                        {
                            dgvPurchases.DataSource = null;
                            lblTotalPurchased.Text = "No customers found.";
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading customers: " + ex.Message); }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadCustomerList(txtSearch.Text.Trim());
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var cellValue = dgvCustomers.Rows[e.RowIndex].Cells[1].Value;
                if (cellValue != null && cellValue != DBNull.Value)
                {
                    LoadPurchaseHistory(cellValue.ToString().Trim());
                }
            }
        }

        private void LoadPurchaseHistory(string customerName)
        {
            int filterDays = 30;
            if (cmbTimeFilter.Text == "Last 7 Days") filterDays = 7;
            else if (cmbTimeFilter.Text == "Last 1 Year") filterDays = 365;

            try
            {
                string query = "SELECT SaleID, SaleDate, TotalAmount FROM Sales " +
                               "WHERE CustomerName = @name AND SaleDate >= DATEADD(day, -@days, GETDATE()) " +
                               "ORDER BY SaleDate DESC";

                // 🔹 UPDATED: Using the global config inside a local connection block
                using (SqlConnection localCon = DatabaseConfig.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, localCon))
                    {
                        cmd.Parameters.AddWithValue("@name", customerName);
                        cmd.Parameters.AddWithValue("@days", filterDays);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvPurchases.DataSource = dt;

                        decimal total = 0;
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row["TotalAmount"] != DBNull.Value)
                                    total += Convert.ToDecimal(row["TotalAmount"]);
                            }
                            lblTotalPurchased.Text = "Total Purchased: ৳ " + total.ToString("N2");
                            lblTotalPurchased.ForeColor = Color.Black;
                        }
                        else
                        {
                            lblTotalPurchased.Text = "No sales found.";
                            lblTotalPurchased.ForeColor = Color.Red;
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine("History Error: " + ex.Message); }
        }

        private void cmbTimeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow != null)
            {
                var nameVal = dgvCustomers.CurrentRow.Cells[1].Value;
                if (nameVal != null && nameVal != DBNull.Value)
                    LoadPurchaseHistory(nameVal.ToString().Trim());
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvPurchases.Rows.Count == 0 || dgvCustomers.CurrentRow == null) return;

            try
            {
                PrintPreviewDialog ppd = new PrintPreviewDialog
                {
                    Document = printDocument1,
                    WindowState = FormWindowState.Maximized
                };
                ppd.ShowDialog(this);
            }
            catch (Exception ex) { MessageBox.Show("Print Error: " + ex.Message); }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            float pWidth = e.PageBounds.Width;
            int y = 40;

            Font fTitle = new Font("Arial", 22, FontStyle.Bold);
            Font fSlogan = new Font("Arial", 10, FontStyle.Italic);
            Font fStore = new Font("Arial", 14, FontStyle.Bold);
            Font fHeader = new Font("Arial", 10, FontStyle.Bold);
            Font fBody = new Font("Arial", 10, FontStyle.Regular);
            Font fFooter = new Font("Arial", 9, FontStyle.Italic);

            try
            {
                string logoPath = @"C:\Users\Roby\OneDrive\Desktop\Project materials\New folder\IMS\IMS\images\STORE mATE.png";
                if (File.Exists(logoPath))
                {
                    Image logo = Image.FromFile(logoPath);
                    g.DrawImage(logo, (pWidth - 80) / 2, y, 80, 80);
                    y += 90;
                }
            }
            catch { y += 10; }

            g.DrawString("STORE MATE", fTitle, Brushes.Black, (pWidth - g.MeasureString("STORE MATE", fTitle).Width) / 2, y); y += 40;
            g.DrawString("Smart Management For Modern Stores", fSlogan, Brushes.Gray, (pWidth - g.MeasureString("Smart Management For Modern Stores", fSlogan).Width) / 2, y); y += 25;
            g.DrawString("Ataur Grocery Shop", fStore, Brushes.Black, (pWidth - g.MeasureString("Ataur Grocery Shop", fStore).Width) / 2, y); y += 25;
            g.DrawString("Branch: Thakurgaon | Bhully Bazar, Thakurgaon - 5100", fBody, Brushes.Black, (pWidth - g.MeasureString("Branch: Thakurgaon | Bhully Bazar, Thakurgaon - 5100", fBody).Width) / 2, y); y += 45;

            g.DrawLine(new Pen(Color.Black, 2), 40, y, pWidth - 40, y); y += 20;

            string cName = dgvCustomers.CurrentRow.Cells[1].Value.ToString();
            g.DrawString("CUSTOMER HISTORY REPORT", fHeader, Brushes.Black, 40, y); y += 25;
            g.DrawString("Customer: " + cName, fBody, Brushes.Black, 40, y);
            g.DrawString("Report Date: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm"), fBody, Brushes.Black, pWidth - 280, y); y += 40;

            g.FillRectangle(Brushes.LightGray, 40, y, pWidth - 80, 30);
            g.DrawRectangle(Pens.Black, 40, y, pWidth - 80, 30);
            g.DrawString("Invoice No", fHeader, Brushes.Black, 50, y + 8);
            g.DrawString("Sale Date", fHeader, Brushes.Black, 250, y + 8);
            g.DrawString("Total Amount", fHeader, Brushes.Black, pWidth - 180, y + 8);
            y += 30;

            foreach (DataGridViewRow row in dgvPurchases.Rows)
            {
                if (row.IsNewRow) continue;
                DataRowView drv = row.DataBoundItem as DataRowView;
                if (drv != null)
                {
                    g.DrawRectangle(Pens.Black, 40, y, pWidth - 80, 25);
                    g.DrawString(drv["SaleID"].ToString(), fBody, Brushes.Black, 50, y + 5);
                    g.DrawString(Convert.ToDateTime(drv["SaleDate"]).ToString("dd-MM-yyyy"), fBody, Brushes.Black, 250, y + 5);
                    g.DrawString("৳ " + Convert.ToDecimal(drv["TotalAmount"]).ToString("N2"), fBody, Brushes.Black, pWidth - 180, y + 5);
                    y += 25;
                }
            }

            y += 20;
            g.DrawString(lblTotalPurchased.Text, fStore, Brushes.Black, pWidth - g.MeasureString(lblTotalPurchased.Text, fStore).Width - 50, y);

            y += 60;
            string footer1 = "This is a computer-generated report. Powered by StoreMate";
            g.DrawString(footer1, fFooter, Brushes.Gray, (pWidth - g.MeasureString(footer1, fFooter).Width) / 2, y); y += 20;
            string footer2 = "This software was proudly developed by Roby";
            g.DrawString(footer2, fHeader, Brushes.Black, (pWidth - g.MeasureString(footer2, fHeader).Width) / 2, y);
        }
    }
}