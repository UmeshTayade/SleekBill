using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;

namespace DataObjects.Interfaces
{
    public interface ICompanyDB
    {
        int AddCompany(Company company);
        List<Company> GetAllCompany();
        Company GetCompany(int companyId);
        void UpdateCompany(Company company);
    }
}
