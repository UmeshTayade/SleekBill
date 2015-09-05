using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
    public class State
    {
        public State()
        {

        }

        public State(string stateCode, string stateName)
        {
            this.StateCode = stateCode;
            this.StateName = stateName;
        }

        public string StateCode { get; set; }
        public string StateName { get; set; }
    }
}
