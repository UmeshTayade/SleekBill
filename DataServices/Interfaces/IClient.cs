using System.Data;
using System.ComponentModel;
using BusinessObjects;
using System.Collections.Generic;

namespace DataServices.Interfaces
{
    public interface IClient
    {
        [DataObjectMethod(DataObjectMethodType.Insert)]
        int AddClient(Client client);
        List<Client> GetAllClients();
        [DataObjectMethod(DataObjectMethodType.Select)]
        Client GetClient(int ClientId);
        [DataObjectMethod(DataObjectMethodType.Update)]
        void UpdateClient(Client client);
    }
}
