using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAssociationTool.Entities
{
  public  class CulturalLocations
    {
        public Int64 RId { get; set; }
        public Int64 Id { get; set; }

        public String location { get; set; }
        public String adminemail { get; set; }

        public DateTime datemodified { get; set; }
        public bool status2 { get; set; }
        public decimal amount { get; set; }

        public DateTime lastdate { get; set; }
        public DateTime eventdate { get; set; }

        public String address { get; set; }
        public String contact_name { get; set; }
        public String contact_phone { get; set; }
      
        
    }
}
