using System;
using System.Windows.Forms;
using BusinessObjects;
using DataServices;
using DataServices.Interfaces;
using Sleek_Bill.Common;
using Sleek_Bill.Controls;
using System.Collections.Generic;

namespace Sleek_Bill.Clients
{
    public partial class AddNewClient : Form
    {
        private IClient clientService = new ClientService();
        private IMaster masterService = new MasterService();
        public int clientID = 0;
        public eFormsState formsState;

        public AddNewClient()
        {
            InitializeComponent();
        }

        private void AddNewClient_Load(object sender, EventArgs e)
        {
            try
            {
                Common.Common.SetDialogCoordinate(this);
                LoadStates();

                if (clientID != 0)
                {
                    ShowSelectedClientData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
        }

        private void ShowSelectedClientData()
        {
            Client clientRecord = clientService.GetClient(clientID);
            txtClientName.Text = clientRecord.ClientName;
            txtContactName.Text = clientRecord.ContactName;
            txtPhone.Text = clientRecord.Phone;
            txtEmail.Text = clientRecord.Email;
            txtTIN.Text = clientRecord.TIN;
            txtPrivateClientDetails.Text = clientRecord.PrivateClientDetails;
            txtOtherClientDetails.Text = clientRecord.OtherClientDetails;
            txtBillingAddress.Text = clientRecord.BillingAddress;
            txtCity.Text = clientRecord.City;
            txtZip.Text = clientRecord.Zip;
            cmbState.SelectedValue = clientRecord.StateCode;
            txtCountry.Text = clientRecord.Country;
            chkDifferentAddress.Checked = clientRecord.ShipToDifferentAddress;
            ShowShippingAddressControls();
            txtShippingAddress.Text = clientRecord.ShippingAddress;
            txtShippingCity.Text = clientRecord.ShippingCity;
            txtShippingZip.Text = clientRecord.ShippingZip;
            cmbShippingState.SelectedValue = clientRecord.ShippingStateCode;
            txtShippingCountry.Text = clientRecord.ShippingCountry;
            btnReset.Visible = false;

            if (formsState == eFormsState.View)
            {
                DisableControls();
            }
        }

        private void DisableControls()
        {
            txtClientName.Enabled = false;
            txtContactName.Enabled = false;
            txtContactName.Enabled = false;
            txtPhone.Enabled = false;
            txtEmail.Enabled = false;
            txtTIN.Enabled = false;
            txtBillingAddress.Enabled = false;
            txtCity.Enabled = false;
            txtZip.Enabled = false;
            cmbState.Enabled = false;
            txtCountry.Enabled = false;
            chkDifferentAddress.Enabled = false;
            txtPrivateClientDetails.Enabled = false;
            txtOtherClientDetails.Enabled = false;
            btnSave.Enabled = false;
        }

        public void LoadStates()
        {
            List<State> lstState = masterService.GetAllStates();
            List<State> lstShippingState = masterService.GetAllStates();     
       
            lstState.Insert(0, new State("DF", "-- Select State --"));
            cmbState.DataSource = lstState;
            cmbState.DisplayMember = "StateName";
            cmbState.ValueMember = "StateCode";

            lstShippingState.Insert(0, new State("DF", "-- Select State --"));
            cmbShippingState.DataSource = lstShippingState;
            cmbShippingState.DisplayMember = "StateName";
            cmbShippingState.ValueMember = "StateCode";
        }

        private void bnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Client client = new Client();
            client.ClientName = txtClientName.Text.Trim();
            client.ContactName = txtContactName.Text.Trim();
            client.Phone = txtPhone.Text.Trim();
            client.Email = txtEmail.Text.Trim();
            client.TIN = txtTIN.Text.Trim();
            client.BillingAddress = txtBillingAddress.Text.Trim();
            client.City = txtCity.Text.Trim();
            client.Country = txtCountry.Text.Trim();
            client.Zip = txtZip.Text.Trim();
            client.StateCode = Convert.ToString(cmbState.SelectedValue);
            client.ShipToDifferentAddress = chkDifferentAddress.Checked;
            client.ShippingAddress = txtShippingAddress.Text.Trim();
            client.ShippingCity = txtShippingCity.Text.Trim();
            client.ShippingStateCode = Convert.ToString(cmbShippingState.SelectedValue);
            client.ShippingZip = txtShippingZip.Text.Trim();
            client.ShippingCountry = txtShippingCountry.Text.Trim();
            client.PrivateClientDetails = txtPrivateClientDetails.Text.Trim();
            client.OtherClientDetails = txtOtherClientDetails.Text.Trim();
            client.Status = true;
            client.CreatedDate = DateTime.Now.Date;

            if (clientID == 0)
            {
                int clientId = clientService.AddClient(client);
                ResetControls();
                CustomMessageBox.Show(string.Format(Constants.SUCCESSFULL_ADD_MESSAGE, Constants.CONSTANT_CLIENT, txtClientName.Text),
                                                              Constants.CONSTANT_INFORMATION,
                                                              Sleek_Bill.Controls.CustomMessageBox.eDialogButtons.OK,
                                                              CustomImages.GetDialogImage(Sleek_Bill.Controls.CustomImages.eCustomDialogImages.Success));
            }
            else
            {
                client.ClientId = clientID;
                clientService.UpdateClient(client);
                CustomMessageBox.Show(string.Format(Constants.SUCCESSFULL_SAVE_MESSAGE, txtClientName.Text),
                                                              Constants.CONSTANT_INFORMATION,
                                                              Sleek_Bill.Controls.CustomMessageBox.eDialogButtons.OK,
                                                              CustomImages.GetDialogImage(Sleek_Bill.Controls.CustomImages.eCustomDialogImages.Success));
            }

            this.Close();
        }

        private void ResetControls()
        {
            txtClientName.Text = String.Empty;
            txtContactName.Text = String.Empty;
            txtPhone.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtTIN.Text = String.Empty;
            txtPrivateClientDetails.Text = String.Empty;
            txtOtherClientDetails.Text = String.Empty;
            txtBillingAddress.Text = String.Empty;
            txtCity.Text = String.Empty;
            txtZip.Text = String.Empty;
            cmbState.SelectedIndex = 0;
            chkDifferentAddress.Checked = false;
            txtShippingAddress.Text = String.Empty;
            txtShippingCity.Text = String.Empty;
            txtShippingZip.Text = String.Empty;
            cmbShippingState.SelectedIndex = 0;
            txtShippingCountry.Text = String.Empty;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        private void chkDifferentAddress_CheckedChanged(object sender, EventArgs e)
        {
            ShowShippingAddressControls();
        }

        private void ShowShippingAddressControls()
        {
            if (chkDifferentAddress.Checked)
            {
                lblShippingAddress.Visible = true;
                lblShippingCity.Visible = true;
                lblShippingCountry.Visible = true;
                lblShippingState.Visible = true;
                lblShippingZip.Visible = true;
                txtShippingAddress.Visible = true;
                txtShippingCity.Visible = true;
                cmbShippingState.Visible = true;
                txtShippingZip.Visible = true;
                txtShippingCountry.Visible = true;
            }
            else
            {
                lblShippingAddress.Visible = false;
                lblShippingCity.Visible = false;
                lblShippingCountry.Visible = false;
                lblShippingState.Visible = false;
                lblShippingZip.Visible = false;
                txtShippingAddress.Visible = false;
                txtShippingCity.Visible = false;
                cmbShippingState.Visible = false;
                txtShippingZip.Visible = false;
                txtShippingCountry.Visible = false;
            }
        }
    }
}