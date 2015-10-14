using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Transactions;
using BusinessObjects;
using DataObjectHelpers;
using DataObjects.Interfaces;

namespace DataObjects.ADO.Net
{
    public class ProductDBHelper : IProductDB
    {
        public int AddProduct(Product product)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                DbParameter parameter = null;
                parameter = Db.CreateParameter("ProductId", DbType.Int32, 8);
                parameter.Direction = ParameterDirection.Output;
                Db.ExecuteNonQuery("usp_Product_InsertProductDetails", CommandType.StoredProcedure,
                    new DbParameter[] { 
                               parameter, 
                               Db.CreateParameter("ProductName", product.ProductName),
                               Db.CreateParameter("Description", product.Description),
                               Db.CreateParameter("UnitPrice", product.UnitPrice),
                               Db.CreateParameter("Quantity", product.Quantity),
                               Db.CreateParameter("TaxId", product.TaxId),
                               Db.CreateParameter("Status", product.Status)
                 });
                scope.Complete();
                return (int)parameter.Value;
            }
        }

        public List<Product> GetAllProducts()
        {
            return Db.MapReader<Product>("usp_Product_GetAllProducts", CommandType.StoredProcedure, new DbParameter[0]);
        }

        public Product GetProduct(int productId)
        {
            return Db.Map<Product>("usp_Product_GetProductDetails", CommandType.StoredProcedure, new DbParameter[] { Db.CreateParameter("ProductId", productId) });
        }

        public void UpdateProduct(Product product)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Db.ExecuteNonQuery("usp_Product_UpdateProduct", CommandType.StoredProcedure,
                    new DbParameter[] { 
                               Db.CreateParameter("ProductId", product.ProductId),
                               Db.CreateParameter("ProductName", product.ProductName),
                               Db.CreateParameter("Description", product.Description),
                               Db.CreateParameter("Quantity", product.Quantity),
                               Db.CreateParameter("UntiPrice", product.UnitPrice),
                               Db.CreateParameter("TaxId", product.TaxId)                               
                 });
                scope.Complete();
            }
        }

        public void DeactivateProduct(int productId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Db.ExecuteNonQuery("usp_Product_DeactivateProduct", CommandType.StoredProcedure,
                    new DbParameter[] { Db.CreateParameter("ProductId", productId) });
                scope.Complete();
            }
        }
    }
}
