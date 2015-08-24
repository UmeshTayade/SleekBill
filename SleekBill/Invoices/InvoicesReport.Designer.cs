namespace Sleek_Bill.Invoices
{
    partial class InvoicesReport
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
            this.lblSearchInvoices = new System.Windows.Forms.Label();
            this.lblClientName = new System.Windows.Forms.Label();
            this.lblInvoiceNo = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbClientName = new System.Windows.Forms.ComboBox();
            this.txtInvoiceNumber = new System.Windows.Forms.TextBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblIssuedBetween = new System.Windows.Forms.Label();
            this.lblDueBetween = new System.Windows.Forms.Label();
            this.lblQuickSearch = new System.Windows.Forms.Label();
            this.txtQuickSearch = new System.Windows.Forms.TextBox();
            this.pnlIssueBetween = new System.Windows.Forms.Panel();
            this.dtpIssueBetn1 = new System.Windows.Forms.DateTimePicker();
            this.lblAnd1 = new System.Windows.Forms.Label();
            this.dtpissueBetn2 = new System.Windows.Forms.DateTimePicker();
            this.pnlDueBetween = new System.Windows.Forms.Panel();
            this.dtpDueBetn2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDueBetn1 = new System.Windows.Forms.DateTimePicker();
            this.pnlSearhinvoices = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblResults = new System.Windows.Forms.Label();
            this.btnNewInvoice = new System.Windows.Forms.Button();
            this.dgvInvoices = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlIssueBetween.SuspendLayout();
            this.pnlDueBetween.SuspendLayout();
            this.pnlSearhinvoices.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.89641F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.29598F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.28753F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.57928F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.02748F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Controls.Add(this.lblSearchInvoices, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblClientName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblInvoiceNo, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblStatus, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cmbClientName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtInvoiceNumber, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmbStatus, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblIssuedBetween, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblDueBetween, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblQuickSearch, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtQuickSearch, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.pnlIssueBetween, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlDueBetween, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.pnlSearhinvoices, 2, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(946, 168);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblSearchInvoices
            // 
            this.lblSearchInvoices.AutoSize = true;
            this.lblSearchInvoices.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchInvoices.Location = new System.Drawing.Point(5, 10);
            this.lblSearchInvoices.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.lblSearchInvoices.Name = "lblSearchInvoices";
            this.lblSearchInvoices.Size = new System.Drawing.Size(109, 18);
            this.lblSearchInvoices.TabIndex = 1;
            this.lblSearchInvoices.Text = "Search Invoices";
            // 
            // lblClientName
            // 
            this.lblClientName.AutoSize = true;
            this.lblClientName.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblClientName.Location = new System.Drawing.Point(5, 43);
            this.lblClientName.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.lblClientName.Name = "lblClientName";
            this.lblClientName.Size = new System.Drawing.Size(73, 15);
            this.lblClientName.TabIndex = 2;
            this.lblClientName.Text = "Client Name";
            // 
            // lblInvoiceNo
            // 
            this.lblInvoiceNo.AutoSize = true;
            this.lblInvoiceNo.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblInvoiceNo.Location = new System.Drawing.Point(5, 76);
            this.lblInvoiceNo.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.lblInvoiceNo.Name = "lblInvoiceNo";
            this.lblInvoiceNo.Size = new System.Drawing.Size(93, 15);
            this.lblInvoiceNo.TabIndex = 3;
            this.lblInvoiceNo.Text = "Invoice Number";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblStatus.Location = new System.Drawing.Point(5, 109);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(41, 15);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Status";
            // 
            // cmbClientName
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.cmbClientName, 2);
            this.cmbClientName.FormattingEnabled = true;
            this.cmbClientName.Location = new System.Drawing.Point(125, 36);
            this.cmbClientName.Name = "cmbClientName";
            this.cmbClientName.Size = new System.Drawing.Size(340, 23);
            this.cmbClientName.TabIndex = 5;
            // 
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtInvoiceNumber.Location = new System.Drawing.Point(127, 71);
            this.txtInvoiceNumber.Margin = new System.Windows.Forms.Padding(5);
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.Size = new System.Drawing.Size(182, 23);
            this.txtInvoiceNumber.TabIndex = 7;
            // 
            // cmbStatus
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.cmbStatus, 2);
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(125, 102);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(184, 23);
            this.cmbStatus.TabIndex = 8;
            // 
            // lblIssuedBetween
            // 
            this.lblIssuedBetween.AutoSize = true;
            this.lblIssuedBetween.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblIssuedBetween.Location = new System.Drawing.Point(492, 43);
            this.lblIssuedBetween.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.lblIssuedBetween.Name = "lblIssuedBetween";
            this.lblIssuedBetween.Size = new System.Drawing.Size(91, 15);
            this.lblIssuedBetween.TabIndex = 9;
            this.lblIssuedBetween.Text = "Issued Between";
            // 
            // lblDueBetween
            // 
            this.lblDueBetween.AutoSize = true;
            this.lblDueBetween.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblDueBetween.Location = new System.Drawing.Point(492, 76);
            this.lblDueBetween.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.lblDueBetween.Name = "lblDueBetween";
            this.lblDueBetween.Size = new System.Drawing.Size(76, 15);
            this.lblDueBetween.TabIndex = 10;
            this.lblDueBetween.Text = "Due Between";
            // 
            // lblQuickSearch
            // 
            this.lblQuickSearch.AutoSize = true;
            this.lblQuickSearch.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblQuickSearch.Location = new System.Drawing.Point(492, 109);
            this.lblQuickSearch.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.lblQuickSearch.Name = "lblQuickSearch";
            this.lblQuickSearch.Size = new System.Drawing.Size(79, 15);
            this.lblQuickSearch.TabIndex = 11;
            this.lblQuickSearch.Text = "Quick Search";
            // 
            // txtQuickSearch
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtQuickSearch, 2);
            this.txtQuickSearch.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtQuickSearch.Location = new System.Drawing.Point(611, 104);
            this.txtQuickSearch.Margin = new System.Windows.Forms.Padding(5);
            this.txtQuickSearch.Name = "txtQuickSearch";
            this.txtQuickSearch.Size = new System.Drawing.Size(311, 23);
            this.txtQuickSearch.TabIndex = 12;
            // 
            // pnlIssueBetween
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pnlIssueBetween, 2);
            this.pnlIssueBetween.Controls.Add(this.dtpissueBetn2);
            this.pnlIssueBetween.Controls.Add(this.lblAnd1);
            this.pnlIssueBetween.Controls.Add(this.dtpIssueBetn1);
            this.pnlIssueBetween.Location = new System.Drawing.Point(609, 36);
            this.pnlIssueBetween.Name = "pnlIssueBetween";
            this.pnlIssueBetween.Size = new System.Drawing.Size(334, 27);
            this.pnlIssueBetween.TabIndex = 13;
            // 
            // dtpIssueBetn1
            // 
            this.dtpIssueBetn1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpIssueBetn1.Location = new System.Drawing.Point(1, 0);
            this.dtpIssueBetn1.Name = "dtpIssueBetn1";
            this.dtpIssueBetn1.Size = new System.Drawing.Size(136, 23);
            this.dtpIssueBetn1.TabIndex = 0;
            // 
            // lblAnd1
            // 
            this.lblAnd1.AutoSize = true;
            this.lblAnd1.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblAnd1.Location = new System.Drawing.Point(140, 6);
            this.lblAnd1.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.lblAnd1.Name = "lblAnd1";
            this.lblAnd1.Size = new System.Drawing.Size(28, 15);
            this.lblAnd1.TabIndex = 5;
            this.lblAnd1.Text = "and";
            // 
            // dtpissueBetn2
            // 
            this.dtpissueBetn2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpissueBetn2.Location = new System.Drawing.Point(169, 1);
            this.dtpissueBetn2.Name = "dtpissueBetn2";
            this.dtpissueBetn2.Size = new System.Drawing.Size(144, 23);
            this.dtpissueBetn2.TabIndex = 1;
            // 
            // pnlDueBetween
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pnlDueBetween, 2);
            this.pnlDueBetween.Controls.Add(this.dtpDueBetn2);
            this.pnlDueBetween.Controls.Add(this.label1);
            this.pnlDueBetween.Controls.Add(this.dtpDueBetn1);
            this.pnlDueBetween.Location = new System.Drawing.Point(609, 69);
            this.pnlDueBetween.Name = "pnlDueBetween";
            this.pnlDueBetween.Size = new System.Drawing.Size(334, 27);
            this.pnlDueBetween.TabIndex = 14;
            // 
            // dtpDueBetn2
            // 
            this.dtpDueBetn2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDueBetn2.Location = new System.Drawing.Point(169, 1);
            this.dtpDueBetn2.Name = "dtpDueBetn2";
            this.dtpDueBetn2.Size = new System.Drawing.Size(144, 23);
            this.dtpDueBetn2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkCyan;
            this.label1.Location = new System.Drawing.Point(140, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "and";
            // 
            // dtpDueBetn1
            // 
            this.dtpDueBetn1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDueBetn1.Location = new System.Drawing.Point(1, 0);
            this.dtpDueBetn1.Name = "dtpDueBetn1";
            this.dtpDueBetn1.Size = new System.Drawing.Size(136, 23);
            this.dtpDueBetn1.TabIndex = 0;
            // 
            // pnlSearhinvoices
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pnlSearhinvoices, 2);
            this.pnlSearhinvoices.Controls.Add(this.btnSearch);
            this.pnlSearhinvoices.Controls.Add(this.btnReset);
            this.pnlSearhinvoices.Location = new System.Drawing.Point(317, 135);
            this.pnlSearhinvoices.Name = "pnlSearhinvoices";
            this.pnlSearhinvoices.Size = new System.Drawing.Size(286, 30);
            this.pnlSearhinvoices.TabIndex = 15;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(66, 4);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(190, 5, 5, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(151, 4);
            this.btnReset.Margin = new System.Windows.Forms.Padding(5);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 12;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.21776F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.78224F));
            this.tableLayoutPanel2.Controls.Add(this.lblResults, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnNewInvoice, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.dgvInvoices, 0, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 197);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.76502F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.76502F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.46996F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(946, 311);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResults.Location = new System.Drawing.Point(5, 10);
            this.lblResults.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(57, 19);
            this.lblResults.TabIndex = 2;
            this.lblResults.Text = "Results";
            // 
            // btnNewInvoice
            // 
            this.btnNewInvoice.Location = new System.Drawing.Point(848, 41);
            this.btnNewInvoice.Margin = new System.Windows.Forms.Padding(5);
            this.btnNewInvoice.Name = "btnNewInvoice";
            this.btnNewInvoice.Size = new System.Drawing.Size(93, 23);
            this.btnNewInvoice.TabIndex = 12;
            this.btnNewInvoice.Text = "New Invoice";
            this.btnNewInvoice.UseVisualStyleBackColor = true;
            this.btnNewInvoice.Click += new System.EventHandler(this.btnNewInvoice_Click);
            // 
            // dgvInvoices
            // 
            this.dgvInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel2.SetColumnSpan(this.dgvInvoices, 2);
            this.dgvInvoices.Location = new System.Drawing.Point(3, 75);
            this.dgvInvoices.Name = "dgvInvoices";
            this.dgvInvoices.Size = new System.Drawing.Size(940, 233);
            this.dgvInvoices.TabIndex = 13;
            // 
            // InvoicesReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(970, 520);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.DarkCyan;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "InvoicesReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Invoices Report";
            this.Load += new System.EventHandler(this.InvoicesReport_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnlIssueBetween.ResumeLayout(false);
            this.pnlIssueBetween.PerformLayout();
            this.pnlDueBetween.ResumeLayout(false);
            this.pnlDueBetween.PerformLayout();
            this.pnlSearhinvoices.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblSearchInvoices;
        private System.Windows.Forms.Label lblClientName;
        private System.Windows.Forms.Label lblInvoiceNo;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbClientName;
        private System.Windows.Forms.TextBox txtInvoiceNumber;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblIssuedBetween;
        private System.Windows.Forms.Label lblDueBetween;
        private System.Windows.Forms.Label lblQuickSearch;
        private System.Windows.Forms.TextBox txtQuickSearch;
        private System.Windows.Forms.Panel pnlIssueBetween;
        private System.Windows.Forms.Label lblAnd1;
        private System.Windows.Forms.DateTimePicker dtpIssueBetn1;
        private System.Windows.Forms.DateTimePicker dtpissueBetn2;
        private System.Windows.Forms.Panel pnlDueBetween;
        private System.Windows.Forms.DateTimePicker dtpDueBetn2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDueBetn1;
        private System.Windows.Forms.Panel pnlSearhinvoices;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.Button btnNewInvoice;
        private System.Windows.Forms.DataGridView dgvInvoices;
    }
}