using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace IMS
{
    public partial class ReportsForm : Form
    {
        // 🔹 UPDATED: Connection is now handled via DatabaseConfig
        // The old private SqlConnection line has been removed to use central config.

        public ReportsForm()
        {
            InitializeComponent();
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today.AddDays(-7);
            dtpTo.Value = DateTime.Today;

            cmbReportType.Items.Clear();
            cmbReportType.Items.Add("Daily Sales");
            cmbReportType.Items.Add("Invoice Wise Sales");
            cmbReportType.Items.Add("Product Wise Sales");
            cmbReportType.Items.Add("Monthly Summary");
            cmbReportType.Items.Add("Low Stock Report");
            cmbReportType.SelectedIndex = 0;
        }

        private void btnGenerate_Click(object sender, EventArgs e) { GenerateReport(); }
        private void btnGenerateReport_Click(object sender, EventArgs e) { GenerateReport(); }

        private void GenerateReport()
        {
            if (cmbReportType.SelectedItem == null) return;

            string selectedReport = cmbReportType.SelectedItem.ToString();

            // Logic for query selection remains exactly as original
            switch (selectedReport)
            {
                case "Daily Sales":
                    LoadData(@"SELECT CAST(SaleDate AS DATE) as [Date], SUM(TotalAmount) as [Revenue] 
                               FROM Sales WHERE SaleDate BETWEEN @from AND @to 
                               GROUP BY CAST(SaleDate AS DATE)");
                    break;
                case "Invoice Wise Sales":
                    LoadData(@"SELECT SaleID, 'INV-' + FORMAT(SaleID, '000000') AS InvoiceNo, 
                               SaleDate, TotalAmount FROM Sales 
                               WHERE SaleDate BETWEEN @from AND @to");
                    break;
                case "Product Wise Sales":
                    LoadData(@"SELECT ProductName, SUM(Quantity) as QtySold, SUM(SubTotal) as Revenue 
                               FROM SaleItems WHERE SaleID IN (SELECT SaleID FROM Sales WHERE SaleDate BETWEEN @from AND @to)
                               GROUP BY ProductName");
                    break;
                case "Monthly Summary":
                    LoadData(@"SELECT FORMAT(SaleDate, 'MMMM yyyy') as [Month], SUM(TotalAmount) as [Total] 
                               FROM Sales GROUP BY FORMAT(SaleDate, 'MMMM yyyy')");
                    break;
                case "Low Stock Report":
                    LoadData(@"SELECT ProductName, Quantity, Price FROM Products WHERE Quantity <= 10");
                    break;
            }
        }

        private void LoadData(string query)
        {
            try
            {
                // 🔹 UPDATED: Using 'using' block with DatabaseConfig
                using (SqlConnection con = DatabaseConfig.GetConnection())
                {
                    con.Open();

                    // 1. Fetch Main Report Data
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@from", dtpFrom.Value.Date);
                        cmd.Parameters.AddWithValue("@to", dtpTo.Value.Date.AddDays(1).AddSeconds(-1));

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvReport.DataSource = dt;

                        // 2. Calculation Logic (Preserved exactly)
                        decimal totalRev = 0;
                        string col = dt.Columns.Contains("TotalAmount") ? "TotalAmount" :
                                     dt.Columns.Contains("Revenue") ? "Revenue" :
                                     dt.Columns.Contains("Total") ? "Total" : "";

                        if (!string.IsNullOrEmpty(col))
                        {
                            foreach (DataRow row in dt.Rows)
                                if (row[col] != DBNull.Value) totalRev += Convert.ToDecimal(row[col]);
                        }

                        lblPeriodTotal.Text = "৳ " + totalRev.ToString("N2");
                        label4.Text = "Total Sales: ৳ " + totalRev.ToString("N2");
                        label5.Text = "Total Invoices: " + dt.Rows.Count.ToString();

                        // 3. Top Product Logic (Preserved exactly)
                        using (SqlCommand topProdCmd = new SqlCommand(@"SELECT TOP 1 ProductName 
                                                                        FROM SaleItems 
                                                                        WHERE SaleID IN (SELECT SaleID FROM Sales WHERE SaleDate BETWEEN @from AND @to)
                                                                        GROUP BY ProductName 
                                                                        ORDER BY SUM(Quantity) DESC", con))
                        {
                            topProdCmd.Parameters.AddWithValue("@from", dtpFrom.Value.Date);
                            topProdCmd.Parameters.AddWithValue("@to", dtpTo.Value.Date.AddDays(1).AddSeconds(-1));

                            object result = topProdCmd.ExecuteScalar();
                            lblTopProducts.Text = result != null ? "Top Product: " + result.ToString() : "Top Product: ---";
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void btnClearReport_Click(object sender, EventArgs e)
        {
            dgvReport.DataSource = null;
            lblPeriodTotal.Text = "----";
            label4.Text = "Total Sales: ৳ 0.00";
            label5.Text = "Total Invoices: 0";
            lblTopProducts.Text = "Top Product: ---";
        }

        // 🔹 PDF PRINT LOGIC: Preserved entirely including iTextSharp formatting
        private void btnPrintPdf_Click(object sender, EventArgs e)
        {
            if (dgvReport.Rows.Count == 0) { MessageBox.Show("No data available to print."); return; }

            SaveFileDialog sfd = new SaveFileDialog { Filter = "PDF (*.pdf)|*.pdf", FileName = "Business_Report.pdf" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Document doc = new Document(PageSize.A4, 25, 25, 30, 30);
                    PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                    doc.Open();

                    BaseFont bfBold = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    BaseFont bfReg = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    iTextSharp.text.Font titleFont = new iTextSharp.text.Font(bfBold, 16);
                    iTextSharp.text.Font subTitleFont = new iTextSharp.text.Font(bfReg, 10);
                    iTextSharp.text.Font headFont = new iTextSharp.text.Font(bfBold, 11);
                    iTextSharp.text.Font normalFont = new iTextSharp.text.Font(bfReg, 10);

                    string imagePath = @"C:\Users\Roby\OneDrive\Desktop\Project materials\New folder\IMS\IMS\images\STORE mATE.png";
                    if (File.Exists(imagePath))
                    {
                        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagePath);
                        logo.ScaleToFit(80f, 80f);
                        logo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(logo);
                    }

                    Paragraph shopName = new Paragraph("Ataur Grocery Shop", titleFont) { Alignment = Element.ALIGN_CENTER };
                    doc.Add(shopName);
                    doc.Add(new Paragraph("Branch: Thakurgaon", subTitleFont) { Alignment = Element.ALIGN_CENTER });
                    doc.Add(new Paragraph("Location: Bhully Bazar, Thakurgaon - 5100", subTitleFont) { Alignment = Element.ALIGN_CENTER });
                    doc.Add(new Paragraph("Smart Management For Modern Stores", new iTextSharp.text.Font(bfReg, 8, iTextSharp.text.Font.ITALIC)) { Alignment = Element.ALIGN_CENTER });
                    doc.Add(new Chunk("\n"));

                    Paragraph reportInfo = new Paragraph($"{cmbReportType.SelectedItem.ToString().ToUpper()} REPORT", headFont) { Alignment = Element.ALIGN_CENTER };
                    doc.Add(reportInfo);
                    doc.Add(new Paragraph($"Period: {dtpFrom.Value.ToShortDateString()} - {dtpTo.Value.ToShortDateString()}", normalFont) { Alignment = Element.ALIGN_CENTER });
                    doc.Add(new Paragraph(lblTopProducts.Text, normalFont) { Alignment = Element.ALIGN_CENTER });
                    doc.Add(new Paragraph($"Generated: {DateTime.Now:dd-MM-yyyy HH:mm}", normalFont) { Alignment = Element.ALIGN_CENTER });
                    doc.Add(new Chunk("\n"));

                    PdfPTable table = new PdfPTable(dgvReport.Columns.Count) { WidthPercentage = 100 };
                    foreach (DataGridViewColumn col in dgvReport.Columns)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(col.HeaderText, headFont))
                        {
                            BackgroundColor = new BaseColor(240, 240, 240),
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            Padding = 6f
                        };
                        table.AddCell(cell);
                    }

                    foreach (DataGridViewRow row in dgvReport.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            PdfPCell dataCell = new PdfPCell(new Phrase(cell.Value?.ToString() ?? "", normalFont))
                            {
                                Padding = 4f,
                                HorizontalAlignment = Element.ALIGN_CENTER
                            };
                            table.AddCell(dataCell);
                        }
                    }
                    doc.Add(table);

                    doc.Add(new Chunk("\n"));
                    PdfPTable summary = new PdfPTable(1) { WidthPercentage = 100 };
                    PdfPCell totalCell = new PdfPCell(new Phrase($"GRAND TOTAL: {lblPeriodTotal.Text}", headFont))
                    {
                        Border = iTextSharp.text.Rectangle.NO_BORDER,
                        HorizontalAlignment = Element.ALIGN_RIGHT
                    };
                    summary.AddCell(totalCell);
                    doc.Add(summary);

                    doc.Add(new Chunk("\n"));
                    Paragraph footer = new Paragraph("Powered by StoreMate", new iTextSharp.text.Font(bfReg, 8)) { Alignment = Element.ALIGN_CENTER };
                    doc.Add(footer);

                    doc.Close();
                    MessageBox.Show("Report successfully exported to PDF!");
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                }
                catch (Exception ex) { MessageBox.Show("PDF Error: " + ex.Message); }
            }
        }
    }
}