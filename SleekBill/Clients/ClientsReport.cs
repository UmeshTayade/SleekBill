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

        public ClientsReport()
        {
            InitializeComponent();
        }

        private void ClientsReport_Load(object sender, EventArgs e)
        {
            Common.Common.SetFormCoordinate(this);
            BindClientReportsDataGrid();
        }

        private void BindClientReportsDataGrid()
        {
            List<Client> clientList = clientService.GetAllClients();
            var columns = from client in clientList
                          select new
                          {
                              ClientId = client.ClientId,
                              ClientName = client.ClientName,
                              ContactName = client.ContactName,
                              BillingAddress = client.BillingAddress,
                              Email = client.Email,
                              Phone = client.Phone,
                              PrivateClientDetails = client.PrivateClientDetails
                          };           
            this.dgvClientResult.DataSource = columns.ToList();
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
            DialogResult dialogRes =  CustomMessageBox.Show(Constants.DELETE_WARNING,
                                  Constants.CONSTANT_INFORMATION,
                                  Sleek_Bill.Controls.CustomMessageBox.eDialogButtons.YesNo,
                                  CustomImages.GetDialogImage(Sleek_Bill.Controls.CustomImages.eCustomDialogImages.Information));

            if (dialogRes == System.Windows.Forms.DialogResult.Yes)
            {
                clientService.DeactivateClient(clientId);
            }

            BindClientReportsDataGrid();
        }
    }
}
