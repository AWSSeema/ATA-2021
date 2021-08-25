using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCAssociationTool.Entities
{
    public class AppInfo
    {
        public Int64 AppInfoId { get; set; }

        public string SiteName { get; set; }

        public string CompanyAddress { get; set; }

        public string CompanyWebSite { get; set; }

        public string CompanyEmail { get; set; }

        public string CompanyPhone { get; set; }

        public string PresidentEmail { get; set; }

        public string PresidentPhone { get; set; }

        public string SecretaryEmail { get; set; }

        public string SecretaryPhone { get; set; }

        public string CustomerCareNumber { get; set; }

        public string TollFreeNumber { get; set; }

        public string FacebookUrl { get; set; }

        public string TwitterUrl { get; set; }

        public string YoutubeUrl { get; set; }

        public string LinkedInUrl { get; set; }
        public string GooglePlus { get; set; }

        public string SupportEmail { get; set; }

        public string EnqueryEmail { get; set; }

        public string PageTitle { get; set; }

         public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        public string Topline { get; set; }
        public Int64 PageItems { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedTime { get; set; }

        public List<WebsiteBanners> lstWebsiteBanners { get; set; }

        public List<News> lstNews { get; set; }

        public InnerPages objInnerPages { get; set; }

        public List<Sponsors> lstSponsors { get; set; }

        public List<SponsorCategories> lstSponsorCategories { get; set; }

        public List<Events> lstEvents { get; set; }
       
    }
}
