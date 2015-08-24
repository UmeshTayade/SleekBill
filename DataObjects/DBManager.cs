using DataObjectHelpers;
using DataObjects.Interfaces;

namespace DataObjects
{
    public static class DBManager
    {

        private static readonly string dataProvider = ProviderFactory.DefProviderFactory.TheProvider;
        private static readonly DataFactory factory = DataFactories.GetFactory(dataProvider);

        public static IClientDB clientDB
        {
            get { return factory.clientDB; }
        }
    }
}
