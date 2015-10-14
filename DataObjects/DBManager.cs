using DataObjectHelpers;
using DataObjects.Interfaces;

namespace DataObjects
{
    public static class DBManager
    {

        private static readonly string dataProvider = ProviderFactory.DefProviderFactory.TheProvider;
        private static readonly DataFactory factory = DataFactories.GetFactory(dataProvider);

        public static IClientDB ClientDB
        {
            get { return factory.clientDB; }
        }

        public static IMasterDB MasterDB
        {
            get
            {
                return factory.masterDB;
            }
        }

        public static ICompanyDB CompanyDB
        {
            get
            {
                return factory.companyDB;
            }
        }

        public static IProductDB ProductDB
        {
            get
            {
                return factory.productDB;
            }
        }

        public static IInvoiceDB InvoiceDB
        {
            get
            {
                return factory.invoiceDB;
            }
        }
    }
}
