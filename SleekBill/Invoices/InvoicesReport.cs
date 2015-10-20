using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects;
using DataServices;
using DataServices.Interfaces;
using Sleek_Bill.Common;

namespace Sleek_Bill.Invoices
{
    public partial class InvoicesReport : Form
    {
        private IInvoice invoiceService = new InvoiceService();
        private IClient clientService = new ClientService();
        int currentPage = 1;
        int TotalPage = 0;

        public InvoicesReport()
        {
            InitializeComponent();
        }

        private void InvoicesReport_Load(object sender, EventArgs e)
        {
            Common.Common.SetFormCoordinate(this);
            BindClientName();
            BindPaymentStatus();
            SetDefaultDates();
            BindInvoiceReportsDataGrid();            
        }

        private void SetDefaultDates()
        {
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            dtpIssueBetn1.Value = startDate.Date;
            dtpissueBetn2.Value = endDate.Date;
        }

        private void BindClientName()
        {
            List<Client> clientList = clientService.GetAllClients();
            clientList.Insert(0, new Client(0, "-- Select Client Name --"));
            cmbClientName.DataSource = clientList;
            cmbClientName.DisplayMember = "ClientName";
            cmbClientName.ValueMember = "ClientId";
        }

        private void BindPaymentStatus()
        {
            List<PaymentStatus> lstPaymentStatus = new List<PaymentStatus>();
            lstPaymentStatus.Insert(0, new PaymentStatus(0, ePaymentStatus.All.ToString()));
            lstPaymentStatus.Insert(1, new PaymentStatus(0, ePaymentStatus.Paid.ToString()));
            lstPaymentStatus.Insert(2, new PaymentStatus(0, ePaymentStatus.Unpaid.ToString()));
            lstPaymentStatus.Insert(3, new PaymentStatus(0, ePaymentStatus.Partial.ToString()));
            lstPaymentStatus.Insert(4, new PaymentStatus(0, ePaymentStatus.Overdue.ToString()));
            cmbStatus.DataSource = lstPaymentStatus;
            cmbStatus.DisplayMember = "PaymentStatusName";
            cmbStatus.ValueMember = "PaymentStatusId";
        }

        private void btnNewInvoice_Click(object sender, EventArgs e)
        {
            AddEditInvoice addEditInvoice = new AddEditInvoice();
            addEditInvoice.StartPosition = FormStartPosition.CenterParent;
            addEditInvoice.ShowDialog(this);
            BindInvoiceReportsDataGrid();
        }

        private void BindInvoiceReportsDataGrid()
        {
            int resultsPerPage = Convert.ToInt32(txtResultsPerPage.Text);
            List<Invoice> invoiceList = invoiceService.GetAllInvoices();

            foreach (Invoice invoice in invoiceList)
            {
                invoice.InvoiceProductList = invoiceService.GetAllInvoiceProducts().Where(v => v.InvoiceId == invoice.InvoiceId).ToList<InvoiceProduct>();
            }


            int clientId = Convert.ToInt32(cmbClientName.SelectedValue);

            if (clientId != 0)
                invoiceList = (from invoice in invoiceList
                                 .Where(v => v.ClientId == clientId)
                               select invoice).ToList<Invoice>();

            if (!String.IsNullOrEmpty(txtInvoiceNumber.Text))
                invoiceList = (from invoice in invoiceList
                                 .Where(v => v.InvoiceId == Convert.ToInt32(txtInvoiceNumber.Text))
                               select invoice).ToList<Invoice>();

            //if (cmbStatus.SelectedIndex != 0)
            //    invoiceList = (from invoice in invoiceList
            //                     .Where(v => v.PaymentStatus.Equals(cmbStatus.Text, StringComparison.OrdinalIgnoreCase))
            //                   select invoice).ToList<Invoice>();

            if (!String.IsNullOrEmpty(dtpIssueBetn1.Value.ToString()) && !String.IsNullOrEmpty(dtpissueBetn2.Value.ToString()))
            {
                invoiceList = (from invoice in invoiceList
                                 .Where(v => v.IssueDate.Date >= Convert.ToDateTime(dtpIssueBetn1.Value).Date && v.IssueDate.Date <= Convert.ToDateTime(dtpissueBetn2.Value).Date)
                               select invoice).ToList<Invoice>();
            }

            if (dtpDueBetn1.Text != "__ /__ /____" && dtpDueBetn2.Text != "__ /__ /____")
            {
                invoiceList = (from invoice in invoiceList
                                 .Where(v => v.DueDate.Date >= Convert.ToDateTime(dtpDueBetn1.Value).Date && v.DueDate.Date <= Convert.ToDateTime(dtpDueBetn2.Value).Date)
                               select invoice).ToList<Invoice>();
            }

            int totalRecords = invoiceList.Count;
            CalculateTotalPages(totalRecords);
            int srNo = 0;

            var columns = from invoice in invoiceList
                          join client in clientService.GetAllClients()
                          on invoice.ClientId equals client.ClientId
                          select new
                          {
                              No = ++srNo,
                              ClientName = client.ClientName,
                              InvoiceNo = invoice.InvoiceId,
                              IssueDate = invoice.IssueDate,
                              DueDate = invoice.DueDate,
                              Amount = invoice.InvoiceProductList.Sum(v => v.TotalPrice),
                              Tax = invoice.InvoiceProductList.Sum(v => v.TaxValue),
                              Total = invoice.TotalAmount,
                              Status = invoice.PaymentStatus,
                              PrivateNotes = invoice.PrivateNotes,
                              AmountPaid = invoice.AmountPaid,
                              Balance = invoice.TotalAmount - invoice.AmountPaid
                          };

            if (currentPage == 1)
            {
                this.dgvInvoices.DataSource = columns.Take(resultsPerPage).ToList();
            }
            else
            {
                int skipRecordsNo = resultsPerPage * (currentPage - 1);
                this.dgvInvoices.DataSource = columns.Skip(skipRecordsNo).Take(resultsPerPage).ToList();
            }

            if (currentPage <= TotalPage)
            {
                txtPageEnd.Visible = true;
                txtPageStart.Visible = true;
                btnLeft.Visible = true;
                btnRight.Visible = true;
                lblSeperator.Visible = true;
                txtPageStart.Text = Convert.ToString(currentPage);
                txtPageEnd.Text = Convert.ToString(TotalPage);
                txtPageEnd.Enabled = false;

                if (currentPage == 1)
                {
                    btnLeft.Enabled = false;
                    btnRight.Enabled = true;
                }
                else if (currentPage == TotalPage)
                {
                    btnRight.Enabled = false;
                    btnLeft.Enabled = true;
                }
                else
                {
                    btnLeft.Enabled = true;
                    btnRight.Enabled = true;
                }

                if (TotalPage == 1)
                {
                    txtPageEnd.Visible = false; ;
                    txtPageStart.Visible = false;
                    btnLeft.Visible = false;
                    btnRight.Visible = false;
                    lblSeperator.Visible = false;
                }
            }
        }

        private void CalculateTotalPages(int rowCount)
        {
            int pageSize = Convert.ToInt32(txtResultsPerPage.Text);
            TotalPage = rowCount / pageSize;

            if (rowCount % pageSize > 0)
                TotalPage += 1;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            BindInvoiceReportsDataGrid();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
                currentPage--;

            BindInvoiceReportsDataGrid();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (currentPage < TotalPage)
                currentPage++;

            BindInvoiceReportsDataGrid();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindInvoiceReportsDataGrid();
        }
    }
}
