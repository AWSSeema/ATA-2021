using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PaymentProcess;
using System.Text;
using System.Globalization;
using TCAssociationTool.Areas.User.Models;
using TCAssociationTool.Entities;
using System.Web.Security;
using TCAssociationTool.BLL;
using BluePayLibrary;

namespace TCAssociationTool.Areas.API.Controllers
{
    public class MemberController : Controller
    {
        BLL.InnerPages _InnerPages = new BLL.InnerPages();
        BLL.Members _Members = new BLL.Members();
        BLL.Volunteers _Volunteers = new BLL.Volunteers();
        BLL.MailTemplates _MailTemplates = new BLL.MailTemplates();
        List<Entities.Members> lstMembers = new List<Entities.Members>();
        Entities.Members objMembers = new Entities.Members();
        BLL.MembershipTypes _MembershipType = new BLL.MembershipTypes();
        List<Entities.MembershipTypes> lstMembershipType = new List<Entities.MembershipTypes>();
        List<Entities.Volunteers> lstVolunteers = new List<Entities.Volunteers>();
        BLL.PaymentSettings _paymentSettings = new BLL.PaymentSettings();
        PayByPayPal objPayByPayPal = new PayByPayPal();
        TCAssociationTool.BLL.AppInfo _appinfo = new BLL.AppInfo();
        Entities.Members objMemberDetails = new Entities.Members();

