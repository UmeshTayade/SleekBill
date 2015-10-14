using System;
using System.Collections.Generic;

namespace BusinessObjects
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int CompanyId { get; set; }
        public int ClientId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public string PurchaseOrderNo { get; set; }
        public int PaymentTermId { get; set; }
        public List<InvoiceProduct> InvoiceProductList { get; set; }
        public decimal Discount { get; set; }
        public bool RoundOffTotal { get; set; }
        public bool MarkInvoicePaid { get; set; }
        public int PaymentTypeId { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal TotalAmount { get; set; }
        public string Notes { get; set; }
        public string NotesForClient { get; set; }
        public string PrivateNotes { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
