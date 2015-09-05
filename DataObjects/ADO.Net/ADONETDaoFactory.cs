using DataObjects.Interfaces;

namespace DataObjects.ADO.Net
{
    public class ADONETDaoFactory : DataFactory
    {
        public override IClientDB clientDB
        {
            get { return new ClientDBHelper(); }
        }

        public override IMasterDB masterDB
        {
            get { return new MasterDBHelper(); }
        }
    }
}
