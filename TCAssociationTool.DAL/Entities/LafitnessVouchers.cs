using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAssociationTool.Entities
{
   public class LafitnessVouchers
    {
        public Int64 RId { get; set; }

        public Int64 Id { get; set; }

        public string voucher { get; set; }

        public string member_id { get; set; }

        public DateTime assigned_on { get; set; }

        public bool status2 { get; set; }

        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime expire_date { get; set; }


    }
}