        #region Membership

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddMember(Int64 MemberId = 0)
        {
            Entities.InnerPages objmembershipcontent = new Entities.InnerPages();
            try
            {
                int _qstatus = 0;
                int PageId = 0;
                int status = 0;
                string PageTitle = "MembershipForm Content";
                objmembershipcontent = _InnerPages.GetInnerPagesListById(PageId, PageTitle, ref _qstatus);
                objMembers = _Members.AddMembershipRequirement(ref _qstatus);

                objMemberDetails = _Members.FEMemberGetFullDetails(MemberId, ref status);

                if (_qstatus == 1)
                {
                    ViewBag.objMembers = objMembers;
                    ViewBag.status = _qstatus;
                }
                else
                {
                    TempData["message"] = "<div class=\"error closable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "Member");
                }
                if (status == 1)
                {
                    ViewBag.objMemberDetails = objMemberDetails;
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"error closable\">Failed Transaction</div>";
                return RedirectToAction("Index", "Member");
            }
            ViewBag.objmembershipcontent = objmembershipcontent;
            return View();
        }

        // Member register by Paypal / Cash/Check
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddMember(Entities.Members objMembers, List<Entities.ChildrenInfo> lstChildrenInfo, Entities.MembershipTypes objMembershipType, HttpPostedFileBase file, string Phc1 = "", string Phc2 = "", string Phc3 = "", string MCaptcha = "")
        {
            try
            {
                if (Session["captchastring"].ToString() == MCaptcha)
                {
                    var image = WebImage.GetImageFromRequest();
                    string imageurl = (image != null ? image.ImageFormat : "NA");
                    Guid guid = TCAssociationTool.BLL.Common.generateGUID();
                    if (objMembers.PaymentMethod == "Cash/Cheque")
                    {
                        objMembers.IsApproved = false;
                    }
                    else
                    {
                        objMembers.IsApproved = false;
                    }
                    objMembers.UpdatedTime = DateTime.UtcNow;
                    objMembers.IsLockedOut = false;
                    objMembers.Password = Password.ComputeHash(objMembers.Password.Trim(), "SHA512", null);
                    objMembers.IsActivated = false;
                    objMembers.RegistrationGUID = guid;
                    objMembers.UserName = objMembers.FirstName + " " + objMembers.LastName;
                    objMembers.MobilePhone = objMembers.MobilePhone;
                    objMembers.DateActivated = DateTime.MinValue;
                    objMembers.LastPasswordChangedDate = DateTime.MinValue;
                    objMembers.SpouseSkils = objMembers.SpouseSkils;
                    objMembers.ExpiryDate = objMembers.ExpiryDate;
                    objMembers.UserName = objMembers.FirstName + " " + objMembers.LastName;

                    Int64 MemberId = 0;
                    objMembers.objMembershipOrder.UpdatedBy = HttpContext.User.Identity.Name.ToString();
                    Int64 _status = _Members.FEInsertMembers(objMembers, ref MemberId, ref imageurl);
                    if (_status == 1)
                    {
                        if (imageurl != "" && imageurl != "NA")
                        {
                            image.Resize(94, 93, true, false);
                            image.Crop(1, 1, 1, 1);
                            image.Save(ConfigurationManager.AppSettings["adminuploadPath"] + "\\Members\\" + imageurl);
                        }
                        if (lstChildrenInfo != null && lstChildrenInfo.Count() != 0)
                        {
                            foreach (Entities.ChildrenInfo objChildrenInfo in lstChildrenInfo)
                            {
                                objChildrenInfo.MemberId = MemberId;
                                if (objChildrenInfo != null && objChildrenInfo.FirstName != null)
                                {
                                    Int64 estatus = _Members.InsertChildrenInfo(objChildrenInfo);
                                    if (estatus == -1)
                                    {
                                        TempData["message"] = "<div class=\"error closable\">Failed inserting member details.</div>";
                                        return RedirectToAction("Acknowledgement", "Member");
                                    }
                                }
                            }
                        }
                        if (objMembers.MemberAmount != 0)
                        {

                            if (objMembers.PaymentMethod == "PayPal")
                            {
                                int status = 0;
                                Entities.PaymentSettings objPaymentSettings = _paymentSettings.GetPaymentSettingsDetails(0, "PayPal", ref status);
                                if (objPaymentSettings != null)
                                {

                                    List<PayByPayPal.CartItems> lstCartItems = new List<PayByPayPal.CartItems>();

                                    PayByPayPal.CartItems objCartItems = new PayByPayPal.CartItems();
                                    objCartItems.ItemName = "Member Type :" + objMembers.MembershipType;
                                    objCartItems.ItemPrice = objMembers.MemberAmount;
                                    lstCartItems.Add(objCartItems);

                                    objPayByPayPal.lstCartItems = lstCartItems;

                                    PayByPayPal.PaymentSettings objpaymentsettings = new PayByPayPal.PaymentSettings();
                                    objpaymentsettings.BusinessEmail = objPaymentSettings.PaymentEmail;
                                    objpaymentsettings.BusinessPassword = objPaymentSettings.PaymentPassword;
                                    objpaymentsettings.CurrencyCode = objPaymentSettings.CurrencyCode;
                                    objpaymentsettings.NotifyUrl = ConfigurationManager.AppSettings["baseurl"] + "member/ipn.html" + "?id=" + MemberId;
                                    objpaymentsettings.PaymentUrl = objPaymentSettings.PaymentUrl;
                                    objpaymentsettings.ReturnUrl = ConfigurationManager.AppSettings["baseurl"].ToString() + "member-registration-acknowledgement.html" + "?MemberId=" + MemberId + "&PaymentMethod=" + objMembers.PaymentMethod;
                                    objpaymentsettings.Cmd = "_cart";
                                    objpaymentsettings.PDTToken = objPaymentSettings.TokenNo;
                                    objpaymentsettings.CBT = "Click here to complete your transaction";
                                    objPaymentSettings.CancelUrl = "index.html";

                                    objPayByPayPal.objPaymentSettings = objpaymentsettings;

                                    string returnurl = objPayByPayPal.CheckOut();

                                    return new RedirectResult(returnurl);
                                }
                            }
                            else if (objMembers.PaymentMethod == "Cash/Cheque")
                            {
                                Int64 result = 0;
                                Entities.MembershipOrders objMembershipOrders = new Entities.MembershipOrders();
                                objMembershipOrders.Amount = objMembers.MemberAmount;
                                objMembershipOrders.ExpiryDate = objMembers.ExpiryDate;
                                objMembershipOrders.PaymentMethod = "Cash/Cheque";
                                objMembershipOrders.PaymentStatus = "Pending";
                                objMembershipOrders.MemberId = MemberId;
                                objMembershipOrders.PaymentBy = objMembers.PaymentBy;
                                objMembershipOrders.MembershipTypeId = objMembers.MembershipTypeId;
                                objMembershipOrders.UpdatedBy = objMembers.UserName;


                                result = _Members.InsertMemberOrder(objMembershipOrders);
                                if (result == 1)
                                { return RedirectToAction("Acknowledgement", "Member", new { MemberId = MemberId, PaymentMethod = objMembers.PaymentMethod }); }
                            }
                        }
                        else
                        {

                            Int64 result = 0;
                            Entities.MembershipOrders objMembershipOrders = new Entities.MembershipOrders();
                            objMembershipOrders.Amount = objMembers.MemberAmount;
                            objMembershipOrders.ExpiryDate = objMembers.ExpiryDate;
                            objMembershipOrders.PaymentMethod = "Free";
                            objMembershipOrders.PaymentStatus = "Pending";
                            objMembershipOrders.MemberId = MemberId;
                            objMembershipOrders.PaymentBy = "N/A";
                            objMembershipOrders.MembershipTypeId = objMembers.MembershipTypeId;
                            objMembershipOrders.UpdatedBy = objMembers.UserName;
                            result = _Members.InsertMemberOrder(objMembershipOrders);
                            int Status = 0;
                            Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("Thank You for Free Membership", 0, ref Status);
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
                                body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objMembers.FirstName + " " + objMembers.LastName));
                                body.Replace("[MemberId]", objMembers.SpouseCell);
                                body.Replace("[MEMBERID]", MemberId.ToString());
                                body.Replace("[UserName]", BLL.Common.UppercaseFirst(objMembers.FirstName + " " + objMembers.LastName));
                                body.Replace("[FirstName]", objMembers.FirstName);
                                body.Replace("[LastName]", objMembers.LastName);
                                body.Replace("[Email]", objMembers.Email);
                                body.Replace("[MobilePhone]", objMembers.MobilePhone);
                                body.Replace("[IntrestedArea]", objMembers.SpouseSkils);
                                body.Replace("[MembershipType]", objMembers.MembershipType);
                                body.Replace("[TransactionId]", objMembers.TransactionId);
                                body.Replace("[PaymentType]", "Credit Card");
                                body.Replace("[PaymentStatus]", objMembers.PaymentStatus);

                                BLL.Common.SendMailwithfrom(objMembers.Email, ConfigurationManager.AppSettings["MemberEmailID"], objTemplates.Subject, body.ToString());
                            }
                            return RedirectToAction("Thankyou", "Member");
                        }
                        TempData["message"] = "<div class=\"success closable\">Your registration completed successfully and our team will contact soon.</div>";
                        return RedirectToAction("Acknowledgement", "Member");

                    }
                    else
                    {
                        TempData["message"] = "<div class=\"error closable\">Failed Transaction.</div>";
                        return RedirectToAction("AddMember", "Member");
                    }
                }
                else
                {
                    TempData["message"] = "<div class=\"error closable\">Failed Transaction.</div>";
                    return RedirectToAction("AddMember", "Member");
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"error closable\">Failed Transaction</div>";
                return RedirectToAction("AddMember", "Member");
            }

        }

