using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;

namespace DataObjects.Interfaces
{
    public interface IMasterDB
    {
        List<State> GetAllStates();
    }
}
