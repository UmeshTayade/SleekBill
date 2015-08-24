using System;
using System.Data;
using System.Data.Common;
using System.Configuration;

namespace DataObjectHelpers
{
    public interface IConnectionFactory
    {
        IDbConnection getNewConnection();
    }

    public class ConnectionFactory : IConnectionFactory
    {
        public static readonly IConnectionFactory DefConectionFactory = new ConnectionFactory();
        private static readonly string ERR_CantOpenConnection = "Error preparing an open connection.";

        public IDbConnection getNewConnection()
        {
            IDbConnection connection = null;
            Exception exception;
            IDbConnection connection2;
            try
            {
                connection = ProviderFactory.DefProviderFactory.GetProviderFactory().CreateConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["PAConnectionString"].ConnectionString;
                try
                {
                    connection.Open();
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    throw exception;
                }
                connection2 = connection;
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw new Exception(ERR_CantOpenConnection, exception);
            }
            return connection2;
        }

        public static void safeClose(IDbConnection con)
        {
            try
            {
                if ((null != con) && (ConnectionState.Open == con.State))
                {
                    con.Close();
                }
            }
            catch
            {
            }
        }
    }
}
