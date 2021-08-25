using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCAssociationTool.Entities
{
    public class PaymentMethods
    {
        private Int64 _PaymentMethodId;

        public Int64 PaymentMethodId
        {
            get { return _PaymentMethodId; }
            set { _PaymentMethodId = value; }
        }
        private string _PaymentMethod;

        public string PaymentMethod
        {
            get { return _PaymentMethod; }
            set { _PaymentMethod = value; }
        }
    }
}
