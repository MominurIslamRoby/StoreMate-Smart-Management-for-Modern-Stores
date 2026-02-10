
namespace IMS
{
    partial class ReportsForm
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.lblReports = new System.Windows.Forms.Label();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.cmbReportType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblFrom = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClearReport = new System.Windows.Forms.Button();
            this.btnPrintPdf = new System.Windows.Forms.Button();
            this.lblPeriodTotal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTopProducts = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.panelHeader.SuspendLayout();
            this.panelFilters.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.label7);
            this.panelHeader.Controls.Add(this.lblReports);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1662, 79);
            this.panelHeader.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(742, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(252, 19);
            this.label7.TabIndex = 10;
            this.label7.Text = "(Sales / Finance / Inventory Reports)";
            // 
            // lblReports
            // 
            this.lblReports.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblReports.AutoSize = true;
            this.lblReports.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReports.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblReports.Location = new System.Drawing.Point(810, 6);
            this.lblReports.Name = "lblReports";
            this.lblReports.Size = new System.Drawing.Size(153, 46);
            this.lblReports.TabIndex = 0;
            this.lblReports.Text = "Reports ";
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = System.Drawing.Color.White;
            this.panelFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFilters.Controls.Add(this.btnGenerate);
            this.panelFilters.Controls.Add(this.cmbReportType);
            this.panelFilters.Controls.Add(this.lblType);
            this.panelFilters.Controls.Add(this.dtpTo);
            this.panelFilters.Controls.Add(this.lblTo);
            this.panelFilters.Controls.Add(this.dtpFrom);
            this.panelFilters.Controls.Add(this.lblFrom);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelFilters.Location = new System.Drawing.Point(0, 79);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(242, 699);
            this.panelFilters.TabIndex = 1;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnGenerate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnGenerate.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.Location = new System.Drawing.Point(28, 316);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(197, 50);
            this.btnGenerate.TabIndex = 6;
            this.btnGenerate.Text = "Generate Report";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // cmbReportType
            // 
            this.cmbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReportType.FormattingEnabled = true;
            this.cmbReportType.Items.AddRange(new object[] {
            "Daily Sales",
            "Invoice Wise Sales",
            "Product Wise Sales",
            "Monthly Summary",
            "Low Stock Report"});
            this.cmbReportType.Location = new System.Drawing.Point(28, 266);
            this.cmbReportType.Name = "cmbReportType";
            this.cmbReportType.Size = new System.Drawing.Size(197, 24);
            this.cmbReportType.TabIndex = 5;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.Location = new System.Drawing.Point(24, 232);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(118, 23);
            this.lblType.TabIndex = 4;
            this.lblType.Text = "Report Type :";
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(28, 194);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(197, 22);
            this.dtpTo.TabIndex = 3;
            this.dtpTo.Value = new System.DateTime(2026, 1, 16, 22, 4, 46, 0);
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(24, 163);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(81, 23);
            this.lblTo.TabIndex = 2;
            this.lblTo.Text = "To Date :";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(28, 126);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(197, 22);
            this.dtpFrom.TabIndex = 1;
            this.dtpFrom.Value = new System.DateTime(2026, 1, 16, 22, 4, 46, 0);
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom.Location = new System.Drawing.Point(24, 94);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(105, 23);
            this.lblFrom.TabIndex = 0;
            this.lblFrom.Text = "From Date :";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnClearReport);
            this.panel1.Controls.Add(this.btnPrintPdf);
            this.panel1.Controls.Add(this.lblPeriodTotal);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblTopProducts);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dgvReport);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(242, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1420, 699);
            this.panel1.TabIndex = 2;
            // 
            // btnClearReport
            // 
            this.btnClearReport.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnClearReport.BackColor = System.Drawing.Color.Red;
            this.btnClearReport.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearReport.ForeColor = System.Drawing.Color.White;
            this.btnClearReport.Location = new System.Drawing.Point(993, 623);
            this.btnClearReport.Name = "btnClearReport";
            this.btnClearReport.Size = new System.Drawing.Size(165, 50);
            this.btnClearReport.TabIndex = 11;
            this.btnClearReport.Text = "Clear Report";
            this.btnClearReport.UseVisualStyleBackColor = false;
            this.btnClearReport.Click += new System.EventHandler(this.btnClearReport_Click);
            // 
            // btnPrintPdf
            // 
            this.btnPrintPdf.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnPrintPdf.BackColor = System.Drawing.Color.DarkGreen;
            this.btnPrintPdf.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintPdf.ForeColor = System.Drawing.Color.White;
            this.btnPrintPdf.Location = new System.Drawing.Point(1196, 623);
            this.btnPrintPdf.Name = "btnPrintPdf";
            this.btnPrintPdf.Size = new System.Drawing.Size(165, 50);
            this.btnPrintPdf.TabIndex = 7;
            this.btnPrintPdf.Text = "Print PDF";
            this.btnPrintPdf.UseVisualStyleBackColor = false;
            this.btnPrintPdf.Click += new System.EventHandler(this.btnPrintPdf_Click);
            // 
            // lblPeriodTotal
            // 
            this.lblPeriodTotal.AutoSize = true;
            this.lblPeriodTotal.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeriodTotal.Location = new System.Drawing.Point(1129, 571);
            this.lblPeriodTotal.Name = "lblPeriodTotal";
            this.lblPeriodTotal.Size = new System.Drawing.Size(38, 23);
            this.lblPeriodTotal.TabIndex = 10;
            this.lblPeriodTotal.Text = "----";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1013, 572);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 23);
            this.label1.TabIndex = 7;
            this.label1.Text = "Grand Total :";
            // 
            // lblTopProducts
            // 
            this.lblTopProducts.AutoSize = true;
            this.lblTopProducts.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTopProducts.Location = new System.Drawing.Point(983, 28);
            this.lblTopProducts.Name = "lblTopProducts";
            this.lblTopProducts.Size = new System.Drawing.Size(138, 23);
            this.lblTopProducts.TabIndex = 9;
            this.lblTopProducts.Text = "Top Product: ---";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(533, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 23);
            this.label5.TabIndex = 8;
            this.label5.Text = "Total Invoices: 0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 23);
            this.label4.TabIndex = 7;
            this.label4.Text = "Total Sales: ৳ 0.00";
            // 
            // dgvReport
            // 
            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Location = new System.Drawing.Point(21, 67);
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersWidth = 51;
            this.dgvReport.RowTemplate.Height = 24;
            this.dgvReport.Size = new System.Drawing.Size(1340, 485);
            this.dgvReport.TabIndex = 0;
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1662, 778);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelFilters);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ReportsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReportsForm";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblReports;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbReportType;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTopProducts;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPeriodTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClearReport;
        private System.Windows.Forms.Button btnPrintPdf;
    }
}