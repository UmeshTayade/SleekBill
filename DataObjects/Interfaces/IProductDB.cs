using System.Collections.Generic;
using BusinessObjects;

namespace DataObjects.Interfaces
{
    public interface IProductDB
    {
        int AddProduct(Product client);
        List<Product> GetAllProducts();
        Product GetProduct(int productId);
        void UpdateProduct(Product product);
        void DeactivateProduct(int productId);
    }
}
