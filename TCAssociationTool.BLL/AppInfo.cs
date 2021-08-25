using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Xml;


namespace TCAssociationTool.BLL
{
    public class AppInfo
    {
        TCAssociationTool.DAL.AppInfo _AppInfo = new TCAssociationTool.DAL.AppInfo();


        #region Admin

        #region Methods

        public Int64 UpdateAppInfoDetails(Entities.AppInfo objAppInfo)
        {
            Int64 _status = 0;
            _status = _AppInfo.UpdateAppInfoDetails(objAppInfo);

            return _status;
        }

        public Int64 GetAppInfoEmail(ref string CompanyEmail)
        {
            Int64 _status = 0;
            _status = _AppInfo.GetAppInfoEmail(ref CompanyEmail);
            return _status;
        }

        #endregion

        #region Entities filling

        public Entities.AppInfo GetAppInfoDetails(ref int Status)
        {
            DataTable dt = _AppInfo.GetAppInfoDetails(ref Status);
            Entities.AppInfo objAppInfo = new Entities.AppInfo();

            if (Status == 1 && dt.Rows.Count == 1)
            {
                objAppInfo.AppInfoId = Convert.ToInt64(dt.Rows[0]["AppInfoId"]);
                objAppInfo.SiteName = dt.Rows[0]["SiteName"].ToString();
                objAppInfo.CompanyAddress = (dt.Rows[0]["CompanyAddress"] != DBNull.Value ? dt.Rows[0]["CompanyAddress"].ToString() : null);
                objAppInfo.CompanyWebSite = (dt.Rows[0]["CompanyWebSite"] != DBNull.Value ? dt.Rows[0]["CompanyWebSite"].ToString() : null);
                objAppInfo.CompanyEmail = (dt.Rows[0]["CompanyEmail"] != DBNull.Value ? dt.Rows[0]["CompanyEmail"].ToString() : null);
                objAppInfo.CompanyPhone = (dt.Rows[0]["CompanyPhone"] != DBNull.Value ? dt.Rows[0]["CompanyPhone"].ToString() : null);
                objAppInfo.PresidentEmail = (dt.Rows[0]["PresidentEmail"] != DBNull.Value ? dt.Rows[0]["PresidentEmail"].ToString() : null);
                objAppInfo.PresidentPhone = (dt.Rows[0]["PresidentPhone"] != DBNull.Value ? dt.Rows[0]["PresidentPhone"].ToString() : null);
                objAppInfo.SecretaryEmail = (dt.Rows[0]["SecretaryEmail"] != DBNull.Value ? dt.Rows[0]["SecretaryEmail"].ToString() : null);
                objAppInfo.SecretaryPhone = (dt.Rows[0]["SecretaryPhone"] != DBNull.Value ? dt.Rows[0]["SecretaryPhone"].ToString() : null);
                objAppInfo.CustomerCareNumber = (dt.Rows[0]["CustomerCareNumber"] != DBNull.Value ? dt.Rows[0]["CustomerCareNumber"].ToString() : null);
                objAppInfo.TollFreeNumber = (dt.Rows[0]["TollFreeNumber"] != DBNull.Value ? dt.Rows[0]["TollFreeNumber"].ToString() : null);
                objAppInfo.FacebookUrl = (dt.Rows[0]["FacebookUrl"] != DBNull.Value ? dt.Rows[0]["FacebookUrl"].ToString() : null);
                objAppInfo.TwitterUrl = (dt.Rows[0]["TwitterUrl"] != DBNull.Value ? dt.Rows[0]["TwitterUrl"].ToString() : null);
                objAppInfo.YoutubeUrl = (dt.Rows[0]["YoutubeUrl"] != DBNull.Value ? dt.Rows[0]["YoutubeUrl"].ToString() : null);
                objAppInfo.SupportEmail = (dt.Rows[0]["SupportEmail"] != DBNull.Value ? dt.Rows[0]["SupportEmail"].ToString() : null);
                objAppInfo.EnqueryEmail = (dt.Rows[0]["EnqueryEmail"] != DBNull.Value ? dt.Rows[0]["EnqueryEmail"].ToString() : null);
                objAppInfo.PageTitle = (dt.Rows[0]["PageTitle"] != DBNull.Value ? dt.Rows[0]["PageTitle"].ToString() : null);
                objAppInfo.MetaDescription = (dt.Rows[0]["MetaDescription"] != DBNull.Value ? dt.Rows[0]["MetaDescription"].ToString() : null);
                objAppInfo.MetaKeywords = (dt.Rows[0]["MetaKeywords"] != DBNull.Value ? dt.Rows[0]["MetaKeywords"].ToString() : null);
                objAppInfo.Topline = (dt.Rows[0]["Topline"] != DBNull.Value ? dt.Rows[0]["Topline"].ToString() : null);
                objAppInfo.PageItems = (dt.Rows[0]["PageItems"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["PageItems"].ToString()) : 0);
                objAppInfo.UpdatedBy = (dt.Rows[0]["UpdatedBy"] != DBNull.Value ? dt.Rows[0]["UpdatedBy"].ToString() : null);
                objAppInfo.UpdatedTime = Convert.ToDateTime(dt.Rows[0]["UpdatedTime"]);

            }
            return objAppInfo;
        }

        #endregion

        #endregion

        #region Front End

        public void FEGetListInitialFlyer(ref Entities.Flyers objFlyers, ref int status)
        {
            DataSet ds = _AppInfo.FEGetListInitialFlyer(ref status);
            // About Us	
            if (ds.Tables[0].Rows.Count != 0)
            {
                objFlyers.FlyerId = Convert.ToInt64(ds.Tables[0].Rows[0]["FlyerId"]);
                objFlyers.FlyerUrl = ds.Tables[0].Rows[0]["FlyerUrl"].ToString();
                objFlyers.RedirectUrl = ds.Tables[0].Rows[0]["RedirectUrl"].ToString();
                objFlyers.PageContent = ds.Tables[0].Rows[0]["PageContent"].ToString();
            }

        }

        public void FEGetListInitialLoad(
           ref List<Entities.News> lstNews,
           ref Entities.InnerPages objPInnerPages,
           ref List<Entities.WebsiteBanners> lstWebsiteBanners,
           ref List<Entities.Events> lstUpcommingEvents,
           ref Entities.InnerPages objPHInnerPages,
           ref List<Entities.Events> lstLatestEvents,
           ref List<Entities.Sponsors> lstSponsors,
           ref List<Entities.Sponsors> lstMediaSponsors,
           ref List<Entities.Videos> lstVideos,
           ref Entities.AppInfo objAppInfo,
           ref List<Entities.SponsorCategories> lstSponsorCategories,
           ref Entities.Flyers objFlyers,
           ref Entities.InnerPages objCInnerPages,
           ref List<Entities.InnerPageCategories> lstMenuItems,
           ref List<Entities.InnerPageCategories> lstMenuItems2,
           ref List<Entities.InnerPageCategories> lstMenuItems3,
           ref List<Entities.InnerPageCategories> lstMenuItems4,
           ref List<Entities.InnerPageCategories> FooterMenuItems,
            ref Entities.InnerPages objSInnerPages,
            ref List<Entities.CommitteeCategories> lstCommitteeCategories,
           ref List<Entities.Events> lstEvents,
           ref List<Entities.Photos> lstPhotos,
           ref int status)
        {
            DataSet ds = _AppInfo.FEGetListInitialLoad(ref status);

            //News List
            if (ds.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Entities.News objNews = new Entities.News();

                    objNews.NewsId = Convert.ToInt64(dr["NewsId"]);
                    objNews.PostDate = Convert.ToDateTime(dr["PostDate"]);
                    objNews.NewsText = dr["NewsText"].ToString();
                    objNews.Title = dr["Title"].ToString();
                    objNews.ImageUrl = dr["ImageUrl"].ToString();

                    lstNews.Add(objNews);
                }
            }

            //President Message
            if (ds.Tables[1].Rows.Count == 1)
            {
                DataTable dt = ds.Tables[1];

                objPInnerPages.InnerPageId = Convert.ToInt64(dt.Rows[0]["InnerPageId"]);
                objPInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
                objPInnerPages.Description = dt.Rows[0]["Description"].ToString();
            }


