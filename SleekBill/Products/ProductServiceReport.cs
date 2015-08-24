using System;
using System.Windows.Forms;

namespace Sleek_Bill.Products
{
    public partial class ProductServiceReport : Form
    {
        public ProductServiceReport()
        {
            InitializeComponent();
        }

        private void btnNewProduct_Click(object sender, EventArgs e)
        {
            AddEditProduct addEditProduct = new AddEditProduct();
            addEditProduct.StartPosition = FormStartPosition.CenterParent;
            addEditProduct.ShowDialog(this);
        }

        private void ProductServiceReport_Load(object sender, EventArgs e)
        {
            Common.Common.SetFormCoordinate(this);
        }
    }
}
