using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,")]
    public class NewsController : Controller
    {
        BLL.News _news = new BLL.News();
        Entities.News objNews = new Entities.News();

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult NewsList(string Search = "", string SortColumn = "UpdatedTime", string SortOrder = "Desc", int PageNo = 1, int Items = 20)
        {
            int Total = 0;
            List<Entities.News> lstNews = new List<Entities.News>();
            try
            {
                string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
                lstNews = _news.GetNewsListByVariable(Search, Sort, PageNo, Items, ref Total);
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.total = Total;
            ViewBag.lstNews = lstNews;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder;
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult EditNews(Int64 NewsId)
        { 
            try
            {
                int status = 0;
                objNews = _news.GetNewsById(NewsId, ref status);
                if (status != -1 && objNews != null)
                {
                    ViewBag.objNews = objNews;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed processing your request.</div>"; 
                }

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return View();
        }

        [Models.SessionClass.SessionExpireFilter] 
        public ActionResult ViewNews(Int64 NewsId)
        { 
            try
            {
                int status = 0;
                objNews = _news.GetNewsById(NewsId, ref status);
                if (status != -1 && objNews != null)
                {
                    ViewBag.objNews = objNews; 
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed</div>"; 
                }

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>"; 
            }
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult AddNews()
        {
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(Entities.News objNews)
        {
            try
            {
                var image = WebImage.GetImageFromRequest();
                string imageurl = (image != null ? image.ImageFormat : "NA");

                objNews.UpdatedBy = HttpContext.User.Identity.Name.ToString();
                Int64 _status = _news.InsertNews(objNews, ref imageurl);
                switch (_status)
                {
                    case 1:
                        if (image != null)
                        {
                            image.Resize(350, 350, true, false);
                            image.Crop(1, 1, 1, 1);
                            image.Save(ConfigurationManager.AppSettings["uploadPath"] + "\\news\\" + imageurl);
                        }
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully</div>";
                        break;
                    case 2:
                        if (image != null)
                        {
                            image.Resize(350, 350, true, false);
                            image.Crop(1, 1, 1, 1);
                            image.Save(ConfigurationManager.AppSettings["uploadPath"] + "\\news\\" + imageurl);
                        }
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                        break;
                    case -1:
                        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed inserting news details.</div>";
                        break;
                    default:
                        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed inserting news details.</div>";
                        break;
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return RedirectToAction("Index", "News");
        }

        [HttpPost]
        [Models.SessionClass.SessionExpireFilter]
        public JsonResult Delete(Int64 NewsId, string filename)
        {
            string str = "";
            bool _bool = true;

            try
            {
                Int64 _status = _news.DeleteNews(NewsId);
                switch (_status)
                {
                    case 1:
                        if (filename != null && filename != "")
                        {
                            System.IO.File.Delete(ConfigurationManager.AppSettings["uploadPath"] + @"\news\" + filename);
                        }

                        str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                        _bool = true;
                        break;
                    case -1:
                        str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting news.</div>";
                        _bool = false;
                        break;
                }
            }
            catch
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                _bool = false;
            }
            return Json(new { ok = _bool, data = str });
        }

        [HttpPost]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public JsonResult NewsStatus(Int64 NewsId)
        {
            string str = "";
            try
            {
                Int64 _status = _news.UpdateNewsStatus(NewsId);
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
        public JsonResult NewsDisplayOrder(int OrderNo, Int64 NewsId)
        {
            string str = "";
            try
            {
                Int64 _status = _news.UpdateNewsDisplayOrder(OrderNo, NewsId);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Updated Order No Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed updating Order No</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }

    }
}
