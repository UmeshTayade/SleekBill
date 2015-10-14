using System;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects;
using DataServices.Interfaces;
using DataServices;
using System.Collections.Generic;
using Sleek_Bill.Common;
using Sleek_Bill.Controls;

namespace Sleek_Bill.Products
{
    public partial class AddEditProduct : Form
    {
        private IProduct productService = new ProductService();
        private IMaster masterService = new MasterService();
        public int productID = 0;
        public eFormsState formsState;

        public AddEditProduct()
        {
            InitializeComponent();
        }

        private void AddEditProduct_Load(object sender, EventArgs e)
        {
            try
            {
                Common.Common.SetDialogCoordinate(this);
                LoadTaxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
        }

        private void LoadTaxes()
        {
            var lstTax = (from tax in masterService.GetAllTaxes()
                                .Where(v => v.Status == true)
                                select new Tax {
                                        TaxId = tax.TaxId,
                                        TaxName = tax.TaxName + " (" + tax.TaxPercentage + ")"                                
                                    }).ToList();

            lstTax.Insert(0, new Tax(0, "-- Select Tax --"));
            cmbTaxRate.DataSource = lstTax;
            cmbTaxRate.DisplayMember = "TaxName";
            cmbTaxRate.ValueMember = "TaxId";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Product product = new Product();
                product.ProductName = txtProductName.Text.Trim();
                product.Description = txtProdDesc.Text.Trim();
                product.UnitPrice = Convert.ToDecimal(txtUnitPrice.Text);
                product.Quantity = Convert.ToInt32(txtQuantity.Text);
                product.TaxId = Convert.ToInt32(cmbTaxRate.SelectedValue);
                product.Status = true;

                if (productID == 0)
                {
                    int productId = productService.AddProduct(product);
                    ResetControls();
                    CustomMessageBox.Show(string.Format(Constants.SUCCESSFULL_ADD_MESSAGE, Constants.CONSTANT_PRODUCT, txtProductName.Text),
                                                              Constants.CONSTANT_INFORMATION,
                                                              Sleek_Bill.Controls.CustomMessageBox.eDialogButtons.OK,
                                                              CustomImages.GetDialogImage(Sleek_Bill.Controls.CustomImages.eCustomDialogImages.Success));
                }
                else
                {
                    product.ProductId = productID;
                    productService.UpdateProduct(product);
                    CustomMessageBox.Show(string.Format(Constants.SUCCESSFULL_SAVE_MESSAGE, txtProductName.Text),
                                                                  Constants.CONSTANT_INFORMATION,
                                                                  Sleek_Bill.Controls.CustomMessageBox.eDialogButtons.OK,
                                                                  CustomImages.GetDialogImage(Sleek_Bill.Controls.CustomImages.eCustomDialogImages.Success));
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
        }

        private void ResetControls()
        {
            txtProductName.Text = String.Empty;
            txtProdDesc.Text = String.Empty;
            txtUnitPrice.Text = String.Empty;
            txtQuantity.Text = String.Empty;
            cmbTaxRate.SelectedIndex = 0;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetControls();
        }
    }
}
