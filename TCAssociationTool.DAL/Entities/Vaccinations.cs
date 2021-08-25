using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAssociationTool.Entities
{
  public  class Vaccinations
    {
        public Int64 RId { get; set; }

        public Int64 Id { get; set; }

        public string membername { get; set; }

        public string position { get; set; }

        public string relation { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }

        public string phone { get; set; }

        public string age { get; set; }

        public DateTime datesent { get; set; }

        public string comments { get; set; }
 
    }
}
