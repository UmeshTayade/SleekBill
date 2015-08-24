using System.Configuration;
using System.Data.Common;

namespace DataObjectHelpers
{
    public sealed class ProviderFactory
    {
        private string __theProvider = ConfigurationManager.ConnectionStrings["PAConnectionString"].ProviderName;
        public static readonly ProviderFactory DefProviderFactory = new ProviderFactory();

        public DbProviderFactory GetProviderFactory()
        {
            return this.GetProviderFactory(this.__theProvider);
        }

        public DbProviderFactory GetProviderFactory(string providerName)
        {
            return DbProviderFactories.GetFactory(providerName);
        }

        public string TheProvider
        {
            get
            {
                return this.__theProvider;
            }
            set
            {
                this.__theProvider = value;
            }
        }
    }
}
