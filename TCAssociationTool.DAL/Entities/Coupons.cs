using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAssociationTool.Entities
{
 public  class Coupons
    {

        public Int64 Rid { get; set; }
        public Int64 Id { get; set; }
        public string code { get; set; }
        public DateTime expirydate { get; set; }
        public bool status2 { get; set; }
        public string membership_type { get; set; }
        public Decimal discount { get; set; }
        public DateTime datemodified { get; set; }

        public String Eexpirydate { get; set; }

    }
}

