using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAssociationTool.Entities
{
    public class Greetings
    {
        public Int64 RId { get; set; }

        public Int64 id { get; set; }

        public string title { get; set; }

        public string imgsrc { get; set; }

        public Int64 imgwidth { get; set; }

        public Int64 top_padding { get; set; }
        public string link { get; set; }

        public Boolean status2 { get; set; }
        public string target { get; set; }
        public DateTime date_modified { get; set; }
        public DateTime lastdate { get; set; }
    }
}
