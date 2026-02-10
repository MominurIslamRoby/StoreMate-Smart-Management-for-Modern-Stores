
namespace IMS
{
    partial class SalesForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesForm));
            this.panelBody = new System.Windows.Forms.Panel();
            this.panelCart = new System.Windows.Forms.Panel();
            this.btnReprintInvoice = new System.Windows.Forms.Button();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.btnClearCart = new System.Windows.Forms.Button();
            this.labelTotalText = new System.Windows.Forms.Label();
            this.lblGrandTotal = new System.Windows.Forms.Label();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.colCartProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCartProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCartPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCartQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCartTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.panelProducts = new System.Windows.Forms.Panel();
            this.dgvProduct = new System.Windows.Forms.DataGridView();
            this.colProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAddToCart = new System.Windows.Forms.Button();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearchProduct = new System.Windows.Forms.Label();
            this.lblProducts = new System.Windows.Forms.Label();
            this.printDocumentInvoice = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialogInvoice = new System.Windows.Forms.PrintPreviewDialog();
            this.panelBody.SuspendLayout();
            this.panelCart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.panelProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBody
            // 
            this.panelBody.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelBody.Controls.Add(this.panelCart);
            this.panelBody.Controls.Add(this.panelProducts);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 0);
            this.panelBody.Name = "panelBody";
            this.panelBody.Size = new System.Drawing.Size(1630, 800);
            this.panelBody.TabIndex = 1;
            // 
            // panelCart
            // 
            this.panelCart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelCart.Controls.Add(this.btnReprintInvoice);
            this.panelCart.Controls.Add(this.btnCheckout);
            this.panelCart.Controls.Add(this.btnClearCart);
            this.panelCart.Controls.Add(this.labelTotalText);
            this.panelCart.Controls.Add(this.lblGrandTotal);
            this.panelCart.Controls.Add(this.dgvCart);
            this.panelCart.Controls.Add(this.label1);
            this.panelCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCart.ForeColor = System.Drawing.Color.Green;
            this.panelCart.Location = new System.Drawing.Point(545, 0);
            this.panelCart.Name = "panelCart";
            this.panelCart.Size = new System.Drawing.Size(1085, 800);
            this.panelCart.TabIndex = 1;
            // 
            // btnReprintInvoice
            // 
            this.btnReprintInvoice.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnReprintInvoice.BackColor = System.Drawing.Color.Green;
            this.btnReprintInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReprintInvoice.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReprintInvoice.ForeColor = System.Drawing.Color.White;
            this.btnReprintInvoice.Location = new System.Drawing.Point(767, 685);
            this.btnReprintInvoice.Name = "btnReprintInvoice";
            this.btnReprintInvoice.Size = new System.Drawing.Size(306, 60);
            this.btnReprintInvoice.TabIndex = 6;
            this.btnReprintInvoice.Text = "Reprint Invoice";
            this.btnReprintInvoice.UseVisualStyleBackColor = false;
            this.btnReprintInvoice.Click += new System.EventHandler(this.btnReprintInvoice_Click);
            // 
            // btnCheckout
            // 
            this.btnCheckout.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCheckout.BackColor = System.Drawing.Color.DarkBlue;
            this.btnCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckout.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckout.ForeColor = System.Drawing.Color.White;
            this.btnCheckout.Location = new System.Drawing.Point(767, 553);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(306, 60);
            this.btnCheckout.TabIndex = 5;
            this.btnCheckout.Text = "💳 Checkout";
            this.btnCheckout.UseVisualStyleBackColor = false;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
            // 
            // btnClearCart
            // 
            this.btnClearCart.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClearCart.BackColor = System.Drawing.Color.Red;
            this.btnClearCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCart.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearCart.ForeColor = System.Drawing.Color.White;
            this.btnClearCart.Location = new System.Drawing.Point(767, 619);
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.Size = new System.Drawing.Size(306, 60);
            this.btnClearCart.TabIndex = 4;
            this.btnClearCart.Text = "🧹 Clear Cart";
            this.btnClearCart.UseVisualStyleBackColor = false;
            this.btnClearCart.Click += new System.EventHandler(this.btnClearCart_Click);
            // 
            // labelTotalText
            // 
            this.labelTotalText.AutoSize = true;
            this.labelTotalText.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalText.ForeColor = System.Drawing.Color.Black;
            this.labelTotalText.Location = new System.Drawing.Point(964, 514);
            this.labelTotalText.Name = "labelTotalText";
            this.labelTotalText.Size = new System.Drawing.Size(66, 23);
            this.labelTotalText.TabIndex = 3;
            this.labelTotalText.Text = "৳ 0.00";
            // 
            // lblGrandTotal
            // 
            this.lblGrandTotal.AutoSize = true;
            this.lblGrandTotal.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrandTotal.ForeColor = System.Drawing.Color.Black;
            this.lblGrandTotal.Location = new System.Drawing.Point(824, 514);
            this.lblGrandTotal.Name = "lblGrandTotal";
            this.lblGrandTotal.Size = new System.Drawing.Size(122, 23);
            this.lblGrandTotal.TabIndex = 2;
            this.lblGrandTotal.Text = "Total Amount :";
            // 
            // dgvCart
            // 
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCart.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCart.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCartProductID,
            this.colCartProductName,
            this.colCartPrice,
            this.colCartQty,
            this.colCartTotal});
            this.dgvCart.Location = new System.Drawing.Point(16, 137);
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.ReadOnly = true;
            this.dgvCart.RowHeadersWidth = 51;
            this.dgvCart.RowTemplate.Height = 24;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(1057, 362);
            this.dgvCart.TabIndex = 1;
            // 
            // colCartProductID
            // 
            this.colCartProductID.DataPropertyName = "ProductID";
            this.colCartProductID.HeaderText = "ID";
            this.colCartProductID.MinimumWidth = 6;
            this.colCartProductID.Name = "colCartProductID";
            this.colCartProductID.ReadOnly = true;
            // 
            // colCartProductName
            // 
            this.colCartProductName.DataPropertyName = "ProductName";
            this.colCartProductName.HeaderText = "Product";
            this.colCartProductName.MinimumWidth = 6;
            this.colCartProductName.Name = "colCartProductName";
            this.colCartProductName.ReadOnly = true;
            // 
            // colCartPrice
            // 
            this.colCartPrice.DataPropertyName = "Price";
            this.colCartPrice.HeaderText = "Price";
            this.colCartPrice.MinimumWidth = 6;
            this.colCartPrice.Name = "colCartPrice";
            this.colCartPrice.ReadOnly = true;
            // 
            // colCartQty
            // 
            this.colCartQty.DataPropertyName = "Quantity";
            this.colCartQty.HeaderText = "Qty";
            this.colCartQty.MinimumWidth = 6;
            this.colCartQty.Name = "colCartQty";
            this.colCartQty.ReadOnly = true;
            // 
            // colCartTotal
            // 
            this.colCartTotal.DataPropertyName = "Total";
            this.colCartTotal.HeaderText = "Total";
            this.colCartTotal.MinimumWidth = 6;
            this.colCartTotal.Name = "colCartTotal";
            this.colCartTotal.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(495, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 59);
            this.label1.TabIndex = 0;
            this.label1.Text = "Invoice";
            // 
            // panelProducts
            // 
            this.panelProducts.BackColor = System.Drawing.Color.White;
            this.panelProducts.Controls.Add(this.dgvProduct);
            this.panelProducts.Controls.Add(this.btnRemove);
            this.panelProducts.Controls.Add(this.btnAddToCart);
            this.panelProducts.Controls.Add(this.nudQuantity);
            this.panelProducts.Controls.Add(this.lblQuantity);
            this.panelProducts.Controls.Add(this.txtSearch);
            this.panelProducts.Controls.Add(this.lblSearchProduct);
            this.panelProducts.Controls.Add(this.lblProducts);
            this.panelProducts.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelProducts.Location = new System.Drawing.Point(0, 0);
            this.panelProducts.Name = "panelProducts";
            this.panelProducts.Size = new System.Drawing.Size(545, 800);
            this.panelProducts.TabIndex = 0;
            // 
            // dgvProduct
            // 
            this.dgvProduct.AllowUserToAddRows = false;
            this.dgvProduct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProduct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProductID,
            this.colProductName,
            this.colPrice,
            this.colStock});
            this.dgvProduct.Location = new System.Drawing.Point(7, 187);
            this.dgvProduct.MultiSelect = false;
            this.dgvProduct.Name = "dgvProduct";
            this.dgvProduct.ReadOnly = true;
            this.dgvProduct.RowHeadersWidth = 51;
            this.dgvProduct.RowTemplate.Height = 24;
            this.dgvProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProduct.Size = new System.Drawing.Size(522, 277);
            this.dgvProduct.TabIndex = 8;
            // 
            // colProductID
            // 
            this.colProductID.DataPropertyName = "ProductID";
            this.colProductID.HeaderText = "Product ID";
            this.colProductID.MinimumWidth = 6;
            this.colProductID.Name = "colProductID";
            this.colProductID.ReadOnly = true;
            this.colProductID.Visible = false;
            // 
            // colProductName
            // 
            this.colProductName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colProductName.DataPropertyName = "ProductName";
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Blue;
            this.colProductName.DefaultCellStyle = dataGridViewCellStyle10;
            this.colProductName.FillWeight = 240.6417F;
            this.colProductName.HeaderText = "Product Name";
            this.colProductName.MinimumWidth = 6;
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;
            this.colProductName.Width = 150;
            // 
            // colPrice
            // 
            this.colPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colPrice.DataPropertyName = "Price";
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.Blue;
            this.colPrice.DefaultCellStyle = dataGridViewCellStyle11;
            this.colPrice.FillWeight = 9.8587F;
            this.colPrice.HeaderText = "Price";
            this.colPrice.MinimumWidth = 6;
            this.colPrice.Name = "colPrice";
            this.colPrice.ReadOnly = true;
            this.colPrice.Width = 125;
            // 
            // colStock
            // 
            this.colStock.DataPropertyName = "Quantity";
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Blue;
            this.colStock.DefaultCellStyle = dataGridViewCellStyle12;
            this.colStock.FillWeight = 49.49959F;
            this.colStock.HeaderText = "Quantity";
            this.colStock.MinimumWidth = 6;
            this.colStock.Name = "colStock";
            this.colStock.ReadOnly = true;
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.Red;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.ForeColor = System.Drawing.Color.White;
            this.btnRemove.Location = new System.Drawing.Point(3, 685);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(509, 74);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "Remove 🧹";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnAddToCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddToCart.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddToCart.ForeColor = System.Drawing.Color.White;
            this.btnAddToCart.Location = new System.Drawing.Point(3, 569);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(509, 79);
            this.btnAddToCart.TabIndex = 6;
            this.btnAddToCart.Text = "➕ Add to Cart";
            this.btnAddToCart.UseVisualStyleBackColor = false;
            this.btnAddToCart.Click += new System.EventHandler(this.btnAddToCart_Click);
            // 
            // nudQuantity
            // 
            this.nudQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudQuantity.Location = new System.Drawing.Point(3, 510);
            this.nudQuantity.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudQuantity.MinimumSize = new System.Drawing.Size(1, 0);
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(509, 30);
            this.nudQuantity.TabIndex = 5;
            this.nudQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuantity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblQuantity.Location = new System.Drawing.Point(3, 476);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(86, 23);
            this.lblQuantity.TabIndex = 4;
            this.lblQuantity.Text = "Quantity :";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(7, 132);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(522, 34);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearchProduct
            // 
            this.lblSearchProduct.AutoSize = true;
            this.lblSearchProduct.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblSearchProduct.Location = new System.Drawing.Point(10, 99);
            this.lblSearchProduct.Name = "lblSearchProduct";
            this.lblSearchProduct.Size = new System.Drawing.Size(135, 23);
            this.lblSearchProduct.TabIndex = 1;
            this.lblSearchProduct.Text = "Search Product :";
            // 
            // lblProducts
            // 
            this.lblProducts.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblProducts.AutoSize = true;
            this.lblProducts.Font = new System.Drawing.Font("Segoe UI", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProducts.ForeColor = System.Drawing.Color.Navy;
            this.lblProducts.Location = new System.Drawing.Point(158, 18);
            this.lblProducts.Name = "lblProducts";
            this.lblProducts.Size = new System.Drawing.Size(208, 60);
            this.lblProducts.TabIndex = 0;
            this.lblProducts.Text = "Products";
            // 
            // printDocumentInvoice
            // 
            this.printDocumentInvoice.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocumentInvoice_PrintPage);
            // 
            // printPreviewDialogInvoice
            // 
            this.printPreviewDialogInvoice.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialogInvoice.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialogInvoice.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialogInvoice.Enabled = true;
            this.printPreviewDialogInvoice.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialogInvoice.Icon")));
            this.printPreviewDialogInvoice.Name = "printPreviewDialogInvoice";
            this.printPreviewDialogInvoice.Visible = false;
            // 
            // SalesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1630, 800);
            this.Controls.Add(this.panelBody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SalesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales / POS";
            this.Load += new System.EventHandler(this.SalesForm_Load);
            this.panelBody.ResumeLayout(false);
            this.panelCart.ResumeLayout(false);
            this.panelCart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.panelProducts.ResumeLayout(false);
            this.panelProducts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.Panel panelProducts;
        private System.Windows.Forms.Panel panelCart;
        private System.Windows.Forms.Label lblProducts;
        private System.Windows.Forms.Label lblSearchProduct;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Button btnAddToCart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.Label lblGrandTotal;
        private System.Windows.Forms.Label labelTotalText;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Button btnClearCart;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.DataGridView dgvProduct;
        private System.Drawing.Printing.PrintDocument printDocumentInvoice;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialogInvoice;
        private System.Windows.Forms.Button btnReprintInvoice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCartProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCartProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCartPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCartQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCartTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStock;
    }
}