        // PayPal IPN
        public ActionResult MemberIPN(Int64 id = 0)
        {
            int result1 = 0;
            Entities.Members objMembers = new Entities.Members();
            PayByPayPal.PDTHolder objPDT = new PayByPayPal.PDTHolder();
            Entities.MembershipOrders objMembershipOrders = new Entities.MembershipOrders();
            objMembers = _Members.GetMemberFullDetailsById(id, ref result1);
            string status = "";
            try
            {
                int qstatus = 0;
                Entities.PaymentSettings objPaymentSettings = _paymentSettings.GetPaymentSettingsDetails(0, "PayPal", ref qstatus);
                objPayByPayPal.objPaymentSettings.PaymentUrl = objPaymentSettings.PaymentUrl;
                status = objPayByPayPal.ipn();
                //BLL.Common.SendMail("g.vinod@arjunweb.in",  "testIPN", status.ToString());
                if (status == "VERIFIED")
                {
                    objPDT = objPayByPayPal.objPDTHolder;
                    objMembershipOrders.PaymentMethod = "PayPal";
                    objMembershipOrders.ExpiryDate = objMembers.ExpiryDate;
                    objMembershipOrders.TransactionId = objPDT.TransactionId;
                    objMembershipOrders.PaymentStatus = objPDT.PaymentStatus;
                    objMembershipOrders.UpdatedBy = objMembers.FirstName;
                    objMembershipOrders.MemberId = objMembers.MemberId;
                    objMembershipOrders.MembershipTypeId = objMembers.MembershipTypeId;
                    objMembershipOrders.Amount = Convert.ToDecimal(objPDT.GrossTotal);

                    Int64 result = _Members.InsertMemberOrder(objMembershipOrders);
                }

            }
            catch
            {
                TempData["message"] = "Your payment did not go through please contact  board@TeluguAssociationToolweb.org for assistance.";

            }

            ViewBag.status = status;

            return View();
        }

