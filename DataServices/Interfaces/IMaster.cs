using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using System.ComponentModel;

namespace DataServices.Interfaces
{
    public interface IMaster
    {
        List<State> GetAllStates();
        [DataObjectMethod(DataObjectMethodType.Insert)]
        int AddTax(Tax tax);
        List<Tax> GetAllTaxes();
        [DataObjectMethod(DataObjectMethodType.Select)]
        Tax GetTax(int taxId);
        [DataObjectMethod(DataObjectMethodType.Update)]
        void UpdateTax(Tax tax);
        void DeactivateTax(int taxId);
        List<PaymentTerms> GetAllPaymentTerms();
    }
}
