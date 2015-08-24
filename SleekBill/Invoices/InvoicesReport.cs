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
    public partial class InvoicesReport : Form
    {
        public InvoicesReport()
        {
            InitializeComponent();
        }

        private void InvoicesReport_Load(object sender, EventArgs e)
        {
            Common.Common.SetFormCoordinate(this);
        }

        private void btnNewInvoice_Click(object sender, EventArgs e)
        {
            AddEditInvoice addEditInvoice = new AddEditInvoice();
            addEditInvoice.StartPosition = FormStartPosition.CenterParent;
            addEditInvoice.ShowDialog(this);
        }
    }
}
