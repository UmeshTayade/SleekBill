using System;
using System.Windows.Forms;

namespace Sleek_Bill.Clients
{
    public partial class ClientsReport : Form
    {
        public ClientsReport()
        {
            InitializeComponent();
        }

        private void ClientsReport_Load(object sender, EventArgs e)
        {
            Common.Common.SetFormCoordinate(this);            
        }

        private void btnNewClient_Click(object sender, EventArgs e)
        {
            AddNewClient addNewClient = new AddNewClient();
            addNewClient.StartPosition = FormStartPosition.CenterParent;
            addNewClient.ShowDialog(this);
        }
    }
}
