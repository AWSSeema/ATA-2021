using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,Administrator,")]
    [TCAssociationTool.Models.SessionClass.SessionExpireFilter]
    public class CommunityNewsController : Controller
    {
        TCAssociationTool.BLL.CommunityNews _CommunityNews = new TCAssociationTool.BLL.CommunityNews();
        List<Entities.CommunityNews> lstCommunityNews = new List<Entities.CommunityNews>();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CommunityNewsList(string search = "", int PageNo = 1, int PageItems = 10, string SortColumn = "", string SortOrder = "")
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;
            try
            {

                lstCommunityNews = _CommunityNews.GetCommunityNewsListByVariable(HttpUtility.UrlDecode(search.Trim()), Sort, PageNo, PageItems, ref Total);

            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
            }
            ViewBag.Total = Total;
            ViewBag.items = PageItems;
            ViewBag.PageNo = PageNo;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder;
            ViewBag.lstCommunityNews = lstCommunityNews;
            return View();
        }

        [HttpPost]
        public ActionResult AddCommunityNews(Entities.CommunityNews objCommunityNews)
        {
            try
            {

                var image = WebImage.GetImageFromRequest();
                string imageurl = (image != null ? image.ImageFormat : "NA");

                objCommunityNews.status2 = true;
                Int64 _status = _CommunityNews.InsertCommunityNews(objCommunityNews, ref imageurl);
                if (_status != -1)
                {
                    if (imageurl != "")
                    {
                        image.Save(ConfigurationManager.AppSettings["uploadPath"] + "\\CommunityNews\\normalimages\\" + imageurl);

                        image.Resize(130, 130, true, false);
                        image.Crop(1, 1, 1, 1);
                        image.Save(ConfigurationManager.AppSettings["uploadPath"] + "\\CommunityNews\\thumbimages\\" + imageurl);

                    }
                    //TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Banner Uploaded successfully.</div>";
                    //return RedirectToAction("Index", "CommunityNews");
                    if (_status == 1)
                    {
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully.</div>";
                    }
                    else if (_status == 2)
                    {
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                    }
                    return RedirectToAction("Index", "CommunityNews");
                }

                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\"Uploading Failed.</div>";
                    return RedirectToAction("Index", "CommunityNews");
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
                return RedirectToAction("Index", "CommunityNews");
            }
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult EditCommunityNews(Int64 id)
        {
           
            try
            {
                
              
                int _qstatus = 0;
                Entities.CommunityNews _objCommunityNews = _CommunityNews.GetCommunityNewsById(id, ref _qstatus);

                if (_qstatus == 1)
                {
                    ViewBag.objCommunityNews = _objCommunityNews;
                    ViewBag.status = _qstatus;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "CommunityNews");
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("Index", "CommunityNews");
            }
            return View();
        }


        [HttpGet]
        public ActionResult AddCommunityNews()
        {
            return View();
        }

        public ActionResult ViewCommunityNews(Int64 id)
        {
            int status = 0;
            Entities.CommunityNews objCommunityNews = new Entities.CommunityNews();
            try
            {
                objCommunityNews = _CommunityNews.GetCommunityNewsById(id, ref status);

                if (objCommunityNews == null)
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Sorry, failed processing your request.</div>";
                    return RedirectToAction("Index", "CommunityNews");
                }

            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
                return RedirectToAction("Index", "CommunityNews");
            }

            ViewBag.objCommunityNews = objCommunityNews;
            return View();
        }

        [HttpPost]
        public JsonResult DeleteCommunityNews(Int64 id, string imgsrc = "")
        {
            string str = "";
            try
            {
                Int64 _status = _CommunityNews.DeleteCommunityNews(id);
                if (_status == 1)
                {
                    if(imgsrc!="")
                    { 
                    
                    System.IO.File.Delete(ConfigurationManager.AppSettings["uploadpath"] + @"\\CommunityNews\\normalimages\\" + imgsrc);
                    System.IO.File.Delete(ConfigurationManager.AppSettings["uploadpath"] + @"\\CommunityNews\\thumbimages\\" + imgsrc);
                    
                    }
                    str = "<div class=\"alert alert-success alert-dismissable\">Successfully Deleted</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting item</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch (Exception ex)
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + ".</div>";
                return Json(new { ok = false, data = str });
            }
        }

        [HttpPost]
        public JsonResult CommunityNewstatus(Int64 id)
        {
            string str = "";
            try
            {
                Int64 _status = _CommunityNews.UpdateCommunityNewsStatus(id);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Updated status successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed updating status</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch (Exception ex)
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
                return Json(new { ok = false, data = str });
            }
        }

        [HttpPost]
        public JsonResult CommunityNewsOrderNo(int orderno, Int64 id)
        {
            string str = "";
            try
            {
                Int64 _status = _CommunityNews.UpdateCommunityNewsOrderNo(orderno, id);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Updated order no successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed updating status</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch (Exception ex)
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
                return Json(new { ok = false, data = str });
            }
        }

    }
}
