using TCAssociationTool.Areas.User.Models;
using TCAssociationTool.Models;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using PaymentProcess;
using PayPal.Payments.Common.Utility;
using PayPal.Payments.Communication;
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

namespace TCAssociationTool.Areas.API.Controllers
{
    public class EventController : Controller
    {
        BLL.InnerPages _innerpage = new BLL.InnerPages();
        BLL.Events _Events = new BLL.Events();
        BLL.Members _Members = new BLL.Members();
        Entities.Members _objMember = new Entities.Members();
        BLL.AppInfo _AppInfo = new BLL.AppInfo();
        Entities.Events objEvents = new Entities.Events();
        List<Entities.Events> lstEvents = new List<Entities.Events>();
        BLL.MailTemplates _MailTemplates = new BLL.MailTemplates();
        Entities.EventUserInfo objEventUserInfo = new Entities.EventUserInfo();
        BLL.PaymentSettings _paymentSettings = new BLL.PaymentSettings();
        PayByPayPal objPayByPayPal = new PayByPayPal();
        List<Entities.Events> lstCurrentEvents = new List<Entities.Events>();
        List<Entities.Events> lstUpcomingEvents = new List<Entities.Events>();
        List<Entities.Events> lstPastEvents = new List<Entities.Events>();
        List<Entities.EventRegistrationTypes> lstEventRegistrationTypes = new List<Entities.EventRegistrationTypes>();
        TCAssociationTool.BLL.AppInfo _appinfo = new BLL.AppInfo();
        TCAssociationTool.BLL.PaymentSettings _PaymentSettings = new BLL.PaymentSettings();

        Entities.InnerPages objInnerPages = new Entities.InnerPages();

        public ActionResult Index(string Type = "upcoming")
        {
            ViewBag.Type = Type;
            return View();
        }

        public ActionResult EventsList(string Type = "upcoming", string search = "", string SortColumn = "StartDate", string SortOrder = "ASC", int PageNo = 1, int PageItems = 10)
        {
            if (Type == "upcoming")
            {
                SortOrder = "ASC";
            }
            else
            {
                SortOrder = "DESC";
            }
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            List<Entities.Events> lstEvents = new List<Entities.Events>();
            int Total = 0;
            try
            {
                lstEvents = _Events.FEGetEventsList(Type, HttpUtility.UrlDecode(search.Trim()), Sort, PageNo, PageItems, ref Total);

            }
            catch
            {
                return RedirectToAction("Error404", "Error");
            }
            ViewBag.total = Total;
            ViewBag.items = PageItems;
            ViewBag.pageno = PageNo;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder;
            ViewBag.Eventslist = lstEvents;
            ViewBag.Type = Type;
            return View();
        }

        public ActionResult EventRegistration(string EventName = "", Int64 MemberId = 0, Int64 eid = 0)
        {
            int status = 0;
            try
            {
                if (EventName != "")
                {
                    objEvents = _Events.GetEventById(eid, EventName, ref lstEventRegistrationTypes, ref status);
                    if (objEvents != null && objEvents.EventId != 0)
                    {
                        if (this.User.Identity.IsAuthenticated && this.User.Identity.Name != null && HttpContext.Session != null && Session["username"] != null)
                        {
                            _objMember = _Members.GetMemberFullDetailsByEmail(HttpContext.User.Identity.Name.ToString(), ref status);
                        }
                        objInnerPages = _innerpage.GetInnerPagesListById(0, "event registration rules", ref status);

                        MemberId = Convert.ToInt64((Session["MemberID"] != null ? Session["MemberID"] : 0));

                    }
                    else
                    {
                        return RedirectToAction("Error404", "Error");
                    }
                }
            }
            catch
            {
                return RedirectToAction("Error404", "Error");
            }
            ViewBag.InnerPage = objInnerPages;
            ViewBag.Memberdetails = _objMember;
            ViewBag.MemberId = MemberId;
            ViewBag.objEvents = objEvents;
            ViewBag.EventName = EventName;
            ViewBag.lstEventRegistrationTypes = lstEventRegistrationTypes;
            return View();
        }

