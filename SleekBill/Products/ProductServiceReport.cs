using System;
using System.Linq;
using System.Windows.Forms;
using DataServices.Interfaces;
using DataServices;
using BusinessObjects;
using System.Collections.Generic;
using Sleek_Bill.Controls;
using Sleek_Bill.Common;

namespace Sleek_Bill.Products
{
    public partial class ProductServiceReport : Form
    {
        private IProduct productService = new ProductService();
        private IMaster masterService = new MasterService();
        int rowIndex = 0;
        int currentPage = 1;
        int TotalPage = 0;

        public ProductServiceReport()
        {
            InitializeComponent();
        }

        private void btnNewProduct_Click(object sender, EventArgs e)
        {
            AddEditProduct addEditProduct = new AddEditProduct();
            addEditProduct.StartPosition = FormStartPosition.CenterParent;
            addEditProduct.ShowDialog(this);
        }

        private void ProductServiceReport_Load(object sender, EventArgs e)
        {
            Common.Common.SetFormCoordinate(this);
            BindProductReportsDataGrid();
        }

        private void BindProductReportsDataGrid()
        {
            int resultsPerPage = Convert.ToInt32(txtResultsPerPage.Text);
            List<Product> productList = productService.GetAllProducts();            
            List<Tax> taxList = masterService.GetAllTaxes();

            if (!String.IsNullOrEmpty(txtProductName.Text))
                productList = (from product in productList
                                 .Where(v => v.ProductName.StartsWith(txtProductName.Text, StringComparison.OrdinalIgnoreCase))
                               select product).ToList<Product>();

            if (!String.IsNullOrEmpty(txtQuantityFrom.Text) && !String.IsNullOrEmpty(txtQuantityTo.Text))
                productList = (from product in productList
                                 .Where(v => v.Quantity >= Convert.ToDecimal(txtQuantityFrom.Text) &&
                                              v.UnitPrice <= Convert.ToDecimal(txtQuantityTo.Text))
                              select product).ToList<Product>();

            if (!String.IsNullOrEmpty(txtUnitPriceFrom.Text) && !String.IsNullOrEmpty(txtUnitPriceTo.Text))
                productList = (from product in productList
                                 .Where(v => v.UnitPrice >= Convert.ToDecimal(txtUnitPriceFrom.Text) &&
                                              v.UnitPrice <= Convert.ToDecimal(txtUnitPriceTo.Text))
                               select product).ToList<Product>();

            

            int totalRecords = productList.Count;
            CalculateTotalPages(totalRecords);
            int srNo = 0;
            var columns = from tax in taxList
                          join product in productList
                          on tax.TaxId equals product.TaxId
                          select new
                          {
                              No = ++srNo,
                              ProductId = product.ProductId,
                              ProductName = product.ProductName,
                              Description = product.Description,
                              Quantity = product.Quantity,
                              UnitPrice = product.UnitPrice,
                              Tax = tax.TaxPercentage
                          };
            if (currentPage == 1)
            {
                this.dgvProducts.DataSource = columns.Take(resultsPerPage).ToList();
            }
            else
            {
                int skipRecordsNo = resultsPerPage * (currentPage - 1);
                this.dgvProducts.DataSource = columns.Skip(skipRecordsNo).Take(resultsPerPage).ToList();
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

        private void btnApply_Click(object sender, EventArgs e)
        {
            BindProductReportsDataGrid();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
                currentPage--;

            BindProductReportsDataGrid();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (currentPage < TotalPage)
                currentPage++;

            BindProductReportsDataGrid();
        }

        private void CalculateTotalPages(int rowCount)
        {
            int pageSize = Convert.ToInt32(txtResultsPerPage.Text);
            TotalPage = rowCount / pageSize;

            if (rowCount % pageSize > 0)
                TotalPage += 1;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindProductReportsDataGrid();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtProductName.Text = String.Empty;
            txtUnitPriceFrom.Text = String.Empty;
            txtUnitPriceTo.Text = String.Empty;
            txtQuantityFrom.Text = String.Empty;
            txtQuantityTo.Text = String.Empty;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvProducts.Rows[rowIndex];
            int productId = Convert.ToInt32(row.Cells["ProductId"].Value);
            AddEditProduct addEditProduct = new AddEditProduct();
            addEditProduct.productID = productId;
            addEditProduct.formsState = Common.eFormsState.Edit;
            addEditProduct.ShowDialog(this);
            BindProductReportsDataGrid();
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvProducts.Rows[rowIndex];
            int productId = Convert.ToInt32(row.Cells["ProductId"].Value);
            AddEditProduct addEditProduct = new AddEditProduct();
            addEditProduct.productID = productId;
            addEditProduct.formsState = Common.eFormsState.View;
            addEditProduct.ShowDialog(this);
            BindProductReportsDataGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvProducts.Rows[rowIndex];
            int productId = Convert.ToInt32(row.Cells["ProductId"].Value);
            DialogResult dialogRes = CustomMessageBox.Show(Constants.DELETE_WARNING,
                                  Constants.CONSTANT_INFORMATION,
                                  Sleek_Bill.Controls.CustomMessageBox.eDialogButtons.YesNo,
                                  CustomImages.GetDialogImage(Sleek_Bill.Controls.CustomImages.eCustomDialogImages.Information));

            if (dialogRes == System.Windows.Forms.DialogResult.Yes)
            {
                productService.DeactivateProduct(productId);
            }

            BindProductReportsDataGrid();
        }
    }
}
