using System;
using System.Windows.Forms;

namespace Sleek_Bill.Invoices
{
    public partial class EditTax : Form
    {
        public EditTax()
        {
            InitializeComponent();
        }

        private void EditTax_Load(object sender, EventArgs e)
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
    }
}
