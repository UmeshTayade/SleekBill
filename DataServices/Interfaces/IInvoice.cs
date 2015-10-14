using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using BusinessObjects;

namespace DataServices.Interfaces
{
    public interface IInvoice
    {
        [DataObjectMethod(DataObjectMethodType.Insert)]
        int AddInvoice(Invoice invoice);
        [DataObjectMethod(DataObjectMethodType.Insert)]
        void AddInvoiceProducts(InvoiceProduct invoiceProduct);
        List<Invoice> GetAllInvoices();
        [DataObjectMethod(DataObjectMethodType.Select)]
        Invoice GetInvoice(int invoiceId);
        List<InvoiceProduct> GetInvoiceProductsByInvoiceId(int invoiceId);
        [DataObjectMethod(DataObjectMethodType.Update)]
        void UpdateInvoice(Invoice invoice);
        [DataObjectMethod(DataObjectMethodType.Update)]
        void UpdateInvoiceProducts(InvoiceProduct invoiceProduct);
        [DataObjectMethod(DataObjectMethodType.Update)]
        void DeactivateInvoice(int invoiceId);
    }
}
