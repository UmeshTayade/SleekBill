using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataServices.Interfaces;
using DataObjects.Interfaces;
using DataObjects;
using BusinessObjects;

namespace DataServices
{
    public class ProductService : IProduct
    {
        private readonly IProductDB productDBObj = DBManager.ProductDB;

        public int AddProduct(Product product)
        {
            return this.productDBObj.AddProduct(product);
        }

        public List<Product> GetAllProducts()
        {
            return this.productDBObj.GetAllProducts();
        }

        public Product GetProduct(int productId)
        {
            return this.productDBObj.GetProduct(productId);
        }

        public void UpdateProduct(Product product)
        {
            this.productDBObj.UpdateProduct(product);
        }

        public void DeactivateProduct(int productId)
        {
            this.productDBObj.DeactivateProduct(productId);
        }
    }
}
