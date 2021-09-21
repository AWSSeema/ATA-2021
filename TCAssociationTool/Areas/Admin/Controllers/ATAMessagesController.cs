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
    [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,Administrator,")]
    [TCAssociationTool.Models.SessionClass.SessionExpireFilter]
    public class ATAMessagesController : Controller
    {
        TCAssociationTool.BLL.ATAMessages _ATAMessages = new TCAssociationTool.BLL.ATAMessages();
        List<Entities.ATAMessages> lstATAMessages = new List<Entities.ATAMessages>();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ATAMessagesList(string search = "", int PageNo = 1, int PageItems = 10, string SortColumn = "", string SortOrder = "")
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;
            try
            {

                lstATAMessages = _ATAMessages.GetATAMessagesListByVariable(HttpUtility.UrlDecode(search.Trim()), Sort, PageNo, PageItems, ref Total);

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
            ViewBag.lstATAMessages = lstATAMessages;
            return View();
        }

        [HttpPost]
        public ActionResult AddATAMessages(Entities.ATAMessages objATAMessages, HttpPostedFileBase attachment)
        {
            try
            {

                var image = (attachment == null ? "NA" : Path.GetExtension(attachment.FileName));

                objATAMessages.status2 = true;
                Int64 _status = _ATAMessages.InsertATAMessages(objATAMessages, ref image);
                if (_status != -1)
                {
                    if (image != "")
                    {
                     
                        string FilePath = Path.Combine(ConfigurationManager.AppSettings["uploadPath"] + "\\ATAMessages\\", image);
                        attachment.SaveAs(FilePath);
                      

                    }
                    //TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Banner Uploaded successfully.</div>";
                    //return RedirectToAction("Index", "ATAMessages");
                    if (_status == 1)
                    {
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully.</div>";
                    }
                    else if (_status == 2)
                    {
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                    }
                    return RedirectToAction("Index", "ATAMessages");
                }

                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\"Uploading Failed.</div>";
                    return RedirectToAction("Index", "ATAMessages");
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
                return RedirectToAction("Index", "ATAMessages");
            }
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult EditATAMessages(Int64 id)
        {

            try
            {


                int _qstatus = 0;
                Entities.ATAMessages _objATAMessages = _ATAMessages.GetATAMessagesById(id, ref _qstatus);

                if (_qstatus == 1)
                {
                    ViewBag.objATAMessages = _objATAMessages;
                    ViewBag.status = _qstatus;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "ATAMessages");
                }
            }
            catch(Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("Index", "ATAMessages");
            }
            return View();
        }


        [HttpGet]
        public ActionResult AddATAMessages()
        {
            return View();
        }

        public ActionResult ViewATAMessages(Int64 id)
        {
            int status = 0;
            Entities.ATAMessages objATAMessages = new Entities.ATAMessages();
            try
            {
                objATAMessages = _ATAMessages.GetATAMessagesById(id, ref status);

                if (objATAMessages == null)
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Sorry, failed processing your request.</div>";
                    return RedirectToAction("Index", "ATAMessages");
                }

            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
                return RedirectToAction("Index", "ATAMessages");
            }

            ViewBag.objATAMessages = objATAMessages;
            return View();
        }

        [HttpPost]
        public JsonResult DeleteATAMessages(Int64 id, string attachment = "")
        {
            string str = "";
            try
            {
                Int64 _status = _ATAMessages.DeleteATAMessages(id);
               
                if (_status == 1)
                {
                    if (attachment != "")
                    {
                        System.IO.File.Delete(ConfigurationManager.AppSettings["uploadpath"] + @"\\ATAMessages\\" + attachment);
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
        public JsonResult ATAMessagestatus(Int64 id)
        {
            string str = "";
            try
            {
                Int64 _status = _ATAMessages.UpdateATAMessagesStatus(id);
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
        public JsonResult ATAMessagesOrderNo(int orderno, Int64 id)
        {
            string str = "";
            try
            {
                Int64 _status = _ATAMessages.UpdateATAMessagesOrderNo(orderno, id);
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
