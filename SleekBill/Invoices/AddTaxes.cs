using System;
using System.Windows.Forms;

namespace Sleek_Bill.Invoices
{
    public partial class AddTaxes : Form
    {
        public AddTaxes()
        {
            InitializeComponent();
        }

        private void AddTaxes_Load(object sender, EventArgs e)
        {
            try
            {
                Common.Common.SetFormCoordinate(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
        }
    }
}