        // Event Registraion by PayPal
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EventRegistration(Entities.EventUserInfo objEventUserInfo, List<Entities.EventParticipants> lstEventParticipants, List<Entities.EventRegistrationCounts> lstEventRegistrationCounts, string Cheque = "", string PaymentBy = "")
        {
            //Int64 EventRegistrationId = 0;
            Int64 EventUserInfoId = 0;
            try
            {
                var image = WebImage.GetImageFromRequest();
                string imageurl = (image != null ? image.ImageFormat : "NA");

                objEventUserInfo.IsApproved = false;
                objEventUserInfo.UpdatedBy = objEventUserInfo.FirstName;
                objEventUserInfo.InsertedBy = PaymentBy;

                int status2 = 0;
                if (lstEventRegistrationCounts != null && lstEventRegistrationCounts.Count != 0 && lstEventRegistrationCounts[0].EventRegistrationTypeId != 0)
                {
                    objEventUserInfo.XMLRegistrationsCounts = BLL.Common.CreateXMLForObject(lstEventRegistrationCounts);
                }
                Int64 _status = _Events.InsertEventUserInfo(objEventUserInfo, ref EventUserInfoId, ref imageurl);
                Entities.AppInfo objAppInfo = _appinfo.GetAppInfoDetails(ref status2);
                //Entities.PaymentSettings objPaymentSettings = _PaymentSettings.GetPaymentSettings(ref status2);
                if (_status == 1)
                {
                    if (lstEventParticipants != null && lstEventParticipants.Count() != 0)
                    {
                        foreach (Entities.EventParticipants objEventParticipants in lstEventParticipants)
                        {
                            objEventParticipants.EventUserInfoId = EventUserInfoId;
                            objEventParticipants.EventId = objEventUserInfo.EventId;
                            if (objEventParticipants != null && objEventParticipants.FirstName != null)
                            {
                                objEventParticipants.IsApproved = false;
                                objEventParticipants.UpdatedBy = objEventParticipants.FirstName;
                                Int64 estatus = _Events.InsertEventParticipant(objEventParticipants);
                                if (estatus == -1)
                                {
                                    TempData["message"] = "<div class=\"error closable\">Failed inserting event registration.</div>";
                                    return RedirectToAction("EventRegistration", "Event", new { eid = objEventUserInfo.EventId, EventName = BLL.Common.EncodeURL(objEventUserInfo.EventName) });
                                }
                            }
                        }
                    }
                    if (objEventUserInfo.AmountPaid != 0)
                    {
                        if (objEventUserInfo.PaymentMethod == "PayPal")
                        {
                            int status1 = 0;
                            Entities.PaymentSettings objPaymentSettings = _paymentSettings.GetPaymentSettingsDetails(0, "PayPal", ref status1);
                            if (objPaymentSettings != null)
                            {

                                List<PayByPayPal.CartItems> lstCartItems = new List<PayByPayPal.CartItems>();

                                PayByPayPal.CartItems objCartItems = new PayByPayPal.CartItems();
                                objCartItems.ItemName = "Event Type :" + objEventUserInfo.ItemName;
                                objCartItems.ItemPrice = objEventUserInfo.AmountPaid;
                                lstCartItems.Add(objCartItems);

                                objPayByPayPal.lstCartItems = lstCartItems;

                                PayByPayPal.PaymentSettings objpaymentsettings = new PayByPayPal.PaymentSettings();
                                objpaymentsettings.BusinessEmail = objPaymentSettings.PaymentEmail;
                                objpaymentsettings.BusinessPassword = objPaymentSettings.PaymentPassword;
                                objpaymentsettings.CurrencyCode = objPaymentSettings.CurrencyCode;
                                objpaymentsettings.NotifyUrl = ConfigurationManager.AppSettings["baseurl"] + "event/ipn.html" + "?id=" + EventUserInfoId;
                                objpaymentsettings.PaymentUrl = objPaymentSettings.PaymentUrl;
                                objpaymentsettings.ReturnUrl = ConfigurationManager.AppSettings["baseurl"].ToString() + "event-registration-acknowledgement.html" + "?id=" + EventUserInfoId + "&PaymentMethod=" + objEventUserInfo.PaymentMethod;
                                objpaymentsettings.Cmd = "_cart";
                                objpaymentsettings.PDTToken = objPaymentSettings.TokenNo;
                                objpaymentsettings.CBT = "Click here to complete your transaction";
                                objpaymentsettings.CancelUrl = ConfigurationManager.AppSettings["baseurl"] + "index.html";

                                objPayByPayPal.objPaymentSettings = objpaymentsettings;

                                string returnurl = objPayByPayPal.CheckOut();

                                return new RedirectResult(returnurl);
                            }
                        }
                        else if (objEventUserInfo.PaymentMethod == "Cash/Cheque")
                        {
                            Int64 result = 0;

                            objEventUserInfo.PaymentStatusId = 2;
                            objEventUserInfo.PaymentMethodId = 3;
                            objEventUserInfo.AmountPaid = objEventUserInfo.AmountPaid;
                            objEventUserInfo.PaymentMethod = "Cash/Cheque";
                            objEventUserInfo.PaymentStatus = "Pending";
                            objEventUserInfo.TransactionId = EventUserInfoId.ToString();
                            objEventUserInfo.UpdatedBy = objEventUserInfo.FirstName;
                            objEventUserInfo.EventUserInfoId = EventUserInfoId;

                            result = _Events.UpdateEventUserPaymentInfo(objEventUserInfo);

                            if (result == 1)
                            {
                                //Send Mail to User
                                string BaseUrl = ConfigurationManager.AppSettings["usersiteurl"].ToString();
                                int MailStatus = 0;
                                Entities.MailTemplates objMailTemplates = new Entities.MailTemplates();
                                objMailTemplates = _MailTemplates.GetMailTemplateById("Event Registration by Cash/Check", 0, ref MailStatus);
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
                                    body.Replace("[TicketID]", objEventUserInfo.TransactionId);
                                    body.Replace("[SiteName]", objAppInfo.SiteName);

                                    BLL.Common.SendMail(objEventUserInfo.Email, objMailTemplates.Subject, body.ToString());

                                    return RedirectToAction("EventAcknowledgement", "Event", new { Type = "check", ch = Cheque, paymentby = PaymentBy });
                                }
                            }
                        }
                        else if (objEventUserInfo.PaymentMethod == "quickPay/Zelle")
                        {
                            Int64 result = 0;

                            objEventUserInfo.PaymentStatusId = 2;
                            objEventUserInfo.PaymentMethodId = 2;
                            objEventUserInfo.AmountPaid = objEventUserInfo.AmountPaid;
                            objEventUserInfo.PaymentMethod = "quickPay/Zelle";
                            objEventUserInfo.PaymentStatus = "Pending";
                            objEventUserInfo.TransactionId = EventUserInfoId.ToString();
                            objEventUserInfo.UpdatedBy = objEventUserInfo.FirstName;
                            objEventUserInfo.EventUserInfoId = EventUserInfoId;


                            result = _Events.UpdateEventUserPaymentInfo(objEventUserInfo);


                            if (result == 1)
                            {
                                //Send Mail to User
                                string BaseUrl = ConfigurationManager.AppSettings["usersiteurl"].ToString();
                                int MailStatus = 0;
                                Entities.MailTemplates objMailTemplates = new Entities.MailTemplates();
                                objMailTemplates = _MailTemplates.GetMailTemplateById("Event Registration by quickPay/Zelle", 0, ref MailStatus);
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
                                    body.Replace("[TicketID]", objEventUserInfo.TransactionId);
                                    body.Replace("[SiteName]", objAppInfo.SiteName);

                                    BLL.Common.SendMail(objEventUserInfo.Email, objMailTemplates.Subject, body.ToString());
                                }

                                return RedirectToAction("EventAcknowledgement", "Event", new { Type = "zelle" });
                            }
                        }
                    }
                    else
                    {
                        int status = 0;
                        Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("Event Registrations", 0, ref status);
                        if (objTemplates != null)
                        {
                            StringBuilder body = new StringBuilder();

                            body.Append(objTemplates.Description);
                            body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                            body.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                            body.Replace("[TWUrl]", objAppInfo.TwitterUrl);
                            body.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                            body.Replace("[GUrl]", objAppInfo.SupportEmail);
                            body.Replace("[TPhone]", objAppInfo.CompanyPhone);
                            body.Replace("[TEmail]", objAppInfo.CompanyEmail);
                            body.Replace("[EventName]", objEventUserInfo.EventName);
                            body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objEventUserInfo.FirstName + " " + objEventUserInfo.LastName));

                            BLL.Common.SendMail(objEventUserInfo.Email, objTemplates.Subject, body.ToString());
                        }
                        if (objEventUserInfo.ItemName != null && objEventUserInfo.ItemName != "")
                        {
                            Entities.MailTemplates objTemplates1 = _MailTemplates.GetMailTemplateById("Cultural Event Registration for Admin", 0, ref status);
                            if (objTemplates1 != null)
                            {
                                StringBuilder body = new StringBuilder();
                                StringBuilder ps = new StringBuilder();
                                StringBuilder subject = new StringBuilder();

                                body.Append(objTemplates1.Description);
                                subject.Append(objTemplates1.Subject);
                                subject.Replace("[EventName]", objEventUserInfo.EventName);
                                body.Replace("[ItemName]", objEventUserInfo.ItemName);
                                body.Replace("[ItemCategory]", objEventUserInfo.ItemCategory);
                                body.Replace("[ItemDuration]", objEventUserInfo.ItemDuration.ToString());
                                body.Replace("[FirstName]", objEventUserInfo.FirstName);
                                body.Replace("[LastName]", objEventUserInfo.LastName);
                                body.Replace("[Email]", objEventUserInfo.Email);
                                body.Replace("[Mobile]", objEventUserInfo.Mobile);
                                body.Replace("[EventName]", objEventUserInfo.EventName);
                                body.Replace("[Description]", objEventUserInfo.ItemDescription);
                                body.Replace("[EventType]", "");

                                if (lstEventParticipants != null && lstEventParticipants.Count() != 0)
                                {
                                    foreach (Entities.EventParticipants objEventParticipants in lstEventParticipants)
                                    {
                                        ps.Append("<tr height=\"35\"><td align=\"left\" style=\"font-family:Arial, Helvetica, sans-serif; color:#000;\">" + objEventParticipants.FirstName + "</td><td align=\"left\" style=\"font-family:Arial, Helvetica, sans-serif; color:#000;\">" + objEventParticipants.LastName + "</td><td align=\"left\" style=\"font-family:Arial, Helvetica, sans-serif; color:#000;\">" + objEventParticipants.Mobile + "</td></tr>");
                                    }
                                }

                                body.Replace("[Participents]", ps.ToString());
                                body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                                body.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                                body.Replace("[TWUrl]", objAppInfo.TwitterUrl);
                                body.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                                body.Replace("[GUrl]", objAppInfo.SupportEmail);
                                body.Replace("[TPhone]", objAppInfo.CompanyPhone);
                                body.Replace("[TEmail]", objAppInfo.CompanyEmail);
                                body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objEventUserInfo.FirstName + " " + objEventUserInfo.LastName));

