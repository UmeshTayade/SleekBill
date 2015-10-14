
using System;
namespace BusinessObjects
{
    public class Tax
    {
        public Tax()
        {

        }

        public Tax(int taxId, string taxName)
        {
            this.TaxId = taxId;
            this.TaxName = taxName;
        }
        public int TaxId { get; set; }
        public string TaxName { get; set; }
        public decimal TaxPercentage { get; set; }
        public bool IsDefault { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
