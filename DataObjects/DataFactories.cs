using DataObjects.ADO.Net;

namespace DataObjects
{
    public class DataFactories
    {
        /// <summary>
        /// Gets a db specific factory 
        /// </summary>
        /// <param name="dataProvider">Database provider.</param>
        /// <returns>Data access object factory.</returns>
        public static DataFactory GetFactory(string dataProvider)
        {
            // Return the requested DaoFactory
            //TODO: Need to configure these providers in web.config
            //TODO: Check if custom providers can be configured in web.config
            switch (dataProvider)
            {
                case "System.Data.SqlClient":
                    return new ADONETDaoFactory();
                // Just in case: the Design Pattern Framework always has something available.
                default:
                    return new ADONETDaoFactory();
            }            
        }
    }
}
