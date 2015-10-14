using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using BusinessObjects;

namespace DataServices.Interfaces
{
    public interface IProduct
    {
        [DataObjectMethod(DataObjectMethodType.Insert)]
        int AddProduct(Product product);
        List<Product> GetAllProducts();
        [DataObjectMethod(DataObjectMethodType.Select)]
        Product GetProduct(int productId);
        [DataObjectMethod(DataObjectMethodType.Update)]
        void UpdateProduct(Product client);
        [DataObjectMethod(DataObjectMethodType.Update)]
        void DeactivateProduct(int productId);
    }
}
