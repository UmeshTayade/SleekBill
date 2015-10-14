using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataObjects.Interfaces;
using DataObjects;
using DataServices.Interfaces;

namespace DataServices
{
    public class MasterService : IMaster
    {
        private readonly IMasterDB masterDBObj = DBManager.MasterDB;

        public List<State> GetAllStates()
        {
            return this.masterDBObj.GetAllStates();
        }

        public int AddTax(Tax tax)
        {
            return this.masterDBObj.AddTax(tax);
        }

        public List<Tax> GetAllTaxes()
        {
            return this.masterDBObj.GetAllTaxes();
        }

        public Tax GetTax(int taxId)
        {
            return this.masterDBObj.GetTax(taxId);
        }

        public void UpdateTax(Tax tax)
        {
            this.masterDBObj.UpdateTax(tax);
        }

        public void DeactivateTax(int taxId)
        {
            this.masterDBObj.DeactivateTax(taxId);
        }

        public List<PaymentTerms> GetAllPaymentTerms()
        {
            return this.masterDBObj.GetAllPaymentTerms();
        }
    }
}
