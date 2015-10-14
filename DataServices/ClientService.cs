using System.Data;
using DataObjects;
using DataObjects.Interfaces;
using DataServices.Interfaces;
using BusinessObjects;
using System.Collections.Generic;

namespace DataServices
{
    public class ClientService : IClient
    {
        private readonly IClientDB clientDBObj = DBManager.ClientDB;

        public int AddClient(Client client)
        {
            return this.clientDBObj.AddClient(client);
        }

        public List<Client> GetAllClients()
        {
            return this.clientDBObj.GetAllClients();
        }

        public Client GetClient(int clientId)
        {
            return this.clientDBObj.GetClient(clientId);
        }

        public void UpdateClient(Client client)
        {
            this.clientDBObj.UpdateClient(client);
        }

        public void DeactivateClient(int clientId)
        {
            this.clientDBObj.DeactivateClient(clientId);
        }
    }
}
