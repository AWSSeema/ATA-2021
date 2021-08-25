using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
     [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,ThemeBanners")]
    public class WebsiteBannersController : Controller
    {
        TCAssociationTool.BLL.WebsiteBanners _WebsiteBanners = new TCAssociationTool.BLL.WebsiteBanners();

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {            
            return View();
        }

        [HttpGet]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult BannersList(string search = "", int PageNo = 1, int PageItems = 10, string SortColumn = "", string SortOrder = "")
        {
            int Total = 0;
            List<Entities.WebsiteBanners> Bannerslist =new List<Entities.WebsiteBanners>();
            try
            {
                string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
               
               Bannerslist = _WebsiteBanners.GetWebsiteBannerListByVariable(HttpUtility.UrlDecode(search.Trim()), Sort, PageNo, PageItems, ref Total);
               
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }

            ViewBag.total = Total;
            ViewBag.items = PageItems;
            ViewBag.pageno = PageNo;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder;
            ViewBag.Bannerslist = Bannerslist;
            return View();
        }

        //[HttpPost]
        //[Areas.Admin.Models.SessionClass.SessionExpireFilter]
        //public ActionResult AddWebsiteBanners(Entities.WebsiteBanners objWebsiteBanner, HttpPostedFileBase BannerUrl)
        //{
        //    try
        //    {
        //        BannerUrl.SaveAs(ConfigurationManager.AppSettings["uploadPath"] + "\\WebsiteBanners\\NormalImages\\" + BannerUrl.FileName);
        //        WebImage image = new WebImage(ConfigurationManager.AppSettings["uploadPath"] + "\\WebsiteBanners\\NormalImages\\" + BannerUrl.FileName);
        //        string imageurl = (image != null ? image.ImageFormat : "NA");

        //        //var image = WebImage.GetImageFromRequest();
        //        //string imageurl = (image != null ? image.ImageFormat : "NA");

        //        objWebsiteBanner.UpdatedBy = this.User.Identity.Name;
        //        Int64 _status = _WebsiteBanners.InsertWebsiteBanners(objWebsiteBanner, ref imageurl);
        //        if (_status != -1)
        //        {
        //            if (imageurl != "")
        //            {
        //                image.Save(ConfigurationManager.AppSettings["uploadPath"] + "\\WebsiteBanners\\NormalImages\\" + imageurl);

        //                image.Resize(130, 130, true, false);
        //                image.Crop(1, 1, 1, 1);
        //                image.Save(ConfigurationManager.AppSettings["uploadPath"] + "\\WebsiteBanners\\ThumbImages\\" + imageurl);
        //                System.IO.File.Delete(ConfigurationManager.AppSettings["uploadpath"] + "\\WebsiteBanners\\NormalImages\\" + BannerUrl.FileName);

        //            }
        //            if (_status == 1)
        //            {
        //            TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully.</div>";
        //            }
        //            else if (_status == 2)
        //            {
        //                TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
        //            }
        //            return RedirectToAction("Index", "WebsiteBanners");

        //        }
        //        else
        //        {
        //            TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\"Uploading Failed.</div>";
        //            return RedirectToAction("Index", "WebsiteBanners");
        //        }
        //    }
        //    catch 
        //    {
        //        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
        //        return RedirectToAction("Index", "WebsiteBanners");
        //    }
        //}


        [HttpPost]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult AddWebsiteBanners(Entities.WebsiteBanners objWebsiteBanner, HttpPostedFileBase BannerUrl)
        {
            try
            {

                if (BannerUrl == null)
                {
                    string imageurl = "";
                    objWebsiteBanner.UpdatedBy = this.User.Identity.Name;
                    Int64 _status = _WebsiteBanners.InsertWebsiteBanners(objWebsiteBanner, ref imageurl);
                    if (_status != -1)
                    {

                        if (_status == 1)
                        {
                            TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully.</div>";
                        }
                        else if (_status == 2)
                        {
                            TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                        }
                        return RedirectToAction("Index", "WebsiteBanners");

                    }
                }
                else
                {
                    BannerUrl.SaveAs(ConfigurationManager.AppSettings["uploadPath"] + "\\WebsiteBanners\\NormalImages\\" + BannerUrl.FileName);
                    WebImage image = new WebImage(ConfigurationManager.AppSettings["uploadPath"] + "\\WebsiteBanners\\NormalImages\\" + BannerUrl.FileName);
                    string imageurl = (image != null ? image.ImageFormat : "NA");



                    //var image = WebImage.GetImageFromRequest();
                    //string imageurl = (image != null ? image.ImageFormat : "NA");
                    objWebsiteBanner.UpdatedBy = this.User.Identity.Name;
                    Int64 _status = _WebsiteBanners.InsertWebsiteBanners(objWebsiteBanner, ref imageurl);
                    if (_status != -1)
                    {

                        if (imageurl != "")
                        {
                            image.Save(ConfigurationManager.AppSettings["uploadPath"] + "\\WebsiteBanners\\NormalImages\\" + imageurl);

                            //image.Resize(130, 130, true, false);
                            //image.Crop(1, 1, 1, 1);
                            image.Save(ConfigurationManager.AppSettings["uploadPath"] + "\\WebsiteBanners\\ThumbImages\\" + imageurl);
                            System.IO.File.Delete(ConfigurationManager.AppSettings["uploadpath"] + "\\WebsiteBanners\\NormalImages\\" + BannerUrl.FileName);

                        }
                        if (_status == 1)
                        {
                            TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully.</div>";
                        }
                        else if (_status == 2)
                        {
                            TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                        }
                        return RedirectToAction("Index", "WebsiteBanners");

                    }

                    else
                    {
                        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\"Uploading Failed.</div>";
                        return RedirectToAction("Index", "WebsiteBanners");
                    }

                }
                return RedirectToAction("Index", "WebsiteBanners");

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("Index", "WebsiteBanners");
            }
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult EditBanner(Int64 WebsiteBannerId)
        {
            string str = "";
            int status = 0;
            try
            {
                Entities.WebsiteBanners objWebsiteBanners = _WebsiteBanners.GetWebsiteBannerById(WebsiteBannerId, ref status);
                if (objWebsiteBanners != null)
                {
                    return Json(new { ok = true, data = objWebsiteBanners });
                }
                else
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Failed Transaction</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch 
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }

        [HttpPost]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]

        public JsonResult DeleteBanner(Int64 WebsiteBannerId, string BannerUrl = "")
        {
            string str = "";
            try
            {
                Int64 _status = _WebsiteBanners.DeleteWebsiteBanner(WebsiteBannerId);
                if (_status == 1)
                {
                    System.IO.File.Delete(ConfigurationManager.AppSettings["uploadpath"] + @"\\WebsiteBanners\\NormalImages\\" + BannerUrl);
                    System.IO.File.Delete(ConfigurationManager.AppSettings["uploadpath"] + @"\\WebsiteBanners\\ThumbImages\\" + BannerUrl);
                    str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting item</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch 
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }

        [HttpPost]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public JsonResult WebsiteBannersStatus(Int64 WebsiteBannerId)
        {
            string str = "";
            try
            {
                Int64 _status = _WebsiteBanners.UpdateWebsiteBannersStatus(WebsiteBannerId);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Updated Status Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed updating status</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch 
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }

        [HttpPost]
        public JsonResult WebsiteBannerOrderNo(int OrderNo, Int64 WebsiteBannerId)
        {
            string str = "";
            try
            {
                Int64 _status = _WebsiteBanners.UpdateWebsiteBannersOrderNo(OrderNo, WebsiteBannerId);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Updated Order Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed updating status</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch 
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }

        public ActionResult Crop(string CategoryName = "", string filename = "", int top = 0, int left = 0, int bottom = 0, int right = 0)
        {
            var image = new WebImage(Path.Combine(ConfigurationManager.AppSettings["uploadPath"] + "\\WebsiteBanners\\NormalImages\\", filename));
            if (image != null)
            {
                image.Crop(top, left, bottom, right);
                image.Resize(145, 80, true, false);
                image.Crop(1, 1, 1, 1);
                image.Save(Path.Combine(ConfigurationManager.AppSettings["uploadPath"] + "\\WebsiteBanners\\ThumbImages\\" + filename));

            }
            return RedirectToAction("Index", "WebsiteBanners", new { Type = "close", CategoryName = CategoryName });
        }

        public ActionResult Preview(Int64 WebsiteBannerId = 0, string filename = "", int imgheight = 0, int imgwidth = 0)
        {
            int status = 0;
            Entities.WebsiteBanners objWebsiteBanners = new Entities.WebsiteBanners();
            try
            {
                objWebsiteBanners = _WebsiteBanners.GetWebsiteBannerById(WebsiteBannerId, ref status);
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }

            var image = new WebImage(Path.Combine(ConfigurationManager.AppSettings["uploadPath"] + "\\WebsiteBanners\\NormalImages\\" + objWebsiteBanners.BannerUrl));
            ViewBag.filename = objWebsiteBanners.BannerUrl;
            ViewBag.imgheight = image.Height;
            ViewBag.imgwidth = image.Width;
            return View();
        }

    }
}
