using System;
using System.Windows.Forms;
using Sleek_Bill.Common;
using DataServices;
using DataServices.Interfaces;
using BusinessObjects;
using Sleek_Bill.Controls;

namespace Sleek_Bill.Invoices
{
    public partial class EditTax : Form
    {
        private IMaster masterService = new MasterService();
        public int TaxId = 0;
        public eFormsState formsState;

        public EditTax()
        {
            InitializeComponent();
        }

        private void EditTax_Load(object sender, EventArgs e)
        {
            try
            {
                Common.Common.SetDialogCoordinate(this);

                if (TaxId != 0)
                {
                    ShowSelectedTaxData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
        }

        private void ShowSelectedTaxData()
        {
            Tax taxRecord = masterService.GetTax(TaxId);
            txtTaxName.Text = taxRecord.TaxName;
            txtTaxPercentage.Text = Convert.ToString(taxRecord.TaxPercentage);

            if (formsState == eFormsState.View)
            {
                DisableControls();
            }
        }

        private void DisableControls()
        {
            txtTaxName.Enabled = false;
            txtTaxPercentage.Enabled = false;            
            btnSave.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Tax tax = new Tax();
            tax.TaxId = TaxId;
            tax.TaxName = txtTaxName.Text.Trim();
            tax.TaxPercentage = Convert.ToDecimal(txtTaxPercentage.Text);
            masterService.UpdateTax(tax);
            CustomMessageBox.Show(string.Format(Constants.SUCCESSFULL_TAX_SAVE_MESSAGE, txtTaxName.Text),
                                                              Constants.CONSTANT_INFORMATION,
                                                              Sleek_Bill.Controls.CustomMessageBox.eDialogButtons.OK,
                                                              CustomImages.GetDialogImage(Sleek_Bill.Controls.CustomImages.eCustomDialogImages.Success));
            this.Close();
        }
    }
}
