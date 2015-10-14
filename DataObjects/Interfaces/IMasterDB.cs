using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;

namespace DataObjects.Interfaces
{
    public interface IMasterDB
    {
        List<State> GetAllStates();
        int AddTax(Tax tax);
        List<Tax> GetAllTaxes();
        Tax GetTax(int taxId);
        void UpdateTax(Tax tax);
        void DeactivateTax(int taxId);
        List<PaymentTerms> GetAllPaymentTerms();
    }
}
