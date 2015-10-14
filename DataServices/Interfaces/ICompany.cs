using System.Collections.Generic;
using System.ComponentModel;
using BusinessObjects;

namespace DataServices.Interfaces
{
    public interface ICompany
    {
        [DataObjectMethod(DataObjectMethodType.Insert)]
        int AddCompany(Company company);
        List<Company> GetAllCompany();
        [DataObjectMethod(DataObjectMethodType.Select)]
        Company GetCompany(int companyId);
        [DataObjectMethod(DataObjectMethodType.Update)]
        void UpdateCompany(Company company);     
    }
}
