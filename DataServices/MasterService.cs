using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataObjects.Interfaces;
using DataObjects;
using DataServices.Interfaces;

namespace DataServices
{
    public class MasterService : IMaster
    {
        private readonly IMasterDB masterDBObj = DBManager.MasterDB;

        public List<State> GetAllStates()
        {
            return this.masterDBObj.GetAllStates();
        }
    }
}
