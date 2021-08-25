using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCAssociationTool.Entities
{
   public class WebsiteBanners
    {
       public Int64 RId { get; set; }

       public Int64 WebsiteBannerId { get; set; }

       public string BannerTitle { get; set; }

       public string BannerUrl { get; set; }

       public string RedirectUrl { get; set; }

       public Int64 OrderNo { get; set; }

       public string Target { get; set; }

       public Boolean IsActive { get; set; }

       public string UpdatedBy { get; set; }

       public DateTime UpdatedTime { get; set; }
    }
}
