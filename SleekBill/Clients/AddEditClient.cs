using System;
using System.Windows.Forms;

namespace Sleek_Bill.Clients
{
    public partial class AddNewClient : Form
    {
        public AddNewClient()
        {
            InitializeComponent();
        }

        private void AddNewClient_Load(object sender, EventArgs e)
        {
            try
            {
                Common.Common.SetDialogCoordinate(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
        }

        private void bnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
