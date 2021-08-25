using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Web.Helpers;
using System.IO;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,Greetings")]
    public class GreetingsController : Controller
    {
        TCAssociationTool.BLL.Greetings _Greetings = new TCAssociationTool.BLL.Greetings();

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult GreetingsList(string search = "", int PageNo = 1, int PageItems = 10, string SortColumn = "", string SortOrder = "")
        {
            int Total = 0;
            List<Entities.Greetings> Greetingslist = new List<Entities.Greetings>();
            try
            {
                string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");

                Greetingslist = _Greetings.GetGreetingsListByVariable(HttpUtility.UrlDecode(search.Trim()), Sort, PageNo, PageItems, ref Total);

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
            ViewBag.Greetingslist = Greetingslist;
            return View();
        }

        [HttpPost]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult AddGreetings(Entities.Greetings objGreetings, HttpPostedFileBase imgsrc)
        {
            try
            {

                if (imgsrc == null)
                {
                    string imageurl = "";
                   
                    Int64 _status = _Greetings.InsertGreetings(objGreetings, ref imageurl);
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
                        return RedirectToAction("Index", "Greetings");

                    }
                }
                else
                {
                    imgsrc.SaveAs(ConfigurationManager.AppSettings["uploadPath"] + "\\Greetings\\NormalImages\\" + imgsrc.FileName);
                    WebImage image = new WebImage(ConfigurationManager.AppSettings["uploadPath"] + "\\Greetings\\NormalImages\\" + imgsrc.FileName);
                    string imageurl = (image != null ? image.ImageFormat : "NA");



                    //var image = WebImage.GetImageFromRequest();
                    //string imageurl = (image != null ? image.ImageFormat : "NA");
                    //objGreetings.UpdatedBy = this.User.Identity.Name;
                    Int64 _status = _Greetings.InsertGreetings(objGreetings, ref imageurl);
                    if (_status != -1)
                    {

                        if (imageurl != "")
                        {
                            image.Save(ConfigurationManager.AppSettings["uploadPath"] + "\\Greetings\\NormalImages\\" + imageurl);

                            //image.Resize(130, 130, true, false);
                            //image.Crop(1, 1, 1, 1);
                            image.Save(ConfigurationManager.AppSettings["uploadPath"] + "\\Greetings\\ThumbImages\\" + imageurl);
                            System.IO.File.Delete(ConfigurationManager.AppSettings["uploadpath"] + "\\Greetings\\NormalImages\\" + imgsrc.FileName);

                        }
                        if (_status == 1)
                        {
                            TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully.</div>";
                        }
                        else if (_status == 2)
                        {
                            TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                        }
                        return RedirectToAction("Index", "Greetings");

                    }

                    else
                    {
                        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\"Uploading Failed.</div>";
                        return RedirectToAction("Index", "Greetings");
                    }

                }
                return RedirectToAction("Index", "Greetings");

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("Index", "Greetings");
            }
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult EditGreetings(Int64 id)
        {
            string str = "";
            int status = 0;
            try
            {
                Entities.Greetings objGreetings = _Greetings.GetGreetingsById(id, ref status);
                if (objGreetings != null)
                {
                    return Json(new { ok = true, data = objGreetings });
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

        public JsonResult DeleteBanner(Int64 id, string imgsrc = "")
        {
            string str = "";
            try
            {
                Int64 _status = _Greetings.DeleteGreetings(id);
                if (_status == 1)
                {
                    System.IO.File.Delete(ConfigurationManager.AppSettings["uploadpath"] + @"\\Greetings\\NormalImages\\" + imgsrc);
                    System.IO.File.Delete(ConfigurationManager.AppSettings["uploadpath"] + @"\\Greetings\\ThumbImages\\" + imgsrc);
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
        public JsonResult GreetingsStatus(Int64 id)
        {
            string str = "";
            try
            {
                Int64 _status = _Greetings.UpdateGreetingsStatus(id);
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

      
        public ActionResult Crop(string CategoryName = "", string filename = "", int top = 0, int left = 0, int bottom = 0, int right = 0)
        {
            var image = new WebImage(Path.Combine(ConfigurationManager.AppSettings["uploadPath"] + "\\Greetings\\NormalImages\\", filename));
            if (image != null)
            {
                image.Crop(top, left, bottom, right);
                image.Resize(145, 80, true, false);
                image.Crop(1, 1, 1, 1);
                image.Save(Path.Combine(ConfigurationManager.AppSettings["uploadPath"] + "\\Greetings\\ThumbImages\\" + filename));

            }
            return RedirectToAction("Index", "Greetings", new { Type = "close", CategoryName = CategoryName });
        }

        public ActionResult Preview(Int64 id = 0, string filename = "", int imgheight = 0, int imgwidth = 0)
        {
            int status = 0;
            Entities.Greetings objGreetings = new Entities.Greetings();
            try
            {
                objGreetings = _Greetings.GetGreetingsById(id, ref status);
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }

            var image = new WebImage(Path.Combine(ConfigurationManager.AppSettings["uploadPath"] + "\\Greetings\\NormalImages\\" + objGreetings.imgsrc));
            ViewBag.filename = objGreetings.imgsrc;
            ViewBag.imgheight = image.Height;
            ViewBag.imgwidth = image.Width;
            return View();
        }

    }
}