                                BLL.Common.SendMailwithCC(objAppInfo.PresidentPhone, objAppInfo.CompanyEmail, objAppInfo.CompanyEmail, subject.ToString(), body.ToString());
                            }
                        }
                        else
                        {
                            Entities.MailTemplates objTemplates1 = _MailTemplates.GetMailTemplateById("Normal Event Registration for Admin", 0, ref status);
                            if (objTemplates1 != null)
                            {
                                StringBuilder body = new StringBuilder();
                                StringBuilder ps = new StringBuilder();
                                StringBuilder subject = new StringBuilder();

                                body.Append(objTemplates1.Description);
                                subject.Append(objTemplates1.Subject);
                                subject.Replace("[EventName]", objEventUserInfo.EventName);
                                body.Replace("[FirstName]", objEventUserInfo.FirstName);
                                body.Replace("[LastName]", objEventUserInfo.LastName);
                                body.Replace("[Email]", objEventUserInfo.Email);
                                body.Replace("[Mobile]", objEventUserInfo.Mobile);
                                body.Replace("[EventName]", objEventUserInfo.EventName);
                                body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                                body.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                                body.Replace("[TWUrl]", objAppInfo.TwitterUrl);
                                body.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                                body.Replace("[GUrl]", objAppInfo.SupportEmail);
                                body.Replace("[TPhone]", objAppInfo.CompanyPhone);
                                body.Replace("[TEmail]", objAppInfo.CompanyEmail);
                                body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objEventUserInfo.FirstName + " " + objEventUserInfo.LastName));

                                BLL.Common.SendMail(ConfigurationManager.AppSettings["adminemailid"], subject.ToString(), body.ToString());
                            }
                        }
                        return RedirectToAction("EventAcknowledgement", "Event");
                    }
                }
                else
                {
                    TempData["message"] = "<div class=\"error closable\">Failed Transaction.</div>";
                    return RedirectToAction("EventRegistration", "Event", new { eid = objEventUserInfo.EventId, EventName = BLL.Common.EncodeURL(objEventUserInfo.EventName) });
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"error closable\">Failed Transaction</div>";
                return RedirectToAction("EventRegistration", "Event", new { eid = objEventUserInfo.EventId, EventName = BLL.Common.EncodeURL(objEventUserInfo.EventName) });
            }
            return RedirectToAction("EventAcknowledgement", "Event");
        }

        public ActionResult Payment()
        {
            string Paymenturl = ConfigurationManager.AppSettings["paymenturl"].ToString();

            return View();
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

                    if (objEvents.BannerUrl != null && objEvents.BannerUrl != "")
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
                    imgBarCode.Height = 78;
                    imgBarCode.Width = 78;
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
                    graphicImage.DrawString("Ticket Id: " + objEventUserInfo.objEventOrderDetails.TransactionId, new Font("Arial", 16, FontStyle.Bold), Brushes.White, new Point(318, 35));

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

        public ActionResult ThankYou(Int64 id = 0, string PaymentMethod = "")
        {
            int status = 0;
            TCAssociationTool.Entities.AppInfo objAppInfo = new TCAssociationTool.Entities.AppInfo();
            TCAssociationTool.BLL.AppInfo _AppInfo = new TCAssociationTool.BLL.AppInfo();
            objAppInfo = _AppInfo.GetAppInfoDetails(ref status);
            PayByPayPal.PDTHolder objPDT = new PayByPayPal.PDTHolder();
            Entities.PaymentSettings objPaymentSettings = _paymentSettings.GetPaymentSettingsDetails(0, "PayPal", ref status);
            objPayByPayPal.objPaymentSettings.PDTToken = objPaymentSettings.TokenNo;
            objPayByPayPal.objPaymentSettings.PaymentUrl = objPaymentSettings.PaymentUrl;

            string amount = Request.Form["amount"], userId = Request.Form["userid"];
            //string statuscode = Request.Form["statuscode"], status = Request.Form["status"], sulekhaordernumber = Request.Form["sulekhaordernumber"], transid = Request.Form["transid"], authcode = Request.Form["authcode"];

            // For Testing
            //string amount = "100", userId = "6";
            //string statuscode = "0", status = "success", sulekhaordernumber = "123", transid = "Test1234Id", authcode = "1234";

            int result1 = 0;
            Int64 result = 0;
            Entities.EventUserInfo objEventUserInfo = new Entities.EventUserInfo();
            Entities.EventUserInfo objEventUserInfo1 = new Entities.EventUserInfo();
            Entities.EventOrderDetails objEventOrderDetails = new Entities.EventOrderDetails();
            List<Entities.EventRegistrationCounts> lstEventRegistrationCounts = new List<Entities.EventRegistrationCounts>();
            Entities.Events objEvents = new Entities.Events();
            List<Entities.EventRegistrationTypes> lstEventRegistrationTypes = new List<Entities.EventRegistrationTypes>();
            List<Entities.EventsTickets> lstEventsTickets = new List<Entities.EventsTickets>();
            try
            {
                Int64 EventId = objEventUserInfo.EventId;
                objEventUserInfo1 = _Events.GetEventUserInfoById(id, ref result1);


                status = objPayByPayPal.Success();
                if (status == 0)
                {
                    objPDT = objPayByPayPal.objPDTHolder;


                    objEventOrderDetails.AmountPaid = Convert.ToDecimal(objEventUserInfo1.AmountPaid);
                    objEventOrderDetails.PaymentMethod = "PayPal";
                    objEventOrderDetails.TransactionId = objPDT.TransactionId;
                    objEventOrderDetails.PaymentStatus = objPDT.PaymentStatus;
                    //objEventOrderDetails.EventId = objEventUserInfo.EventId;
                    objEventOrderDetails.EventUserInfoId = Convert.ToInt64(userId);
                    objEventOrderDetails.PaymentDate = ConvertPayPalDateTime(objPDT.PaymentDate.ToString());
                    objEventOrderDetails.UpdatedBy = objEventUserInfo1.FirstName;
                    objEventOrderDetails.InsertedBy = objEventUserInfo1.FirstName;

                    objEventUserInfo1.PaymentStatusId = 1;
                    objEventUserInfo1.PaymentMethodId = 1;
                    objEventUserInfo1.AmountPaid = objEventOrderDetails.AmountPaid;
                    objEventUserInfo1.PaymentMethod = objEventOrderDetails.PaymentMethod;
                    objEventUserInfo1.PaymentStatus = objPDT.PaymentStatus;
                    objEventUserInfo1.TransactionId = objPDT.TransactionId;
                    objEventUserInfo1.UpdatedBy = objEventUserInfo1.FirstName;



                    result = _Events.UpdateEventUserPaymentInfo(objEventUserInfo1);


                    objEventUserInfo = _Events.GetEventUserInfoFullDetailsById(id, EventId, ref objEvents, ref lstEventRegistrationCounts, ref lstEventsTickets, ref result1);


                    var attachment = GetAttachment(objEventUserInfo, objEvents, lstEventsTickets);
                    if (result == 1)
                    {
                        //Send Mail to User
                        string BaseUrl = ConfigurationManager.AppSettings["usersiteurl"].ToString();
                        int MailStatus = 0;
                        Entities.MailTemplates objMailTemplates = new Entities.MailTemplates();
                        objMailTemplates = _MailTemplates.GetMailTemplateById("Event Registrations", 0, ref MailStatus);
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
                            body.Replace("[TransactionId]", objEventUserInfo.TransactionId);
                            body.Replace("[PaymentType]", "PayPal");
                            body.Replace("[PaymentStatus]", objEventUserInfo.PaymentStatus);
                            body.Replace("[PaymentDate]", objEventUserInfo.PaymentDate.ToString("MM/dd/yyyy"));
                            body.Replace("[AmountPaid]", objEventUserInfo.AmountPaid.ToString());
                            body.Replace("[SiteName]", objAppInfo.SiteName);

                            //BLL.Common.SendMailwithAttachment(objEventUserInfo.Email, objMailTemplates.Subject, body.ToString(), attachment);
                            var sts = BLL.Common.SendMailwithAttachment(objEventUserInfo.Email, objMailTemplates.Subject, body.ToString(), "tickets-" + BLL.Common.EncodeURL(objEventUserInfo.EventName) + "-" + objEventUserInfo.EventUserInfoId + ".pdf");

                        }

                        //Send Mail to Admin
                        int statusAdmin = 0;
                        Entities.MailTemplates objTemplatesAdmin = _MailTemplates.GetMailTemplateById("Admin Event Registration", 0, ref statusAdmin);
                        if (objTemplatesAdmin != null && objTemplatesAdmin.MailTemplateId != 0)
                        {
                            DateTime Date1 = ConvertPayPalDateTime(objPDT.PaymentDate.ToString());

                            StringBuilder body1 = new StringBuilder();
                            body1.Append(objTemplatesAdmin.Description);
                            body1.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                            body1.Replace("[EUrl]", objAppInfo.SupportEmail);
                            body1.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                            body1.Replace("[InUrl]", objAppInfo.TwitterUrl);
                            body1.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                            body1.Replace("[TPhone]", objAppInfo.CompanyPhone);
                            body1.Replace("[TEmail]", objAppInfo.CompanyEmail);
                            body1.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objEventUserInfo.FirstName + " " + objEventUserInfo.LastName));
                            body1.Replace("[EventName]", BLL.Common.UppercaseFirst(objEventUserInfo.EventName));
                            body1.Replace("[Email]", objEventUserInfo.Email);
                            body1.Replace("[TransactionId]", objEventUserInfo.TransactionId);
                            body1.Replace("[PaymentType]", "PayPal");
                            body1.Replace("[PaymentStatus]", objEventUserInfo.PaymentStatus);
                            body1.Replace("[PaymentDate]", Date1.ToString("MM/dd/yyyy"));
                            body1.Replace("[AmountPaid]", objEventUserInfo.AmountPaid.ToString());

                            BLL.Common.SendMailwithCC(objAppInfo.CompanyEmail, objAppInfo.CompanyEmail, objAppInfo.CompanyEmail, "Registered Event Details - Gitanjali Art of Giving", body1.ToString());
                        }
                    }
                }
                else
                {
                    objEventUserInfo = _Events.GetEventUserInfoById(id, ref result1);

                    objEventOrderDetails.AmountPaid = objEventUserInfo.AmountPaid;
                    objEventOrderDetails.PaymentMethod = "PayPal";
                    objEventOrderDetails.TransactionId = objPDT.PaymentStatus;
                    objEventOrderDetails.PaymentStatus = "Pending";
                    //objEventOrderDetails.EventId = objEventUserInfo.EventId;
                    objEventOrderDetails.EventUserInfoId = Convert.ToInt64(userId);
                    objEventOrderDetails.PaymentDate = DateTime.UtcNow;
                    objEventOrderDetails.UpdatedBy = objEventUserInfo.FirstName;
                    objEventOrderDetails.InsertedBy = objEventUserInfo.FirstName;
                    objEventUserInfo.PaymentStatusId = 1;
                    objEventUserInfo.PaymentMethodId = 1;
                    objEventUserInfo.AmountPaid = objEventOrderDetails.AmountPaid;
                    objEventUserInfo.PaymentMethod = objEventOrderDetails.PaymentMethod;
                    objEventUserInfo.PaymentStatus = objPDT.PaymentStatus;
                    objEventUserInfo.TransactionId = objPDT.TransactionId;
                    objEventUserInfo.UpdatedBy = objEventUserInfo.FirstName;


                    result = _Events.UpdateEventUserPaymentInfo(objEventUserInfo);

                    return RedirectToAction("Error404", "Error");
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"error closable\">" + ex + "</div>";
            }

            ViewBag.PayerEmail = objPDT.PayerEmail;
            ViewBag.UserName = objEventUserInfo.FirstName;
            ViewBag.ReceiverEmail = objPDT.ReceiverEmail;
            ViewBag.AddressCity = objPDT.AddressCity;
            ViewBag.AddressCountry = objPDT.AddressCountry;
            ViewBag.AddressName = objPDT.AddressName;
            ViewBag.AddressState = objPDT.AddressState;
            ViewBag.AddressStreet = objPDT.AddressStreet;
            ViewBag.AddressZip = objPDT.AddressZip;
            ViewBag.GrossTotal = objPDT.GrossTotal;
            ViewBag.PaymentStatus = objPDT.PaymentStatus;
            ViewBag.TransactionId = objPDT.TransactionId;
            DateTime Date = ConvertPayPalDateTime(objPDT.PaymentDate.ToString());
            ViewBag.PaymentDate = Date.ToString("MM/dd/yyyy");
            ViewBag.UserPhone = objEventUserInfo.Mobile;
            ViewBag.Date = DateTime.UtcNow;

            return View();
        }

        public ActionResult CulturalRegistration(string EventName = "", Int64 MemberId = 0, Int64 eid = 0)
        {
            int status = 0;
            try
            {
                if (EventName != "")
                {
                    objEvents = _Events.GetEventById(eid, EventName, ref lstEventRegistrationTypes, ref status);
                    if (objEvents != null && objEvents.EventId != 0)
                    {
                        if (this.User.Identity.IsAuthenticated && this.User.Identity.Name != null && HttpContext.Session != null && Session["username"] != null)
                        {
                            _objMember = _Members.GetMemberFullDetailsByEmail(HttpContext.User.Identity.Name.ToString(), ref status);
                        }
                        objInnerPages = _innerpage.GetInnerPagesListById(0, "event registration rules", ref status);

                        MemberId = Convert.ToInt64((Session["MemberID"] != null ? Session["MemberID"] : 0));
                    }
                    else
                    {
                        return RedirectToAction("Error404", "Error");
                    }
                }
            }
            catch
            {
                return RedirectToAction("Error404", "Error");
            }

            ViewBag.InnerPage = objInnerPages;
            ViewBag.Memberdetails = _objMember;
            ViewBag.MemberId = MemberId;
            ViewBag.objEvents = objEvents;
            ViewBag.EventName = EventName;
            return View();
        }

        // Cultural Event Registraion by PayPal
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CulturalRegistration(Entities.EventUserInfo objEventUserInfo, List<Entities.EventParticipants> lstEventParticipants, HttpPostedFileBase file)
        {
            try
            {
                string filename = file.FileName;
                //var image = WebImage.GetImageFromRequest();
                string imageurl = (file == null ? "NA" : Path.GetExtension(file.FileName));
                Int64 EventUserInfoId = 0;
                objEventUserInfo.IsApproved = false;
                objEventUserInfo.UpdatedBy = objEventUserInfo.FirstName;

                int Status = 0;
                TCAssociationTool.Entities.AppInfo objAppInfo = new TCAssociationTool.Entities.AppInfo();
                TCAssociationTool.BLL.AppInfo _AppInfo = new TCAssociationTool.BLL.AppInfo();
                objAppInfo = _AppInfo.GetAppInfoDetails(ref Status);

                Int64 _status = _Events.InsertEventUserInfo(objEventUserInfo, ref EventUserInfoId, ref imageurl);
                if (_status == 1)
                {
                    if (imageurl != "")
                    {
                        file.SaveAs(ConfigurationManager.AppSettings["adminuploadPath"] + "\\Culturals\\" + imageurl);
                    }
                    if (lstEventParticipants != null && lstEventParticipants.Count() != 0)
                    {
                        foreach (Entities.EventParticipants objEventParticipants in lstEventParticipants)
                        {
                            objEventParticipants.EventUserInfoId = EventUserInfoId;
                            objEventParticipants.EventId = objEventUserInfo.EventId;
                            if (objEventParticipants != null && objEventParticipants.FirstName != null)
                            {
                                objEventParticipants.IsApproved = false;
                                objEventParticipants.UpdatedBy = objEventParticipants.FirstName;
                                Int64 estatus = _Events.InsertEventParticipant(objEventParticipants);
                                if (estatus == -1)
                                {
                                    TempData["message"] = "<div class=\"error closable\">Failed inserting event registration.</div>";
                                    return RedirectToAction("EventRegistration", "Event", new { eid = objEventUserInfo.EventId, EventName = BLL.Common.EncodeURL(objEventUserInfo.EventName) });
                                }
                            }
                        }
                    }
                    if (objEventUserInfo.AmountPaid != 0)
                    {
                        int status1 = 0;
                        Entities.PaymentSettings objPaymentSettings = _paymentSettings.GetPaymentSettingsDetails(0, "PayPal", ref status1);
                        if (objPaymentSettings != null)
                        {
                            List<PayByPayPal.CartItems> lstCartItems = new List<PayByPayPal.CartItems>();

                            PayByPayPal.CartItems objCartItems = new PayByPayPal.CartItems();
                            objCartItems.ItemName = "Event :" + objEventUserInfo.EventName;

                            if (objEventUserInfo.AmountPaid != 0)
                            {
                                objCartItems.ItemPrice = objEventUserInfo.AmountPaid;
                            }
                            else
                            {
                                int status = 0;
                                Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("Cultural Event Registrations", 0, ref status);
                                if (objTemplates != null)
                                {
                                    StringBuilder body = new StringBuilder();

                                    body.Append(objTemplates.Description);
                                    body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                                    body.Replace("[FBUrl]", ConfigurationManager.AppSettings["FBUrl"].ToString());
                                    body.Replace("[TWUrl]", ConfigurationManager.AppSettings["TWUrl"].ToString());
                                    body.Replace("[YUrl]", ConfigurationManager.AppSettings["YUrl"].ToString());
                                    body.Replace("[TPhoneNo]", ConfigurationManager.AppSettings["TPhoneNo"].ToString());
                                    body.Replace("[TEmailId]", ConfigurationManager.AppSettings["TEmailId"].ToString());
                                    body.Replace("[EventName]", objEventUserInfo.EventName);
                                    body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objEventUserInfo.FirstName + " " + objEventUserInfo.LastName));

                                    BLL.Common.SendMail(objEventUserInfo.Email, objTemplates.Subject, body.ToString());
                                }
                                return RedirectToAction("Thankyou", "Event");
                            }

                            lstCartItems.Add(objCartItems);

                            objPayByPayPal.lstCartItems = lstCartItems;

                            PayByPayPal.PaymentSettings objpaymentsettings = new PayByPayPal.PaymentSettings();
                            objpaymentsettings.BusinessEmail = objPaymentSettings.PaymentEmail;
                            objpaymentsettings.BusinessPassword = objPaymentSettings.PaymentPassword;
                            objpaymentsettings.CurrencyCode = objPaymentSettings.CurrencyCode;
                            objpaymentsettings.NotifyUrl = ConfigurationManager.AppSettings["baseurl"] + "event/ipn.html" + "?id=" + EventUserInfoId;
                            objpaymentsettings.PaymentUrl = objPaymentSettings.PaymentUrl;
                            objpaymentsettings.ReturnUrl = ConfigurationManager.AppSettings["baseurl"].ToString() + "event-registration-acknowledgement.html?id=" + EventUserInfoId;
                            objpaymentsettings.Cmd = "_cart";
                            objpaymentsettings.PDTToken = objPaymentSettings.TokenNo;
                            objpaymentsettings.CBT = "Click here to complete your transaction";

                            objPayByPayPal.objPaymentSettings = objpaymentsettings;

                            string returnurl = objPayByPayPal.CheckOut();

                            return new RedirectResult(returnurl);
                        }
                    }
                    else
                    {
                        int status = 0;
                        Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("Cultural Event Registrations", 0, ref status);
                        if (objTemplates != null)
                        {
                            StringBuilder body = new StringBuilder();

                            body.Append(objTemplates.Description);
                            body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                            body.Replace("[FBUrl]", ConfigurationManager.AppSettings["FBUrl"].ToString());
                            body.Replace("[TWUrl]", ConfigurationManager.AppSettings["TWUrl"].ToString());
                            body.Replace("[YUrl]", ConfigurationManager.AppSettings["YUrl"].ToString());
                            body.Replace("[TPhoneNo]", ConfigurationManager.AppSettings["TPhoneNo"].ToString());
                            body.Replace("[TEmailId]", ConfigurationManager.AppSettings["TEmailId"].ToString());
                            body.Replace("[EventName]", objEventUserInfo.EventName);
                            body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objEventUserInfo.FirstName + " " + objEventUserInfo.LastName));

                            BLL.Common.SendMail(objEventUserInfo.Email, objTemplates.Subject, body.ToString());
                        }
                        if (objEventUserInfo.ItemName != null && objEventUserInfo.ItemName != "")
                        {
                            Entities.MailTemplates objTemplates1 = _MailTemplates.GetMailTemplateById("Cultural Event Registration for Admin", 0, ref status);
                            if (objTemplates1 != null)
                            {
                                StringBuilder body = new StringBuilder();
                                StringBuilder ps = new StringBuilder();
                                StringBuilder subject = new StringBuilder();


                                body.Append(objTemplates1.Description);
                                subject.Append(objTemplates1.Subject);
                                subject.Replace("[EventName]", objEventUserInfo.EventName);
                                body.Replace("[ItemName]", objEventUserInfo.ItemName);
                                body.Replace("[ItemCategory]", objEventUserInfo.ItemCategory);
                                body.Replace("[ItemDuration]", objEventUserInfo.ItemDuration.ToString());
                                body.Replace("[FirstName]", objEventUserInfo.FirstName);
                                body.Replace("[LastName]", objEventUserInfo.LastName);
                                body.Replace("[Email]", objEventUserInfo.Email);
                                body.Replace("[Mobile]", objEventUserInfo.Mobile);
                                body.Replace("[EventName]", objEventUserInfo.EventName);
                                body.Replace("[Description]", objEventUserInfo.ItemDescription);
                                body.Replace("[EventType]", "");


                                if (lstEventParticipants != null && lstEventParticipants.Count() != 0)
                                {
                                    foreach (Entities.EventParticipants objEventParticipants in lstEventParticipants)
                                    {
                                        ps.Append("<tr height=\"35\"><td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; color:#000;\" height=\"30\" width=\"25%\">" + objEventParticipants.FirstName + "</td><td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; color:#000;\" height=\"30\" width=\"25%\">" + objEventParticipants.LastName + "</td><td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; color:#000;\" height=\"30\" width=\"25%\">" + objEventParticipants.Mobile + "</td></tr>");
                                    }
                                }
                                body.Replace("[Participents]", ps.ToString());
                                body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                                body.Replace("[FBUrl]", ConfigurationManager.AppSettings["FBUrl"].ToString());
                                body.Replace("[TWUrl]", ConfigurationManager.AppSettings["TWUrl"].ToString());
                                body.Replace("[YUrl]", ConfigurationManager.AppSettings["YUrl"].ToString());
                                body.Replace("[TPhoneNo]", ConfigurationManager.AppSettings["TPhoneNo"].ToString());
                                body.Replace("[TEmailId]", ConfigurationManager.AppSettings["TEmailId"].ToString());
                                body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objEventUserInfo.FirstName + " " + objEventUserInfo.LastName));

                                BLL.Common.SendMailwithCC(objAppInfo.PresidentPhone, objAppInfo.CompanyEmail, objAppInfo.CompanyEmail, subject.ToString(), body.ToString());
                            }
                        }
                        else
                        {
                            Entities.MailTemplates objTemplates1 = _MailTemplates.GetMailTemplateById("Normal Event Registration for Admin", 0, ref status);
                            if (objTemplates1 != null)
                            {
                                StringBuilder body = new StringBuilder();
                                StringBuilder ps = new StringBuilder();
                                StringBuilder subject = new StringBuilder();


                                body.Append(objTemplates1.Description);
                                subject.Append(objTemplates1.Subject);
                                subject.Replace("[EventName]", objEventUserInfo.EventName);
                                body.Replace("[FirstName]", objEventUserInfo.FirstName);
                                body.Replace("[LastName]", objEventUserInfo.LastName);
                                body.Replace("[Email]", objEventUserInfo.Email);
                                body.Replace("[Mobile]", objEventUserInfo.Mobile);
                                body.Replace("[EventName]", objEventUserInfo.EventName);
                                body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                                body.Replace("[FBUrl]", ConfigurationManager.AppSettings["FBUrl"].ToString());
                                body.Replace("[TWUrl]", ConfigurationManager.AppSettings["TWUrl"].ToString());
                                body.Replace("[YUrl]", ConfigurationManager.AppSettings["YUrl"].ToString());
                                body.Replace("[TPhoneNo]", ConfigurationManager.AppSettings["TPhoneNo"].ToString());
                                body.Replace("[TEmailId]", ConfigurationManager.AppSettings["TEmailId"].ToString());
                                body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objEventUserInfo.FirstName + " " + objEventUserInfo.LastName));

                                BLL.Common.SendMailwithCC(objAppInfo.PresidentPhone, objAppInfo.CompanyEmail, objAppInfo.CompanyEmail, subject.ToString(), body.ToString());
                            }
                        }
                        return RedirectToAction("Thankyou", "Event");
                    }
                }
                else
                {
                    TempData["message"] = "<div class=\"error closable\">Failed Transaction.</div>";
                    return RedirectToAction("EventRegistration", "Event", new { eid = objEventUserInfo.EventId, EventName = BLL.Common.EncodeURL(objEventUserInfo.EventName) });
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"error closable\">" + ex.Message + "</div>";
                return RedirectToAction("EventRegistration", "Event", new { eid = objEventUserInfo.EventId, EventName = BLL.Common.EncodeURL(objEventUserInfo.EventName) });
            }
            return RedirectToAction("Thankyou", "Event");
        }

        // Paypal IPN
        public ActionResult EventIPN(Int64 id = 0)
        {

            PayByPayPal.PDTHolder objPDT = new PayByPayPal.PDTHolder();
            int result1 = 0;
            Entities.EventOrderDetails objEventOrderDetails = new Entities.EventOrderDetails();
            objEventUserInfo = _Events.GetEventUserInfoById(id, ref result1);
            string status = "";
            try
            {
                int qstatus = 0;
                Entities.PaymentSettings objPaymentSettings = _paymentSettings.GetPaymentSettingsDetails(0, "PayPal", ref qstatus);
                objPayByPayPal.objPaymentSettings.PaymentUrl = objPaymentSettings.PaymentUrl;
                status = objPayByPayPal.ipn();
                //BLL.Common.SendMailwithfrom("g.vinod@arjunweb.in", ConfigurationManager.AppSettings["adminemailid"].ToString(), "testIPN", status.ToString());
                if (status == "VERIFIED")
                {
                    objPDT = objPayByPayPal.objPDTHolder;
                    objEventOrderDetails.PaymentMethod = "PayPal";
                    objEventOrderDetails.TransactionId = objPDT.TransactionId;
                    objEventOrderDetails.PaymentStatus = objPDT.PaymentStatus;
                    objEventOrderDetails.EventId = objEventUserInfo.EventId;
                    objEventOrderDetails.PaymentDate = ConvertPayPalDateTime(objPDT.PaymentDate.ToString());
                    objEventOrderDetails.UpdatedBy = objEventUserInfo.FirstName;
                    objEventOrderDetails.InsertedBy = objEventUserInfo.FirstName;
                    objEventOrderDetails.AmountPaid = Convert.ToDecimal(objPDT.GrossTotal);

                    Int64 result = _Events.InsertEventOrderDetail(objEventOrderDetails);
                }

            }
            catch
            {
                TempData["message"] = "Your payment did not go through please contact board@TeluguAssociationToolweb.org for assistance.";

            }

            ViewBag.status = status;

            return View();
        }

        // Paypal Acknowledgement
        public ActionResult PaymentEventAcknowledgement(Int64 id = 0)
        {
            try
            {
                int status = 0;
                Int64 result = 0;
                int result1 = 0;
                string Username = "";
                if (id != 0)
                {
                    Entities.AppInfo objAppInfo = _AppInfo.GetAppInfoDetails(ref status);
                    objEventUserInfo = _Events.GetEventUserInfoById(id, ref result1);
                    PayByPayPal.PDTHolder objPDT = new PayByPayPal.PDTHolder();
                    Entities.PaymentSettings objPaymentSettings = _paymentSettings.GetPaymentSettingsDetails(0, "PayPal", ref status);
                    objPayByPayPal.objPaymentSettings.PDTToken = objPaymentSettings.TokenNo;
                    objPayByPayPal.objPaymentSettings.PaymentUrl = objPaymentSettings.PaymentUrl;

                    status = objPayByPayPal.Success();
                    if (status == 0)
                    {
                        objPDT = objPayByPayPal.objPDTHolder;
                        if (objEventUserInfo.FirstName != null && objEventUserInfo.FirstName != "")
                        {
                            Username = objEventUserInfo.FirstName + objEventUserInfo.LastName;
                        }

                        if (Username != null)
                        {
                            if (result1 == 1)
                            {
                                Entities.EventOrderDetails objEventOrderDetails = new Entities.EventOrderDetails();

                                objEventOrderDetails.AmountPaid = Convert.ToDecimal(objPDT.GrossTotal);
                                objEventOrderDetails.PaymentMethod = "PayPal";
                                objEventOrderDetails.TransactionId = objPDT.TransactionId;
                                objEventOrderDetails.PaymentStatus = objPDT.PaymentStatus;
                                objEventOrderDetails.EventId = objEventUserInfo.EventId;
                                objEventOrderDetails.EventUserInfoId = objEventUserInfo.EventUserInfoId;
                                objEventOrderDetails.PaymentDate = ConvertPayPalDateTime(objPDT.PaymentDate.ToString());
                                objEventOrderDetails.UpdatedBy = Username;
                                objEventOrderDetails.InsertedBy = Username;

                                result = _Events.InsertEventOrderDetail(objEventOrderDetails);
                                if (result == 1)
                                {
                                    ViewBag.PayerEmail = objPDT.PayerEmail;
                                    ViewBag.UserName = Username;
                                    ViewBag.ReceiverEmail = objPDT.ReceiverEmail;
                                    ViewBag.AddressCity = objPDT.AddressCity;
                                    ViewBag.AddressCountry = objPDT.AddressCountry;
                                    ViewBag.AddressName = objPDT.AddressName;
                                    ViewBag.AddressState = objPDT.AddressState;
                                    ViewBag.AddressStreet = objPDT.AddressStreet;
                                    ViewBag.AddressZip = objPDT.AddressZip;
                                    ViewBag.GrossTotal = objPDT.GrossTotal;
                                    ViewBag.PaymentStatus = objPDT.PaymentStatus;
                                    ViewBag.TransactionId = objPDT.TransactionId;
                                    DateTime Date = ConvertPayPalDateTime(objPDT.PaymentDate.ToString());
                                    ViewBag.PaymentDate = Date.ToString("MM/dd/yyyy");
                                }
                            }
                            Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("Event Registrations", 0, ref status);
                            if (objTemplates != null)
                            {
                                StringBuilder body = new StringBuilder();
                                body.Append(objTemplates.Description);
                                body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                                body.Replace("[FBUrl]", ConfigurationManager.AppSettings["FBUrl"].ToString());
                                body.Replace("[TWUrl]", ConfigurationManager.AppSettings["TWUrl"].ToString());
                                body.Replace("[YUrl]", ConfigurationManager.AppSettings["YUrl"].ToString());
                                body.Replace("[TPhoneNo]", ConfigurationManager.AppSettings["TPhoneNo"].ToString());
                                body.Replace("[TEmailId]", ConfigurationManager.AppSettings["TEmailId"].ToString());
                                body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objEventUserInfo.FirstName + " " + objEventUserInfo.LastName));
                                body.Replace("[EventName]", BLL.Common.UppercaseFirst(objEventUserInfo.EventName));

                                BLL.Common.SendMail(objEventUserInfo.Email, objTemplates.Subject, body.ToString());
                            }
                        }
                    }
                    else
                    {
                        Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("Event Registrations", 0, ref status);
                        if (objTemplates != null)
                        {
                            StringBuilder body = new StringBuilder();
                            body.Append(objTemplates.Description);
                            body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                            body.Replace("[FBUrl]", ConfigurationManager.AppSettings["FBUrl"].ToString());
                            body.Replace("[TWUrl]", ConfigurationManager.AppSettings["TWUrl"].ToString());
                            body.Replace("[YUrl]", ConfigurationManager.AppSettings["YUrl"].ToString());
                            body.Replace("[TPhoneNo]", ConfigurationManager.AppSettings["TPhoneNo"].ToString());
                            body.Replace("[TEmailId]", ConfigurationManager.AppSettings["TEmailId"].ToString());
                            body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objEventUserInfo.FirstName + " " + objEventUserInfo.LastName));
                            body.Replace("[EventName]", BLL.Common.UppercaseFirst(objEventUserInfo.EventName));

                            BLL.Common.SendMailwithCC(objAppInfo.PresidentPhone, objAppInfo.CompanyEmail, objAppInfo.CompanyEmail, objTemplates.Subject, body.ToString());
                        }
                        return RedirectToAction("Thankyou", "Event");
                    }
                }
            }
            catch
            {
                return RedirectToAction("Error404", "Error");
            }
            return View();
        }

        //public ActionResult Thankyou(string EventName = "", string Type = "", Int64 eid = 0)
        //{

        //    ViewBag.EventName = EventName;
        //    ViewBag.Type = Type;
        //    return View();
        //}

        public ActionResult EventAcknowledgement(string Type = "", string ch = "", string paymentby = "")
        {
            ViewBag.Type = Type;
            ViewBag.ch = ch;
            ViewBag.paymentby = paymentby;
            return View();
        }

        public DateTime ConvertPayPalDateTime(string payPalDateTime)
        {
            CultureInfo enUS = new CultureInfo("en-US");
            // accept a few different date formats because of PST/PDT timezone and slight month difference in sandbox vs. prod.
            string[] dateFormats = { "HH:mm:ss MMM dd, yyyy PST", "HH:mm:ss MMM. dd, yyyy PST", "HH:mm:ss MMM dd, yyyy PDT", "HH:mm:ss MMM. dd, yyyy PDT",
                             "HH:mm:ss dd MMM yyyy PST", "HH:mm:ss dd MMM. yyyy PST", "HH:mm:ss dd MMM yyyy PDT", "HH:mm:ss dd MMM. yyyy PDT"};
            DateTime outputDateTime;

            DateTime.TryParseExact(payPalDateTime, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out outputDateTime);

            // convert to local timezone
            TimeZoneInfo hwZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");

            outputDateTime = TimeZoneInfo.ConvertTime(outputDateTime, hwZone, TimeZoneInfo.Local);

            return outputDateTime;
        }

        public ActionResult EventDetails(string EventName = "", string Type = "", Int64 eid = 0)
        {
            try
            {
                int status = 0;
                if (EventName != "")
                {
                    objEvents = _Events.GetEventById(eid, EventName, ref lstEventRegistrationTypes, ref status);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error404", "Error");
            }
            ViewBag.objEvents = objEvents;
            ViewBag.lstEventRegistrationTypes = lstEventRegistrationTypes;
            ViewBag.Type = Type;
            ViewBag.EventName = EventName;
            return View();
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

    }
}
