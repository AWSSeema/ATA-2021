using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAssociationTool.Entities
{
    public class Feedbacks
    {
        public Int64 RId {get;set;}
        public Int64 Id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string message { get; set; }
        public DateTime datesent { get; set; }

        public string admincomments { get; set; }

    }
}
