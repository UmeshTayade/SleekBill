using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
    public class PaymentTerms
    {
        public int PaymentTermId { get; set; }
        public string PaymentTermName { get; set; }
        public int PaymentTerm { get; set; }
        public bool Status { get; set; }
    }
}
