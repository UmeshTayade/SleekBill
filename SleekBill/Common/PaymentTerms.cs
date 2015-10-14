using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sleek_Bill.Common
{
    public enum ePaymentTerms
    {
        Default = 0,
        OnSpecifiedDate = 1,
        OnReceipt = 2,
        Net10 = 10,
        Net20 = 20,
        Net30 = 30,
        Net45 = 45,
        Net60 = 60        
    }
}
