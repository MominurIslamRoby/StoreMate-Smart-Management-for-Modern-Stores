using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace IMS
{
    public partial class ProductsForm : Form
    {
        // 🔹 UPDATED DATABASE CONNECTION FOR MICROSOFT SQL SERVER
        // Using Initial Catalog for the attached database and Encrypt=False to avoid SSL certificate errors
        SqlConnection con = new SqlConnection(
            @"Data Source=.\SQLEXPRESS;Initial Catalog=StoreMateDB;Integrated Security=True;Encrypt=False"
        );

        private DataTable productsTable;
        private int selectedProductId = -1;
        private const int LOW_STOCK_LIMIT = 5;
        private bool lowStockAlertShown = false;

        public ProductsForm()
        {
            InitializeComponent();
            printDocument1.PrintPage += new PrintPageEventHandler(PrintProductsPage);
        }

        private void ProductsForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
            CheckLowStockAlert();
        }

        private void LoadProducts()
        {
            try
            {
                // Ensure connection is open if needed, though DataAdapter handles it automatically
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT ProductID, ProductName, Category, Price, Quantity, Description, CreatedAt FROM Products",
                    con
                );
                productsTable = new DataTable();
                da.Fill(productsTable);
                dgvProducts.AutoGenerateColumns = false;
                dgvProducts.DataSource = productsTable;

                if (dgvProducts.Columns.Contains("colLowStock")) dgvProducts.Columns["colLowStock"].Visible = false;

                HighlightLowStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data from SQL Server: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProducts();
            if (txtSearch != null) txtSearch.Clear();
        }

        // ===================== PRO PRINT & AUTO-SAVE LOGIC =====================
        private void btnPrintProducts_Click(object sender, EventArgs e)
        {
            try
            {
                string folderPath = @"C:\Users\Roby\OneDrive\Desktop\Products list";
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                string fileName = $"Inventory_Report_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                string fullPath = Path.Combine(folderPath, fileName);

                printDocument1.PrinterSettings.PrinterName = "Microsoft Print to PDF";
                printDocument1.PrinterSettings.PrintToFile = true;
                printDocument1.PrinterSettings.PrintFileName = fullPath;
                printDocument1.PrintController = new StandardPrintController();

                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.WindowState = FormWindowState.Maximized;

                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                    MessageBox.Show($"Success! Report saved to:\n{fullPath}", "IMS Auto-Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.Diagnostics.Process.Start("explorer.exe", folderPath);
                }
            }
            catch (Exception ex) { MessageBox.Show("Printing Error: " + ex.Message); }
        }

        private void PrintProductsPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            float pageWidth = e.PageSettings.PrintableArea.Width;
            int marginL = 50, marginR = (int)pageWidth - 50, y = 30;

            Font fontHeader = new Font("Segoe UI", 20, FontStyle.Bold);
            Font fontSubHeader = new Font("Segoe UI", 10, FontStyle.Regular);
            Font fontTableHead = new Font("Segoe UI", 10, FontStyle.Bold);
            Font fontBodyRegular = new Font("Segoe UI", 9, FontStyle.Regular);
            Font fontBodyRedBold = new Font("Segoe UI", 9, FontStyle.Bold);
            Font fontFooter = new Font("Segoe UI", 9, FontStyle.Bold);

            // LOGO Logic
            string logoPath = @"C:\Users\Roby\OneDrive\Desktop\Project materials\New folder\IMS\IMS\images\STORE mATE.png";
            if (File.Exists(logoPath))
            {
                Image logo = Image.FromFile(logoPath);
                g.DrawImage(logo, (pageWidth / 2) - 45, y, 90, 90);
                y += 100;
            }

            StringFormat centerFormat = new StringFormat() { Alignment = StringAlignment.Center };
            g.DrawString("STORE MATE", fontHeader, Brushes.Navy, new RectangleF(0, y, pageWidth, 35), centerFormat);
            y += 35;
            g.DrawString("Smart Management For Modern Stores", fontSubHeader, Brushes.DimGray, new RectangleF(0, y, pageWidth, 20), centerFormat);
            y += 45;

            g.DrawString("OFFICIAL INVENTORY AUDIT REPORT", fontTableHead, Brushes.Black, marginL, y);
            g.DrawString("Date: " + DateTime.Now.ToString("dd MMM yyyy, HH:mm"), fontSubHeader, Brushes.Black, marginR - 200, y);
            y += 25;
            g.DrawLine(new Pen(Color.Navy, 2), marginL, y, marginR, y);
            y += 10;

            int col1 = marginL + 5, col2 = 330, col3 = 530, col4 = 680;
            g.DrawString("ITEM DESCRIPTION", fontTableHead, Brushes.Black, col1, y);
            g.DrawString("CATEGORY", fontTableHead, Brushes.Black, col2, y);
            g.DrawString("UNIT PRICE", fontTableHead, Brushes.Black, col3, y);
            g.DrawString("STOCK LEVEL", fontTableHead, Brushes.Black, col4, y);
            y += 25;
            g.DrawLine(Pens.Black, marginL, y, marginR, y);
            y += 10;

            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                if (row.IsNewRow) continue;
                string pName = row.Cells["colProductName"].Value?.ToString() ?? "-";
                string pCat = row.Cells["colCategory"].Value?.ToString() ?? "-";
                string pPrice = "৳ " + (row.Cells["colPrice"].Value?.ToString() ?? "0.00");
                int pQty = Convert.ToInt32(row.Cells["colQuantity"].Value ?? 0);

                Brush textBrush = Brushes.Black;
                Font rowFont = fontBodyRegular;

                if (pQty <= LOW_STOCK_LIMIT)
                {
                    textBrush = Brushes.Red;
                    rowFont = fontBodyRedBold;
                    g.FillRectangle(new SolidBrush(Color.FromArgb(15, 255, 0, 0)), marginL, y - 2, marginR - marginL, 22);
                }

                g.DrawString(pName, rowFont, textBrush, col1, y);
                g.DrawString(pCat, rowFont, textBrush, col2, y);
                g.DrawString(pPrice, rowFont, textBrush, col3, y);
                g.DrawString(pQty.ToString(), rowFont, textBrush, col4, y);

                y += 22;
                g.DrawLine(Pens.LightGray, marginL, y, marginR, y);
                y += 5;

                if (y > e.MarginBounds.Bottom - 80) { e.HasMorePages = true; return; }
            }

            y = e.MarginBounds.Bottom - 30;
            g.DrawLine(Pens.Black, marginL, y, marginR, y);
            y += 5;
            g.DrawString("This software is proudly developed by Roby", fontFooter, Brushes.Black, new RectangleF(0, y, pageWidth, 20), centerFormat);
        }

        // ===================== CORE CRUD LOGIC =====================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();

                // 🚨 DUPLICATE CHECK
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Products WHERE ProductName = @n", con);
                checkCmd.Parameters.AddWithValue("@n", txtProductName.Text);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show($"'{txtProductName.Text}' is already in stock!", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SqlCommand cmd = new SqlCommand(@"INSERT INTO Products (ProductName, Category, Price, Quantity, Description, CreatedAt) 
                                                VALUES (@n,@c,@p,@q,@d,GETDATE())", con);
                cmd.Parameters.AddWithValue("@n", txtProductName.Text);
                cmd.Parameters.AddWithValue("@c", txtCategory.Text);
                cmd.Parameters.AddWithValue("@p", decimal.Parse(txtPrice.Text)); // Parse to decimal for SQL compatibility
                cmd.Parameters.AddWithValue("@q", int.Parse(txtQuantity.Text)); // Parse to int for SQL compatibility
                cmd.Parameters.AddWithValue("@d", txtDescription.Text);
                cmd.ExecuteNonQuery();

                LoadProducts();
                ClearFields();
                MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("Insert Error: " + ex.Message); }
            finally { con.Close(); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedProductId == -1) { MessageBox.Show("Please select a product to update."); return; }
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                SqlCommand cmd = new SqlCommand(@"UPDATE Products SET ProductName=@n, Category=@c, Price=@p, Quantity=@q, Description=@d WHERE ProductID=@id", con);
                cmd.Parameters.AddWithValue("@id", selectedProductId);
                cmd.Parameters.AddWithValue("@n", txtProductName.Text);
                cmd.Parameters.AddWithValue("@c", txtCategory.Text);
                cmd.Parameters.AddWithValue("@p", decimal.Parse(txtPrice.Text));
                cmd.Parameters.AddWithValue("@q", int.Parse(txtQuantity.Text));
                cmd.Parameters.AddWithValue("@d", txtDescription.Text);
                cmd.ExecuteNonQuery();

                LoadProducts();
                ClearFields();
                MessageBox.Show("Product updated successfully!", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("Update Error: " + ex.Message); }
            finally { con.Close(); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedProductId == -1) { MessageBox.Show("Please select a product to delete."); return; }
            if (MessageBox.Show("Are you sure you want to delete this product?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Products WHERE ProductID=@id", con);
                cmd.Parameters.AddWithValue("@id", selectedProductId);
                cmd.ExecuteNonQuery();

                LoadProducts();
                ClearFields();
                MessageBox.Show("Product deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("Delete Error: " + ex.Message); }
            finally { con.Close(); }
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvProducts.Rows[e.RowIndex];
            selectedProductId = Convert.ToInt32(row.Cells["colProductID"].Value);
            txtProductName.Text = row.Cells["colProductName"].Value?.ToString();
            txtCategory.Text = row.Cells["colCategory"].Value?.ToString();
            txtPrice.Text = row.Cells["colPrice"].Value?.ToString();
            txtQuantity.Text = row.Cells["colQuantity"].Value?.ToString();
            txtDescription.Text = row.Cells["colDescription"].Value?.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (productsTable == null) return;
            // Sanitizing input for basic protection
            string filter = txtSearch.Text.Replace("'", "''");
            productsTable.DefaultView.RowFilter = $"ProductName LIKE '%{filter}%' OR Category LIKE '%{filter}%'";
            HighlightLowStock();
        }

        private void CheckLowStockAlert()
        {
            if (productsTable == null || lowStockAlertShown) return;
            int count = 0;
            foreach (DataRow row in productsTable.Rows)
                if (Convert.ToInt32(row["Quantity"]) <= LOW_STOCK_LIMIT) count++;

            if (count > 0)
            {
                MessageBox.Show($"Alert: {count} items are running low on stock.", "Stock Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lowStockAlertShown = true;
            }
        }

        private void HighlightLowStock()
        {
            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                if (row.IsNewRow) continue;
                int qty = Convert.ToInt32(row.Cells["colQuantity"].Value);
                if (qty <= LOW_STOCK_LIMIT)
                {
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void ClearFields()
        {
            txtProductName.Clear();
            txtCategory.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            txtDescription.Clear();
            selectedProductId = -1;
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearFields();

        // DESIGNER STUBS
        private void label1_Click(object sender, EventArgs e) { }
        private void panelLeft_Paint(object sender, PaintEventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
    }
}