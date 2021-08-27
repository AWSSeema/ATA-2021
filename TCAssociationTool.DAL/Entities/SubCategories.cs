using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAssociationTool.Entities
{
   public class SubCategories
    {
        public Int64 RId { get; set; }

        public Int64 id { get; set; }

        public string scname { get; set; }
        public string catname { get; set; }
        public Int64 catid { get; set; }
        public Int32 orderno { get; set; }
        public string pageurl { get; set; }
        public DateTime datemodified { get; set; }
    }
}
