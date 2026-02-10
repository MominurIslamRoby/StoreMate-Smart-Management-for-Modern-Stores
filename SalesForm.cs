using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;

namespace IMS
{
    public partial class SalesForm : Form
    {
        // 🔹 UPDATED: Now uses the central DatabaseConfig class
        private SqlConnection con = DatabaseConfig.GetConnection();

        private DataTable cartTable;
        private DataTable productsTable;

        private int currentSaleId = 0;
        private string invoiceNumber = "";
        private string customerName = "";
        private string customerPhone = "";
        private decimal netPayable = 0;
        private decimal amountPaid = 0;
        private decimal returnAmount = 0;
        private Bitmap qrBitmap;

        // --- LOGO PATH ---
        private string logoPath = @"C:\Users\Roby\OneDrive\Desktop\Project materials\New folder\IMS\IMS\images\STORE mATE.png";

        public SalesForm()
        {
            InitializeComponent();
        }

        private void SalesForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
            SetupCartTable();
            if (printPreviewDialogInvoice != null)
                printPreviewDialogInvoice.Document = printDocumentInvoice;
        }

        private void LoadProducts()
        {
            try
            {
                // 🔹 UPDATED: Ensuring we use the correct connection instance
                using (SqlConnection connection = DatabaseConfig.GetConnection())
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT ProductID, ProductName, Price, Quantity FROM Products WHERE Quantity > 0", connection);
                    productsTable = new DataTable();
                    da.Fill(productsTable);
                    dgvProduct.AutoGenerateColumns = false;
                    dgvProduct.DataSource = productsTable;
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading products: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void SetupCartTable()
        {
            cartTable = new DataTable();
            cartTable.Columns.Add("ProductID", typeof(int));
            cartTable.Columns.Add("ProductName", typeof(string));
            cartTable.Columns.Add("Price", typeof(decimal));
            cartTable.Columns.Add("Quantity", typeof(int));
            cartTable.Columns.Add("Total", typeof(decimal));
            dgvCart.AutoGenerateColumns = false;
            dgvCart.DataSource = cartTable;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (productsTable != null)
            {
                DataView dv = productsTable.DefaultView;
                dv.RowFilter = string.Format("ProductName LIKE '%{0}%'", txtSearch.Text.Replace("'", "''"));
                dgvProduct.DataSource = dv.ToTable();
            }
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (dgvProduct.CurrentRow == null) return;

            int pid = Convert.ToInt32(dgvProduct.CurrentRow.Cells["colProductID"].Value);
            string name = dgvProduct.CurrentRow.Cells["colProductName"].Value.ToString();
            decimal price = Convert.ToDecimal(dgvProduct.CurrentRow.Cells["colPrice"].Value);
            int stock = Convert.ToInt32(dgvProduct.CurrentRow.Cells["colStock"].Value);
            int qty = (int)nudQuantity.Value;

            foreach (DataRow row in cartTable.Rows)
            {
                if ((int)row["ProductID"] == pid)
                {
                    int newQty = (int)row["Quantity"] + qty;
                    if (newQty > stock) { MessageBox.Show("Stock limit reached for " + name, "Inventory Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                    row["Quantity"] = newQty;
                    row["Total"] = newQty * price;
                    UpdateGrandTotal();
                    return;
                }
            }

            if (qty > stock) { MessageBox.Show("Not enough stock available.", "Inventory Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            cartTable.Rows.Add(pid, name, price, qty, price * qty);
            UpdateGrandTotal();
        }

        private void UpdateGrandTotal()
        {
            decimal total = 0;
            foreach (DataRow row in cartTable.Rows) total += Convert.ToDecimal(row["Total"]);
            labelTotalText.Text = "৳ " + total.ToString("0.00");
            netPayable = total;
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (cartTable.Rows.Count == 0) { MessageBox.Show("Cart is empty!", "Checkout Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            // Customer Details Dialog
            using (Form f = new Form())
            {
                f.Text = "Customer Information"; f.Size = new Size(350, 220); f.StartPosition = FormStartPosition.CenterParent;
                f.FormBorderStyle = FormBorderStyle.FixedDialog; f.MaximizeBox = false; f.MinimizeBox = false;
                Label l1 = new Label { Text = "Customer Name:", Left = 20, Top = 20, Width = 150 };
                TextBox t1 = new TextBox { Left = 20, Top = 45, Width = 280, Text = "Guest" };
                Label l2 = new Label { Text = "Contact Number:", Left = 20, Top = 80, Width = 150 };
                TextBox t2 = new TextBox { Left = 20, Top = 105, Width = 280 };
                Button b = new Button { Text = "Proceed to Payment", Left = 180, Top = 145, Width = 120, Height = 30, BackColor = Color.LightBlue };

                b.Click += (s, ev) => { customerName = t1.Text; customerPhone = t2.Text; f.DialogResult = DialogResult.OK; };

                t1.KeyDown += (s, ev) => { if (ev.KeyCode == Keys.Enter) { ev.SuppressKeyPress = true; t2.Focus(); } };
                t2.KeyDown += (s, ev) => { if (ev.KeyCode == Keys.Enter) { ev.SuppressKeyPress = true; b.PerformClick(); } };

                f.Controls.AddRange(new Control[] { l1, t1, l2, t2, b });
                if (f.ShowDialog() != DialogResult.OK) return;
            }

            // Payment Dialog
            using (Form f = new Form())
            {
                f.Text = "Payment Gateway"; f.Size = new Size(300, 200); f.StartPosition = FormStartPosition.CenterParent;
                f.FormBorderStyle = FormBorderStyle.FixedDialog;
                Label l = new Label { Text = "Total Payable: ৳" + netPayable.ToString("N2"), Left = 20, Top = 20, Width = 250, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                Label l2 = new Label { Text = "Enter Cash Received:", Left = 20, Top = 55, Width = 200 };
                TextBox t = new TextBox { Left = 20, Top = 80, Width = 240, Font = new Font("Segoe UI", 12) };
                Button b = new Button { Text = "Finalize Transaction", Left = 70, Top = 120, Width = 150, Height = 30, BackColor = Color.LightGreen };

                b.Click += (s, ev) => {
                    if (decimal.TryParse(t.Text, out amountPaid) && amountPaid >= netPayable)
                    {
                        returnAmount = amountPaid - netPayable;
                        f.DialogResult = DialogResult.OK;
                    }
                    else { MessageBox.Show("Insufficient or invalid amount entered.", "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                };

                t.KeyDown += (s, ev) => { if (ev.KeyCode == Keys.Enter) { ev.SuppressKeyPress = true; b.PerformClick(); } };

                f.Controls.AddRange(new Control[] { l, l2, t, b });
                if (f.ShowDialog() != DialogResult.OK) return;
            }

            // 🔹 UPDATED: Using a local connection inside a using block for safety
            using (SqlConnection checkoutCon = DatabaseConfig.GetConnection())
            {
                checkoutCon.Open();
                SqlTransaction tran = checkoutCon.BeginTransaction();

                try
                {
                    string cQ = "IF NOT EXISTS(SELECT 1 FROM Customers WHERE Phone=@p) INSERT INTO Customers (Name, Phone, JoinDate) VALUES (@n, @p, @d)";
                    SqlCommand cmd1 = new SqlCommand(cQ, checkoutCon, tran);
                    cmd1.Parameters.AddWithValue("@n", customerName);
                    cmd1.Parameters.AddWithValue("@p", customerPhone);
                    cmd1.Parameters.AddWithValue("@d", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmd1.ExecuteNonQuery();

                    string sQ = "INSERT INTO Sales (SaleDate, TotalAmount, PaymentMethod, CreatedBy, CustomerName) OUTPUT INSERTED.SaleID VALUES (@d,@t,@m,@c,@cn)";
                    SqlCommand cmd2 = new SqlCommand(sQ, checkoutCon, tran);
                    cmd2.Parameters.AddWithValue("@d", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmd2.Parameters.AddWithValue("@t", netPayable);
                    cmd2.Parameters.AddWithValue("@m", "Cash");
                    cmd2.Parameters.AddWithValue("@c", "Admin");
                    cmd2.Parameters.AddWithValue("@cn", customerName);
                    currentSaleId = (int)cmd2.ExecuteScalar();
                    invoiceNumber = "INV-" + currentSaleId.ToString("D6");

                    foreach (DataRow r in cartTable.Rows)
                    {
                        string iQ = "INSERT INTO SaleItems (SaleID, ProductID, ProductName, Price, Quantity, SubTotal) VALUES (@sid,@pid,@pn,@pr,@q,@sub)";
                        SqlCommand cmd3 = new SqlCommand(iQ, checkoutCon, tran);
                        cmd3.Parameters.AddWithValue("@sid", currentSaleId);
                        cmd3.Parameters.AddWithValue("@pid", r["ProductID"]);
                        cmd3.Parameters.AddWithValue("@pn", r["ProductName"]);
                        cmd3.Parameters.AddWithValue("@pr", r["Price"]);
                        cmd3.Parameters.AddWithValue("@q", r["Quantity"]);
                        cmd3.Parameters.AddWithValue("@sub", r["Total"]);
                        cmd3.ExecuteNonQuery();

                        SqlCommand cmdStock = new SqlCommand("UPDATE Products SET Quantity = Quantity - @qty WHERE ProductID=@id", checkoutCon, tran);
                        cmdStock.Parameters.AddWithValue("@qty", r["Quantity"]);
                        cmdStock.Parameters.AddWithValue("@id", r["ProductID"]);
                        cmdStock.ExecuteNonQuery();
                    }

                    tran.Commit();
                    qrBitmap = GenerateQRCode(invoiceNumber);

                    SaveInvoicePDF();

                    Form previewForm = (Form)printPreviewDialogInvoice;
                    previewForm.WindowState = FormWindowState.Maximized;
                    printPreviewDialogInvoice.ShowDialog();

                    MessageBox.Show("Transaction Completed Successfully!\nChange to Return: ৳" + returnAmount.ToString("N2"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cartTable.Clear(); UpdateGrandTotal(); LoadProducts();
                }
                catch (Exception ex)
                {
                    if (tran != null) tran.Rollback();
                    MessageBox.Show("Database Transaction Failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Bitmap GenerateQRCode(string text)
        {
            var writer = new BarcodeWriter { Format = BarcodeFormat.QR_CODE, Options = new EncodingOptions { Height = 100, Width = 100, Margin = 1 } };
            return writer.Write(text);
        }

        private void SaveInvoicePDF()
        {
            try
            {
                string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "StoreMate_Invoices");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                printDocumentInvoice.PrinterSettings.PrinterName = "Microsoft Print to PDF";
                printDocumentInvoice.PrinterSettings.PrintToFile = true;
                printDocumentInvoice.PrinterSettings.PrintFileName = Path.Combine(folder, invoiceNumber + ".pdf");
                printDocumentInvoice.Print();
            }
            catch { }
        }

        private void printDocumentInvoice_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font fTitle = new Font("Arial", 18, FontStyle.Bold);
            Font fSub = new Font("Arial", 10);
            Font fHead = new Font("Arial", 10, FontStyle.Bold);
            Font fBody = new Font("Arial", 10);

            int startY = 20;

            // --- DRAW LOGO AT TOP CENTER ---
            if (File.Exists(logoPath))
            {
                Image logo = Image.FromFile(logoPath);
                int logoWidth = 80;
                int logoHeight = 80;
                int logoX = (e.PageBounds.Width / 2) - (logoWidth / 2);
                g.DrawImage(logo, logoX, startY, logoWidth, logoHeight);
                startY += 90;
            }

            g.DrawString("STORE MATE", fTitle, Brushes.DarkBlue, 320, startY);
            g.DrawString("Smart Management For Modern Stores", new Font("Arial", 9, FontStyle.Italic), Brushes.Black, 310, startY + 35);
            g.DrawString("Ataur Grocery Shop", fHead, Brushes.Black, 340, startY + 60);
            g.DrawString("Branch: Thakurgaon", fSub, Brushes.Black, 345, startY + 80);
            g.DrawString("Location: Bhully Bazar, Thakurgaon - 5100", fSub, Brushes.Black, 290, startY + 95);

            int y = startY + 140;

            g.DrawString("Invoice No: " + invoiceNumber, fHead, Brushes.Black, 50, y);
            g.DrawString("Date: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm"), fSub, Brushes.Black, 580, y);
            g.DrawString("Customer: " + customerName, fSub, Brushes.Black, 50, y + 25);

            y += 60;
            g.DrawLine(Pens.Black, 50, y, 750, y); y += 10;
            g.DrawString("Item Description", fHead, Brushes.Black, 60, y);
            g.DrawString("Qty", fHead, Brushes.Black, 450, y);
            g.DrawString("Price", fHead, Brushes.Black, 550, y);
            g.DrawString("Total", fHead, Brushes.Black, 670, y);
            y += 25; g.DrawLine(Pens.Black, 50, y, 750, y); y += 15;

            foreach (DataRow r in cartTable.Rows)
            {
                g.DrawString(r["ProductName"].ToString(), fBody, Brushes.Black, 60, y);
                g.DrawString(r["Quantity"].ToString(), fBody, Brushes.Black, 450, y);
                g.DrawString(Convert.ToDecimal(r["Price"]).ToString("N2"), fBody, Brushes.Black, 550, y);
                g.DrawString(Convert.ToDecimal(r["Total"]).ToString("N2"), fBody, Brushes.Black, 670, y);
                y += 25;
            }

            y += 20; g.DrawLine(Pens.Black, 50, y, 750, y); y += 15;

            g.DrawString("GRAND TOTAL:", fHead, Brushes.Black, 500, y);
            g.DrawString("৳ " + netPayable.ToString("N2"), fHead, Brushes.Black, 650, y); y += 25;
            g.DrawString("Amount Paid:", fBody, Brushes.Black, 500, y);
            g.DrawString("৳ " + amountPaid.ToString("N2"), fBody, Brushes.Black, 650, y); y += 25;
            g.DrawString("Change Return:", fBody, Brushes.Black, 500, y);
            g.DrawString("৳ " + returnAmount.ToString("N2"), fBody, Brushes.Black, 650, y);

            y += 40;

            if (qrBitmap != null)
            {
                int qrX = (e.PageBounds.Width - 100) / 2;
                g.DrawImage(qrBitmap, qrX, y, 100, 100);
                y += 110;
            }

            g.DrawString("Thank you for your purchase!", fHead, Brushes.Black, 310, y); y += 20;
            g.DrawString("Terms: Goods once sold are not returnable.", fSub, Brushes.Gray, 290, y);

            y += 60;
            StringFormat sf = new StringFormat { Alignment = StringAlignment.Center };
            g.DrawString("This software was proudly developed by Roby", new Font("Arial", 8, FontStyle.Bold), Brushes.DimGray, new RectangleF(0, y, 827, 20), sf);
        }

        private void btnReprintInvoice_Click(object sender, EventArgs e)
        {
            using (Form f = new Form())
            {
                f.Text = "Reprint System"; f.Size = new Size(300, 160); f.StartPosition = FormStartPosition.CenterParent;
                Label lbl = new Label { Text = "Enter Invoice ID:", Left = 20, Top = 10 };
                TextBox txt = new TextBox { Left = 20, Top = 35, Width = 240 };
                Button btn = new Button { Text = "Open Invoice PDF", Left = 80, Top = 75, Width = 120 };

                btn.Click += (s, ev) => {
                    string id = txt.Text.ToUpper().Contains("INV-") ? txt.Text : "INV-" + txt.Text.PadLeft(6, '0');
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "StoreMate_Invoices", id + ".pdf");
                    if (File.Exists(path)) System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(path) { UseShellExecute = true });
                    else MessageBox.Show("Invoice file not found.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                };

                txt.KeyDown += (s, ev) => { if (ev.KeyCode == Keys.Enter) { ev.SuppressKeyPress = true; btn.PerformClick(); } };

                f.Controls.AddRange(new Control[] { lbl, txt, btn });
                f.ShowDialog();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvCart.CurrentRow != null) { dgvCart.Rows.RemoveAt(dgvCart.CurrentRow.Index); UpdateGrandTotal(); }
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear the cart?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                cartTable.Clear(); UpdateGrandTotal();
            }
        }
    }
}