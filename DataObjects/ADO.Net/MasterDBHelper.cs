using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using DataObjectHelpers;
using System.Data;
using System.Data.Common;
using DataObjects.Interfaces;

namespace DataObjects.ADO.Net
{
    public class MasterDBHelper : IMasterDB
    {
        public List<State> GetAllStates()
        {
            return Db.MapReader<State>("usp_State_GetAllStates", CommandType.StoredProcedure, new DbParameter[0]);
        }
    }
}
