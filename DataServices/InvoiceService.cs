using System.Collections.Generic;
using BusinessObjects;
using DataObjects;
using DataObjects.Interfaces;
using DataServices.Interfaces;

namespace DataServices
{
    public class InvoiceService : IInvoice
    {
        private readonly IInvoiceDB invoiceDBObj = DBManager.InvoiceDB;

        public int AddInvoice(Invoice invoice)
        {
            return this.invoiceDBObj.AddInvoice(invoice);
        }

        public void AddInvoiceProducts(InvoiceProduct invoiceProduct)
        {
            this.invoiceDBObj.AddInvoiceProducts(invoiceProduct);
        }

        public List<Invoice> GetAllInvoices()
        {
            return this.invoiceDBObj.GetAllInvoices();
        }

        public List<InvoiceProduct> GetAllInvoiceProducts()
        {
            return this.invoiceDBObj.GetAllInvoiceProducts();
        }

        public Invoice GetInvoice(int invoiceId)
        {
            return this.invoiceDBObj.GetInvoice(invoiceId);
        }

        public List<InvoiceProduct> GetInvoiceProductsByInvoiceId(int invoiceId)
        {
            return this.invoiceDBObj.GetInvoiceProductsByInvoiceId(invoiceId);
        }

        public void UpdateInvoice(Invoice invoice)
        {
            this.invoiceDBObj.UpdateInvoice(invoice);
        }

        public void UpdateInvoiceProducts(InvoiceProduct invoiceProduct)
        {
            this.invoiceDBObj.UpdateInvoiceProducts(invoiceProduct);
        }

        public void DeactivateInvoice(int invoiceId)
        {
            this.invoiceDBObj.DeactivateInvoice(invoiceId);
        }
    }
}
