using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
     [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,Videos")]
    public class VideoGalleryController : Controller
    {
        TCAssociationTool.BLL.VideoCategories _VideoCategory = new TCAssociationTool.BLL.VideoCategories();
        List<Entities.VideoCategories> lstVideoCategories = new List<Entities.VideoCategories>();
        TCAssociationTool.BLL.Videos _Video = new TCAssociationTool.BLL.Videos();
        List<Entities.Videos> lstVideos = new List<Entities.Videos>();

        #region Video Category

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult VideosCategoryList(string Search = "", string SortColumn = "", string SortOrder = "", int PageNo = 1, int Items = 20)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;

            try
            {
                lstVideoCategories = _VideoCategory.GetVideoCategoriesListByVariable(Search, Sort, PageNo, Items, ref Total);               
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }

            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstVideoCategories = lstVideoCategories;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult AddVideoCategory(Entities.VideoCategories objVideoCategory)
        {
            string str = "";
            bool _bool = true;
            try
            {
                objVideoCategory.UpdatedBy = HttpContext.User.Identity.Name.ToString();

                Int64 _status = _VideoCategory.InsertVideoCategories(objVideoCategory);
                switch (_status)
                {
                    case 1:
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully</div>";
                        _bool = true;
                        break;
                    case 2:
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                        _bool = true;
                        break;
                    case -1:
                        TempData["message"] = "<div class=\"error-alert closable\">Failed processing your request.</div>";
                        _bool = false;
                        break;
                    default:
                        TempData["message"] = "<div class=\"error-alert closable\">Failed processing your request.</div>";
                        _bool = false;
                        break;
                }
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                _bool = false;
            }
            return RedirectToAction("Index", "VideoGallery");
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult EditVideoCategory(Int64 VideoCategoryId = 0)
        {
            string str = "";
            try
            {
                int _qstatus = 0;
                Entities.VideoCategories objVideoCategory = _VideoCategory.GetVideoCategoryById(VideoCategoryId, ref _qstatus);

                if (objVideoCategory != null)
                {
                    return Json(new { ok = true, data = objVideoCategory });
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

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public JsonResult DeleteVideoCategory(Int64 VideoCategoryId)
        {
            string str = "";
            try
            {
                Int64 _status = _VideoCategory.DeleteVideoCategory(VideoCategoryId);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting page</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch 
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }

        #endregion

        #region Videos

        [HttpGet]
        [Models.SessionClass.SessionExpireFilter]
        public ActionResult Videos(Int64 VideoCategoryId = 0)
        {
            int Total = 0;
            ViewBag.VideoCategoryId = VideoCategoryId;
            ViewBag.catlist = _VideoCategory.GetVideoCategoriesList(ref Total);
            
            return View();
        }

        [HttpGet]
        [Models.SessionClass.SessionExpireFilter]
        public ActionResult VideosList(Int64 VideoCategoryId = 0, string Search = "", string SortColumn = "UpdatedTime", string SortOrder = "Desc", int PageNo = 1, int Items = 20)
        {
            int Total = 0;
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            List<Entities.Videos> lstVideos =new List<Entities.Videos>();
            try
            {
                lstVideos = _Video.GetVideosListByVariable(VideoCategoryId, Search, Sort, PageNo, Items, ref Total);
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.VideoCategoryId = VideoCategoryId;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();
            ViewBag.total = Total;
            ViewBag.lstVideosLst = lstVideos;
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult AddVideo(Int64 VideoCategoryId = 0)
        {
            int _qstatus = 0;
            try
            {
                List<Entities.VideoCategories> lstVideoCategories = _VideoCategory.GetVideoCategoriesList(ref _qstatus);

                if (_qstatus == 1)
                {
                    ViewBag.lstVideoCategories = lstVideoCategories;
                    ViewBag.status = _qstatus;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "VideoGallery");
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.VideoCategoryId = VideoCategoryId;
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult AddVideo(Entities.Videos objVideo)
        {
            try
            { 
                objVideo.UpdatedBy = HttpContext.User.Identity.Name.ToString();
                Int64 status = _Video.InsertVideos(objVideo);

                if (status == 1)
                {
                    TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully</div>";
                    return RedirectToAction("Videos", "VideoGallery", new { VideoCategoryId = objVideo.VideoCategoryId });
                }
                if (status == 2)
                {
                    TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                    return RedirectToAction("Videos", "VideoGallery", new { VideoCategoryId = objVideo.VideoCategoryId });
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed uploading page.</div>";
                    return RedirectToAction("AddVideo", "VideoGallery", new { VideoCategoryId = objVideo.VideoCategoryId });
                }
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("AddVideo", "VideoGallery");
            }            
        }

        
        [HttpPost]
        public JsonResult VideoDelete(Int64 VideoId)
        {
            string str = "";

            try
            {
                Int64 _status = _Video.DeleteVideo(VideoId);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"error-alert closable\">Failed deleting food point image.</div>";
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
        public JsonResult VideoDeleteAll(string VideoIds)
        {
            string str = "";
            string ImageUrl = "";
            try
            {
                Int64 _status = _Video.DeleteAllVideos(VideoIds);
                if (_status == 1)
                {
                    string[] Url = ImageUrl.Split(',');
                    foreach (var item in Url)
                    {
                        if (item != "")
                        {
                            System.IO.File.Delete(ConfigurationManager.AppSettings["uploadPath"] + @"\Videogallery\normalVideo\" + item);
                            System.IO.File.Delete(ConfigurationManager.AppSettings["uploadPath"] + @"\Videogallery\thumb\" + item);
                        }
                    }

                    str = "<div class=\"alert alert-success alert-dismissable\">Records Deleted Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"error-alert closable\">Failed deleting Videos.</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch 
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult VideoEdit(Int64 VideoId)
        {
            try
            {
                int _qstatus = 0;
                Entities.Videos objVideos = _Video.GetVideoById(VideoId, ref _qstatus);
                int qstatus = 0;
                List<Entities.VideoCategories> lstVideoCategories = _VideoCategory.GetVideoCategoriesList(ref qstatus);

                if (_qstatus == 1 && qstatus == 1)
                {
                    ViewBag.objVideos = objVideos;
                    ViewBag.lstVideoCategories = lstVideoCategories;
                    ViewBag.status = _qstatus;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Videos", "VideoGallery");
                }
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("Videos", "VideoGallery");
            }
            return View();
        }

        [HttpPost]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public JsonResult VideoStatus(Int64 VideoId)
        {
            string str = "";
            try
            {
                Int64 _status = _Video.UpdateWebsiteBannersStatus(VideoId);
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
        public JsonResult VideoDisplayOrder(int DisplayOrder, Int64 VideoId)
        {
            string str = "";
            try
            {
                Int64 _status = _Video.UpdateVideosDisplayOrder(DisplayOrder, VideoId);
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

        #endregion

    }
}
