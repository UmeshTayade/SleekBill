using System;

namespace BusinessObjects
{
    public class Product
    {
        public Product()
        {

        }

        public Product(int productId, string productName)
        {
            this.ProductId = productId;
            this.ProductName = productName;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int TaxId { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
