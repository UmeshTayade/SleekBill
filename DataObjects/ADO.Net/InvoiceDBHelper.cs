using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Transactions;
using BusinessObjects;
using DataObjectHelpers;
using DataObjects.Interfaces;

namespace DataObjects.ADO.Net
{
    public class InvoiceDBHelper : IInvoiceDB
    {
        public int AddInvoice(Invoice invoice)
        {
            DbParameter parameter = null;
            parameter = Db.CreateParameter("InvoiceId", DbType.Int32, 8);
            parameter.Direction = ParameterDirection.Output;
            Db.ExecuteNonQuery("usp_Invoice_InsertInvoiceDetails", CommandType.StoredProcedure,
                new DbParameter[] { 
                               parameter,
                               Db.CreateParameter("CompanyId", invoice.CompanyId),
                               Db.CreateParameter("ClientId", invoice.ClientId),
                               Db.CreateParameter("IssueDate", invoice.IssueDate),
                               Db.CreateParameter("PurchaseOrderNo", invoice.PurchaseOrderNo),
                               Db.CreateParameter("PaymentTermId", invoice.PaymentTermId),
                               Db.CreateParameter("DueDate", invoice.DueDate),                               
                               Db.CreateParameter("Discount", invoice.Discount),
                               Db.CreateParameter("RoundOffTotal", invoice.RoundOffTotal),
                               Db.CreateParameter("MarkInvoicePaid", invoice.MarkInvoicePaid),
                               Db.CreateParameter("PaymentTypeId", invoice.PaymentTypeId),
                               Db.CreateParameter("AmountPaid", invoice.AmountPaid),
                               Db.CreateParameter("Notes", invoice.Notes),
                               Db.CreateParameter("TotalAmount", invoice.TotalAmount),
                               Db.CreateParameter("NotesForClient", invoice.NotesForClient),
                               Db.CreateParameter("PrivateNotes", invoice.PrivateNotes),
                               Db.CreateParameter("Status", invoice.Status)
                 });

            return (int)parameter.Value;
        }

        public void AddInvoiceProducts(InvoiceProduct invoiceProduct)
        {
            DbParameter parameter = null;
            Db.ExecuteNonQuery("usp_Invoice_InsertInvoiceProducts", CommandType.StoredProcedure,
                new DbParameter[] { 
                               parameter,
                               Db.CreateParameter("InvoiceId", invoiceProduct.InvoiceId),
                               Db.CreateParameter("ProductId", invoiceProduct.ProductId),
                               Db.CreateParameter("ProductName", invoiceProduct.ProductName),
                               Db.CreateParameter("Description", invoiceProduct.Description),
                               Db.CreateParameter("Quantity", invoiceProduct.Quantity),
                               Db.CreateParameter("UnitPrice", invoiceProduct.UnitPrice),                               
                               Db.CreateParameter("TotalPrice", invoiceProduct.TotalPrice),
                               Db.CreateParameter("TaxId", invoiceProduct.TaxId),
                               Db.CreateParameter("TaxValue", invoiceProduct.TaxValue)
                 });
        }

        public List<Invoice> GetAllInvoices()
        {
            return Db.MapReader<Invoice>("usp_Invoice_GetAllInvoices", CommandType.StoredProcedure, new DbParameter[0]);
        }

        public Invoice GetInvoice(int invoiceId)
        {
            return Db.Map<Invoice>("usp_Invoice_GetInvoiceDetails", CommandType.StoredProcedure, new DbParameter[] { Db.CreateParameter("InvoiceId", invoiceId) });
        }

        public List<InvoiceProduct> GetInvoiceProductsByInvoiceId(int invoiceId)
        {
            return Db.MapReader<InvoiceProduct>("usp_InvoiceProduct_GetInvoiceProductsByInvoiceId", CommandType.StoredProcedure, new DbParameter[] { Db.CreateParameter("InvoiceId", invoiceId) });
        }

        public void UpdateInvoice(Invoice invoice)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Db.ExecuteNonQuery("usp_Invoice_UpdateInvoice", CommandType.StoredProcedure,
                    new DbParameter[] { 
                               Db.CreateParameter("InvoiceId", invoice.InvoiceId),
                               Db.CreateParameter("CompanyId", invoice.CompanyId),
                               Db.CreateParameter("ClientId", invoice.ClientId),                               
                               Db.CreateParameter("IssueDate", invoice.IssueDate),
                               Db.CreateParameter("PurchaseOrderNo", invoice.PurchaseOrderNo),
                               Db.CreateParameter("PaymentTermId", invoice.PaymentTermId),
                               Db.CreateParameter("DueDate", invoice.DueDate),                               
                               Db.CreateParameter("Discount", invoice.Discount),
                               Db.CreateParameter("RoundOffTotal", invoice.RoundOffTotal),
                               Db.CreateParameter("MarkInvoicePaid", invoice.MarkInvoicePaid),
                               Db.CreateParameter("PaymentTypeId", invoice.PaymentTypeId),
                               Db.CreateParameter("AmountPaid", invoice.AmountPaid),
                               Db.CreateParameter("Notes", invoice.Notes),
                               Db.CreateParameter("TotalAmount", invoice.TotalAmount),
                               Db.CreateParameter("NotesForClient", invoice.NotesForClient),
                               Db.CreateParameter("PrivateNotes", invoice.PrivateNotes)
                               
                 });
                scope.Complete();
            }
        }

        public void UpdateInvoiceProducts(InvoiceProduct invoiceProduct)
        {
            Db.ExecuteNonQuery("usp_Invoice_UpdateInvoiceProducts", CommandType.StoredProcedure,
                    new DbParameter[] { 
                               Db.CreateParameter("InvoiceId", invoiceProduct.InvoiceId),
                               Db.CreateParameter("ProductId", invoiceProduct.ProductId),
                               Db.CreateParameter("ProductName", invoiceProduct.ProductName),
                               Db.CreateParameter("Description", invoiceProduct.Description),
                               Db.CreateParameter("Quantity", invoiceProduct.Quantity),
                               Db.CreateParameter("UnitPrice", invoiceProduct.UnitPrice),                               
                               Db.CreateParameter("TotalPrice", invoiceProduct.TotalPrice),
                               Db.CreateParameter("TaxId", invoiceProduct.TaxId),
                               Db.CreateParameter("TaxValue", invoiceProduct.TaxValue)                               
                 });
        }

        public void DeactivateInvoice(int invoiceId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Db.ExecuteNonQuery("usp_Invoice_DeactivateInvoice", CommandType.StoredProcedure,
                    new DbParameter[] { Db.CreateParameter("InvoiceId", invoiceId) });
                scope.Complete();
            }
        }
    }
}
