using System;

namespace BusinessObjects
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int ClientId { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string PaymentTerms { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public Product product { get; set; }
        public int TaxId { get; set; }
        public decimal Discount { get; set; }
        public bool RoundOffTotal { get; set; }
        public bool MarkInvoicePaid { get; set; }
        public string PaymentType { get; set; }
        public decimal AmountPaid { get; set; }
        public string Notes { get; set; }
        public string NotesForClient { get; set; }
        public string PrivateNotes { get; set; }
    }
}
