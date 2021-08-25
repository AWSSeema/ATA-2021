using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;

namespace TCAssociationTool.Models
{
    
    public class Rss
    {
        public string Link { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<Rss> IRss { get; set; }
    }
}