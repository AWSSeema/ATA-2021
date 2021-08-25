using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;

namespace TCAssociationTool.Areas.User.Controllers
{
    public class VolunteerController : Controller
    {
        BLL.Volunteers _Volunteers = new BLL.Volunteers();
        BLL.MailTemplates _MailTemplates = new BLL.MailTemplates();
        List<Entities.Volunteers> lstVolunteers = new List<Entities.Volunteers>();
        BLL.VolunteerCategories _VolunteerCategory = new BLL.VolunteerCategories();
        List<Entities.VolunteerCategories> lstVolunteerCategory = new List<Entities.VolunteerCategories>();
        BLL.Events _Events = new BLL.Events();
         BLL.Members _Members = new BLL.Members();
         Entities.Members _objMember = new Entities.Members();
         TCAssociationTool.BLL.AppInfo _appinfo = new BLL.AppInfo();
         HomeController _HomeController = new HomeController();

        [HttpGet]
        public ActionResult AddVolunteer(Int64 VolunteerCategoryId = 0, Int64 EventId = 0, Int64 MemberId = 0)
        {
            int _qstatus = 0;
            int status = 0;
            List<Entities.VolunteerCategories> lstVolunteerCategory = _VolunteerCategory.GetVolunteerCategoriesList(ref _qstatus);

            List<Entities.Events> lstEvents = _Events.GetEventsListall(ref status);

            if (this.User.Identity.IsAuthenticated && this.User.Identity.Name != null && HttpContext.Session != null && Session["username"] != null)
                        {
                            _objMember = _Members.GetMemberFullDetailsByEmail(HttpContext.User.Identity.Name.ToString(), ref status);
                        }
              MemberId = Convert.ToInt64((Session["MemberID"] != null ? Session["MemberID"] : 0));

            ViewBag.lstEvents = lstEvents;
              ViewBag.Memberdetails = _objMember;
             ViewBag.MemberId = MemberId;
            ViewBag.lstVolunteerCategory = lstVolunteerCategory;
            ViewBag.VolunteerCategoryId = VolunteerCategoryId;
            ViewBag.EventId = EventId;
            ViewBag.MemberId = MemberId;
            return View();
        } 

