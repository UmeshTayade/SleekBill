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
    public class CompanyService : ICompany
    {
        private readonly ICompanyDB companyDBObj = DBManager.CompanyDB;

        public int AddCompany(Company company)
        {
            return this.companyDBObj.AddCompany(company);
        }

        public List<Company> GetAllCompany()
        {
            return this.companyDBObj.GetAllCompany();
        }

        public Company GetCompany(int companyId)
        {
            return this.companyDBObj.GetCompany(companyId);
        }

        public void UpdateCompany(Company company)
        {
            this.companyDBObj.UpdateCompany(company);
        }
    }
}
