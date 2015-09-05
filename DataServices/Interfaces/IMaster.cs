using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;

namespace DataServices.Interfaces
{
    public interface IMaster
    {
        List<State> GetAllStates();
    }
}
