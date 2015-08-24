using System;
using System.Windows.Forms;

namespace Sleek_Bill.Products
{
    public partial class AddEditProduct : Form
    {
        public AddEditProduct()
        {
            InitializeComponent();
        }

        private void bnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddEditProduct_Load(object sender, EventArgs e)
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
