using TCAssociationTool.Areas.Admin.Models;
using TCAssociationTool.Models;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRCoder;
using ClosedXML.Excel;
using System.Data;

namespace TCAssociationTool.Areas.Admin.Controllers
{
     [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,Events,SubAdmin")]
    public class EventRegistrationsController : Controller
    { 
        BLL.Events _Events = new BLL.Events();
        DAL.Events _DEvents = new DAL.Events();
        BLL.MailTemplates _MailTemplates = new BLL.MailTemplates();
        List<Entities.EventUserInfo> lstEventUserInfo = new List<Entities.EventUserInfo>();
        List<Entities.EventRegistrationCounts> lstRegistrationCount = new List<Entities.EventRegistrationCounts>();
        Entities.EventUserInfo objEventUserInfo = new Entities.EventUserInfo();
        BLL.Members _Members = new BLL.Members();
        Entities.Members objMembers = new Entities.Members();
        BLL.AppInfo _AppInfo = new BLL.AppInfo();

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index(Int64 EventId = 0 , string EventName="")
        {
           
            ViewBag.EventId = EventId;
            ViewBag.EventName = EventName;
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult EventUsersList(Int64 EventId = 0, string EventCategory = "", string Search = "", string SortColumn = "InsertedTime", string SortOrder = "DESC", int PageNo = 1, int Items = 20)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;
            try
            {
                lstEventUserInfo = _Events.GetEventUserInfoListByVariable(EventId, EventCategory,Search, Sort, PageNo, Items, ref Total);
               
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }

            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstEventUserInfo = lstEventUserInfo;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Registrations()
        {
            int Status = 0;
            List<Entities.Events> lstEvents = _Events.RegistrationGetEventsList(ref Status);
            ViewBag.lstEvents = lstEvents;
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult RegistrationsEventUsersList(Int64 EventId = 0, string EventCategory = "Cultural Events", string Search = "", string SortColumn = "InsertedTime", string SortOrder = "DESC", int PageNo = 1, int Items = 20)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;
            try
            {
                lstEventUserInfo = _Events.RegistrationGetEventUserInfoListByVariable(EventId, EventCategory, Search, Sort, PageNo, Items, ref Total);

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }

            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstEventUserInfo = lstEventUserInfo;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();
            return View();
        }


        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult EditEventUser(Int64 EventUserInfoId = 0)
        {
            Int64 EventId = 0;
            try
            {
                int qstatus = 0;
                objMembers = _Members.AddMembershipRequirement(ref qstatus);

                if (qstatus == 1)
                {
                    ViewBag.objMembers = objMembers;
                    ViewBag.status = qstatus;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "EventRegistrations");
                }

                int _qstatus = 0;
                List<Entities.EventRegistrationCounts> lstEventRegistrationCounts = new List<Entities.EventRegistrationCounts>();
                List<Entities.EventsTickets> lstEventsTickets = new List<Entities.EventsTickets>();
                Entities.Events objEvents = new Entities.Events();
                objEventUserInfo = _Events.GetEventUserInfoFullDetailsById(EventUserInfoId, EventId, ref objEvents, ref lstEventRegistrationCounts, ref lstEventsTickets, ref _qstatus);
                if (_qstatus == 1)
                {
                    ViewBag.lstEventRegistrationCounts = lstEventRegistrationCounts;
                    ViewBag.objEventUserInfo = objEventUserInfo;
                    ViewBag.status = _qstatus;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "EventRegistrations");
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("Index", "EventRegistrations");
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateEventUser(Entities.EventUserInfo objEventUserInfo, string PaymentType = "", string type = "")
        {
            Entities.Events objEvents = new Entities.Events();
            List<Entities.EventRegistrationTypes> lstEventRegistrationTypes = new List<Entities.EventRegistrationTypes>();
            List<Entities.EventsTickets> lstEventsTickets = new List<Entities.EventsTickets>();
            List<Entities.EventRegistrationCounts> lstEventRegistrationCounts = new List<Entities.EventRegistrationCounts>();
            Entities.EventUserInfo objEventUserInfo1 = new Entities.EventUserInfo();
            int result1 = 0;
            int status = 0;
            try
            {

                TCAssociationTool.Entities.AppInfo objAppInfo = new TCAssociationTool.Entities.AppInfo();
                TCAssociationTool.BLL.AppInfo _AppInfo = new TCAssociationTool.BLL.AppInfo();
                objAppInfo = _AppInfo.GetAppInfoDetails(ref status);

                objEventUserInfo.UpdatedBy = HttpContext.User.Identity.Name.ToString();
                objEventUserInfo.InsertedBy = HttpContext.User.Identity.Name.ToString();
                Int64 _status = _Events.UpdateEventUserInfo(objEventUserInfo);

                Int64 EventId = objEventUserInfo.EventId;
                objEventUserInfo1 = _Events.GetEventUserInfoFullDetailsById(objEventUserInfo.EventUserInfoId, EventId, ref objEvents, ref lstEventRegistrationCounts, ref lstEventsTickets, ref result1);


                if (_status != -1)
                {
                    if (_status == 1)
                    {
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully.</div>";
                    }
                    else if (_status == 2)
                    {
                        if (PaymentType == "Completed")
                        {
                            var attachment = GetAttachment(objEventUserInfo1, objEvents, lstEventsTickets);

                            Entities.MailTemplates objMailTemplates = _MailTemplates.GetMailTemplateById("Confirm Event Registration", 0, ref status);
                            if (objMailTemplates != null)
                            {
                                StringBuilder body = new StringBuilder();

                                body.Append(objMailTemplates.Description);
                                body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                                body.Replace("[EUrl]", objAppInfo.SupportEmail);
                                body.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                                body.Replace("[InUrl]", objAppInfo.TwitterUrl);
                                body.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                                body.Replace("[TPhone]", objAppInfo.CompanyPhone);
                                body.Replace("[TEmail]", objAppInfo.CompanyEmail);
                                body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objEventUserInfo1.FirstName + " " + objEventUserInfo1.LastName));
                                body.Replace("[EventName]", BLL.Common.UppercaseFirst(objEventUserInfo1.EventName));
                                body.Replace("[TransactionId]", objEventUserInfo1.objEventOrderDetails.TransactionId);
                                body.Replace("[PaymentType]", objEventUserInfo1.objEventOrderDetails.PaymentMethod);
                                body.Replace("[PaymentStatus]", objEventUserInfo1.objEventOrderDetails.PaymentStatus);
                                body.Replace("[PaymentDate]", DateTime.UtcNow.ToString());
                                body.Replace("[AmountPaid]", objEventUserInfo1.objEventOrderDetails.AmountPaid.ToString());
                                body.Replace("[SiteName]", objAppInfo.SiteName);

                                //BLL.Common.SendMailwithAttachment(objEventUserInfo.Email, objMailTemplates.Subject, body.ToString(), attachment);
                                var sts = BLL.Common.SendMailwithAttachment(objEventUserInfo1.Email, objMailTemplates.Subject, body.ToString(), "tickets-" + BLL.Common.EncodeURL(objEventUserInfo1.EventName) + "-" + objEventUserInfo1.EventUserInfoId + ".pdf");
                            }

                            int statusAdmin = 0;
                            Entities.MailTemplates objTemplatesAdmin = _MailTemplates.GetMailTemplateById("Admin Event Registration", 0, ref statusAdmin);
                            if (objTemplatesAdmin != null && objTemplatesAdmin.MailTemplateId != 0)
                            {

                                StringBuilder body1 = new StringBuilder();
                                body1.Append(objTemplatesAdmin.Description);
                                body1.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                                body1.Replace("[EUrl]", objAppInfo.SupportEmail);
                                body1.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                                body1.Replace("[InUrl]", objAppInfo.TwitterUrl);
                                body1.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                                body1.Replace("[TPhone]", objAppInfo.CompanyPhone);
                                body1.Replace("[TEmail]", objAppInfo.CompanyEmail);
                                body1.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objEventUserInfo1.FirstName + " " + objEventUserInfo1.LastName));
                                body1.Replace("[EventName]", BLL.Common.UppercaseFirst(objEventUserInfo1.EventName));
                                body1.Replace("[Email]", objEventUserInfo1.Email);
                                body1.Replace("[TransactionId]", objEventUserInfo1.objEventOrderDetails.TransactionId);
                                body1.Replace("[PaymentType]", objEventUserInfo1.objEventOrderDetails.PaymentMethod);
                                body1.Replace("[PaymentStatus]", objEventUserInfo1.objEventOrderDetails.PaymentStatus);
                                body1.Replace("[PaymentDate]", DateTime.UtcNow.ToString());
                                body1.Replace("[AmountPaid]", objEventUserInfo1.objEventOrderDetails.AmountPaid.ToString());

                                BLL.Common.SendMailwithCC(objAppInfo.CompanyEmail, objAppInfo.CompanyEmail, objAppInfo.CompanyEmail, "Registered Event Details - Gitanjali Art of Giving", body1.ToString());
                            }

                        }
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                    }
                    if (type == "Ticket Master")
                    {
                        return RedirectToAction("TicketMaster", "EventRegistrations", new { eid = objEvents.EventId, ename = objEvents.EventName, tid = objEventUserInfo1.objEventOrderDetails.TransactionId });
                    }
                    else
                    {
                        return RedirectToAction("EditEventUser", "EventRegistrations", new { EventUserInfoId = objEventUserInfo.EventUserInfoId });
                    }
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed Transaction.</div>";

                    if (type == "Ticket Master")
                    {
                        return RedirectToAction("TicketMaster", "EventRegistrations", new { eid = objEvents.EventId, ename = objEvents.EventName, tid = objEventUserInfo1.TransactionId });
                    }
                    else
                    {
                        return RedirectToAction("EditEventUser", "EventRegistrations", new { EventUserInfoId = objEventUserInfo.EventUserInfoId });
                    }
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";

                if (type == "Ticket Master")
                {
                    return RedirectToAction("TicketMaster", "EventRegistrations", new { eid = objEvents.EventId, ename = objEvents.EventName, tid = objEventUserInfo1.TransactionId });
                }
                else
                {
                    return RedirectToAction("EditEventUser", "EventRegistrations", new { EventUserInfoId = objEventUserInfo.EventUserInfoId });
                }
            }

        }


        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult ViewEventUser(Int64 EventUserInfoId = 0)
        {
            Int64 EventId = 0;
            try
            {

                int _qstatus = 0;
                List<Entities.EventRegistrationCounts> lstEventRegistrationCounts = new List<Entities.EventRegistrationCounts>();
                List<Entities.EventsTickets> lstEventsTickets = new List<Entities.EventsTickets>();
                Entities.Events objEvents = new Entities.Events();
                objEventUserInfo = _Events.GetEventUserInfoFullDetailsById(EventUserInfoId, EventId, ref objEvents, ref lstEventRegistrationCounts, ref lstEventsTickets, ref _qstatus);

                if (_qstatus == 1)
                {
                    ViewBag.lstEventRegistrationCounts = lstEventRegistrationCounts;
                    ViewBag.objEventUserInfo = objEventUserInfo;
                    ViewBag.status = _qstatus;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "EventRegistrations");
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("Index", "EventRegistrations");
            }
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public JsonResult DeleteEventUser(Int64 EventUserInfoId)
        {
            string str = "";
            try
            {
               
                Int64 _status = _Events.DeleteEventUserInfoById(EventUserInfoId);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting user</div>";
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
        public JsonResult EventUserInfoStatus(Int64 EventUserInfoId)
        {
            string str = "";
            try
            {
                Int64 _status = _Events.UpdateEventUserInfoStatus(EventUserInfoId);
                int status = 0;
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Updated Status Successfully</div>";
                    Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("Confirm Event Registration", 0, ref status);
                    if (objTemplates != null)
                    {
                        Entities.EventUserInfo objEventUserInfo = _Events.GetEventUserInfoById(EventUserInfoId,ref status);
                        StringBuilder body = new StringBuilder();
                        if (objEventUserInfo.IsApproved == true)
                        {
                            body.Append(objTemplates.Description);
                            body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["usersiteurl"].ToString());
                            body.Replace("[FBUrl]", ConfigurationManager.AppSettings["FBUrl"].ToString());
                            body.Replace("[TWUrl]", ConfigurationManager.AppSettings["TWUrl"].ToString());
                            body.Replace("[YUrl]", ConfigurationManager.AppSettings["YUrl"].ToString());
                            body.Replace("[TPhoneNo]", ConfigurationManager.AppSettings["TPhoneNo"].ToString());
                            body.Replace("[TEmailId]", ConfigurationManager.AppSettings["TEmailId"].ToString());
                            body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objEventUserInfo.FirstName + "" + objEventUserInfo.LastName));
                            body.Replace("[EVENTNAME]", BLL.Common.UppercaseFirst(objEventUserInfo.EventName));
                            BLL.Common.SendMail(objEventUserInfo.Email, objTemplates.Subject, body.ToString());
                        }
                    }
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed updating user status</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch 
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = true, data = str });
            }
        }

        #region export to excel

        public void EventuserExporttoExcel(Int64 EventId = 0, string Search = "", string SortColumn = "InsertedTime", string SortOrder = "DESC")
        {
            try
            {
                string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
                DataTable dtUni = _DEvents.ExportToEventUserInfoList(EventId, Search, Sort);
                //MemoryStream stream = BLL.Common.CSVUtility.GetCSV(dtUni);

                //var filename = "CSV-Donor-Registration-Export-" + DateTime.UtcNow.ToString("dd-MM-yyyy") + ".csv";
                //var contenttype = "text/csv";
                //Response.Clear();
                //Response.ContentType = contenttype;
                //Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.BinaryWrite(stream.ToArray());
                //Response.End();

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dtUni, "Donor-Registration-Export");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=CSV-EventUsers-Export-" + DateTime.UtcNow.ToString("dd-MM-yyyy") + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
            }
        }

        #endregion

        #region EventTicketMaster

        //[Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult TicketMaster(Int64 eid = 0, string ename = "", string tid = "")
        {
            if (Session["username"] == null)
            {
                return new RedirectResult("admin-login.html?returnurl=" + BLL.Common.Encode(Request.Url.AbsoluteUri));
            }

            ViewBag.ename = ename;
            ViewBag.eid = eid;
            ViewBag.tid = tid;
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult EventTicketMasterList(Int64 EventId = 0, string Mobile = "", string Search = "", string SortColumn = "EventRegCountId", string SortOrder = "DESC", int PageNo = 1, int Items = 20)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;
            int QStatus = 0;
            Int64 EventUserInfoId = 0;
            Entities.EventUserInfo objEventUserInfo = new Entities.EventUserInfo();

            Entities.Members objMembers = new Entities.Members();

            List<Entities.PaymentStatus> lstPaymentStatus = new List<Entities.PaymentStatus>();
            try
            {

                lstRegistrationCount = _Events.EventTicketMasterGetListByVariable(EventId, Mobile, Search, Sort, PageNo, Items, ref Total);

                int _qstatus = 0;
                objMembers = _Members.AddMembershipRequirement(ref _qstatus);

                objEventUserInfo = _Events.GetEventUserInfoFullDetailsByMobile(EventUserInfoId, EventId, Mobile, Search, ref QStatus);

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }

            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstRegistrationCount = lstRegistrationCount;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();
            ViewBag.objEventUserInfo = objEventUserInfo;
            ViewBag.objMembers = objMembers;
            return View();
        }

        [HttpPost]
        public JsonResult EventTicketMasterVisitCountUpdate(int VisitCount, Int64 EventRegCountId)
        {
            string str = "";
            try
            {
                Int64 _status = _Events.EventTicketMasterVisitCountUpdate(VisitCount, EventRegCountId);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Updated Visit Count Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed updating Visit Count</div>";
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

        #region Sending_Ticket

        [HttpPost]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public JsonResult SendTickets(Int64 EventUserInfoId)
        {
            string str = "";
            int result1 = 0;
            int status1 = 0;
            Entities.Events objEvents = new Entities.Events();
            List<Entities.EventRegistrationTypes> lstEventRegistrationTypes = new List<Entities.EventRegistrationTypes>();
            List<Entities.EventsTickets> lstEventsTickets = new List<Entities.EventsTickets>();
            List<Entities.EventRegistrationCounts> lstEventRegistrationCounts = new List<Entities.EventRegistrationCounts>();
            Entities.EventUserInfo objEventUserInfo = new Entities.EventUserInfo();
            try
            {
                TCAssociationTool.Entities.AppInfo objAppInfo = new TCAssociationTool.Entities.AppInfo();
                TCAssociationTool.BLL.AppInfo _AppInfo = new TCAssociationTool.BLL.AppInfo();
                objAppInfo = _AppInfo.GetAppInfoDetails(ref status1);

                Int64 EventId = objEventUserInfo.EventId;
                objEventUserInfo = _Events.GetEventUserInfoFullDetailsById(EventUserInfoId, EventId, ref objEvents, ref lstEventRegistrationCounts, ref lstEventsTickets, ref result1);

                if (EventUserInfoId != 0)
                {
                    var attachment = GetAttachment(objEventUserInfo, objEvents, lstEventsTickets);
                    int status = 0;
                    Entities.MailTemplates objMailTemplates = _MailTemplates.GetMailTemplateById("Confirm Event Registration", 0, ref status);
                    if (objMailTemplates != null)
                    {
                        StringBuilder body = new StringBuilder();

                        body.Append(objMailTemplates.Description);
                        body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                        body.Replace("[EUrl]", objAppInfo.SupportEmail);
                        body.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                        body.Replace("[InUrl]", objAppInfo.TwitterUrl);
                        body.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                        body.Replace("[TPhone]", objAppInfo.CompanyPhone);
                        body.Replace("[TEmail]", objAppInfo.CompanyEmail);
                        body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objEventUserInfo.FirstName + " " + objEventUserInfo.LastName));
                        body.Replace("[EventName]", BLL.Common.UppercaseFirst(objEventUserInfo.EventName));
                        body.Replace("[TransactionId]", objEventUserInfo.objEventOrderDetails.TransactionId);
                        body.Replace("[PaymentType]", objEventUserInfo.objEventOrderDetails.PaymentMethod);
                        body.Replace("[PaymentStatus]", objEventUserInfo.objEventOrderDetails.PaymentStatus);
                        body.Replace("[PaymentDate]", DateTime.UtcNow.ToString());
                        body.Replace("[AmountPaid]", objEventUserInfo.objEventOrderDetails.AmountPaid.ToString());
                        body.Replace("[SiteName]", objAppInfo.SiteName);

                        var sts = BLL.Common.SendMailwithAttachment(objEventUserInfo.Email, objMailTemplates.Subject, body.ToString(), "tickets-" + BLL.Common.EncodeURL(objEventUserInfo.EventName) + "-" + objEventUserInfo.EventUserInfoId + ".pdf");
                    }

                    str = "<div class=\"alert alert-success alert-dismissable\">Ticket(s) sent successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed Ticket(s) sening</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch (Exception ex)
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }

        private Attachment GetAttachment(Entities.EventUserInfo objEventUserInfo, Entities.Events objEvents, List<Entities.EventsTickets> lstEventsTickets)
        {

            //objEvents = _Events.FEGetEventById(EventId, "", ref lstEventRegistrationTypes, ref status);

            var file = ConvertHtmlToPDF(objEventUserInfo, objEvents, lstEventsTickets);

            FileStream file1 = new FileStream(ConfigurationManager.AppSettings["uploadPath"] + "\\events\\TicketImage\\tickets-" + BLL.Common.EncodeURL(objEventUserInfo.EventName) + "-" + objEventUserInfo.EventUserInfoId + ".pdf", FileMode.Create, FileAccess.Write);
            file.WriteTo(file1);
            file1.Close();

            file.Seek(0, SeekOrigin.Begin);
            Attachment attachment = new Attachment(file, "tickets-" + BLL.Common.EncodeURL(objEventUserInfo.EventName) + "-" + objEventUserInfo.EventUserInfoId + ".pdf", "application/pdf");
            ContentDisposition disposition = attachment.ContentDisposition;
            disposition.CreationDate = System.DateTime.Now;
            disposition.ModificationDate = System.DateTime.Now;
            disposition.DispositionType = DispositionTypeNames.Attachment;
            return attachment;
        }

        private MemoryStream ConvertHtmlToPDF(Entities.EventUserInfo objEventUserInfo, Entities.Events objEvents, List<Entities.EventsTickets> lstEventsTickets)
        {
            //Create a MemoryStream which will hold the data
            var ms = new MemoryStream();
            int status = 0;
            string pdffilepath = "";
            try
            {
                Entities.AppInfo objAppInfo = _AppInfo.GetAppInfoDetails(ref status);
                logreport("Started ticket generation", Request.Path, objEventUserInfo.FirstName);
                //Create an iTextSharp Document
                using (var doc = new iTextSharp.text.Document())
                {
                    //Create a pdf writer that will hold the instance of PDF abstraction which is doc and the memory stream
                    var writer = PdfWriter.GetInstance(doc, ms);

                    //Open the document for writing
                    doc.Open();
                    int i = 1;



                    #region Ticket Image Generation
                    System.Drawing.Image orgImg;

                    if (objEvents.TopLine != null && objEvents.TopLine != "")
                    {
                        string filePath = Path.Combine(ConfigurationManager.AppSettings["uploadPath"] + "\\events\\TicketImage\\", objEvents.TopLine);
                        orgImg = System.Drawing.Image.FromFile(filePath);
                    }
                    else
                    {
                        string filePath = Path.Combine(ConfigurationManager.AppSettings["uploadPath"] + "\\events\\banners\\", "no-image.png");
                        orgImg = System.Drawing.Image.FromFile(filePath);
                    }

                    string filePath1 = Path.Combine(ConfigurationManager.AppSettings["uploadPath"] + "\\Tickets\\", "event-img-bg.png");
                    System.Drawing.Image orgImg1 = System.Drawing.Image.FromFile(filePath1);

                    //Load the Image to be written on.
                    Bitmap bitMapImage = new System.Drawing.Bitmap(Server.MapPath("~/Content/Tickets/ticketsample.jpg"));
                    Graphics graphicImage = Graphics.FromImage(bitMapImage);

                    // Pasting Image
                    graphicImage.DrawImage(orgImg, 11, 69, 233, 233);

                    //Smooth graphics is nice.
                    graphicImage.SmoothingMode = SmoothingMode.AntiAlias;

                    PrivateFontCollection pfcoll = new PrivateFontCollection();
                    //put a font file under a Fonts directory within your application root
                    pfcoll.AddFontFile(Server.MapPath("~/Content/User/font/FREE3OF9.TTF"));
                    FontFamily ff = pfcoll.Families[0];

                    // Barcode
                    //graphicImage.DrawString(ConfigurationManager.AppSettings["baseurl"].ToString() + "ticket-master.html?eid=" + objEvents.EventId + "&ename=" + objEvents.EventName + "&ph=" + objEventUserInfo.Mobile, new Font(ff, 35), Brushes.Black, new Point(302, 116));

                    //QRCode

                    pdffilepath = System.Configuration.ConfigurationManager.AppSettings["baseurl"].ToString() + "ticket-master.html?eid=" + objEvents.EventId + "&ename=" + objEvents.EventName + "&tid=" + objEventUserInfo.objEventOrderDetails.TransactionId;

                    string code = pdffilepath;
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                    System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                    imgBarCode.Height = 140;
                    imgBarCode.Width = 140;
                    string strB = System.Configuration.ConfigurationManager.AppSettings["uploadPath"] + "\\EventProfile\\Profiledocs\\QR-of-" + objEventUserInfo.EventUserInfoId + ".jpg";
                    using (Bitmap bitMap = qrCode.GetGraphic(20))
                    {
                        using (MemoryStream msa = new MemoryStream())
                        {
                            bitMap.Save(msa, System.Drawing.Imaging.ImageFormat.Png);
                            byte[] byteImage = msa.ToArray();
                            imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                            bitMap.Save(strB);
                        }
                    }

                    string QRCode = System.Configuration.ConfigurationManager.AppSettings["uploadPath"] + "\\EventProfile\\Profiledocs\\QR-of-" + objEventUserInfo.EventUserInfoId + ".jpg";

                    System.Drawing.Image QRImage = System.Drawing.Image.FromFile(QRCode);
                    graphicImage.DrawImage(QRImage, 296, 63, 110, 110);

                    // Barcode
                    //graphicImage.DrawString(ConfigurationManager.AppSettings["baseurl"].ToString() + "Admin/EventRegistrations/TicketMaster?eid=" + objEvents.EventId + "&ename=" + objEvents.EventName + "&ph=" + objEventUserInfo.Mobile, new Font("Arial", 12), Brushes.Black, new Point(317, 150));


                    // Ticket Id
                    graphicImage.DrawString("Ticket Id: " + objEventUserInfo.objEventOrderDetails.TransactionId, new Font("Arial", 16, FontStyle.Bold), Brushes.White, new Point(318, 24));

                    // Event Name
                    graphicImage.DrawString(objEvents.EventName, new Font("Arial", 18, FontStyle.Bold), Brushes.Yellow, new Point(11, 24));





                    // Price
                    if (objEventUserInfo.TotalAmount > 0)
                    {
                        string pricepath = Path.Combine(ConfigurationManager.AppSettings["uploadPath"] + "\\Tickets\\", "price-img.png");
                        System.Drawing.Image priceImg2 = System.Drawing.Image.FromFile(pricepath);
                        graphicImage.DrawImage(priceImg2, 677, 182, 111, 111);
                        graphicImage.DrawString("$" + Math.Round((objEventUserInfo.TotalAmount), 0).ToString(), new Font("Arial", 20, FontStyle.Bold), Brushes.White, new Point(707, 238));
                    }

                    // Write Phone
                    //graphicImage.DrawString("+1 " + objAppInfo.CompanyPhone, new Font("Arial", 14, FontStyle.Bold), Brushes.White, new Point(233, 412));

                    // Write Email
                    //graphicImage.DrawString(objAppInfo.CompanyEmail, new Font("Arial", 12, FontStyle.Bold), Brushes.White, new Point(483, 412));

                    // Start Date End Date
                    graphicImage.DrawString(@objEvents.StartDate.ToString("dddd") + " , " + @objEvents.StartDate.ToString("MMM dd, yyyy"), new Font("Arial", 12, FontStyle.Bold), Brushes.White, new Point(280, 215));

                    //// Address
                    if (objEvents.Location != "" && objEvents.Location != null)
                    {
                        graphicImage.DrawString(objEvents.Location, new Font("Arial", 10, FontStyle.Bold), Brushes.White, new Point(276, 289));
                        // City, State, Country, Zip
                        graphicImage.DrawString(objEvents.City + (objEvents.City != null && objEvents.City != "" ? ", " : "") + objEvents.StateName + (objEvents.StateName != null && objEvents.StateName != "" ? ", " : "") + objEvents.ZipCode, new Font("Arial", 12, FontStyle.Bold), Brushes.White, new Point(280, 311));
                    }
                    else
                    {
                        graphicImage.DrawString(objEvents.City + (objEvents.City != null && objEvents.City != "" ? ", " : "") + objEvents.StateName + (objEvents.StateName != null && objEvents.StateName != "" ? ", " : "") + objEvents.ZipCode, new Font("Arial", 12, FontStyle.Bold), Brushes.White, new Point(280, 289));
                    }

                    //Ticket Type with Count

                    graphicImage.DrawString(objEventUserInfo.TicketTypesWithCount, new Font("Arial", 14, FontStyle.Bold), Brushes.White, new Point(22, 365));

                    // Short Description
                    //graphicImage.DrawString(objEvents.EventName + " held at " + objEvents.Location + ", " + objEvents.City + ", " + objEvents.StateName, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(81, 395));



                    // Pasting Image
                    graphicImage.DrawImage(orgImg1, 0, 60, 260, 260);

                    //if (objEventUserInfo.MemberId != null && objEventUserInfo.MemberId != 0)
                    //{
                    //    // Member ID
                    //    graphicImage.DrawString(item.RegistrationTitle.ToString(), new Font("Arial", 12, FontStyle.Bold), Brushes.White, new Point(264, 58));
                    //    graphicImage.DrawString("Member #" + " " + objEventUserInfo.EventUserInfoId.ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.White, new Point(264, 85));
                    //}
                    //else
                    //{
                    //    graphicImage.DrawString((item.RegistrationTitle.Length > 33 ? item.RegistrationTitle.Substring(0, 28) + "..." : item.RegistrationTitle.ToString()), new Font("Arial", 16, FontStyle.Bold), Brushes.White, new Point(264, 58));
                    //}

                    // Image Saving
                    //Response.ContentType = "image/jpeg";
                    bitMapImage.Save(Server.MapPath("~/Content/Tickets/" + objEventUserInfo.EventUserInfoId + "-" + objEventUserInfo.FirstName + "-" + objEventUserInfo.LastName + ".jpg"));

                    //Clean house.
                    graphicImage.Dispose();
                    bitMapImage.Dispose();
                    #endregion

                    #region Pdf Witer

                    iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph(" ");
                    string imageURL = System.Configuration.ConfigurationManager.AppSettings["baseurl"] + "Content/Tickets/" + objEventUserInfo.EventUserInfoId + "-" + objEventUserInfo.FirstName + "-" + objEventUserInfo.LastName + ".jpg";
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(500f, 280f);
                    //Give some space after the image
                    jpg.SpacingAfter = 10f;

                    jpg.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                    doc.Add(paragraph);
                    doc.Add(jpg);
                    doc.Add(paragraph);
                    if (i == 1)
                    {
                        i++;
                    }
                    else
                    {
                        doc.NewPage();
                        i = 1;
                    }
                    #endregion


                    writer.CloseStream = false;

                    //close the document
                    doc.Close();
                    //return the stream 
                    logreport("Ticket generation Completed", Request.Path, objEventUserInfo.FirstName);
                }
            }
            catch (Exception ex)
            {
                logreport(ex.Message, Request.Path, objEventUserInfo.FirstName);
            }
            return ms;
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public Byte[] getBarcodeImage(string barcode, string file)
        {
            try
            {
                BarCode39 _barcode = new BarCode39();
                int barSize = 16;
                string fontFile = Server.MapPath("~/Content/font/FREE3OF9.TTF");
                return (_barcode.Code39(barcode, barSize, true, file, fontFile));
            }
            catch (Exception ex)
            {
                //ErrorLog.WriteErrorLog("Barcode", ex.ToString(), ex.Message);
            }
            return null;
        }

        public JsonResult GetAmountEncode(decimal Amount = 0)
        {
            string EAmount = "";
            if (Amount != 0)
            {
                EAmount = BLL.Common.Encrypt(Amount.ToString());
            }
            return Json(new { ok = true, data = EAmount });
        }


        public void logreport(string error = "", string pageName = "", string fileName = "")
        {
            fileName = "Log_" + DateTime.Now.ToString("dd-MM-yyyy") + BLL.Common.EncodeURL(fileName) + ".txt";
            var relativePath = "~/Content/logfiles/" + fileName;
            var filepath = HttpContext.Server.MapPath(relativePath);

            if (System.IO.File.Exists(filepath))
            {
                using (StreamWriter stwriter = new StreamWriter(filepath, true))
                {
                    stwriter.WriteLine("-------------------START-------------" + DateTime.Now);
                    stwriter.WriteLine("Page :" + pageName);
                    stwriter.WriteLine(error);
                    stwriter.WriteLine("-------------------END-------------" + DateTime.Now);
                }
            }
            else
            {
                StreamWriter stwriter = System.IO.File.CreateText(filepath);
                stwriter.WriteLine("-------------------START-------------" + DateTime.Now);
                stwriter.WriteLine("Page :" + pageName);
                stwriter.WriteLine(error);
                stwriter.WriteLine("-------------------END-------------" + DateTime.Now);
                stwriter.Close();
            }
        }

        #endregion
    }
}
