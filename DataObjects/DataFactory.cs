using DataObjects.Interfaces;

namespace DataObjects
{
    public abstract class DataFactory
    {
        protected DataFactory()
        {
        }

        public abstract IClientDB clientDB { get; }
        public abstract IMasterDB masterDB { get; }
    }
}
