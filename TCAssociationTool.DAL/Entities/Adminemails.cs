using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAssociationTool.Entities
{
    public class Adminemails
    {
        public Int64 RId { get; set; }

        public Int64 Id { get; set; }

        public string name { get; set; }

        public string designation { get; set; }

        public string email { get; set; }

        public bool isdonation { get; set; }

        public bool ismembership { get; set; }
        public DateTime datemodified { get; set; }
        public bool defaultmembership { get; set; }
        public bool defaultdonation { get; set; }


    }
}
