using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sleek_Bill_Company;
using Sleek_Bill.Clients;
using Sleek_Bill.Products;
using Sleek_Bill.Invoices;

namespace Sleek_Bill
{
    public partial class SleekBillParentContainer : Form
    {
        public SleekBillParentContainer()
        {
            InitializeComponent();            
        }

        private void companyDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] childForms = this.MdiChildren;
            foreach (Form childForm in childForms)
                childForm.Close();
            AddCompanyDetails addCompanyDetails = new AddCompanyDetails();
            addCompanyDetails.StartPosition = FormStartPosition.CenterParent;
            addCompanyDetails.MdiParent = this;
            addCompanyDetails.Show();
        }

        private void clientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] childForms = this.MdiChildren;
            foreach (Form childForm in childForms)
                childForm.Close();
            ClientsReport clientsReport = new ClientsReport();
            clientsReport.StartPosition = FormStartPosition.CenterParent;
            clientsReport.MdiParent = this;
            clientsReport.Show();
        }

        private void SleekBillParentContainer_Load(object sender, EventArgs e)
        {
            IsMdiContainer = true;
            MdiClient ctlMDI;

            foreach (Control ctl in this.Controls)
            {
                try
                {
                    ctlMDI = (MdiClient)ctl;
                    ctlMDI.BackColor = Color.DarkCyan;
                }
                catch (InvalidCastException ex)
                {
                    // Catch and ignore the error if casting failed.
                }
            }
        }

        private void productServicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] childForms = this.MdiChildren;
            foreach (Form childForm in childForms)
                childForm.Close();
            ProductServiceReport productServiceReport = new ProductServiceReport();
            productServiceReport.StartPosition = FormStartPosition.CenterParent;
            productServiceReport.MdiParent = this;
            productServiceReport.Show();
        }

        private void invoicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] childForms = this.MdiChildren;
            foreach (Form childForm in childForms)
                childForm.Close();
            InvoicesReport invoicesReport = new InvoicesReport();
            invoicesReport.StartPosition = FormStartPosition.CenterParent;
            invoicesReport.MdiParent = this;
            invoicesReport.Show();
        }

        private void taxesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] childForms = this.MdiChildren;
            foreach (Form childForm in childForms)
                childForm.Close();
            AddTaxes addTaxes = new AddTaxes();
            addTaxes.StartPosition = FormStartPosition.CenterParent;
            addTaxes.MdiParent = this;
            addTaxes.Show();
        }
    }
}
