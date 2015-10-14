using DataObjects.Interfaces;

namespace DataObjects
{
    public abstract class DataFactory
    {
        protected DataFactory()
        {
        }

        public abstract IClientDB clientDB { get; }
        public abstract IMasterDB masterDB { get; }
        public abstract ICompanyDB companyDB { get; }
        public abstract IProductDB productDB { get; }
        public abstract IInvoiceDB invoiceDB { get; }
    }
}
