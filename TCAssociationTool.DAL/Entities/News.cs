using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCAssociationTool.Entities
{
   public class News
    {
       public Int64 RId { get; set; }
       
       public Int64 NewsId { get; set; }

        public string Title { get; set; }

        public string NewsText { get; set; }

        public string ImageUrl { get; set; }

        public DateTime PostDate { get; set; }

        public Int64 OrderNo { get; set; }

        public Boolean IsActive { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedTime { get; set; }
    }
}
