using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects;
using DataServices;
using DataServices.Interfaces;
using Sleek_Bill.Controls;
using Sleek_Bill.Common;

namespace Sleek_Bill.Clients
{
    public partial class ClientsReport : Form
    {
        private IClient clientService = new ClientService();
        int rowIndex = 0;
        int currentPage = 1;
        int TotalPage = 0;

        public ClientsReport()
        {
            InitializeComponent();
        }

        private void ClientsReport_Load(object sender, EventArgs e)
        {
            Common.Common.SetFormCoordinate(this);
            BindClientReportsDataGrid();
            BindClientName();
        }

        private void BindClientName()
        {
            List<Client> clientList = clientService.GetAllClients();
            clientList.Insert(0, new Client(0, "-- Select Client Name --"));
            cmbClientName.DataSource = clientList;
            cmbClientName.DisplayMember = "ClientName";
            cmbClientName.ValueMember = "ClientId";
        }

        private void BindClientReportsDataGrid()
        {
            int resultsPerPage = Convert.ToInt32(txtResultsPerPage.Text);
            List<Client> clientList = clientService.GetAllClients();
            int clientId = Convert.ToInt32(cmbClientName.SelectedValue);

            if (clientId != 0)
                clientList = (from client in clientList
                                 .Where(v => v.ClientId == clientId)
                              select client).ToList<Client>();

            if (!String.IsNullOrEmpty(txtContactName.Text))
                clientList = (from client in clientList
                                 .Where(v => v.ContactName.StartsWith(txtContactName.Text, StringComparison.OrdinalIgnoreCase))
                              select client).ToList<Client>();

            if (!String.IsNullOrEmpty(txtEmail.Text))
                clientList = (from client in clientList
                                 .Where(v => v.Email.StartsWith(txtEmail.Text, StringComparison.OrdinalIgnoreCase))
                              select client).ToList<Client>();

            if (!String.IsNullOrEmpty(txtPhone.Text))
                clientList = (from client in clientList
                                 .Where(v => v.Phone.StartsWith(txtPhone.Text, StringComparison.OrdinalIgnoreCase))
                              select client).ToList<Client>();

            int totalRecords = clientList.Count;
            CalculateTotalPages(totalRecords);
            int srNo = 0;
            var columns = from client in clientList
                          select new
                          {
                              No = ++srNo,
                              ClientId = client.ClientId,
                              ClientName = client.ClientName,
                              ContactName = client.ContactName,
                              BillingAddress = client.BillingAddress,
                              Email = client.Email,
                              Phone = client.Phone,
                              PrivateClientDetails = client.PrivateClientDetails
                          };
            if (currentPage == 1)
            {
                this.dgvClientResult.DataSource = columns.Take(resultsPerPage).ToList();
            }
            else
            {
                int skipRecordsNo = resultsPerPage * (currentPage - 1);
                this.dgvClientResult.DataSource = columns.Skip(skipRecordsNo).Take(resultsPerPage).ToList();
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

        private void btnNewClient_Click(object sender, EventArgs e)
        {
            AddNewClient addNewClient = new AddNewClient();
            addNewClient.StartPosition = FormStartPosition.CenterParent;
            addNewClient.ShowDialog(this);
            BindClientReportsDataGrid();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvClientResult.Rows[rowIndex];
            int clientId = Convert.ToInt32(row.Cells["ClientId"].Value);
            AddNewClient addNewClient = new AddNewClient();
            addNewClient.clientID = clientId;
            addNewClient.formsState = Common.eFormsState.Edit;
            addNewClient.ShowDialog(this);
            BindClientReportsDataGrid();
        }

        private void dgvClientResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvClientResult.Rows[rowIndex];
            int clientId = Convert.ToInt32(row.Cells["ClientId"].Value);
            AddNewClient addNewClient = new AddNewClient();
            addNewClient.clientID = clientId;
            addNewClient.formsState = Common.eFormsState.View;
            addNewClient.ShowDialog(this);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvClientResult.Rows[rowIndex];
            int clientId = Convert.ToInt32(row.Cells["ClientId"].Value);
            DialogResult dialogRes = CustomMessageBox.Show(Constants.DELETE_WARNING,
                                  Constants.CONSTANT_INFORMATION,
                                  Sleek_Bill.Controls.CustomMessageBox.eDialogButtons.YesNo,
                                  CustomImages.GetDialogImage(Sleek_Bill.Controls.CustomImages.eCustomDialogImages.Information));

            if (dialogRes == System.Windows.Forms.DialogResult.Yes)
            {
                clientService.DeactivateClient(clientId);
            }

            BindClientReportsDataGrid();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindClientReportsDataGrid();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cmbClientName.SelectedIndex = 0;
            txtContactName.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtPhone.Text = String.Empty;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            BindClientReportsDataGrid();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (currentPage < TotalPage)
                currentPage++;

            BindClientReportsDataGrid();
        }

        private void CalculateTotalPages(int rowCount)
        {
            int pageSize = Convert.ToInt32(txtResultsPerPage.Text);
            TotalPage = rowCount / pageSize;

            if (rowCount % pageSize > 0)
                TotalPage += 1;
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
                currentPage--;

            BindClientReportsDataGrid();
        }
    }
}