        [HttpPost]
        public ActionResult AddVolunteerRegistration(Entities.Volunteers objVolunteers)
        {
            try
            {
                objVolunteers.InsertedBy = objVolunteers.FirstName;
                objVolunteers.UpdatedBy = HttpContext.User.Identity.Name.ToString();
                objVolunteers.IsActive = false;
                string Password = "";
                if (objVolunteers.Field1 == null || objVolunteers.Field1 == "")
                {
                    Password = BLL.Common.GetRandomString(8);
                    string _passwordhash = BLL.Password.ComputeHash(Password.Trim(), "SHA512", null);
                    objVolunteers.Field1 = _passwordhash.ToString();
                }
                else {
                    Password = objVolunteers.Field1;
                    string _passwordhash = BLL.Password.ComputeHash(objVolunteers.Field1.Trim(), "SHA512", null);
                    objVolunteers.Field1 = _passwordhash.ToString();
                }
                Int64 _status = _Volunteers.FEVolunteerInsert(objVolunteers);
                int status1 = 0;
                Entities.AppInfo objAppInfo = _appinfo.GetAppInfoDetails(ref status1);
                if (_status == 1)
                {
                    int status = 0;
                    Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("User Volunteer Registration", 0, ref status);
                    if (objTemplates != null)
                    {

                        StringBuilder body = new StringBuilder();
                        body.Append(objTemplates.Description);
                        body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                        body.Replace("[FirstName]", BLL.Common.UppercaseFirst(objVolunteers.FirstName));
                        body.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                        body.Replace("[TWUrl]", objAppInfo.TwitterUrl);
                        body.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                        body.Replace("[GUrl]", objAppInfo.SupportEmail);
                        body.Replace("[TPhone]", objAppInfo.CompanyPhone);
                        body.Replace("[TEmail]", objAppInfo.CompanyEmail);
                        body.Replace("[SiteName]", objAppInfo.SiteName);
                        body.Replace("[Email]", objVolunteers.Email);
                        body.Replace("[PASSWORD]", Password);
                        body.Replace("[ApplicationName]", objAppInfo.SiteName);

                        BLL.Common.SendMailwithfrom(objVolunteers.Email, objAppInfo.CompanyEmail, "Thanks For Becoming A Volunteer - TASA", body.ToString());
                    }

                    int status2 = 0;
                    Entities.MailTemplates objTemplates1 = _MailTemplates.GetMailTemplateById("Volunteer Registration For Admin", 0, ref status2);
                    if (objTemplates1 != null)
                    {

                        StringBuilder body = new StringBuilder();
                        body.Append(objTemplates1.Description);
                        body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                        body.Replace("[FirstName]", BLL.Common.UppercaseFirst(objVolunteers.FirstName));
                        body.Replace("[LastName]", BLL.Common.UppercaseFirst(objVolunteers.LastName));
                        body.Replace("[Email]", objVolunteers.Email);
                        body.Replace("[PhoneNo]", objVolunteers.PhoneNo);
                        body.Replace("[HoursSpent]", objVolunteers.HoursSpent);
                        body.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                        body.Replace("[TWUrl]", objAppInfo.TwitterUrl);
                        body.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                        body.Replace("[GUrl]", objAppInfo.SupportEmail);
                        body.Replace("[TPhone]", objAppInfo.CompanyPhone);
                        body.Replace("[TEmail]", objAppInfo.CompanyEmail);
                        body.Replace("[SiteName]", objAppInfo.SiteName);
                        body.Replace("[ApplicationName]", objAppInfo.SiteName);

                        BLL.Common.SendMail(objAppInfo.CompanyEmail, "You have a new Volunteer Registration from TASA", body.ToString());
                    }
                    return RedirectToAction("ThankYou", "Volunteer");

                    }
                if (_status == 3)
                {
                    int status = 0;
                    Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("Volunteer Registration", 0, ref status);
                    if (objTemplates != null)
                    {

                        StringBuilder body = new StringBuilder();
                        body.Append(objTemplates.Description);
                        body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                        body.Replace("[FirstName]", BLL.Common.UppercaseFirst(objVolunteers.FirstName));
                        body.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                        body.Replace("[TWUrl]", objAppInfo.TwitterUrl);
                        body.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                        body.Replace("[GUrl]", objAppInfo.SupportEmail);
                        body.Replace("[TPhone]", objAppInfo.CompanyPhone);
                        body.Replace("[TEmail]", objAppInfo.CompanyEmail);
                        body.Replace("[SiteName]", objAppInfo.SiteName);
                        body.Replace("[EventName]", objVolunteers.EventName);
                        body.Replace("[ApplicationName]", objAppInfo.SiteName);

                        BLL.Common.SendMailwithfrom(objVolunteers.Email, objAppInfo.EnqueryEmail, "Thanks For Becoming A Volunteer", body.ToString());
                    }

                    int status2 = 0;
                    Entities.MailTemplates objTemplates1 = _MailTemplates.GetMailTemplateById("Volunteer Registration For Admin", 0, ref status2);
                    if (objTemplates1 != null)
                    {

                        StringBuilder body = new StringBuilder();
                        body.Append(objTemplates1.Description);
                        body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                        body.Replace("[FirstName]", BLL.Common.UppercaseFirst(objVolunteers.FirstName));
                        body.Replace("[LastName]", BLL.Common.UppercaseFirst(objVolunteers.LastName));
                        body.Replace("[Email]", objVolunteers.Email);
                        body.Replace("[PhoneNo]", objVolunteers.PhoneNo);
                        body.Replace("[HoursSpent]", objVolunteers.HoursSpent);
                        body.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                        body.Replace("[TWUrl]", objAppInfo.TwitterUrl);
                        body.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                        body.Replace("[GUrl]", objAppInfo.SupportEmail);
                        body.Replace("[TPhone]", objAppInfo.CompanyPhone);
                        body.Replace("[TEmail]", objAppInfo.CompanyEmail);
                        body.Replace("[SiteName]", objAppInfo.SiteName);
                        body.Replace("[ApplicationName]", objAppInfo.SiteName);

                        BLL.Common.SendMail(objAppInfo.CompanyEmail, "You have a new Volunteer Registration from TASA", body.ToString());
                    }
                    return RedirectToAction("ThankYou", "Volunteer");
                }
                else
                {
                    TempData["message"] = "<div class=\"error closable\">Failed uploading Enquiry Details.</div>";
                    return RedirectToAction("AddVolunteer", "Volunteer");
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"error closable\">Failed Transaction</div>";
                return RedirectToAction("AddVolunteer", "Volunteer");
            }
        }

        public ActionResult ThankYou()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Certicate()
        {
            int status = 0;
            List<Entities.Events> lstEvents = _Events.GetEventsListall(ref status);

            ViewBag.lstEvents = lstEvents;
            return View();
        }

        public ActionResult FEVolunteersList(Int64 VolunteerCategoryId = 0, Int64 EventId = 0, string Email = "", string Search = "", string SortColumn = "UpdatedDate", string SortOrder = "Desc", int PageNo = 1, int Items = 20)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;
            List<Entities.Volunteers> lstVolunteers = new List<Entities.Volunteers>();

            try
            {
                lstVolunteers = _Volunteers.FEGetVolunteersList(VolunteerCategoryId, EventId, Email, Search, Sort, PageNo, Items, ref Total);

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstVolunteers = lstVolunteers;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();
            ViewBag.Email = Email;
            return View();
        }


        [HttpPost]
        public JsonResult CheckEmailAvailability(string Email)
        {
            int status = 0;
            try
            {
                Entities.Volunteers objVolunteers = _Volunteers.VolunteerValidationByEmail(Email, ref status);
                bool data = (objVolunteers != null && objVolunteers.VolunteerId == 0 ? true : false);

                return Json(new { ok = true, data = data, message = "" });
            }
            catch
            {
                return Json(new { ok = false, message = "<div class=\"error closable\">Failed transaction.</div>" });
            }
        }

        [HttpPost]
        public JsonResult GetCertificateandSave(Int64 VolunteerId)
        {
            string str = "";
            int _qstatus = 0;

            string filename = "Log_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            var relativePath = "~/Content/logfiles/" + filename;
            var filepath1 = HttpContext.Server.MapPath(relativePath);

            Entities.Volunteers objVolunteerDetails = new Entities.Volunteers();
            objVolunteerDetails = _Volunteers.GetVolunteerInfoById(VolunteerId, ref _qstatus);

            _HomeController.logreport("Created Volunteer Account", Request.Path, filepath1);

            string filepath = System.Configuration.ConfigurationManager.AppSettings["usersiteurl"] + "Content/Certificates/VolunteerCertificateofTCA" + VolunteerId + ".pdf";

            string pdfsavepath = System.Configuration.ConfigurationManager.AppSettings["servermapurl"] + "Content\\Certificates\\VolunteerCertificateofTCA" + VolunteerId + ".pdf";

            dynamic output = new FileStream(pdfsavepath, FileMode.Create);
            if (objVolunteerDetails != null && objVolunteerDetails.VolunteerId !=0)
            {
                _HomeController.logreport("Entered into Create Image", Request.Path, filepath1);

                using (var doc = new iTextSharp.text.Document())
                {
                    //Create a pdf writer that will hold the instance of PDF abstraction which is doc and the memory stream 
                    iTextSharp.text.pdf.PdfWriter.GetInstance(doc, output);
                    doc.Open();
                    //Open the document for writing
                    doc.Open();

                    #region Ticket Image Generation


                    string str2 = System.Configuration.ConfigurationManager.AppSettings["servermapurl"] + "Content/uploads/images/VolunteerCertificateofTCA.jpg";

                    _HomeController.logreport("Created Str2", Request.Path, filepath1);

                    //Load the Image to be written on.
                    Bitmap bitMapImage = new System.Drawing.Bitmap(str2);
                    Graphics graphicImage = Graphics.FromImage(bitMapImage);

                    //Smooth graphics is nice.
                    graphicImage.SmoothingMode = SmoothingMode.AntiAlias;

                    PrivateFontCollection pfcoll = new PrivateFontCollection();
                    //put a font file under a Fonts directory within your application root
                    string str3 = System.Configuration.ConfigurationManager.AppSettings["servermapurl"] + "Content\\User\\font\\Sofia-Regular.ttf";

                    _HomeController.logreport("Created Str3", Request.Path, filepath1);

                    pfcoll.AddFontFile(str3);
                    FontFamily ff = pfcoll.Families[0];

                    // Pasting Image
                    //graphicImage.DrawImage(orgImg2, 266, 120, 175, 58);
                    Rectangle rect1 = new Rectangle(10, 10, 600, 380);

                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;
                     
                    // Name
                    graphicImage.DrawString((objVolunteerDetails.FirstName + " " + objVolunteerDetails.LastName), new Font(ff, 35), Brushes.Black, new RectangleF(0, -70, bitMapImage.Width, bitMapImage.Height), stringFormat);

                    // Hours
                    string font1 = System.Configuration.ConfigurationManager.AppSettings["servermapurl"] + "Content\\User\\font\\LHANDW.TTF";

                    pfcoll.AddFontFile(font1);
                    FontFamily ff1 = pfcoll.Families[0];

                    graphicImage.DrawString(objVolunteerDetails.HoursSpent + " hours" + " ", new Font(ff1, 22, FontStyle.Bold), Brushes.OrangeRed, new Point(376, 584));

                    // Image Saving
                    //Response.ContentType = "image/jpeg";
                    string str4 = System.Configuration.ConfigurationManager.AppSettings["servermapurl"] + "Content\\Certificates\\" + objVolunteerDetails.VolunteerId + ".jpg";
                    bitMapImage.Save(str4);

                    _HomeController.logreport("Images Saved in Folder", Request.Path, filepath1);

                    //Clean house.
                    graphicImage.Dispose();
                    bitMapImage.Dispose();
                    #endregion
                        #region Pdf Witer

                    iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph("");
                    string imageURL = str4;
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(600, 380);
                    //Give some space after the image
                    jpg.SpacingAfter = 10f;

                    jpg.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                    doc.Add(paragraph);
                    doc.Add(jpg);
                    doc.Add(paragraph);

                    _HomeController.logreport("Created Successflly", Request.Path, filepath1);

                        #endregion
                    doc.Close();
                    str = "<div class=\"success closable\">Certififcate Saved Successfully</div>";
                    return Json(new { ok = true, data = str, data1 = filepath });
                }

            }
            else
            {
                str = "<div class=\"error closable\">No Certififcate Found</div>";
                return Json(new { ok = true, data = str, data1 = "" });
            }
        }

        public JsonResult UpdateVolunteerHours(string HoursSpent, Int64 VolunteerId)
        {
            Int64 _status = 0;
            string str = "";
            _status = _Volunteers.UpdateVolunteerHours(HoursSpent, VolunteerId);
            if (_status == 1)
            {
                str = "<div class=\"alert alert-success alert-dismissable\">Update Hours Successfully</div>";
                return Json(new { ok = true, data = str });
            }
            else
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed Transaction</div>";
                return Json(new { ok = false, data = str });
            }

        }

    }
}