        // PayPal Acknowledgement
        public ActionResult Acknowledgement(Int64 MemberId = 0, decimal amt = 0, string PaymentMethod = "PayPal", string status = "", string OrderType = "", string transid = "", string message = "", string avs = "", string cvv2 = "", string cardtype = "", string authcode = "")
        {
            DateTime paydate = DateTime.UtcNow;
            try
            {
                Entities.Members objMembers = new Entities.Members();
                int status1 = 0;
                Int64 result = 0;
                int result1 = 0;
                string Username = "";
                objMembers = _Members.GetMemberFullDetailsById(MemberId, ref result1);
                Entities.AppInfo objAppInfo = _appinfo.GetAppInfoDetails(ref status1);
                if (PaymentMethod == "PayPal")
                {
                    PayByPayPal.PDTHolder objPDT = new PayByPayPal.PDTHolder();
                    Entities.PaymentSettings objPaymentSettings = _paymentSettings.GetPaymentSettingsDetails(0, "PayPal", ref status1);
                    objPayByPayPal.objPaymentSettings.PDTToken = objPaymentSettings.TokenNo;
                    objPayByPayPal.objPaymentSettings.PaymentUrl = objPaymentSettings.PaymentUrl;

                    status1 = objPayByPayPal.Success();
                    if (status1 == 0)
                    {
                        objPDT = objPayByPayPal.objPDTHolder;
                        if (objMembers.UserName != null && objMembers.UserName != "")
                        {
                            Username = objMembers.UserName;
                        }
                        else
                        {
                            Username = HttpContext.User.Identity.Name.ToString();
                        }
                        if (Username != null)
                        {
                            if (result1 == 1)
                            {
                                Entities.MembershipOrders objMembershipOrders = new Entities.MembershipOrders();
                                objMembershipOrders.OrderType = OrderType;
                                objMembershipOrders.Amount = objMembers.MemberAmount;
                                objMembershipOrders.ExpiryDate = objMembers.ExpiryDate;
                                objMembershipOrders.PaymentMethod = "PayPal";
                                objMembershipOrders.TransactionId = objPDT.TransactionId;
                                objMembershipOrders.PaymentStatus = objPDT.PaymentStatus;
                                objMembershipOrders.MemberId = objMembers.MemberId;
                                objMembershipOrders.MembershipTypeId = objMembers.MembershipTypeId;
                                objMembershipOrders.MembershipType = objMembers.MembershipType;
                                objMembershipOrders.OrderDate = ConvertPayPalDateTime(objPDT.PaymentDate.ToString());
                                objMembershipOrders.UpdatedBy = Username;

                                result = _Members.InsertMemberOrder(objMembershipOrders);
                                if (result == 1)
                                {
                                    ViewBag.PayerEmail = objPDT.PayerEmail;
                                    ViewBag.OrderType = objMembershipOrders.OrderType;
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
                                    ViewBag.MemberId = MemberId;
                                    DateTime Date = paydate = DateTime.UtcNow;
                                    ViewBag.PaymentDate = Date.ToString("MM/dd/yyyy");
                                }

                                int _status1 = 0; Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("Thank you for Becoming a Member", 0, ref _status1);
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
                                    body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objMembers.FirstName + " " + objMembers.LastName));
                                    body.Replace("[MemberId]", objMembers.SpouseCell);
                                    body.Replace("[MEMBERID]", objMembers.MemberId.ToString());
                                    body.Replace("[FirstName]", objMembers.FirstName);
                                    body.Replace("[LastName]", objMembers.LastName);
                                    body.Replace("[IntrestedArea]", objMembers.SpouseSkils);
                                    body.Replace("[SpouseEmail]", objMembers.SpouseEmail);
                                    body.Replace("[Email]", objMembers.Email);
                                    body.Replace("[MobilePhone]", objMembers.MobilePhone);
                                    body.Replace("[MembershipType]", objMembers.MembershipType);
                                    body.Replace("[TransactionId]", objPDT.TransactionId);
                                    body.Replace("[PaymentType]", "PayPal");
                                    body.Replace("[PaymentStatus]", objPDT.PaymentStatus);
                                    body.Replace("[PaymentDate]", paydate.ToShortDateString());
                                    body.Replace("[SiteName]", objAppInfo.SiteName);
                                    BLL.Common.SendMail(objMembers.Email, objTemplates.Subject, body.ToString());
                                }


                                int statusAdmin = 0;
                                Entities.MailTemplates objTemplatesAdmin = _MailTemplates.GetMailTemplateById("Admin Member Registration", 0, ref statusAdmin);
                                if (objTemplatesAdmin != null && objTemplatesAdmin.MailTemplateId != 0)
                                {
                                    StringBuilder body1 = new StringBuilder();
                                    body1.Append(objTemplatesAdmin.Description);
                                    body1.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                                    body1.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                                    body1.Replace("[TWUrl]", objAppInfo.TwitterUrl);
                                    body1.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                                    body1.Replace("[GUrl]", objAppInfo.SupportEmail);
                                    body1.Replace("[TPhone]", objAppInfo.CompanyPhone);
                                    body1.Replace("[TEmail]", objAppInfo.CompanyEmail);
                                    body1.Replace("[MemberId]", objMembers.SpouseCell);
                                    body1.Replace("[MEMBERID]", objMembers.MemberId.ToString());
                                    body1.Replace("[FirstName]", objMembers.FirstName);
                                    body1.Replace("[LastName]", objMembers.LastName);
                                    body1.Replace("[Email]", objMembers.Email);
                                    body1.Replace("[MobilePhone]", objMembers.MobilePhone);
                                    body1.Replace("[MemberSkils]", objMembers.MemberSkils);
                                    body1.Replace("[IntrestedArea]", objMembers.SpouseSkils);
                                    body1.Replace("[SpouseCell]", objMembers.SpouseCell);
                                    body1.Replace("[SpouseEmail]", objMembers.SpouseEmail);
                                    body1.Replace("[MembershipType]", objMembers.MembershipType);
                                    body1.Replace("[TransactionId]", objPDT.TransactionId);
                                    body1.Replace("[PaymentType]", "PayPal");
                                    body1.Replace("[PaymentStatus]", objPDT.PaymentStatus);
                                    body1.Replace("[PaymentDate]", paydate.ToShortDateString());
                                    body1.Replace("[SiteName]", objAppInfo.SiteName);

                                    BLL.Common.SendMailwithCC(objAppInfo.SecretaryPhone, ConfigurationManager.AppSettings["adminemailid"], objAppInfo.PresidentPhone + "," + objAppInfo.CompanyEmail, "Registered Member Details - HTCA", body1.ToString());
                                }
                            }
                        }
                    }
                    else
                    {
                        int _status1 = 0; Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("Thank you for Becoming a Member", 0, ref _status1);
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
                            body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objMembers.FirstName + " " + objMembers.LastName));
                            body.Replace("[MemberId]", objMembers.SpouseCell);
                            body.Replace("[MEMBERID]", objMembers.MemberId.ToString());
                            body.Replace("[FirstName]", objMembers.FirstName);
                            body.Replace("[LastName]", objMembers.LastName);
                            body.Replace("[Email]", objMembers.Email);
                            body.Replace("[MobilePhone]", objMembers.MobilePhone);
                            body.Replace("[MemberSkils]", objMembers.MemberSkils);
                            body.Replace("[IntrestedArea]", objMembers.SpouseSkils);
                            body.Replace("[SpouseCell]", objMembers.SpouseCell);
                            body.Replace("[SpouseEmail]", objMembers.SpouseEmail);
                            body.Replace("[MembershipType]", objMembers.MembershipType);
                            body.Replace("[TransactionId]", objPDT.TransactionId);
                            body.Replace("[PaymentType]", "PayPal");
                            body.Replace("[PaymentStatus]", objPDT.PaymentStatus);
                            body.Replace("[PaymentDate]", paydate.ToShortDateString());
                            body.Replace("[SiteName]", objAppInfo.SiteName);
                            BLL.Common.SendMail(objMembers.Email, objTemplates.Subject, body.ToString());
                        }
                        return RedirectToAction("Thankyou", "Member");
                    }
                }
                else if (PaymentMethod == "Cash/Cheque")
                {
                    int _status1 = 0; Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("Thank you for Part of a Member", 0, ref _status1);
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
                        body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objMembers.FirstName + " " + objMembers.LastName));
                        body.Replace("[MemberId]", objMembers.SpouseCell);
                        body.Replace("[MEMBERID]", objMembers.MemberId.ToString());
                        body.Replace("[FirstName]", objMembers.FirstName);
                        body.Replace("[LastName]", objMembers.LastName);
                        body.Replace("[Email]", objMembers.Email);
                        body.Replace("[MobilePhone]", objMembers.MobilePhone);
                        body.Replace("[SpouseCell]", objMembers.SpouseCell);
                        body.Replace("[SpouseEmail]", objMembers.SpouseEmail);
                        body.Replace("[MembershipType]", objMembers.MembershipType);
                        body.Replace("[TransactionId]", "");
                        body.Replace("[PaymentType]", "Cash/Cheque");
                        body.Replace("[PaymentStatus]", "pending");
                        body.Replace("[PaymentDate]", paydate.ToShortDateString());
                        body.Replace("[SiteName]", objAppInfo.SiteName);

                        BLL.Common.SendMail(objMembers.Email, objTemplates.Subject, body.ToString());
                    }
                    int statusAdmin = 0;
                    Entities.MailTemplates objTemplatesAdmin = _MailTemplates.GetMailTemplateById("Admin Member Registration Details", 0, ref statusAdmin);
                    if (objTemplatesAdmin != null && objTemplatesAdmin.MailTemplateId != 0)
                    {
                        StringBuilder body1 = new StringBuilder();
                        body1.Append(objTemplatesAdmin.Description);
                        body1.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                        body1.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                        body1.Replace("[TWUrl]", objAppInfo.TwitterUrl);
                        body1.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                        body1.Replace("[GUrl]", objAppInfo.SupportEmail);
                        body1.Replace("[TPhone]", objAppInfo.CompanyPhone);
                        body1.Replace("[TEmail]", objAppInfo.CompanyEmail);
                        body1.Replace("[MemberId]", objMembers.SpouseCell);
                        body1.Replace("[MEMBERID]", objMembers.MemberId.ToString());
                        body1.Replace("[FirstName]", objMembers.FirstName);
                        body1.Replace("[LastName]", objMembers.LastName);
                        body1.Replace("[Email]", objMembers.Email);
                        body1.Replace("[MobilePhone]", objMembers.MobilePhone);
                        body1.Replace("[SpouseCell]", objMembers.SpouseCell);
                        body1.Replace("[SpouseEmail]", objMembers.SpouseEmail);
                        body1.Replace("[MembershipType]", objMembers.MembershipType);
                        body1.Replace("[TransactionId]", "");
                        body1.Replace("[PaymentType]", "Cash/Cheque");
                        body1.Replace("[PaymentStatus]", "pending");
                        body1.Replace("[PaymentDate]", paydate.ToShortDateString());
                        body1.Replace("[SiteName]", objAppInfo.SiteName);

                        BLL.Common.SendMailwithCC(objAppInfo.SecretaryPhone, ConfigurationManager.AppSettings["adminemailid"], objAppInfo.PresidentPhone + "," + objAppInfo.CompanyEmail, "Member Registration Details - HTCA", body1.ToString());
                    }
                    ViewBag.UserName = objMembers.UserName;
                    return RedirectToAction("Thankyou", "Member");
                }
            }
            catch
            {
                return RedirectToAction("Error404", "Error");
            }
            return View();
        }

        public ActionResult Thankyou()
        {
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

        public ActionResult GetMemberAmount(Int64 MembershipTypeId = 0)
        {
            string str = "";
            try
            {
                int _qstatus = 0;
                Entities.MembershipTypes _objMembershipTypes = _MembershipType.GetMembershipTypeById(MembershipTypeId, ref _qstatus);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true, data = _objMembershipTypes });
                }
                else
                {
                    str = "<div class=\"error closable\">Failed Transaction</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch
            {
                str = "<div class=\"error closable\">Failed Transaction</div>";
                return Json(new { ok = false, data = str });
            }
        }

        [HttpPost]
        public JsonResult CheckProfileEmailAvailability(Int64 MemberId, string Email)
        {
            int status = 0;
            try
            {
                objMembers = _Members.GetMemberFullDetailsByEmail(Email, ref status);
                bool data = (objMembers.MemberId == MemberId || objMembers.MemberId == 0 ? true : false);

                return Json(new { ok = true, data = data, message = "" });
            }
            catch
            {
                return Json(new { ok = false, message = "<div class=\"error closable\">Failed Transaction</div>" });
            }
        }

        [HttpPost]
        public JsonResult CheckEmailAvailability(string Email)
        {
            int status = 0;
            try
            {
                objMembers = _Members.FEGetMemberFullDetailsByEmail(Email, ref status);
                bool data = (objMembers != null && objMembers.MemberId != 0 ? false : true);

                return Json(new { ok = true, data = data, message = "" });
            }
            catch
            {
                return Json(new { ok = false, message = "<div class=\"error closable\">Failed Transaction</div>" });
            }
        }

        [HttpPost]
        public JsonResult CheckUserNameAvailability(string UserName)
        {
            int status = 0;
            try
            {
                objMembers = _Members.GetMemberFullDetailsByUserName(UserName, ref status);
                bool data = (objMembers != null && objMembers.MemberId != 0 ? false : true);

                return Json(new { ok = true, data = data, message = "" });
            }
            catch
            {
                return Json(new { ok = false, message = "<div class=\"error closable\">Failed Transaction</div>" });
            }
        }

        [HttpPost]
        public JsonResult CheckMemberIdAvailability(string SpouseCell, string LastName)
        {
            int status = 0;
            try
            {
                objMembers = _Members.GetMemberFullDetailsBySpouseCell(SpouseCell, LastName, ref status);
                bool data = (objMembers != null && objMembers.MemberId != 0 ? false : true);

                return Json(new { ok = true, data = data, message = "" });
            }
            catch
            {
                return Json(new { ok = false, message = "<div class=\"error closable\">Failed Transaction</div>" });
            }
        }

        [HttpPost]
        public JsonResult CheckMemberIdAvailabilityBySpouse(string SpouseCell)
        {
            int status = 0;
            try
            {
                objMembers = _Members.GetMemberFullDetailsBySpouse(SpouseCell, ref status);
                bool data = (objMembers != null && objMembers.MemberId != 0 ? false : true);

                return Json(new { ok = true, data = data, message = "" });
            }
            catch
            {
                return Json(new { ok = false, message = "<div class=\"error closable\">Failed Transaction</div>" });
            }
        }

        [HttpPost]
        public JsonResult CheckMemberIdAvailabilityByLastName(string LastName)
        {
            int status = 0;
            try
            {
                objMembers = _Members.GetMemberFullDetailsByLastName(LastName, ref status);
                bool data = (objMembers != null && objMembers.MemberId != 0 ? false : true);

                return Json(new { ok = true, data = data, message = "" });
            }
            catch
            {
                return Json(new { ok = false, message = "<div class=\"error closable\">Failed Transaction</div>" });
            }
        }

        #endregion

    }
}
