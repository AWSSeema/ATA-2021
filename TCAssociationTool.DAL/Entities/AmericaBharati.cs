using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAssociationTool.Entities
{
   public class AmericaBharati
    {
        public Int64 RId { get; set; }

        public Int64 id { get; set; }

        public string heading { get; set; }

        public string description { get; set; }
        public DateTime datemodified { get; set; }

        public string pagetitle { get; set; }

        public string metakeywords { get; set; }

        public Boolean status2 { get; set; }

        public string metadesc { get; set; }

        public string pageurl { get; set; }

        public string filename { get; set; }
    }
}
