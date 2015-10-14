using System;
using System.Windows.Forms;
using BusinessObjects;
using DataServices;
using DataServices.Interfaces;
using Sleek_Bill.Common;
using Sleek_Bill.Controls;
using System.Collections.Generic;
using System.Linq;

namespace Sleek_Bill.Invoices
{
    public partial class AddTaxes : Form
    {
        private IMaster masterService = new MasterService();
        int rowIndex = 0;
        int currentPage = 1;
        int TotalPage = 0;

        public AddTaxes()
        {
            InitializeComponent();
        }

        private void AddTaxes_Load(object sender, EventArgs e)
        {
            try
            {
                Common.Common.SetFormCoordinate(this);
                BindTaxDetailsDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
        }

        private void BindTaxDetailsDataGrid()
        {
            int resultsPerPage = Convert.ToInt32(txtResultsPerPage.Text);
            List<Tax> taxList = masterService.GetAllTaxes();
            string taxName = txtTaxName.Text;

            if (!String.IsNullOrEmpty(txtTaxName.Text))
                taxList = (from tax in taxList
                                 .Where(v => v.TaxName.StartsWith(txtTaxName.Text, StringComparison.OrdinalIgnoreCase))
                              select tax).ToList<Tax>();

            if (!String.IsNullOrEmpty(txtTaxPercentage.Text))
                taxList = (from tax in taxList
                                 .Where(v => v.TaxPercentage == Convert.ToDecimal(txtTaxPercentage.Text))
                              select tax).ToList<Tax>();

            int totalRecords = taxList.Count;
            CalculateTotalPages(totalRecords);
            int srNo = 0;
            var columns = from tax in taxList
                          select new
                          {
                              No = ++srNo,
                              TaxId = tax.TaxId,
                              TaxName = tax.TaxName,
                              TaxPercentage = tax.TaxPercentage
                          };

            if (currentPage == 1)
            {
                this.dgvTaxes.DataSource = columns.Take(resultsPerPage).ToList();
            }
            else
            {
                int skipRecordsNo = resultsPerPage * (currentPage - 1);
                this.dgvTaxes.DataSource = columns.Skip(skipRecordsNo).Take(resultsPerPage).ToList();
            }
            
                

            if (currentPage <= TotalPage)
            {
                txtPageEnd.Visible = true;
                txtPageStart.Visible = true;
                btnLeft.Visible = true;
                btnRight.Visible = true;
                lblSeperator.Visible = true;
                txtPageStart.Text = Convert.ToString(currentPage);
                txtPageEnd.Text = Convert.ToString(TotalPage);
                txtPageEnd.Enabled = false;

                if (currentPage == 1)
                {
                    btnLeft.Enabled = false;
                    btnRight.Enabled = true;
                }
                else if (currentPage == TotalPage)
                {
                    btnRight.Enabled = false;
                    btnLeft.Enabled = true;
                }
                else
                {
                    btnLeft.Enabled = true;
                    btnRight.Enabled = true;
                }

                if (TotalPage == 1)
                {
                    txtPageEnd.Visible = false; ;
                    txtPageStart.Visible = false;
                    btnLeft.Visible = false;
                    btnRight.Visible = false;
                    lblSeperator.Visible = false;
                }
            }
        }

        private void btnAddTax_Click(object sender, EventArgs e)
        {
            Tax tax = new Tax();
            tax.TaxName = txtTaxName.Text.Trim();
            tax.TaxPercentage = Math.Round(Convert.ToDecimal(txtTaxPercentage.Text),2);
            tax.IsDefault = false;
            tax.Status = true;
            int taxId = masterService.AddTax(tax);
            CustomMessageBox.Show(Constants.SUCCESSFULL_TAX_ADD_MESSAGE,
                                                              Constants.CONSTANT_INFORMATION,
                                                              Sleek_Bill.Controls.CustomMessageBox.eDialogButtons.OK,
                                                              CustomImages.GetDialogImage(Sleek_Bill.Controls.CustomImages.eCustomDialogImages.Success));
            BindTaxDetailsDataGrid();
        }

        private void CalculateTotalPages(int rowCount)
        {
            int pageSize = Convert.ToInt32(txtResultsPerPage.Text);
            TotalPage = rowCount / pageSize;

            if (rowCount % pageSize > 0)
                TotalPage += 1;
        }

        private void dgvTaxes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvTaxes.Rows[rowIndex];
            int taxId = Convert.ToInt32(row.Cells["TaxId"].Value);
            EditTax editTax = new EditTax();
            editTax.TaxId = taxId;
            editTax.formsState = Common.eFormsState.Edit;
            editTax.ShowDialog(this);
            BindTaxDetailsDataGrid();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvTaxes.Rows[rowIndex];
            int taxId = Convert.ToInt32(row.Cells["TaxId"].Value);
            EditTax editTax = new EditTax();
            editTax.TaxId = taxId;
            editTax.formsState = Common.eFormsState.View;
            editTax.ShowDialog(this);
            BindTaxDetailsDataGrid();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            BindTaxDetailsDataGrid();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
                currentPage--;

            BindTaxDetailsDataGrid();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (currentPage < TotalPage)
                currentPage++;

            BindTaxDetailsDataGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvTaxes.Rows[rowIndex];
            int taxId = Convert.ToInt32(row.Cells["TaxId"].Value);
            DialogResult dialogRes = CustomMessageBox.Show(Constants.DELETE_WARNING,
                                  Constants.CONSTANT_INFORMATION,
                                  Sleek_Bill.Controls.CustomMessageBox.eDialogButtons.YesNo,
                                  CustomImages.GetDialogImage(Sleek_Bill.Controls.CustomImages.eCustomDialogImages.Information));

            if (dialogRes == System.Windows.Forms.DialogResult.Yes)
            {
                masterService.DeactivateTax(taxId);
            }

            BindTaxDetailsDataGrid();
        }
    }
}
