﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCAssociationTool.Entities
{
    public class InnerPages
    {
        public Int64 RId { get; set; }

        public Int64 InnerPageId { get; set; }

        public Int64 InnerPageCategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Heading { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public Boolean IsActive { get; set; }

        public string PageTitle { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        public string Topline { get; set; }

        public string InsertedBy { get; set; }

        public DateTime InsertedTime { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedTime { get; set; }

        public List<Entities.Sponsors> lstMediaSponsors { get; set; }
        public List<Entities.Sponsors> lstSponsors { get; set; }
        public List<Entities.SponsorCategories> lstSponsorCategories { get; set; }
        public List<Entities.Events> lstUpcommingEvents { get; set; }
        public List<Entities.News> lstNews { get; set; }
        public List<Entities.InnerPages> lstInnerPages { get; set; }
        public List<Entities.InnerPageCategories> lstInnerPageCategories { get; set; }
        public Entities.Members objMembers { get; set; }
        public Entities.InnerPages objCInnerPages { get; set; }
        public Entities.InnerPages objPInnerPages { get; set; }
        public Entities.InnerPages objPHInnerPages { get; set; }
        public Entities.InnerPages objPMInnerPages { get; set; }
        public Entities.InnerPages objSInnerPages { get; set; }
        public Entities.InnerPages objPhotosInnerPages { get; set; }
        public Entities.InnerPages objVideoInnerPages { get; set; }


        public List<Entities.DonationCategories> lstDonationCategories { get; set; }
        public List<Entities.CommitteeCategories> lstCommitteeCategories { get; set; }
    }
}
