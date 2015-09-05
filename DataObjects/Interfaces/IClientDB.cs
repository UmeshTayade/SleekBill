using System.Data;
using BusinessObjects;
using System.Collections.Generic;

namespace DataObjects.Interfaces
{
    public interface IClientDB
    {
        int AddClient(Client client);
        List<Client> GetAllClients();
        Client GetClient(int clientId);
        void UpdateClient(Client client);
        void DeactivateClient(int clientId);
    }
}
