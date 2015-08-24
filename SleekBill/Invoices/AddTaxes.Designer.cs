namespace Sleek_Bill.Invoices
{
    partial class AddTaxes
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTaxName = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblTaxPercentage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnAddTax = new System.Windows.Forms.Button();
            this.lblAddTax = new System.Windows.Forms.Label();
            this.tlpTaxes = new System.Windows.Forms.TableLayoutPanel();
            this.btnCombinedTaxes = new System.Windows.Forms.Button();
            this.dgvTaxes = new System.Windows.Forms.DataGridView();
            this.lblTaxes = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tlpTaxes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaxes)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.914712F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.47335F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.74414F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.39232F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.8742F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.13433F));
            this.tableLayoutPanel1.Controls.Add(this.lblTaxName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTaxPercentage, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnAddTax, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblAddTax, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(938, 76);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblTaxName
            // 
            this.lblTaxName.AutoSize = true;
            this.lblTaxName.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblTaxName.Location = new System.Drawing.Point(98, 50);
            this.lblTaxName.Margin = new System.Windows.Forms.Padding(6, 12, 6, 6);
            this.lblTaxName.Name = "lblTaxName";
            this.lblTaxName.Size = new System.Drawing.Size(59, 15);
            this.lblTaxName.TabIndex = 2;
            this.lblTaxName.Text = "Tax Name";
            // 
            // textBox2
            // 
            this.textBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox2.Location = new System.Drawing.Point(214, 43);
            this.textBox2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(264, 23);
            this.textBox2.TabIndex = 9;
            // 
            // lblTaxPercentage
            // 
            this.lblTaxPercentage.AutoSize = true;
            this.lblTaxPercentage.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblTaxPercentage.Location = new System.Drawing.Point(491, 50);
            this.lblTaxPercentage.Margin = new System.Windows.Forms.Padding(6, 12, 6, 6);
            this.lblTaxPercentage.Name = "lblTaxPercentage";
            this.lblTaxPercentage.Size = new System.Drawing.Size(88, 15);
            this.lblTaxPercentage.TabIndex = 10;
            this.lblTaxPercentage.Text = "Tax Percentage";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblPercentage);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Location = new System.Drawing.Point(622, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(95, 25);
            this.panel1.TabIndex = 1;
            // 
            // lblPercentage
            // 
            this.lblPercentage.AutoSize = true;
            this.lblPercentage.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblPercentage.Location = new System.Drawing.Point(74, 5);
            this.lblPercentage.Margin = new System.Windows.Forms.Padding(6, 12, 6, 6);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(16, 15);
            this.lblPercentage.TabIndex = 11;
            this.lblPercentage.Text = "%";
            // 
            // textBox1
            // 
            this.textBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox1.Location = new System.Drawing.Point(0, 1);
            this.textBox1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(72, 23);
            this.textBox1.TabIndex = 10;
            // 
            // btnAddTax
            // 
            this.btnAddTax.Location = new System.Drawing.Point(725, 43);
            this.btnAddTax.Margin = new System.Windows.Forms.Padding(5);
            this.btnAddTax.Name = "btnAddTax";
            this.btnAddTax.Size = new System.Drawing.Size(75, 23);
            this.btnAddTax.TabIndex = 12;
            this.btnAddTax.Text = "Add Tax";
            this.btnAddTax.UseVisualStyleBackColor = true;
            // 
            // lblAddTax
            // 
            this.lblAddTax.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblAddTax, 2);
            this.lblAddTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddTax.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblAddTax.Location = new System.Drawing.Point(3, 3);
            this.lblAddTax.Margin = new System.Windows.Forms.Padding(3);
            this.lblAddTax.Name = "lblAddTax";
            this.lblAddTax.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.lblAddTax.Size = new System.Drawing.Size(72, 27);
            this.lblAddTax.TabIndex = 1;
            this.lblAddTax.Text = "Add Tax";
            // 
            // tlpTaxes
            // 
            this.tlpTaxes.ColumnCount = 2;
            this.tlpTaxes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.90192F));
            this.tlpTaxes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.09808F));
            this.tlpTaxes.Controls.Add(this.btnCombinedTaxes, 1, 0);
            this.tlpTaxes.Controls.Add(this.dgvTaxes, 0, 1);
            this.tlpTaxes.Controls.Add(this.lblTaxes, 0, 0);
            this.tlpTaxes.Location = new System.Drawing.Point(14, 108);
            this.tlpTaxes.Name = "tlpTaxes";
            this.tlpTaxes.RowCount = 2;
            this.tlpTaxes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.81819F));
            this.tlpTaxes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.18181F));
            this.tlpTaxes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpTaxes.Size = new System.Drawing.Size(938, 251);
            this.tlpTaxes.TabIndex = 1;
            // 
            // btnCombinedTaxes
            // 
            this.btnCombinedTaxes.Location = new System.Drawing.Point(791, 5);
            this.btnCombinedTaxes.Margin = new System.Windows.Forms.Padding(5);
            this.btnCombinedTaxes.Name = "btnCombinedTaxes";
            this.btnCombinedTaxes.Size = new System.Drawing.Size(142, 23);
            this.btnCombinedTaxes.TabIndex = 13;
            this.btnCombinedTaxes.Text = "% Combined Taxes";
            this.btnCombinedTaxes.UseVisualStyleBackColor = true;
            // 
            // dgvTaxes
            // 
            this.dgvTaxes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tlpTaxes.SetColumnSpan(this.dgvTaxes, 2);
            this.dgvTaxes.Location = new System.Drawing.Point(3, 45);
            this.dgvTaxes.Name = "dgvTaxes";
            this.dgvTaxes.Size = new System.Drawing.Size(932, 203);
            this.dgvTaxes.TabIndex = 14;
            // 
            // lblTaxes
            // 
            this.lblTaxes.AutoSize = true;
            this.lblTaxes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxes.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblTaxes.Location = new System.Drawing.Point(3, 3);
            this.lblTaxes.Margin = new System.Windows.Forms.Padding(3);
            this.lblTaxes.Name = "lblTaxes";
            this.lblTaxes.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.lblTaxes.Size = new System.Drawing.Size(58, 27);
            this.lblTaxes.TabIndex = 2;
            this.lblTaxes.Text = "Taxes";
            // 
            // AddTaxes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(964, 371);
            this.Controls.Add(this.tlpTaxes);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.DarkCyan;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AddTaxes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Taxes";
            this.Load += new System.EventHandler(this.AddTaxes_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tlpTaxes.ResumeLayout(false);
            this.tlpTaxes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaxes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblTaxName;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblTaxPercentage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPercentage;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnAddTax;
        private System.Windows.Forms.Label lblAddTax;
        private System.Windows.Forms.TableLayoutPanel tlpTaxes;
        private System.Windows.Forms.Button btnCombinedTaxes;
        private System.Windows.Forms.DataGridView dgvTaxes;
        private System.Windows.Forms.Label lblTaxes;
    }
}