            //WebsiteBanners List   
            if (ds.Tables[2].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    Entities.WebsiteBanners objWebsiteBanners = new Entities.WebsiteBanners();

                    objWebsiteBanners.WebsiteBannerId = Convert.ToInt64(dr["WebsiteBannerId"]);
                    objWebsiteBanners.BannerTitle = dr["BannerTitle"].ToString();
                    objWebsiteBanners.BannerUrl = dr["BannerUrl"].ToString();
                    objWebsiteBanners.Target = dr["Target"].ToString();
                    objWebsiteBanners.RedirectUrl = dr["RedirectUrl"].ToString();
                    objWebsiteBanners.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

                    lstWebsiteBanners.Add(objWebsiteBanners);
                }
            }

            // Upcomming Events List 
            if (ds.Tables[3].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[3].Rows)
                {
                    Entities.Events objUpcommingEvents = new Entities.Events();

                    objUpcommingEvents.EventId = Convert.ToInt64(dr["EventId"]);
                    objUpcommingEvents.StartDate = (dr["StartDate"] != DBNull.Value ? Convert.ToDateTime(dr["StartDate"]) : DateTime.MinValue);
                    objUpcommingEvents.EndDate = (dr["EndDate"] != DBNull.Value ? Convert.ToDateTime(dr["EndDate"]) : DateTime.MinValue);
                    objUpcommingEvents.EventName = dr["EventName"].ToString();
                    objUpcommingEvents.Location = dr["Location"].ToString();
                    objUpcommingEvents.BannerUrl = dr["BannerUrl"].ToString();
                    objUpcommingEvents.EventDetails = dr["EventDetails"].ToString();
                    objUpcommingEvents.City = dr["City"].ToString();
                    objUpcommingEvents.StateName = dr["StateName"].ToString();
                    objUpcommingEvents.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
                    objUpcommingEvents.IsRegistration = (dr["IsRegistration"] != DBNull.Value ? Convert.ToBoolean(dr["IsRegistration"].ToString()) : false);
                    objUpcommingEvents.RegistrationStartDate = (dr["RegistrationStartDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationStartDate"]) : DateTime.MinValue);
                    objUpcommingEvents.RegistrationEndDate = (dr["RegistrationEndDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationEndDate"]) : DateTime.MinValue);

                    lstUpcommingEvents.Add(objUpcommingEvents);
                }
            }

            //Welcome Message
            if (ds.Tables[4].Rows.Count == 1)
            {
                DataTable dt = ds.Tables[4];
                objPHInnerPages.InnerPageId = Convert.ToInt64(dt.Rows[0]["InnerPageId"]);
                objPHInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
                objPHInnerPages.Description = dt.Rows[0]["Description"].ToString();
            }

            // Latest Events List 
            if (ds.Tables[5].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[5].Rows)
                {
                    Entities.Events objLatestEvents = new Entities.Events();

                    objLatestEvents.EventId = Convert.ToInt64(dr["EventId"]);
                    objLatestEvents.StartDate = (dr["StartDate"] != DBNull.Value ? Convert.ToDateTime(dr["StartDate"]) : DateTime.MinValue);
                    objLatestEvents.EndDate = (dr["EndDate"] != DBNull.Value ? Convert.ToDateTime(dr["EndDate"]) : DateTime.MinValue);
                    objLatestEvents.EventName = dr["EventName"].ToString();
                    objLatestEvents.Location = dr["Location"].ToString();
                    objLatestEvents.BannerUrl = dr["BannerUrl"].ToString();
                    objLatestEvents.EventDetails = dr["EventDetails"].ToString();
                    objLatestEvents.City = dr["City"].ToString();
                    objLatestEvents.StateName = dr["StateName"].ToString();
                    objLatestEvents.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
                    objLatestEvents.IsRegistration = (dr["IsRegistration"] != DBNull.Value ? Convert.ToBoolean(dr["IsRegistration"].ToString()) : false);
                    objLatestEvents.RegistrationStartDate = (dr["RegistrationStartDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationStartDate"]) : DateTime.MinValue);
                    objLatestEvents.RegistrationEndDate = (dr["RegistrationEndDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationEndDate"]) : DateTime.MinValue);

                    lstLatestEvents.Add(objLatestEvents);
                }
            }

            // Sponsors List  
            if (ds.Tables[6].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[6].Rows)
                {
                    Entities.Sponsors objHTCASponsors = new Entities.Sponsors();

                    objHTCASponsors.SponsorId = Convert.ToInt64(dr["SponsorId"]);
                    objHTCASponsors.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"]);
                    objHTCASponsors.LogoUrl = dr["LogoUrl"].ToString();
                    objHTCASponsors.Target = dr["Target"].ToString();
                    objHTCASponsors.RedirectUrl = dr["RedirectUrl"].ToString();
                    objHTCASponsors.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);

                    lstSponsors.Add(objHTCASponsors);
                }
            }

            // Sponsors List  
            if (ds.Tables[7].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[7].Rows)
                {
                    Entities.Sponsors objHTCASponsors = new Entities.Sponsors();

                    objHTCASponsors.SponsorId = Convert.ToInt64(dr["SponsorId"]);
                    objHTCASponsors.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"]);
                    objHTCASponsors.LogoUrl = dr["LogoUrl"].ToString();
                    objHTCASponsors.Target = dr["Target"].ToString();
                    objHTCASponsors.RedirectUrl = dr["RedirectUrl"].ToString();
                    objHTCASponsors.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);

                    lstMediaSponsors.Add(objHTCASponsors);
                }
            }

            // Videos List  
            if (ds.Tables[8].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[8].Rows)
                {
                    Entities.Videos objVideos = new Entities.Videos();

                    objVideos.VideoId = Convert.ToInt64(dr["VideoId"]);
                    objVideos.VideoCategoryId = Convert.ToInt64(dr["VideoCategoryId"]);
                    objVideos.Heading = dr["Heading"].ToString();
                    objVideos.VideoUrl = dr["VideoUrl"].ToString();
                    objVideos.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

                    lstVideos.Add(objVideos);
                }
            }

            //AppInfo List  
            if (ds.Tables[9].Rows.Count != 0)
            {
                if (ds.Tables[9].Rows.Count == 1)
                {
                    objAppInfo.AppInfoId = Convert.ToInt64(ds.Tables[9].Rows[0]["AppInfoId"]);
                    objAppInfo.SiteName = ds.Tables[9].Rows[0]["SiteName"].ToString();
                    objAppInfo.CompanyAddress = ds.Tables[9].Rows[0]["CompanyAddress"].ToString();
                    objAppInfo.CompanyWebSite = ds.Tables[9].Rows[0]["CompanyWebSite"].ToString();
                    objAppInfo.CompanyEmail = ds.Tables[9].Rows[0]["CompanyEmail"].ToString();
                    objAppInfo.CompanyPhone = ds.Tables[9].Rows[0]["CompanyPhone"].ToString();
                    objAppInfo.PresidentPhone = ds.Tables[9].Rows[0]["PresidentPhone"].ToString();
                    objAppInfo.PresidentEmail = ds.Tables[9].Rows[0]["PresidentEmail"].ToString();
                    objAppInfo.SecretaryEmail = ds.Tables[9].Rows[0]["SecretaryEmail"].ToString();
                    objAppInfo.SecretaryPhone = ds.Tables[9].Rows[0]["SecretaryPhone"].ToString();
                    objAppInfo.CustomerCareNumber = ds.Tables[9].Rows[0]["CustomerCareNumber"].ToString();
                    objAppInfo.TollFreeNumber = ds.Tables[9].Rows[0]["TollFreeNumber"].ToString();
                    objAppInfo.FacebookUrl = ds.Tables[9].Rows[0]["FacebookUrl"].ToString();
                    objAppInfo.TwitterUrl = ds.Tables[9].Rows[0]["TwitterUrl"].ToString();
                    objAppInfo.YoutubeUrl = ds.Tables[9].Rows[0]["YoutubeUrl"].ToString();
                    objAppInfo.SupportEmail = ds.Tables[9].Rows[0]["SupportEmail"].ToString();
                    objAppInfo.EnqueryEmail = ds.Tables[9].Rows[0]["EnqueryEmail"].ToString();
                    objAppInfo.PageTitle = ds.Tables[9].Rows[0]["PageTitle"].ToString();
                    objAppInfo.MetaDescription = ds.Tables[9].Rows[0]["MetaDescription"].ToString();
                    objAppInfo.MetaKeywords = ds.Tables[9].Rows[0]["MetaKeywords"].ToString();
                    objAppInfo.Topline = ds.Tables[9].Rows[0]["Topline"].ToString();
                    objAppInfo.PageItems = (ds.Tables[9].Rows[0]["PageItems"] != DBNull.Value ? Convert.ToInt64(ds.Tables[9].Rows[0]["PageItems"]) : 0);
                    objAppInfo.UpdatedTime = Convert.ToDateTime(ds.Tables[9].Rows[0]["UpdatedTime"]);
                }
            }

            //Sponsor Categories 
            if (ds.Tables[10].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[10].Rows)
                {
                    TCAssociationTool.Entities.SponsorCategories objSponsorCategories = new TCAssociationTool.Entities.SponsorCategories();

                    objSponsorCategories.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"].ToString());
                    //objSponsorCategories.SponsorsCount = Convert.ToInt64(dr["SponsorsCount"].ToString());
                    objSponsorCategories.CategoryName = dr["CategoryName"].ToString();
                    lstSponsorCategories.Add(objSponsorCategories);
                }
            }

            //Flyers List   
            if (ds.Tables[11].Rows.Count != 0)
            {
                objFlyers.FlyerId = Convert.ToInt64(ds.Tables[11].Rows[0]["FlyerId"]);
                objFlyers.FlyerUrl = ds.Tables[11].Rows[0]["FlyerUrl"].ToString();
                objFlyers.RedirectUrl = ds.Tables[11].Rows[0]["RedirectUrl"].ToString();
            }

            //Committe Categories List   
            if (ds.Tables[12].Rows.Count == 1)
            {
                DataTable dt = ds.Tables[12];

                objCInnerPages.InnerPageId = Convert.ToInt64(dt.Rows[0]["InnerPageId"]);
                objCInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
                objCInnerPages.Description = dt.Rows[0]["Description"].ToString();
            }

            if (ds.Tables[13].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[13].Rows)
                {
                    if (Convert.ToInt32(dr["PageLevel"]) == 1)
                    {
                        Entities.InnerPageCategories objInnerPageCategories = new Entities.InnerPageCategories();
                        objInnerPageCategories.InnerPageCategoryId = Convert.ToInt64(dr["InnerPageCategoryId"]);
                        objInnerPageCategories.DisplayName = dr["DisplayName"].ToString();
                        objInnerPageCategories.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objInnerPageCategories.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objInnerPageCategories.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objInnerPageCategories.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objInnerPageCategories.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
                        //objInnerPageCategories.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);

                        lstMenuItems.Add(objInnerPageCategories);
                    }
                    if (Convert.ToInt32(dr["PageLevel"]) == 2)
                    {
                        Entities.InnerPageCategories objInnerPageCategories = new Entities.InnerPageCategories();
                        objInnerPageCategories.InnerPageCategoryId = Convert.ToInt64(dr["InnerPageCategoryId"]);
                        objInnerPageCategories.DisplayName = dr["DisplayName"].ToString();
                        objInnerPageCategories.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objInnerPageCategories.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objInnerPageCategories.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objInnerPageCategories.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objInnerPageCategories.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
                        //objInnerPageCategories.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);

                        lstMenuItems2.Add(objInnerPageCategories);
                    }
                    if (Convert.ToInt32(dr["PageLevel"]) == 3)
                    {
                        Entities.InnerPageCategories objInnerPageCategories = new Entities.InnerPageCategories();
                        objInnerPageCategories.InnerPageCategoryId = Convert.ToInt64(dr["InnerPageCategoryId"]);
                        objInnerPageCategories.DisplayName = dr["DisplayName"].ToString();
                        objInnerPageCategories.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objInnerPageCategories.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objInnerPageCategories.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objInnerPageCategories.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objInnerPageCategories.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
                        //objInnerPageCategories.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
                        lstMenuItems3.Add(objInnerPageCategories);
                    }
                    if (Convert.ToInt32(dr["PageLevel"]) == 4)
                    {
                        Entities.InnerPageCategories objInnerPageCategories = new Entities.InnerPageCategories();
                        objInnerPageCategories.InnerPageCategoryId = Convert.ToInt64(dr["InnerPageCategoryId"]);
                        objInnerPageCategories.DisplayName = dr["DisplayName"].ToString();
                        objInnerPageCategories.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objInnerPageCategories.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objInnerPageCategories.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objInnerPageCategories.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);

                        objInnerPageCategories.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
                        //objInnerPageCategories.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);

                        lstMenuItems4.Add(objInnerPageCategories);
                    }
                }
            }
            if (ds.Tables[14].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[14].Rows)
                {
                    Entities.InnerPageCategories objInnerPageCategories = new Entities.InnerPageCategories();

                    objInnerPageCategories.InnerPageCategoryId = Convert.ToInt64(dr["InnerPageCategoryId"]);
                    objInnerPageCategories.DisplayName = dr["DisplayName"].ToString();
                    objInnerPageCategories.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                    objInnerPageCategories.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                    objInnerPageCategories.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                    objInnerPageCategories.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);

                    objInnerPageCategories.ParentPageName = (dr["ParentPageName"] != DBNull.Value ? dr["ParentPageName"].ToString() : "");

                    objInnerPageCategories.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
                    //objInnerPageCategories.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);


                    FooterMenuItems.Add(objInnerPageCategories);
                }
            }

            //President Message
            if (ds.Tables[15].Rows.Count == 1)
            {
                DataTable dt = ds.Tables[15];

                objSInnerPages.InnerPageId = Convert.ToInt64(dt.Rows[0]["InnerPageId"]);
                objSInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
                objSInnerPages.Description = dt.Rows[0]["Description"].ToString();
            }


            // Committee Categories List  
            if (ds.Tables[16].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[16].Rows)
                {
                    Entities.CommitteeCategories objCommitteeCategories = new Entities.CommitteeCategories();

                    objCommitteeCategories.CommitteeCategoryId = Convert.ToInt64(dr["CommitteeCategoryId"]);
                    objCommitteeCategories.CategoryName = dr["CategoryName"].ToString();
                    objCommitteeCategories.DisplayOrder = (dr["DisplayOrder"] != DBNull.Value ? Convert.ToInt32(dr["DisplayOrder"]) : 0);
                    objCommitteeCategories.Type = (dr["Type"] != DBNull.Value ? dr["Type"].ToString() : null);

                    lstCommitteeCategories.Add(objCommitteeCategories);
                }
            }

            // Events List 
            if (ds.Tables[17].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[17].Rows)
                {
                    Entities.Events objEvents = new Entities.Events();

                    objEvents.EventId = Convert.ToInt64(dr["EventId"]);
                    objEvents.EventName = dr["EventName"].ToString();

                    lstEvents.Add(objEvents);
                }
            }

            if (ds.Tables[18].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[18].Rows)
                {
                    TCAssociationTool.Entities.Photos objPhotos = new TCAssociationTool.Entities.Photos();

                    objPhotos.PhotoId = Convert.ToInt64(dr["PhotoId"].ToString());
                    objPhotos.PhotoCategoryId = Convert.ToInt64(dr["PhotoCategoryId"].ToString());
                    objPhotos.ImageUrl = (dr["ImageUrl"] != DBNull.Value ? dr["ImageUrl"].ToString() : null);
                    objPhotos.ImageDescription = (dr["ImageDescription"] != DBNull.Value ? dr["ImageDescription"].ToString() : null);
                    objPhotos.AlbumLink = (dr["AlbumLink"] != DBNull.Value ? dr["AlbumLink"].ToString() : null);

                    lstPhotos.Add(objPhotos);
                }
            }

        }

        public void FEGetListAppInfo(ref Entities.AppInfo objAppInfo, ref List<Entities.News> lstLatestNews, ref int status)
        {
            DataSet ds = _AppInfo.FEGetListAppInfo(ref status);

            //AppInfo List  
            if (ds.Tables[0].Rows.Count != 0)
            {
                if (ds.Tables[0].Rows.Count == 1)
                {
                    objAppInfo.AppInfoId = Convert.ToInt64(ds.Tables[0].Rows[0]["AppInfoId"]);
                    objAppInfo.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                    objAppInfo.CompanyAddress = ds.Tables[0].Rows[0]["CompanyAddress"].ToString();
                    objAppInfo.CompanyWebSite = ds.Tables[0].Rows[0]["CompanyWebSite"].ToString();
                    objAppInfo.CompanyEmail = ds.Tables[0].Rows[0]["CompanyEmail"].ToString();
                    objAppInfo.CompanyPhone = ds.Tables[0].Rows[0]["CompanyPhone"].ToString();
                    objAppInfo.PresidentPhone = ds.Tables[0].Rows[0]["PresidentPhone"].ToString();
                    objAppInfo.PresidentEmail = ds.Tables[0].Rows[0]["PresidentEmail"].ToString();
                    objAppInfo.SecretaryEmail = ds.Tables[0].Rows[0]["SecretaryEmail"].ToString();
                    objAppInfo.SecretaryPhone = ds.Tables[0].Rows[0]["SecretaryPhone"].ToString();
                    objAppInfo.CustomerCareNumber = ds.Tables[0].Rows[0]["CustomerCareNumber"].ToString();
                    objAppInfo.TollFreeNumber = ds.Tables[0].Rows[0]["TollFreeNumber"].ToString();
                    objAppInfo.FacebookUrl = ds.Tables[0].Rows[0]["FacebookUrl"].ToString();
                    objAppInfo.TwitterUrl = ds.Tables[0].Rows[0]["TwitterUrl"].ToString();
                    objAppInfo.YoutubeUrl = ds.Tables[0].Rows[0]["YoutubeUrl"].ToString();
                    objAppInfo.SupportEmail = ds.Tables[0].Rows[0]["SupportEmail"].ToString();
                    objAppInfo.EnqueryEmail = ds.Tables[0].Rows[0]["EnqueryEmail"].ToString();
                    objAppInfo.PageTitle = ds.Tables[0].Rows[0]["PageTitle"].ToString();
                    objAppInfo.MetaDescription = ds.Tables[0].Rows[0]["MetaDescription"].ToString();
                    objAppInfo.MetaKeywords = ds.Tables[0].Rows[0]["MetaKeywords"].ToString();
                    objAppInfo.Topline = ds.Tables[0].Rows[0]["Topline"].ToString();
                    objAppInfo.PageItems = (ds.Tables[0].Rows[0]["PageItems"] != DBNull.Value ? Convert.ToInt64(ds.Tables[0].Rows[0]["PageItems"]) : 0);
                    objAppInfo.UpdatedTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["UpdatedTime"]);
                }
            }

            //LatestNews List
            if (ds.Tables[1].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    Entities.News objLatestNews = new Entities.News();

                    objLatestNews.NewsId = Convert.ToInt64(dr["NewsId"]);
                    objLatestNews.Title = dr["Title"].ToString();
                    objLatestNews.NewsText = dr["NewsText"].ToString();
                    objLatestNews.ImageUrl = dr["ImageUrl"].ToString();
                    objLatestNews.PostDate = Convert.ToDateTime(dr["PostDate"]);
                    objLatestNews.OrderNo = (dr["OrderNo"] != DBNull.Value ? Convert.ToInt64(dr["OrderNo"]) : 0);
                    objLatestNews.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

                    lstLatestNews.Add(objLatestNews);
                }
            }

          
        }



        public Entities.InnerPages FEGetListMainLayout(
            string Email,
            ref List<Entities.News> lstNews,
            ref List<Entities.Sponsors> lstSponsors,
            ref List<Entities.Sponsors> lstMediaSponsors,
            ref Entities.AppInfo objAppInfo,
            ref List<Entities.SponsorCategories> lstSponsorCategories,
            ref List<Entities.Videos> lstVideos,
            ref List<Entities.InnerPageCategories> lstMenuItems,
            ref List<Entities.InnerPageCategories> lstMenuItems2,
            ref List<Entities.InnerPageCategories> lstMenuItems3,
            ref List<Entities.InnerPageCategories> lstMenuItems4,
            ref List<Entities.InnerPageCategories> FooterMenuItems,
             ref Entities.InnerPages objSInnerPages,
            ref List<Entities.CommitteeCategories> lstCommitteeCategories,
             ref List<Entities.Events> lstEvents,
            ref List<Entities.Photos> lstPhotos,
            ref int status)
        {
            TCAssociationTool.Entities.InnerPages objInnerPages = new TCAssociationTool.Entities.InnerPages();
            TCAssociationTool.Entities.Members objMembers = new TCAssociationTool.Entities.Members();
            DataSet ds = _AppInfo.FEGetListMainLayout(Email, ref status);

            //News List
            if (ds.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Entities.News objNews = new Entities.News();

                    objNews.NewsId = Convert.ToInt64(dr["NewsId"]);
                    objNews.PostDate = Convert.ToDateTime(dr["PostDate"]);
                    objNews.NewsText = dr["NewsText"].ToString();
                    objNews.Title = dr["Title"].ToString();
                    objNews.ImageUrl = dr["ImageUrl"].ToString();

                    lstNews.Add(objNews);
                }
            }

            objInnerPages.lstNews = lstNews;

            // Sponsors List  
            if (ds.Tables[1].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    Entities.Sponsors objHTCASponsors = new Entities.Sponsors();

                    objHTCASponsors.SponsorId = Convert.ToInt64(dr["SponsorId"]);
                    objHTCASponsors.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"]);
                    objHTCASponsors.LogoUrl = dr["LogoUrl"].ToString();
                    objHTCASponsors.Target = dr["Target"].ToString();
                    objHTCASponsors.RedirectUrl = dr["RedirectUrl"].ToString();
                    objHTCASponsors.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);

                    lstSponsors.Add(objHTCASponsors);
                }
            }

            objInnerPages.lstSponsors = lstSponsors;

            // Sponsors List  
            if (ds.Tables[2].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    Entities.Sponsors objHTCASponsors = new Entities.Sponsors();

                    objHTCASponsors.SponsorId = Convert.ToInt64(dr["SponsorId"]);
                    objHTCASponsors.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"]);
                    objHTCASponsors.LogoUrl = dr["LogoUrl"].ToString();
                    objHTCASponsors.Target = dr["Target"].ToString();
                    objHTCASponsors.RedirectUrl = dr["RedirectUrl"].ToString();
                    objHTCASponsors.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);

                    lstMediaSponsors.Add(objHTCASponsors);
                }
            }
            objInnerPages.lstMediaSponsors = lstMediaSponsors;
            //AppInfo List  
            if (ds.Tables[3].Rows.Count != 0)
            {
                if (ds.Tables[3].Rows.Count == 1)
                {
                    objAppInfo.AppInfoId = Convert.ToInt64(ds.Tables[3].Rows[0]["AppInfoId"]);
                    objAppInfo.SiteName = ds.Tables[3].Rows[0]["SiteName"].ToString();
                    objAppInfo.CompanyAddress = ds.Tables[3].Rows[0]["CompanyAddress"].ToString();
                    objAppInfo.CompanyWebSite = ds.Tables[3].Rows[0]["CompanyWebSite"].ToString();
                    objAppInfo.CompanyEmail = ds.Tables[3].Rows[0]["CompanyEmail"].ToString();
                    objAppInfo.CompanyPhone = ds.Tables[3].Rows[0]["CompanyPhone"].ToString();
                    objAppInfo.PresidentPhone = ds.Tables[3].Rows[0]["PresidentPhone"].ToString();
                    objAppInfo.PresidentEmail = ds.Tables[3].Rows[0]["PresidentEmail"].ToString();
                    objAppInfo.SecretaryEmail = ds.Tables[3].Rows[0]["SecretaryEmail"].ToString();
                    objAppInfo.SecretaryPhone = ds.Tables[3].Rows[0]["SecretaryPhone"].ToString();
                    objAppInfo.CustomerCareNumber = ds.Tables[3].Rows[0]["CustomerCareNumber"].ToString();
                    objAppInfo.TollFreeNumber = ds.Tables[3].Rows[0]["TollFreeNumber"].ToString();
                    objAppInfo.FacebookUrl = ds.Tables[3].Rows[0]["FacebookUrl"].ToString();
                    objAppInfo.TwitterUrl = ds.Tables[3].Rows[0]["TwitterUrl"].ToString();
                    objAppInfo.YoutubeUrl = ds.Tables[3].Rows[0]["YoutubeUrl"].ToString();
                    objAppInfo.SupportEmail = ds.Tables[3].Rows[0]["SupportEmail"].ToString();
                    objAppInfo.EnqueryEmail = ds.Tables[3].Rows[0]["EnqueryEmail"].ToString();
                    objAppInfo.PageTitle = ds.Tables[3].Rows[0]["PageTitle"].ToString();
                    objAppInfo.MetaDescription = ds.Tables[3].Rows[0]["MetaDescription"].ToString();
                    objAppInfo.MetaKeywords = ds.Tables[3].Rows[0]["MetaKeywords"].ToString();
                    objAppInfo.Topline = ds.Tables[3].Rows[0]["Topline"].ToString();
                    objAppInfo.PageItems = (ds.Tables[3].Rows[0]["PageItems"] != DBNull.Value ? Convert.ToInt64(ds.Tables[3].Rows[0]["PageItems"]) : 0);
                    objAppInfo.UpdatedTime = Convert.ToDateTime(ds.Tables[3].Rows[0]["UpdatedTime"]);
                }
            }

            //Sponsor Categories 
            if (ds.Tables[4].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[4].Rows)
                {
                    TCAssociationTool.Entities.SponsorCategories objSponsorCategories = new TCAssociationTool.Entities.SponsorCategories();

                    objSponsorCategories.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"].ToString());
                    objSponsorCategories.SponsorsCount = Convert.ToInt64(dr["SponsorsCount"].ToString());
                    objSponsorCategories.CategoryName = dr["CategoryName"].ToString();
                    lstSponsorCategories.Add(objSponsorCategories);
                }
            }

            objInnerPages.lstSponsorCategories = lstSponsorCategories;

            if (ds.Tables[5].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[5].Rows)
                {
                    if (Convert.ToInt32(dr["PageLevel"]) == 1)
                    {
                        Entities.InnerPageCategories objInnerPageCategories = new Entities.InnerPageCategories();
                        objInnerPageCategories.InnerPageCategoryId = Convert.ToInt64(dr["InnerPageCategoryId"]);
                        objInnerPageCategories.DisplayName = dr["DisplayName"].ToString();
                        objInnerPageCategories.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objInnerPageCategories.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objInnerPageCategories.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objInnerPageCategories.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objInnerPageCategories.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
                        objInnerPageCategories.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);

                        lstMenuItems.Add(objInnerPageCategories);
                    }
                    if (Convert.ToInt32(dr["PageLevel"]) == 2)
                    {
                        Entities.InnerPageCategories objInnerPageCategories = new Entities.InnerPageCategories();
                        objInnerPageCategories.InnerPageCategoryId = Convert.ToInt64(dr["InnerPageCategoryId"]);
                        objInnerPageCategories.DisplayName = dr["DisplayName"].ToString();
                        objInnerPageCategories.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objInnerPageCategories.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objInnerPageCategories.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objInnerPageCategories.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objInnerPageCategories.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
                        objInnerPageCategories.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);

                        lstMenuItems2.Add(objInnerPageCategories);
                    }
                    if (Convert.ToInt32(dr["PageLevel"]) == 3)
                    {
                        Entities.InnerPageCategories objInnerPageCategories = new Entities.InnerPageCategories();
                        objInnerPageCategories.InnerPageCategoryId = Convert.ToInt64(dr["InnerPageCategoryId"]);
                        objInnerPageCategories.DisplayName = dr["DisplayName"].ToString();
                        objInnerPageCategories.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objInnerPageCategories.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objInnerPageCategories.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objInnerPageCategories.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objInnerPageCategories.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
                        objInnerPageCategories.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
                        lstMenuItems3.Add(objInnerPageCategories);
                    }
                    if (Convert.ToInt32(dr["PageLevel"]) == 4)
                    {
                        Entities.InnerPageCategories objInnerPageCategories = new Entities.InnerPageCategories();
                        objInnerPageCategories.InnerPageCategoryId = Convert.ToInt64(dr["InnerPageCategoryId"]);
                        objInnerPageCategories.DisplayName = dr["DisplayName"].ToString();
                        objInnerPageCategories.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objInnerPageCategories.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objInnerPageCategories.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objInnerPageCategories.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);

                        objInnerPageCategories.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
                        objInnerPageCategories.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);

                        lstMenuItems4.Add(objInnerPageCategories);
                    }
                }
            }
            if (ds.Tables[6].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[6].Rows)
                {
                    Entities.InnerPageCategories objInnerPageCategories = new Entities.InnerPageCategories();

                    objInnerPageCategories.InnerPageCategoryId = Convert.ToInt64(dr["InnerPageCategoryId"]);
                    objInnerPageCategories.DisplayName = dr["DisplayName"].ToString();
                    objInnerPageCategories.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                    objInnerPageCategories.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                    objInnerPageCategories.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                    objInnerPageCategories.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);

                    objInnerPageCategories.ParentPageName = (dr["ParentPageName"] != DBNull.Value ? dr["ParentPageName"].ToString() : "");

                    objInnerPageCategories.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
                    objInnerPageCategories.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);


                    FooterMenuItems.Add(objInnerPageCategories);
                }
            }

            // Videos List  
            if (ds.Tables[7].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[7].Rows)
                {
                    Entities.Videos objVideos = new Entities.Videos();

                    objVideos.VideoId = Convert.ToInt64(dr["VideoId"]);
                    objVideos.VideoCategoryId = Convert.ToInt64(dr["VideoCategoryId"]);
                    objVideos.Heading = dr["Heading"].ToString();
                    objVideos.VideoUrl = dr["VideoUrl"].ToString();
                    objVideos.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

                    lstVideos.Add(objVideos);
                }
            }

            //President Message
            if (ds.Tables[8].Rows.Count != 0)
            {
                if (ds.Tables[8].Rows.Count == 1)
                {
                    objSInnerPages.InnerPageId = Convert.ToInt64(ds.Tables[8].Rows[0]["InnerPageId"]);
                    objSInnerPages.Heading = ds.Tables[8].Rows[0]["Heading"].ToString();
                    objSInnerPages.Description = ds.Tables[8].Rows[0]["Description"].ToString();

                    objInnerPages.objSInnerPages = objSInnerPages;
                }
            }

            // Committee Categories List  
            if (ds.Tables[9].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[9].Rows)
                {
                    Entities.CommitteeCategories objCommitteeCategories = new Entities.CommitteeCategories();

                    objCommitteeCategories.CommitteeCategoryId = Convert.ToInt64(dr["CommitteeCategoryId"]);
                    objCommitteeCategories.CategoryName = dr["CategoryName"].ToString();
                    objCommitteeCategories.DisplayOrder = (dr["DisplayOrder"] != DBNull.Value ? Convert.ToInt32(dr["DisplayOrder"]) : 0);
                    objCommitteeCategories.Type = (dr["Type"] != DBNull.Value ? dr["Type"].ToString() : null);

                    lstCommitteeCategories.Add(objCommitteeCategories);
                }
            }

            objInnerPages.lstCommitteeCategories = lstCommitteeCategories;

            // Upcomming Events List 
            if (ds.Tables[10].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[10].Rows)
                {
                    Entities.Events objEvents = new Entities.Events();

                    objEvents.EventId = Convert.ToInt64(dr["EventId"]);
                    objEvents.EventName = dr["EventName"].ToString();

                    lstEvents.Add(objEvents);
                }
            }

            if (ds.Tables[11].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[11].Rows)
                {
                    TCAssociationTool.Entities.Photos objPhotos = new TCAssociationTool.Entities.Photos();

                    objPhotos.PhotoId = Convert.ToInt64(dr["PhotoId"].ToString());
                    objPhotos.PhotoCategoryId = Convert.ToInt64(dr["PhotoCategoryId"].ToString());
                    objPhotos.ImageUrl = (dr["ImageUrl"] != DBNull.Value ? dr["ImageUrl"].ToString() : null);
                    objPhotos.ImageDescription = (dr["ImageDescription"] != DBNull.Value ? dr["ImageDescription"].ToString() : null);
                    objPhotos.AlbumLink = (dr["AlbumLink"] != DBNull.Value ? dr["AlbumLink"].ToString() : null);

                    lstPhotos.Add(objPhotos);
                }
            }

            return objInnerPages;
        }


        #endregion

        #region API

        public void APIFEGetListInitialLoad(
         ref Entities.InnerPages objPInnerPages,
         ref List<Entities.WebsiteBanners> lstWebsiteBanners,
         ref List<Entities.Events> lstUpcommingEvents,         
         ref List<Entities.Sponsors> lstSponsors,        
         ref List<Entities.Videos> lstVideos,
         ref List<Entities.Events> lstPastEvents,
         ref List<Entities.Events> lstCurrentEvents,         
         ref int status)
        {
            DataSet ds = _AppInfo.APIFEGetListInitialLoad(ref status);
            string newsurl = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "news/";
            string WebsiteBanners = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "WebsiteBanners/NormalImages/";
            string eventsurl = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "events/banners/";
            string photourl = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "photogallery/thumb/";
            string Flyersurl = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "Flyers/NormalImages/";
            string Sponsorsurl = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "Sponsors/";
           
            //President Message
            if (ds.Tables[0].Rows.Count == 1)
            {
                DataTable dt = ds.Tables[0];

                objPInnerPages.InnerPageId = Convert.ToInt64(dt.Rows[0]["InnerPageId"]);
                objPInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
                objPInnerPages.Description = dt.Rows[0]["Description"].ToString();
            }


            //WebsiteBanners List   
            if (ds.Tables[1].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    Entities.WebsiteBanners objWebsiteBanners = new Entities.WebsiteBanners();

                    objWebsiteBanners.WebsiteBannerId = Convert.ToInt64(dr["WebsiteBannerId"]);
                    objWebsiteBanners.BannerTitle = dr["BannerTitle"].ToString();
                    //objWebsiteBanners.BannerUrl = dr["BannerUrl"].ToString();
                    objWebsiteBanners.BannerUrl = (dr["BannerUrl"] != DBNull.Value ? WebsiteBanners + dr["BannerUrl"].ToString() : "");
                    objWebsiteBanners.Target = dr["Target"].ToString();
                    objWebsiteBanners.RedirectUrl = dr["RedirectUrl"].ToString();
                    objWebsiteBanners.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

                    lstWebsiteBanners.Add(objWebsiteBanners);
                }
            }

            // Upcomming Events List 
            if (ds.Tables[2].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    Entities.Events objUpcommingEvents = new Entities.Events();

                    objUpcommingEvents.EventId = Convert.ToInt64(dr["EventId"]);
                    objUpcommingEvents.StartDate = (dr["StartDate"] != DBNull.Value ? Convert.ToDateTime(dr["StartDate"]) : DateTime.MinValue);
                    objUpcommingEvents.EndDate = (dr["EndDate"] != DBNull.Value ? Convert.ToDateTime(dr["EndDate"]) : DateTime.MinValue);
                    objUpcommingEvents.EventName = dr["EventName"].ToString();
                    objUpcommingEvents.Location = dr["Location"].ToString();
                    //objUpcommingEvents.BannerUrl = dr["BannerUrl"].ToString();
                    objUpcommingEvents.BannerUrl = (dr["BannerUrl"] != DBNull.Value ? eventsurl + dr["BannerUrl"].ToString() : "");
                    objUpcommingEvents.EventDetails = dr["EventDetails"].ToString();
                    objUpcommingEvents.City = dr["City"].ToString();
                    objUpcommingEvents.StateName = dr["StateName"].ToString();
                    objUpcommingEvents.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
                    objUpcommingEvents.IsRegistration = (dr["IsRegistration"] != DBNull.Value ? Convert.ToBoolean(dr["IsRegistration"].ToString()) : false);
                    objUpcommingEvents.RegistrationStartDate = (dr["RegistrationStartDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationStartDate"]) : DateTime.MinValue);
                    objUpcommingEvents.RegistrationEndDate = (dr["RegistrationEndDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationEndDate"]) : DateTime.MinValue);

                    lstUpcommingEvents.Add(objUpcommingEvents);
                }
            }

            
            // Sponsors List  
            if (ds.Tables[3].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[3].Rows)
                {
                    Entities.Sponsors objHTCASponsors = new Entities.Sponsors();

                    objHTCASponsors.SponsorId = Convert.ToInt64(dr["SponsorId"]);
                    objHTCASponsors.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"]);
                    //objHTCASponsors.LogoUrl = dr["LogoUrl"].ToString();
                    objHTCASponsors.LogoUrl = (dr["LogoUrl"] != DBNull.Value ? Sponsorsurl + dr["LogoUrl"].ToString() : "");
                    objHTCASponsors.Target = dr["Target"].ToString();
                    objHTCASponsors.RedirectUrl = dr["RedirectUrl"].ToString();
                    objHTCASponsors.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);

                    lstSponsors.Add(objHTCASponsors);
                }
            }           

            // Videos List  
            if (ds.Tables[4].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[4].Rows)
                {
                    Entities.Videos objVideos = new Entities.Videos();

                    objVideos.VideoId = Convert.ToInt64(dr["VideoId"]);
                    objVideos.VideoCategoryId = Convert.ToInt64(dr["VideoCategoryId"]);
                    objVideos.Heading = dr["Heading"].ToString();
                    objVideos.VideoUrl = dr["VideoUrl"].ToString();
                    objVideos.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

                    lstVideos.Add(objVideos);
                }
            }

            // Past Events List 
            if (ds.Tables[5].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[5].Rows)
                {
                    Entities.Events objUpcommingEvents = new Entities.Events();

                    objUpcommingEvents.EventId = Convert.ToInt64(dr["EventId"]);
                    objUpcommingEvents.StartDate = (dr["StartDate"] != DBNull.Value ? Convert.ToDateTime(dr["StartDate"]) : DateTime.MinValue);
                    objUpcommingEvents.EndDate = (dr["EndDate"] != DBNull.Value ? Convert.ToDateTime(dr["EndDate"]) : DateTime.MinValue);
                    objUpcommingEvents.EventName = dr["EventName"].ToString();
                    objUpcommingEvents.Location = dr["Location"].ToString();
                    //objUpcommingEvents.BannerUrl = dr["BannerUrl"].ToString();
                    objUpcommingEvents.BannerUrl = (dr["BannerUrl"] != DBNull.Value ? eventsurl + dr["BannerUrl"].ToString() : "");
                    objUpcommingEvents.EventDetails = dr["EventDetails"].ToString();
                    objUpcommingEvents.City = dr["City"].ToString();
                    objUpcommingEvents.StateName = dr["StateName"].ToString();
                    objUpcommingEvents.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
                    objUpcommingEvents.IsRegistration = (dr["IsRegistration"] != DBNull.Value ? Convert.ToBoolean(dr["IsRegistration"].ToString()) : false);
                    objUpcommingEvents.RegistrationStartDate = (dr["RegistrationStartDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationStartDate"]) : DateTime.MinValue);
                    objUpcommingEvents.RegistrationEndDate = (dr["RegistrationEndDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationEndDate"]) : DateTime.MinValue);

                    lstUpcommingEvents.Add(objUpcommingEvents);
                }
            }

            // Upcomming Events List 
            if (ds.Tables[6].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[6].Rows)
                {
                    Entities.Events objUpcommingEvents = new Entities.Events();

                    objUpcommingEvents.EventId = Convert.ToInt64(dr["EventId"]);
                    objUpcommingEvents.StartDate = (dr["StartDate"] != DBNull.Value ? Convert.ToDateTime(dr["StartDate"]) : DateTime.MinValue);
                    objUpcommingEvents.EndDate = (dr["EndDate"] != DBNull.Value ? Convert.ToDateTime(dr["EndDate"]) : DateTime.MinValue);
                    objUpcommingEvents.EventName = dr["EventName"].ToString();
                    objUpcommingEvents.Location = dr["Location"].ToString();
                    //objUpcommingEvents.BannerUrl = dr["BannerUrl"].ToString();
                    objUpcommingEvents.BannerUrl = (dr["BannerUrl"] != DBNull.Value ? eventsurl + dr["BannerUrl"].ToString() : "");
                    objUpcommingEvents.EventDetails = dr["EventDetails"].ToString();
                    objUpcommingEvents.City = dr["City"].ToString();
                    objUpcommingEvents.StateName = dr["StateName"].ToString();
                    objUpcommingEvents.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
                    objUpcommingEvents.IsRegistration = (dr["IsRegistration"] != DBNull.Value ? Convert.ToBoolean(dr["IsRegistration"].ToString()) : false);
                    objUpcommingEvents.RegistrationStartDate = (dr["RegistrationStartDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationStartDate"]) : DateTime.MinValue);
                    objUpcommingEvents.RegistrationEndDate = (dr["RegistrationEndDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationEndDate"]) : DateTime.MinValue);

                    lstUpcommingEvents.Add(objUpcommingEvents);
                }
            }
        }

        //public void APIFEGetListInitialLoad(
        // ref List<Entities.News> lstNews,
        // ref Entities.InnerPages objPInnerPages,
        // ref List<Entities.WebsiteBanners> lstWebsiteBanners,
        // ref List<Entities.Events> lstUpcommingEvents,
        // ref Entities.InnerPages objWMInnerPages,
        // ref List<Entities.Events> lstLatestEvents,
        // ref List<Entities.Sponsors> lstSponsors,
        // ref List<Entities.Sponsors> lstMediaSponsors,
        // ref List<Entities.Photos> lstPhotos,
        // ref List<Entities.Videos> lstVideos,
        // ref Entities.AppInfo objAppInfo,
        // ref List<Entities.SponsorCategories> lstSponsorCategories,
        // ref Entities.Flyers objFlyers,
        // ref List<Entities.CommitteeCategories> lstCommitteeCategories,

        // ref int status)
        //{
        //    DataSet ds = _AppInfo.FEGetListInitialLoad(ref status);
        //    string newsurl = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "news/";
        //    string WebsiteBanners = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "WebsiteBanners/NormalImages/";
        //    string eventsurl = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "events/banners/";
        //    string photourl = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "photogallery/thumb/";
        //    string Flyersurl = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "Flyers/NormalImages/";
        //    string Sponsorsurl = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "Sponsors/NormalImages/";
        //    //News List
        //    if (ds.Tables[0].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            Entities.News objNews = new Entities.News();

        //            objNews.NewsId = Convert.ToInt64(dr["NewsId"]);
        //            objNews.PostDate = Convert.ToDateTime(dr["PostDate"]);
        //            objNews.NewsText = dr["NewsText"].ToString();
        //            objNews.Title = dr["Title"].ToString();
        //            //objNews.ImageUrl = dr["ImageUrl"].ToString();
        //            objNews.ImageUrl = (dr["ImageUrl"] != DBNull.Value ? newsurl + dr["ImageUrl"].ToString() : "");

        //            lstNews.Add(objNews);
        //        }
        //    }

        //    //President Message
        //    if (ds.Tables[1].Rows.Count == 1)
        //    {
        //        DataTable dt = ds.Tables[1];

        //        objPInnerPages.InnerPageId = Convert.ToInt64(dt.Rows[0]["InnerPageId"]);
        //        objPInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
        //        objPInnerPages.Description = dt.Rows[0]["Description"].ToString();
        //    }


        //    //WebsiteBanners List   
        //    if (ds.Tables[2].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[2].Rows)
        //        {
        //            Entities.WebsiteBanners objWebsiteBanners = new Entities.WebsiteBanners();

        //            objWebsiteBanners.WebsiteBannerId = Convert.ToInt64(dr["WebsiteBannerId"]);
        //            objWebsiteBanners.BannerTitle = dr["BannerTitle"].ToString();
        //            //objWebsiteBanners.BannerUrl = dr["BannerUrl"].ToString();
        //            objWebsiteBanners.BannerUrl = (dr["BannerUrl"] != DBNull.Value ? WebsiteBanners + dr["BannerUrl"].ToString() : "");
        //            objWebsiteBanners.Target = dr["Target"].ToString();
        //            objWebsiteBanners.RedirectUrl = dr["RedirectUrl"].ToString();
        //            objWebsiteBanners.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

        //            lstWebsiteBanners.Add(objWebsiteBanners);
        //        }
        //    }

        //    // Upcomming Events List 
        //    if (ds.Tables[3].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[3].Rows)
        //        {
        //            Entities.Events objUpcommingEvents = new Entities.Events();

        //            objUpcommingEvents.EventId = Convert.ToInt64(dr["EventId"]);
        //            objUpcommingEvents.StartDate = (dr["StartDate"] != DBNull.Value ? Convert.ToDateTime(dr["StartDate"]) : DateTime.MinValue);
        //            objUpcommingEvents.EndDate = (dr["EndDate"] != DBNull.Value ? Convert.ToDateTime(dr["EndDate"]) : DateTime.MinValue);
        //            objUpcommingEvents.EventName = dr["EventName"].ToString();
        //            objUpcommingEvents.Location = dr["Location"].ToString();
        //            //objUpcommingEvents.BannerUrl = dr["BannerUrl"].ToString();
        //            objUpcommingEvents.BannerUrl = (dr["BannerUrl"] != DBNull.Value ? eventsurl + dr["BannerUrl"].ToString() : "");
        //            objUpcommingEvents.EventDetails = dr["EventDetails"].ToString();
        //            objUpcommingEvents.City = dr["City"].ToString();
        //            objUpcommingEvents.StateName = dr["StateName"].ToString();
        //            objUpcommingEvents.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
        //            objUpcommingEvents.IsRegistration = (dr["IsRegistration"] != DBNull.Value ? Convert.ToBoolean(dr["IsRegistration"].ToString()) : false);
        //            objUpcommingEvents.RegistrationStartDate = (dr["RegistrationStartDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationStartDate"]) : DateTime.MinValue);
        //            objUpcommingEvents.RegistrationEndDate = (dr["RegistrationEndDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationEndDate"]) : DateTime.MinValue);

        //            lstUpcommingEvents.Add(objUpcommingEvents);
        //        }
        //    }

        //    //Welcome Message
        //    if (ds.Tables[4].Rows.Count == 1)
        //    {
        //        DataTable dt = ds.Tables[4];
        //        objWMInnerPages.InnerPageId = Convert.ToInt64(dt.Rows[0]["InnerPageId"]);
        //        objWMInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
        //        objWMInnerPages.Description = dt.Rows[0]["Description"].ToString();
        //    }

        //    // Latest Events List 
        //    if (ds.Tables[5].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[5].Rows)
        //        {
        //            Entities.Events objLatestEvents = new Entities.Events();

        //            objLatestEvents.EventId = Convert.ToInt64(dr["EventId"]);
        //            objLatestEvents.StartDate = (dr["StartDate"] != DBNull.Value ? Convert.ToDateTime(dr["StartDate"]) : DateTime.MinValue);
        //            objLatestEvents.EndDate = (dr["EndDate"] != DBNull.Value ? Convert.ToDateTime(dr["EndDate"]) : DateTime.MinValue);
        //            objLatestEvents.EventName = dr["EventName"].ToString();
        //            objLatestEvents.Location = dr["Location"].ToString();
        //            //objLatestEvents.BannerUrl = dr["BannerUrl"].ToString();
        //            objLatestEvents.BannerUrl = (dr["BannerUrl"] != DBNull.Value ? eventsurl + dr["BannerUrl"].ToString() : "");
        //            objLatestEvents.EventDetails = dr["EventDetails"].ToString();
        //            objLatestEvents.City = dr["City"].ToString();
        //            objLatestEvents.StateName = dr["StateName"].ToString();
        //            objLatestEvents.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
        //            objLatestEvents.IsRegistration = (dr["IsRegistration"] != DBNull.Value ? Convert.ToBoolean(dr["IsRegistration"].ToString()) : false);
        //            objLatestEvents.RegistrationStartDate = (dr["RegistrationStartDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationStartDate"]) : DateTime.MinValue);
        //            objLatestEvents.RegistrationEndDate = (dr["RegistrationEndDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationEndDate"]) : DateTime.MinValue);

        //            lstLatestEvents.Add(objLatestEvents);
        //        }
        //    }

        //    // Sponsors List  
        //    if (ds.Tables[6].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[6].Rows)
        //        {
        //            Entities.Sponsors objHTCASponsors = new Entities.Sponsors();

        //            objHTCASponsors.SponsorId = Convert.ToInt64(dr["SponsorId"]);
        //            objHTCASponsors.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"]);
        //            //objHTCASponsors.LogoUrl = dr["LogoUrl"].ToString();
        //            objHTCASponsors.LogoUrl = (dr["LogoUrl"] != DBNull.Value ? Sponsorsurl + dr["LogoUrl"].ToString() : "");
        //            objHTCASponsors.Target = dr["Target"].ToString();
        //            objHTCASponsors.RedirectUrl = dr["RedirectUrl"].ToString();
        //            objHTCASponsors.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);

        //            lstSponsors.Add(objHTCASponsors);
        //        }
        //    }

        //    // Sponsors List  
        //    if (ds.Tables[7].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[7].Rows)
        //        {
        //            Entities.Sponsors objHTCASponsors = new Entities.Sponsors();

        //            objHTCASponsors.SponsorId = Convert.ToInt64(dr["SponsorId"]);
        //            objHTCASponsors.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"]);
        //            //objHTCASponsors.LogoUrl = dr["LogoUrl"].ToString();
        //            objHTCASponsors.LogoUrl = (dr["LogoUrl"] != DBNull.Value ? Sponsorsurl + dr["LogoUrl"].ToString() : "");
        //            objHTCASponsors.Target = dr["Target"].ToString();
        //            objHTCASponsors.RedirectUrl = dr["RedirectUrl"].ToString();
        //            objHTCASponsors.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);

        //            lstMediaSponsors.Add(objHTCASponsors);
        //        }
        //    }


        //    //Photos section
        //    if (ds.Tables[8].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[8].Rows)
        //        {
        //            Entities.Photos objPhotos = new Entities.Photos();

        //            objPhotos.PhotoId = Convert.ToInt64(dr["PhotoId"]);
        //            objPhotos.PhotoCategoryId = Convert.ToInt64(dr["PhotoCategoryId"]);
        //            objPhotos.ImageDescription = dr["ImageDescription"].ToString();
        //            //objPhotos.ImageUrl = dr["ImageUrl"].ToString();
        //            objPhotos.ImageUrl = (dr["ImageUrl"] != DBNull.Value ? photourl + dr["ImageUrl"].ToString() : "");
        //            objPhotos.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

        //            lstPhotos.Add(objPhotos);
        //        }
        //    }

        //    // Videos List  
        //    if (ds.Tables[9].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[9].Rows)
        //        {
        //            Entities.Videos objVideos = new Entities.Videos();

        //            objVideos.VideoId = Convert.ToInt64(dr["VideoId"]);
        //            objVideos.VideoCategoryId = Convert.ToInt64(dr["VideoCategoryId"]);
        //            objVideos.Heading = dr["Heading"].ToString();
        //            objVideos.VideoUrl = dr["VideoUrl"].ToString();
        //            objVideos.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

        //            lstVideos.Add(objVideos);
        //        }
        //    }

        //    //AppInfo List  
        //    if (ds.Tables[10].Rows.Count != 0)
        //    {
        //        if (ds.Tables[10].Rows.Count == 1)
        //        {
        //            objAppInfo.AppInfoId = Convert.ToInt64(ds.Tables[10].Rows[0]["AppInfoId"]);
        //            objAppInfo.SiteName = ds.Tables[10].Rows[0]["SiteName"].ToString();
        //            objAppInfo.CompanyAddress = ds.Tables[10].Rows[0]["CompanyAddress"].ToString();
        //            objAppInfo.CompanyWebSite = ds.Tables[10].Rows[0]["CompanyWebSite"].ToString();
        //            objAppInfo.CompanyEmail = ds.Tables[10].Rows[0]["CompanyEmail"].ToString();
        //            objAppInfo.CompanyPhone = ds.Tables[10].Rows[0]["CompanyPhone"].ToString();
        //            objAppInfo.PresidentPhone = ds.Tables[10].Rows[0]["PresidentPhone"].ToString();
        //            objAppInfo.PresidentEmail = ds.Tables[10].Rows[0]["PresidentEmail"].ToString();
        //            objAppInfo.SecretaryEmail = ds.Tables[10].Rows[0]["SecretaryEmail"].ToString();
        //            objAppInfo.SecretaryPhone = ds.Tables[10].Rows[0]["SecretaryPhone"].ToString();
        //            objAppInfo.CustomerCareNumber = ds.Tables[10].Rows[0]["CustomerCareNumber"].ToString();
        //            objAppInfo.TollFreeNumber = ds.Tables[10].Rows[0]["TollFreeNumber"].ToString();
        //            objAppInfo.FacebookUrl = ds.Tables[10].Rows[0]["FacebookUrl"].ToString();
        //            objAppInfo.TwitterUrl = ds.Tables[10].Rows[0]["TwitterUrl"].ToString();
        //            objAppInfo.YoutubeUrl = ds.Tables[10].Rows[0]["YoutubeUrl"].ToString();
        //            objAppInfo.SupportEmail = ds.Tables[10].Rows[0]["SupportEmail"].ToString();
        //            objAppInfo.EnqueryEmail = ds.Tables[10].Rows[0]["EnqueryEmail"].ToString();
        //            objAppInfo.PageTitle = ds.Tables[10].Rows[0]["PageTitle"].ToString();
        //            objAppInfo.MetaDescription = ds.Tables[10].Rows[0]["MetaDescription"].ToString();
        //            objAppInfo.MetaKeywords = ds.Tables[10].Rows[0]["MetaKeywords"].ToString();
        //            objAppInfo.Topline = ds.Tables[10].Rows[0]["Topline"].ToString();
        //            objAppInfo.PageItems = (ds.Tables[10].Rows[0]["PageItems"] != DBNull.Value ? Convert.ToInt64(ds.Tables[10].Rows[0]["PageItems"]) : 0);
        //            objAppInfo.UpdatedTime = Convert.ToDateTime(ds.Tables[10].Rows[0]["UpdatedTime"]);
        //        }
        //    }

        //    //Sponsor Categories 
        //    if (ds.Tables[11].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[11].Rows)
        //        {
        //            TCAssociationTool.Entities.SponsorCategories objSponsorCategories = new TCAssociationTool.Entities.SponsorCategories();

        //            objSponsorCategories.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"].ToString());
        //            objSponsorCategories.SponsorsCount = Convert.ToInt64(dr["SponsorsCount"].ToString());
        //            objSponsorCategories.CategoryName = dr["CategoryName"].ToString();
        //            lstSponsorCategories.Add(objSponsorCategories);
        //        }
        //    }

        //    //Flyers List   
        //    if (ds.Tables[12].Rows.Count != 0)
        //    {
        //        objFlyers.FlyerId = Convert.ToInt64(ds.Tables[12].Rows[0]["FlyerId"]);
        //        //objFlyers.FlyerUrl = ds.Tables[12].Rows[0]["FlyerUrl"].ToString();
        //        objFlyers.FlyerUrl = (ds.Tables[12].Rows[0]["FlyerUrl"] != DBNull.Value ? Flyersurl + ds.Tables[12].Rows[0]["FlyerUrl"].ToString() : "");
        //        objFlyers.RedirectUrl = ds.Tables[12].Rows[0]["RedirectUrl"].ToString();
        //    }

        //    //WebsiteBanners List   
        //    if (ds.Tables[13].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[13].Rows)
        //        {
        //            Entities.CommitteeCategories objCommitteeCategories = new Entities.CommitteeCategories();

        //            objCommitteeCategories.CommitteeCategoryId = Convert.ToInt64(dr["CommitteeCategoryId"]);
        //            objCommitteeCategories.CategoryName = dr["CategoryName"].ToString();

        //            lstCommitteeCategories.Add(objCommitteeCategories);
        //        }
        //    }


        //}
        #endregion
    }


}