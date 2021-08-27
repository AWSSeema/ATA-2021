using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAssociationTool.Entities
{
   public class ATAMessages
    {
        public Int64 RId { get; set; }

        public Int64 id { get; set; }

        public string heading { get; set; }

        public string attachment { get; set; }

        public string shortdesc { get; set; }

        public string pageurl { get; set; }
        public string target { get; set; }
        public Int32 orderno { get; set; }

        public bool status2 { get; set; }
        public DateTime datemodified { get; set; }
        public DateTime pdate { get; set; }

    }
}
