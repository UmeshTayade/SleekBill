using DataObjects.Interfaces;

namespace DataObjects.ADO.Net
{
    class ADONETDaoFactory : DataFactory
    {
        public override IClientDB clientDB
        {
            get { return new ClientDBHelper(); }
        }
    }
}
