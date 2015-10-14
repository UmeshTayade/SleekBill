using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;

namespace DataObjects.Interfaces
{
    public interface IInvoiceDB
    {
        int AddInvoice(Invoice invoice);
        void AddInvoiceProducts(InvoiceProduct invoiceProduct);
        List<Invoice> GetAllInvoices();
        Invoice GetInvoice(int invoiceId);
        List<InvoiceProduct> GetInvoiceProductsByInvoiceId(int invoiceId);
        void UpdateInvoice(Invoice invoice);
        void UpdateInvoiceProducts(InvoiceProduct invoiceProduct);
        void DeactivateInvoice(int invoiceId);
    }
}
