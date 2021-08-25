using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TCAssociationTool.Areas.API.Controllers
{
    public class HomeAPIController : ApiController
    {
        BLL.AppInfo _AppInfo = new BLL.AppInfo();
        BLL.Sponsors _Sponsors = new BLL.Sponsors();
        BLL.SponsorCategories _SponsorCategories = new BLL.SponsorCategories();
        BLL.Videos _Videos = new BLL.Videos();
        BLL.Photos _Photos = new BLL.Photos();
        BLL.PhotoCategories _PhotoCategories = new BLL.PhotoCategories();
        BLL.VideoCategories _VideoCategories = new BLL.VideoCategories();
        BLL.Enquiries _Enquiries = new BLL.Enquiries();

        [HttpGet]
        public HttpResponseMessage IndexAPI()
        {
            Entities.InnerPages objPInnerPages = new Entities.InnerPages();
            List<Entities.WebsiteBanners> lstWebsiteBanners = new List<Entities.WebsiteBanners>();
            List<Entities.Events> lstUpcommingEvents = new List<Entities.Events>();
            List<Entities.Events> lstPastEvents = new List<Entities.Events>();
            List<Entities.Events> lstCurrentEvents = new List<Entities.Events>();
            List<Entities.Sponsors> lstSponsors = new List<Entities.Sponsors>();
            List<Entities.Videos> lstVideos = new List<Entities.Videos>();

            int status = 0;

            string imgurl = System.Configuration.ConfigurationManager.AppSettings["baseurl"] + "Content/events/banners/";
            string bannerurl = System.Configuration.ConfigurationManager.AppSettings["baseurl"] + "Content/WebsiteBanners/NormalImages/";

            try
            {
                _AppInfo.APIFEGetListInitialLoad(ref objPInnerPages, ref lstWebsiteBanners, ref lstUpcommingEvents, ref lstSponsors, ref lstVideos, ref lstPastEvents, ref lstCurrentEvents, ref status);
                string msg = "success";

                var WebsiteBannersList = lstWebsiteBanners.Select(W => new { W.WebsiteBannerId, W.BannerTitle, W.BannerUrl, W.Target, W.RedirectUrl, PostedDate = Convert.ToDateTime(W.UpdatedTime).ToString("dd MMM, yyyy hh:mm tt") });
                var SponsorsList = lstSponsors.Select(S => new { S.SponsorId, S.SponsorCategoryId, S.LogoUrl, S.Target, S.RedirectUrl, PostedDate = Convert.ToDateTime(S.InsertedTime).ToString("dd MMM, yyyy") });
                var VideosList = lstVideos.Select(V => new { V.VideoId, V.VideoCategoryId, V.Heading, V.VideoUrl, PostedDate = Convert.ToDateTime(V.UpdatedTime).ToString("dd MMM, yyyy") });
                                
    
                if (lstUpcommingEvents != null && lstUpcommingEvents.Count != 0)
                {
                    var Eventslist = lstUpcommingEvents.Select(U => new { U.EventId, EventName = U.EventName, StartDate = Convert.ToDateTime(U.StartDate).ToString("dd MMM, yyyy hh:mm tt"), EndDate = Convert.ToDateTime(U.EndDate).ToString("dd MMM, yyyy hh:mm tt"), RegistrationStartDate = Convert.ToDateTime(U.RegistrationStartDate).ToString("dd MMM, yyyy hh:mm tt"), RegistrationEndDate = Convert.ToDateTime(U.RegistrationEndDate).ToString("dd MMM, yyyy hh:mm tt"), U.BannerUrl, U.Location, U.City, U.StateName, U.IsRegistration, UpdatedTime = Convert.ToDateTime(U.UpdatedTime).ToString("dd MMM, yyyy") });

                   return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, objPInnerPages.InnerPageId, objPInnerPages.Heading, objPInnerPages.Description, WebsiteBannersList, Eventslist, SponsorsList, VideosList });
                  
                }
                else
                {
                    var Eventslist = lstPastEvents.Select(U => new { U.EventId, EventName = U.EventName, StartDate = Convert.ToDateTime(U.StartDate).ToString("dd MMM, yyyy hh:mm tt"), EndDate = Convert.ToDateTime(U.EndDate).ToString("dd MMM, yyyy hh:mm tt"), RegistrationStartDate = Convert.ToDateTime(U.RegistrationStartDate).ToString("dd MMM, yyyy hh:mm tt"), RegistrationEndDate = Convert.ToDateTime(U.RegistrationEndDate).ToString("dd MMM, yyyy hh:mm tt"), U.BannerUrl, U.Location, U.City, U.StateName, U.IsRegistration, UpdatedTime = Convert.ToDateTime(U.UpdatedTime).ToString("dd MMM, yyyy") });
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, objPInnerPages.InnerPageId, objPInnerPages.Heading, objPInnerPages.Description, WebsiteBannersList, Eventslist, SponsorsList, VideosList });
                }


            }
            catch
            {
                string msg = "failed";
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, msg);
            }
        }

        [HttpGet]
        public HttpResponseMessage SponsorsList()
        {
            string msg = "";
            List<Entities.Sponsors> lstSponsors = new List<Entities.Sponsors>();
            try
            {
                int status = 0;
                lstSponsors = _Sponsors.GetSponsorsList(ref status);
                string imgurl = System.Configuration.ConfigurationManager.AppSettings["baseurl"] + "Content/Sponsors/";


                msg = "Success";
                var SponsorsList = lstSponsors.Select(C => new { C.SponsorId, C.SponsorCategoryId, C.CategoryName, Logo = (BLL.Common.ValidateImage(imgurl + C.LogoUrl, imgurl + "no-image.jpg")), C.InsertedBy, InsertedTime = Convert.ToDateTime(C.InsertedTime).ToString("dd MMM,yyyy") });
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, SponsorsList });
            }
            catch (Exception ex)
            {
                msg = "failed";
                string str = ex.Message;
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, msg, str);
            }
        }

        //Photos List
        [HttpGet]
        public HttpResponseMessage PhotosList()
        {
            string msg = "";
            List<Entities.Photos> lstPhotos = new List<Entities.Photos>();
            try
            {
                int status = 0;
                lstPhotos = _Photos.GetPhotosList(ref status);
                string imgurl = System.Configuration.ConfigurationManager.AppSettings["baseurl"] + "Content/photogallery/normalphoto/";

                msg = "Success";
                var PhotosList = lstPhotos.Select(C => new { C.PhotoId, C.PhotoCategoryId, C.CategoryName, Logo = (BLL.Common.ValidateImage(imgurl + C.ImageUrl, imgurl + "no-image.jpg")), C.UpdatedBy, InsertedTime = Convert.ToDateTime(C.UpdatedTime).ToString("dd MMM,yyyy") });
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, PhotosList });
            }
            catch (Exception ex)
            {
                msg = "failed";
                string str = ex.Message;
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, msg, str);
            }
        }

        [HttpGet]
        public HttpResponseMessage PhotoCategories()
        {
            int status = 0;
            string msg = "";

            List<Entities.PhotoCategories> lstPhotoCategories = new List<Entities.PhotoCategories>();
            try
            {
                msg = "Success";

                lstPhotoCategories = _PhotoCategories.GetPhotoCategoriesList(ref status);

                var PhotoCategories = lstPhotoCategories.Select(PC => new{PC.PhotoCategoryId, PC.CategoryName, PC.Year, PC.UpdatedBy, PostedDate = Convert.ToDateTime(PC.UpdatedTime).ToString("dd MMM,yyyy")});

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, PhotoCategories });

            }
            catch (Exception ex)
            {
                msg = "failed";
                string str = ex.Message;
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, msg, str);
            }
        }

        [HttpGet]
        public HttpResponseMessage Album(string Year = "")
        {
            int status = 0;

            List<Entities.PhotoCategories> lstPhotoCategories = new List<Entities.PhotoCategories>();
            List<Entities.Photos> lstPhotos = new List<Entities.Photos>();

            string msg = "";

            try
            {
                lstPhotos = _Photos.FETOP4GetPhotoList(ref lstPhotoCategories, Year, ref status);

                string imgurl = System.Configuration.ConfigurationManager.AppSettings["baseurl"] + "Content/photogallery/normalphoto/";

                msg = "Success";
                var PhotosList = lstPhotos.Select(C => new { C.PhotoId, C.PhotoCategoryId, Logo = (BLL.Common.ValidateImage(imgurl + C.ImageUrl, imgurl + "no-image.jpg")), C.UpdatedBy, InsertedTime = Convert.ToDateTime(C.UpdatedTime).ToString("dd MMM,yyyy") });
                var PhotoCategory = lstPhotoCategories.Select(PC => new { PC.PhotoCategoryId, PC.CategoryName, PC.UpdatedBy, PC.Year, PC.PhotosCount });

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, PhotosList, PhotoCategory });

            }
            catch (Exception ex)
            {
                msg = "failed";
                string str = ex.Message;
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, msg, str);
            }
        }

        [HttpGet]
        public HttpResponseMessage AlbumList(string CategoryName)
        {
            int status = 0;
            List<Entities.PhotoCategories> lstPhotoCategories = new List<Entities.PhotoCategories>();
            List<Entities.Photos> lstPhotos = new List<Entities.Photos>();

            string msg = "";
            try
            {
                lstPhotos = _Photos.FEGetPhotosListById(CategoryName, ref lstPhotoCategories, ref status);

                string imgurl = System.Configuration.ConfigurationManager.AppSettings["baseurl"] + "Content/photogallery/normalphoto/";

                msg = "Success";
                var PhotosList = lstPhotos.Select(C => new { C.PhotoId, C.PhotoCategoryId, C.CategoryName, Logo = (BLL.Common.ValidateImage(imgurl + C.ImageUrl, imgurl + "no-image.jpg")), C.UpdatedBy, InsertedTime = Convert.ToDateTime(C.UpdatedTime).ToString("dd MMM,yyyy") });
                var PhotoCategory = lstPhotoCategories.Select(PC => new { PC.PhotoCategoryId, PC.CategoryName, PC.UpdatedBy, PC.Year, PC.PhotosCount });

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, PhotosList, PhotoCategory });
            }
            catch (Exception ex)
            {
                msg = "failed";
                string str = ex.Message;
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, msg, str);
            }
        }

        //Videos List
        [HttpGet]
        public HttpResponseMessage VideosList()
        {
            string msg = "";
            List<Entities.Videos> lstVideos = new List<Entities.Videos>();
            try
            {
                int status = 0;
                lstVideos = _Videos.GetVideosList(ref status);

                msg = "Success";
                var VideosList = lstVideos.Select(C => new { C.VideoId, C.Heading, C.VideoCategoryId, C.CategoryName, C.VideoUrl, C.UpdatedBy, InsertedTime = Convert.ToDateTime(C.UpdatedTime).ToString("dd MMM,yyyy") });
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, VideosList });
            }
            catch (Exception ex)
            {
                msg = "failed";
                string str = ex.Message;
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, msg, str);
            }
        }

        [HttpGet]
        public HttpResponseMessage VideoCategories()
        {
            int status = 0;
            string msg = "";

            List<Entities.VideoCategories> lstVideoCategories = new List<Entities.VideoCategories>();
            try
            {
                msg = "Success";

                lstVideoCategories = _VideoCategories.GetVideoCategoriesList(ref status);

                var VideoCategories = lstVideoCategories.Select(VC => new { VC.VideoCategoryId, VC.CategoryName, VC.Year, VC.UpdatedBy, PostedDate = Convert.ToDateTime(VC.UpdatedTime).ToString("dd MMM,yyyy") });

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, VideoCategories });

            }
            catch (Exception ex)
            {
                msg = "failed";
                string str = ex.Message;
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, msg, str);
            }
        }

        [HttpGet]
        public HttpResponseMessage Videos(string Year = "")
        {
            int status = 0;

            List<Entities.VideoCategories> lstVideoCategories = new List<Entities.VideoCategories>();
            List<Entities.Videos> lstVideos = new List<Entities.Videos>();

            string msg = "";

            try
            {
                lstVideos = _Videos.FETOP4GetVideosList(ref lstVideoCategories, Year, ref status);
                

                msg = "Success";
                var VideosList = lstVideos.Select(C => new { C.VideoId, C.Heading, C.VideoCategoryId, C.VideoUrl, C.UpdatedBy, InsertedTime = Convert.ToDateTime(C.UpdatedTime).ToString("dd MMM,yyyy") });
                var VideoCategories = lstVideoCategories.Select(VC => new { VC.VideoCategoryId, VC.CategoryName, VC.Year, VC.UpdatedBy, VC.VideosCount });

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, VideosList, VideoCategories });

            }
            catch (Exception ex)
            {
                msg = "failed";
                string str = ex.Message;
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, msg, str);
            }
        }


        [HttpGet]
        public HttpResponseMessage VideosListbyCategory(string CategoryName)
        {
            int status = 0;
            List<Entities.VideoCategories> lstVideoCategories = new List<Entities.VideoCategories>();
            List<Entities.Videos> lstVideos = new List<Entities.Videos>();

            string msg = "";
            try
            {
                lstVideos = _Videos.FEGetVideosListById(CategoryName, ref lstVideoCategories, ref status);
                

                msg = "Success";
                var VideosList = lstVideos.Select(C => new { C.VideoId, C.Heading, C.VideoCategoryId, C.VideoUrl, UpdatedBy = (C.UpdatedBy != null && C.UpdatedBy != "" ? C.UpdatedBy : " "), InsertedTime = Convert.ToDateTime(C.UpdatedTime).ToString("dd MMM,yyyy") });
                var VideoCategories = lstVideoCategories.Select(VC => new { VC.VideoCategoryId, VC.CategoryName, VC.Year, VC.UpdatedBy, VC.VideosCount });

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, VideosList, VideoCategories });
            }
            catch (Exception ex)
            {
                msg = "failed";
                string str = ex.Message;
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, msg, str);
            }
        }

        [HttpGet]
        public HttpResponseMessage SponsorCategories()
        {
            string msg = "";
            int status = 0;
            List<Entities.SponsorCategories> lstSponsorCategories = new List<Entities.SponsorCategories>();
            try
            {
                lstSponsorCategories = _SponsorCategories.GetSponsorCategoriesList(ref status);

                msg = "Success";
                var sponsorcategories = lstSponsorCategories.Select(S => new { S.SponsorCategoryId, S.CategoryName, S.UpdatedBy, Date = Convert.ToDateTime(S.UpdatedTime).ToString("dd MMM,yyyy") });
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, sponsorcategories });
            }
            catch (Exception ex)
            {
                msg = "failed";
                string str = ex.Message;
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, msg, str);
            }

        }

        //Enquiry Form

        //Enquiry Form
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage EnquiryInsert(
           string Name = "",
           string Email = "",
           string PhoneNo = "",
           string Address = "",
           string Subject = "",
           string Comments = ""

            )
        {
            string msg = "";
            try
            {
                Entities.Enquiries objEnquiries = new Entities.Enquiries();


                objEnquiries.Name = Name;
                objEnquiries.Email = Email;
                objEnquiries.PhoneNo = PhoneNo;
                objEnquiries.Subject = Name;
                objEnquiries.Description = Comments;
                objEnquiries.IsActive = true;
                objEnquiries.InsertedTime = DateTime.UtcNow;


                Int64 EnquiryId = 0;

                Int64 _status = _Enquiries.InsertEnquiries(objEnquiries, ref EnquiryId);
                switch (_status)
                {
                    case 1:

                        //List<Entities.MailTemplates> lstMailTemplates = new List<Entities.MailTemplates>();
                        //int mstatus = 0;
                        //lstMailTemplates = _MailTemplates.GetMailTemplatesList("", ref mstatus);

                        //Entities.MailTemplates SubscriptionTemplate = lstMailTemplates.Find(x => x.Heading == "Subscription Rewards to Ambassador");
                        //if (SubscriptionTemplate != null && SubscriptionTemplate.MailTemplateId != 0)
                        //{
                        //    StringBuilder body2 = new StringBuilder();
                        //    body2.Append(SubscriptionTemplate.Description);
                        //    body2.Replace("[USERNAME]", objEnquiries.Name);
                        //    body2.Replace("[Email]", objEnquiries.Email);
                        //    body2.Replace("[StudentName]", objStudentSubscriptions.FirstName);
                        //    body2.Replace("[usersiteurl]", ConfigurationManager.AppSettings["usersiteurl"].ToString());
                        //    BLL.Common.SendMail((AmbEmail != "" ? AmbEmail : StdEmail), SubscriptionTemplate.Subject, body2.ToString());
                        //}

                        //Entities.MailTemplates StudentTemplate = lstMailTemplates.Find(x => x.Heading == "Student Subscription");

                        //if (StudentTemplate != null && StudentTemplate.MailTemplateId != 0)
                        //{
                        //    StringBuilder body3 = new StringBuilder();
                        //    body3.Append(StudentTemplate.Description);
                        //    body3.Replace("[Ambassador]", (AmbEmail != "" ? _objAmbassadors.UserName : _objStudents.FullName));
                        //    body3.Replace("[USERNAME]", objStudentSubscriptions.FirstName);
                        //    var link = ConfigurationManager.AppSettings["usersiteurl"] + "StudentSubscriptions/Unsubscribe?StudentSubId=" + StudentSubId + "&Email=" + objStudentSubscriptions.Email;
                        //    body3.Replace("[Link]", link);
                        //    body3.Replace("[usersiteurl]", ConfigurationManager.AppSettings["usersiteurl"].ToString());
                        //    BLL.Common.SendMail(objStudentSubscriptions.Email, StudentTemplate.Subject, body3.ToString());
                        //}
                        msg = "Success";
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg });
                    case 2:
                        msg = "Updated";
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg });
                    case -1:
                        msg = "Failed";
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, new { msg });
                    default:
                        msg = "Failed";
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, new { msg });
                }
            }
            catch (Exception EX)
            {
                msg = "Failed";
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, new { msg });
            }

        }

        
        [HttpGet]
        public HttpResponseMessage ApplicationSettings()
        {
            string msg = "";
            int status = 0;

            Entities.AppInfo objAppInfo = new Entities.AppInfo();
            try
            {
                objAppInfo = _AppInfo.GetAppInfoDetails(ref status);

                msg = "Success";

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, objAppInfo.SiteName, objAppInfo.CompanyEmail, CompanyPhone = (objAppInfo.CompanyPhone != null && objAppInfo.CompanyPhone != "" ? objAppInfo.CompanyPhone : " "), WebsiteUrl = objAppInfo.CompanyWebSite, CompanyAddress = objAppInfo.CompanyAddress, FacebookUrl = objAppInfo.FacebookUrl, TwitterUrl = objAppInfo.TwitterUrl, YoutubeUrl = objAppInfo.YoutubeUrl, GooglePlusUrl = objAppInfo.SupportEmail });
            }
            catch (Exception ex)
            {
                msg = "failed";
                string str = ex.Message;
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, msg, str);
            }

        }

    }
}
