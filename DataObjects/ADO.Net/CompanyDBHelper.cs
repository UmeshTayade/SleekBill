using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObjects.Interfaces;
using BusinessObjects;
using System.Transactions;
using System.Data.Common;
using System.Data;
using DataObjectHelpers;

namespace DataObjects.ADO.Net
{
    public class CompanyDBHelper : ICompanyDB
    {
        public int AddCompany(Company company)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                DbParameter parameter = null;
                parameter = Db.CreateParameter("CompanyId", DbType.Int32, 8);
                parameter.Direction = ParameterDirection.Output;
                Db.ExecuteNonQuery("usp_Company_InsertCompanyDetails", CommandType.StoredProcedure,
                    new DbParameter[] { 
                               parameter, 
                               Db.CreateParameter("CompanyName", company.CompanyName),
                               Db.CreateParameter("Country", company.Country),
                               Db.CreateParameter("Address", company.Address),
                               Db.CreateParameter("City", company.City),
                               Db.CreateParameter("State", company.State),
                               Db.CreateParameter("CompanyPhone", company.CompanyPhone),
                               Db.CreateParameter("Email", company.Email),
                               Db.CreateParameter("Website", company.Website),
                               Db.CreateParameter("TIN", company.TIN),
                               Db.CreateParameter("ServiceTaxNo", company.ServiceTaxNo),
                               Db.CreateParameter("AdditionalDetails", company.AdditionalDetails),
                               Db.CreateParameter("PAN", company.PAN),
                               Db.CreateParameter("Currency", company.Currency),
                               Db.CreateParameter("Logo", company.Logo),
                               Db.CreateParameter("Status", company.Status)
                 });
                scope.Complete();

                return (int)parameter.Value;
            }
        }

        public List<Company> GetAllCompany()
        {
            return Db.MapReader<Company>("usp_Company_GetAllCompany", CommandType.StoredProcedure, new DbParameter[0]);
        }

        public Company GetCompany(int companyId)
        {
            return Db.Map<Company>("usp_Company_GetCompanyDetails", CommandType.StoredProcedure, new DbParameter[] { Db.CreateParameter("CompanyId", companyId) });
        }

        public void UpdateCompany(Company company)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Db.ExecuteNonQuery("usp_Company_UpdateCompany", CommandType.StoredProcedure,
                    new DbParameter[] { 
                               Db.CreateParameter("CompanyId", company.CompanyId),
                               Db.CreateParameter("CompanyName", company.CompanyName),
                               Db.CreateParameter("Country", company.Country),
                               Db.CreateParameter("Address", company.Address),
                               Db.CreateParameter("City", company.City),
                               Db.CreateParameter("State", company.State),
                               Db.CreateParameter("CompanyPhone", company.CompanyPhone),
                               Db.CreateParameter("Email", company.Email),
                               Db.CreateParameter("Website", company.Website),
                               Db.CreateParameter("TIN", company.TIN),
                               Db.CreateParameter("ServiceTaxNo", company.ServiceTaxNo),
                               Db.CreateParameter("AdditionalDetails", company.AdditionalDetails),
                               Db.CreateParameter("PAN", company.PAN),
                               Db.CreateParameter("Currency", company.Currency),
                               Db.CreateParameter("Logo", company.Logo)
                               
                 });
                scope.Complete();
            }
        }
    }
}
