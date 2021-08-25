using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAssociationTool.Entities
{
    public class Womensday
    {
        public Int64 Id { get; set; }
        public Int64 RId { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }

        public decimal amount { get; set; }
        public DateTime datesent { get; set; }

        public string address { get; set; }
        public string location { get; set; }
        public string comments { get; set; }
        public string city { get; set; }
        public string state { get; set; }

        public Int64 event_id { get; set; }



    }
    public class WomensdayLocations
    {
        public Int64 Id { get; set; }
        public Int64 RId { get; set; }
        public string location { get; set; }
        public string adminemail { get; set; }

        public decimal amount { get; set; }
        public DateTime datemodified { get; set; }

        public bool status2 { get; set; }
        public Int64 event_details { get; set; }

   
    }
}
