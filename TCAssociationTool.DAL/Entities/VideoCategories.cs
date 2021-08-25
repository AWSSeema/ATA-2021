using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCAssociationTool.Entities
{
 public  class VideoCategories
    {

        public Int64 RId { get; set; }

        public Int64 VideoCategoryId { get; set; }

        public string CategoryName { get; set; }

        public Int64 Year { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedTime { get; set; }

        public Int64 VideosCount { get; set; }
    }
}
