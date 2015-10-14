using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataObjectHelpers;
using System.Data;
using System.Data.Common;
using DataObjects.Interfaces;
using System.Transactions;

namespace DataObjects.ADO.Net
{
    public class MasterDBHelper : IMasterDB
    {
        public List<State> GetAllStates()
        {
            return Db.MapReader<State>("usp_State_GetAllStates", CommandType.StoredProcedure, new DbParameter[0]);
        }

        public int AddTax(Tax tax)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                DbParameter parameter = null;
                parameter = Db.CreateParameter("TaxId", DbType.Int32, 8);
                parameter.Direction = ParameterDirection.Output;
                Db.ExecuteNonQuery("usp_Tax_InsertTaxDetails", CommandType.StoredProcedure,
                    new DbParameter[] { 
                               parameter, 
                               Db.CreateParameter("TaxName", tax.TaxName),
                               Db.CreateParameter("TaxPercentage", tax.TaxPercentage),
                               Db.CreateParameter("Status", tax.Status),
                               
                 });
                scope.Complete();
                return (int)parameter.Value;
            }
        }

        public List<Tax> GetAllTaxes()
        {
            return Db.MapReader<Tax>("usp_Tax_GetAllTaxes", CommandType.StoredProcedure, new DbParameter[0]);
        }

        public Tax GetTax(int taxId)
        {
            return Db.Map<Tax>("usp_Tax_GetTaxDetails", CommandType.StoredProcedure, new DbParameter[] { Db.CreateParameter("TaxId", taxId) });
        }

        public void UpdateTax(Tax tax)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Db.ExecuteNonQuery("usp_Tax_UpdateTax", CommandType.StoredProcedure,
                    new DbParameter[] { 
                               Db.CreateParameter("TaxId", tax.TaxId),
                               Db.CreateParameter("TaxName", tax.TaxName),
                               Db.CreateParameter("TaxPercentage", tax.TaxPercentage)
                               
                 });
                scope.Complete();
            }
        }

        public void DeactivateTax(int taxId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Db.ExecuteNonQuery("usp_Tax_DeactivateTax", CommandType.StoredProcedure,
                    new DbParameter[] { Db.CreateParameter("TaxId", taxId) });
                scope.Complete();
            }
        }

        public List<PaymentTerms> GetAllPaymentTerms()
        {
            return Db.MapReader<PaymentTerms>("usp_PaymentTerms_GetAllPaymentTerms", CommandType.StoredProcedure, new DbParameter[0]);
        }
    }
}
