using DataObjects.Interfaces;

namespace DataObjects.ADO.Net
{
    public class ADONETDaoFactory : DataFactory
    {
        public override IClientDB clientDB
        {
            get { return new ClientDBHelper(); }
        }

        public override IMasterDB masterDB
        {
            get { return new MasterDBHelper(); }
        }

        public override ICompanyDB companyDB
        {
            get { return new CompanyDBHelper(); }
        }

        public override IProductDB productDB
        {
            get { return new ProductDBHelper(); }
        }

        public override IInvoiceDB invoiceDB
        {
            get { return new InvoiceDBHelper(); }
        }
    }
}
