using System.Data;
using DataObjectHelpers;
using DataObjects.Interfaces;
using BusinessObjects;
using System.Collections.Generic;
using System.Data.Common;
using System.Transactions;

namespace DataObjects.ADO.Net
{
    public class ClientDBHelper : IClientDB
    {
        public int AddClient(Client client)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                DbParameter parameter = null;
                parameter = Db.CreateParameter("ClientId", DbType.Int32, 8);
                parameter.Direction = ParameterDirection.Output;
                Db.ExecuteNonQuery("usp_Client_InsertClientDetails", CommandType.StoredProcedure, 
                    new DbParameter[] { 
                               parameter, 
                               Db.CreateParameter("ClientName", client.ClientName),
                               Db.CreateParameter("ContactName", client.ContactName),
                               Db.CreateParameter("Phone", client.Phone),
                               Db.CreateParameter("Email", client.Email),
                               Db.CreateParameter("TIN", client.TIN),
                               Db.CreateParameter("PrivateClientDetails", client.PrivateClientDetails),
                               Db.CreateParameter("OtherClientDetails", client.OtherClientDetails),
                               Db.CreateParameter("BillingAddress", client.BillingAddress),
                               Db.CreateParameter("City", client.City),
                               Db.CreateParameter("StateCode", client.StateCode),
                               Db.CreateParameter("Zip", client.Zip),
                               Db.CreateParameter("Country", client.Country),
                               Db.CreateParameter("ShipToDifferentAddress", client.ShipToDifferentAddress),
                               Db.CreateParameter("ShippingAddress", client.ShippingAddress),
                               Db.CreateParameter("ShippingCity", client.ShippingCity),
                               Db.CreateParameter("ShippingStateCode", client.ShippingStateCode),
                               Db.CreateParameter("ShippingZip", client.ShippingZip),
                               Db.CreateParameter("ShippingCountry", client.ShippingCountry)                               
                 });
                scope.Complete();
                return (int)parameter.Value;
            }
        }
        
        public List<Client> GetAllClients()
        {
            return Db.MapReader<Client>("usp_Client_GetAllClients", CommandType.StoredProcedure, new DbParameter[0]);
        }

        public Client GetClient(int clientId)
        {
            return Db.Map<Client>("usp_Client_GetClientDetails", CommandType.StoredProcedure, new DbParameter[] { Db.CreateParameter("ClientId", clientId) });
        }

        public void UpdateClient(Client client)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Db.ExecuteNonQuery("usp_Client_UpdateClient", CommandType.StoredProcedure, 
                    new DbParameter[] { 
                               Db.CreateParameter("ClientId", client.ClientId),
                               Db.CreateParameter("ClientName", client.ClientName),
                               Db.CreateParameter("ContactName", client.ContactName),
                               Db.CreateParameter("Phone", client.Phone),
                               Db.CreateParameter("Email", client.Email),
                               Db.CreateParameter("TIN", client.TIN),
                               Db.CreateParameter("PrivateClientDetails", client.PrivateClientDetails),
                               Db.CreateParameter("OtherClientDetails", client.OtherClientDetails),
                               Db.CreateParameter("BillingAddress", client.BillingAddress),
                               Db.CreateParameter("City", client.City),
                               Db.CreateParameter("StateCode", client.StateCode),
                               Db.CreateParameter("Zip", client.Zip),
                               Db.CreateParameter("Country", client.Country),
                               Db.CreateParameter("ShipToDifferentAddress", client.ShipToDifferentAddress),
                               Db.CreateParameter("ShippingAddress", client.ShippingAddress),
                               Db.CreateParameter("ShippingCity", client.ShippingCity),
                               Db.CreateParameter("ShippingStateCode", client.ShippingStateCode),
                               Db.CreateParameter("ShippingZip", client.ShippingZip),
                               Db.CreateParameter("ShippingCountry", client.ShippingCountry),
                               Db.CreateParameter("Status", client.Status),
                               
                 });
                scope.Complete();
            }
        }

        public void DeactivateClient(int clientId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Db.ExecuteNonQuery("usp_Client_DeactivateClient", CommandType.StoredProcedure,
                    new DbParameter[] { Db.CreateParameter("ClientId", clientId)});
                scope.Complete();
            }
        }

    }
}
