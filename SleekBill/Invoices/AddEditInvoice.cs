using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sleek_Bill.Invoices
{
    public partial class AddEditInvoice : Form
    {
        public AddEditInvoice()
        {
            InitializeComponent();
        }

        private void AddEditInvoice_Load(object sender, EventArgs e)
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
