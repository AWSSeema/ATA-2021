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

namespace TCAssociationTool.Areas.User.Controllers
{
    public class MembersController : Controller
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
               string PageTitle ="MembershipForm Content";
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
                    return RedirectToAction("Index", "Members");
                }
                if (status == 1)
                {
                    ViewBag.objMemberDetails = objMemberDetails;
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"error closable\">Failed Transaction</div>";
                return RedirectToAction("Index", "Members");
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
                                        return RedirectToAction("Acknowledgement", "Members");
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
                                    //objPaymentSettings.CancelUrl = "index.html";
                                    objpaymentsettings.CancelUrl = ConfigurationManager.AppSettings["baseurl"] + "index.html";

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
                                { return RedirectToAction("Acknowledgement", "Members", new { MemberId = MemberId, PaymentMethod = objMembers.PaymentMethod }); }
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
                            return RedirectToAction("Thankyou", "Members");
                        }
                        TempData["message"] = "<div class=\"success closable\">Your registration completed successfully and our team will contact soon.</div>";
                        return RedirectToAction("Acknowledgement", "Members");

                    }
                    else
                    {
                        TempData["message"] = "<div class=\"error closable\">Failed Transaction.</div>";
                        return RedirectToAction("AddMember", "Members");
                    }
                }
                else {
                    TempData["message"] = "<div class=\"error closable\">Failed Transaction.</div>";
                    return RedirectToAction("AddMember", "Members");
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"error closable\">Failed Transaction</div>";
                return RedirectToAction("AddMember", "Members");
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
        public ActionResult Acknowledgement(Int64 MemberId = 0, decimal amt = 0, string PaymentMethod = "PayPal", string status="", string OrderType = "", string transid = "", string message = "", string avs = "", string cvv2 = "", string cardtype = "", string authcode = "")
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

                                    BLL.Common.SendMailwithfrom(objAppInfo.CompanyEmail, ConfigurationManager.AppSettings["adminemailid"], "Registered Member Details - TLCA", body1.ToString());
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
                        return RedirectToAction("Thankyou", "Members");
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

                        BLL.Common.SendMailwithfrom(objAppInfo.CompanyEmail, ConfigurationManager.AppSettings["adminemailid"], "Member Registration Details - TLCA", body1.ToString());
                    }
                    ViewBag.UserName = objMembers.UserName;
                    return RedirectToAction("Thankyou", "Members");
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

        #region Member Account

        public ActionResult LogOn()
        {
            string url = "profile.html";
            if (Request.UrlReferrer != null && Request.UrlReferrer.ToString() != ConfigurationManager.AppSettings["baseurl"] + "member-login.html" && Request.UrlReferrer.ToString() != ConfigurationManager.AppSettings["baseurl"] + "index.html" && Request.UrlReferrer.ToString() != ConfigurationManager.AppSettings["baseurl"] + "forgot-password.html" && Request.UrlReferrer.ToString().Contains(ConfigurationManager.AppSettings["baseurl"] + "member-acknowledgement.html") && Request.UrlReferrer.ToString().Contains(ConfigurationManager.AppSettings["baseurl"] + "member-registration-acknowledgement.html"))
            {
                url = Request.UrlReferrer.ToString();
            } 
            ViewBag.returnurl = url;
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string ReturnUrl = "")
        {
            string error = "";
            string lmsg = "";
            try
            {

                ViewBag.returnurl = ReturnUrl;
                int _status = 0;
                Entities.Members objMembers = _Members.GetMemberFullDetailsByEmail(model.Email, ref _status);

                if (objMembers != null && objMembers.MemberId != 0)
                {
                    if (objMembers.IsApproved == true)
                    {
                        int _qstatus = 0;
                        string password = _Members.GetPassword(objMembers.MemberId, ref _qstatus);
                        if (_qstatus == 1)
                        {
                            if (TCAssociationTool.BLL.Password.VerifyHash(model.Password.Trim(), "SHA512", password) == true)
                            {
                                Session["username"] = objMembers.UserName;
                                Session["MemberID"] = objMembers.MemberId;
                                FormsAuthentication.SetAuthCookie(objMembers.Email, model.RememberMe);
                                return Json(new { ok = true, data = "test" });
                                //if ((ReturnUrl != null && ReturnUrl != "" && ReturnUrl != ConfigurationManager.AppSettings["baseurl"] + "member-registration.html" && ReturnUrl != ConfigurationManager.AppSettings["baseurl"] + "index.html" && ReturnUrl != ConfigurationManager.AppSettings["baseurl"] + "member-login.html") || Request.UrlReferrer.ToString().Contains(ConfigurationManager.AppSettings["baseurl"] + "member-acknowledgement.html") || Request.UrlReferrer.ToString().Contains(ConfigurationManager.AppSettings["baseurl"] + "member-registration-acknowledgement.html"))
                                //{
                                //    return new RedirectResult(ReturnUrl);
                                //}
                            }
                            else
                            {
                                lmsg = "Email or password is incorrect.";
                                return Json(new { ok = false, data = lmsg });
                            }
                        }
                        else
                        {
                            lmsg = "Email or password is incorrect.";
                            return Json(new { ok = false, data = lmsg });
                        }

                    }
                    else
                    {
                        lmsg = "Your status has been deactivated.Please contact to admin.";
                        return Json(new { ok = false, data = lmsg });
                    }
                }
                else
                {
                    lmsg = "Email or password is incorrect.";
                    return Json(new { ok = false, data = lmsg });
                }
            }
            catch
            {
                lmsg = "Failed Transaction";
                return Json(new { ok = false, data = lmsg });
            }
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            try
            {
                int _status = 0;
                Entities.Members _objMember = new Entities.Members();
                if (model.Email != null && model.Email != "")
                {
                    _objMember = _Members.GetMemberFullDetailsByEmail(model.Email, ref _status);
                }
                else if (model.UserName != null && model.UserName != "")
                {
                    _objMember = _Members.GetMemberFullDetailsByEmail(model.UserName, ref _status);
                }

                if (_objMember.MemberId == 0)
                {
                    TempData["forgot"] = "Email or User Name is not valid.";
                }
                else
                {
                    string _password = BLL.Password.GetUniqueKey(8);
                    string _passwordhash = BLL.Password.ComputeHash(_password, "SHA512", null);

                    Int64 _pstatus = _Members.ChangePassword(_objMember.MemberId, _passwordhash);
                    if (_pstatus == 1)
                    {
                        int status = 0;
                        Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("Forgot Password ", 0, ref status);
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
                            body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(_objMember.FirstName));
                            body.Replace("[EMAIL]", _objMember.Email);
                            body.Replace("[PASSWORD]", _password);
                            BLL.Common.SendMail(_objMember.Email, objTemplates.Subject, body.ToString());
                        }

                        TempData["forgot"] = "Password details sent to Email Id registered.";
                    }
                    else
                    {
                        TempData["forgot"] = "Failed generating password.";
                    }
                }
            }
            catch
            {
                TempData["forgot"] = "Failed Transaction";
            }
            return RedirectToAction("Index1", "Home");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear(); // This may not be needed -- but can't hurt

            // Clear authentication cookie
            HttpCookie rFormsCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            rFormsCookie.Expires = DateTime.UtcNow.AddYears(-1);
            Response.Cookies.Add(rFormsCookie);

            // Clear session cookie
            HttpCookie rSessionCookie = new HttpCookie("ASP.NET_SessionId", "");
            rSessionCookie.Expires = DateTime.UtcNow.AddYears(-1);
            Response.Cookies.Add(rSessionCookie);

            return RedirectToAction("Index1", "Home");
        }

        #endregion

        #region Profile

        [TCAssociationTool.Areas.User.Models.SessionClass.SessionExpireFilter]
        public ActionResult Profile()
        {
            Entities.Members _objMember = new Entities.Members();
            try
            {
                int _qstatus = 0;
                _objMember = _Members.GetMemberFullDetailsByEmail(HttpContext.User.Identity.Name.ToString(), ref _qstatus);

                Int64 MemberId = _objMember.MemberId;

                int status = 0;

                lstVolunteers = _Volunteers.FEVolunteersList(MemberId, ref status);

                if (_qstatus == 1)
                {
                    ViewBag.objMemberDetails = _objMember;
                    ViewBag.status = _qstatus;
                }
                if (status == 1)
                {
                    ViewBag.lstVolunteers = lstVolunteers;
                }
                else
                {
                    TempData["message"] = "<div class=\"error closable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "Members");
                }

                if (_objMember.MemberId == 0)
                {
                    TempData["message"] = "<div class=\"error closable\">Failed transaction.</div>";
                    return RedirectToAction("LogOff", "Members");
                }
            }
            catch
            {
                return RedirectToAction("Error404", "Error");
            }

            ViewBag.objMembers = _objMember;
            return View();
        }


        [TCAssociationTool.Areas.User.Models.SessionClass.SessionExpireFilter]
        public ActionResult ProfileEdit()
        {
            Entities.Members _objMember = new Entities.Members();
            try
            {
                int _qstatus = 0;
                _objMember = _Members.GetMemberFullDetailsByEmail(HttpContext.User.Identity.Name.ToString(), ref _qstatus);

                if (_objMember.MemberId == 0)
                {
                    TempData["message"] = "<div class=\"error closable\">Failed transaction.</div>";
                    return RedirectToAction("Index1", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Error404", "Error");
            }
            ViewBag.objMembers = _objMember;
            return View();
        }

        public ActionResult MembershipRenewal(string MembershipType = "", string str = "")
        {
            Entities.Members _objMember = new Entities.Members();
            try
            {
                int _qstatus = 0;
                string Email = "";
                if (HttpContext.User.Identity.Name.ToString() == "")
                { }
                objMembers = _Members.AddMembershipRequirement(ref _qstatus);
                _objMember = _Members.GetMemberFullDetailsByEmail(HttpContext.User.Identity.Name.ToString(), ref _qstatus);

                if (_objMember.MemberId == 0)
                {
                    TempData["message"] = "<div class=\"error closable\">Failed transaction.</div>";
                    return RedirectToAction("Index1", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Error404", "Error");
            }
            ViewBag.MembershipType = MembershipType;

            ViewBag.objMemberDetails = _objMember;
            ViewBag.objMembers = objMembers;
            ViewBag.str = str;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MembershipRenewal(Entities.Members objMembers)
        {
            try
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
                        objCartItems.ItemPrice = Math.Round(objMembers.MemberAmount);
                        lstCartItems.Add(objCartItems);

                        objPayByPayPal.lstCartItems = lstCartItems;

                        PayByPayPal.PaymentSettings objpaymentsettings = new PayByPayPal.PaymentSettings();
                        objpaymentsettings.BusinessEmail = objPaymentSettings.PaymentEmail;
                        objpaymentsettings.BusinessPassword = objPaymentSettings.PaymentPassword;
                        objpaymentsettings.CurrencyCode = objPaymentSettings.CurrencyCode;
                        objpaymentsettings.NotifyUrl = objPaymentSettings.NotifyUrl;
                        objpaymentsettings.PaymentUrl = objPaymentSettings.PaymentUrl;
                        objpaymentsettings.ReturnUrl = ConfigurationManager.AppSettings["baseurl"].ToString() + "member-renewal-acknowledgement.html" + "?MemberId=" + objMembers.MemberId + "&PaymentMethod=" + objMembers.PaymentMethod;
                        objpaymentsettings.Cmd = "_cart";
                        objpaymentsettings.PDTToken = objPaymentSettings.TokenNo;
                        objpaymentsettings.CBT = "Click here to complete your transaction";

                        objPayByPayPal.objPaymentSettings = objpaymentsettings;

                        string returnurl = objPayByPayPal.CheckOut();
                        Int64 result = 0;
                        Entities.MembershipOrders objMembershipOrders = new Entities.MembershipOrders();
                        objMembershipOrders.Amount = objMembers.MemberAmount;
                        objMembershipOrders.MembershipOrderId = objMembers.MembershipOrderId;
                        objMembershipOrders.PaymentMethod = "PayPal";
                        objMembershipOrders.PaymentStatus = "Pending";
                        objMembershipOrders.OrderType = "renewal";
                        objMembershipOrders.MemberId = objMembers.MemberId;
                        objMembershipOrders.IsVolunteer = objMembers.IsVolunteer;
                        objMembershipOrders.MembershipTypeId = objMembers.MembershipTypeId;
                        Session["MembersType"] = objMembers.MembershipType;

                        objMembershipOrders.UpdatedBy = HttpContext.User.Identity.Name.ToString();

                        result = _Members.InsertMemberOrder(objMembershipOrders);

                        return new RedirectResult(returnurl);
                    }
                }
                else if (objMembers.PaymentMethod == "Cash/Cheque")
                {
                    Int64 result = 0;
                    Entities.MembershipOrders objMembershipOrders = new Entities.MembershipOrders();
                    objMembershipOrders.Amount = objMembers.MemberAmount;
                    objMembershipOrders.OrderType = objMembers.OrderType;
                    objMembershipOrders.MembershipOrderId = objMembers.MembershipOrderId;
                    objMembershipOrders.ExpiryDate = objMembers.ExpiryDate;
                    objMembershipOrders.PaymentMethod = "Cash/Cheque";
                    objMembershipOrders.PaymentStatus = "Pending";
                    objMembershipOrders.MemberId = objMembers.MemberId;
                    objMembershipOrders.UserComment = objMembers.UserComment;
                    objMembershipOrders.MembershipTypeId = objMembers.MembershipTypeId;
                    objMembershipOrders.PaymentBy = objMembers.PaymentBy;
                    objMembershipOrders.UpdatedBy = HttpContext.User.Identity.Name.ToString();
                    objMembershipOrders.IsVolunteer = objMembers.IsVolunteer;
                    objMembershipOrders.BankName = objMembers.BankName;
                    objMembershipOrders.ChequeNo = objMembers.ChequeNo;
                    objMembershipOrders.ChequeDate = objMembers.ChequeDate;
                    Session["MembersType"] = objMembers.MembershipType;

                    result = _Members.InsertMemberOrder(objMembershipOrders);
                    if (result == 1)
                    { return RedirectToAction("RenewalThankYou", "Members"); }
                }
                TempData["message"] = "<div class=\"success closable\">Your membership renewaled successfully.</div>";
                return RedirectToAction("RenewalAcknowledgement", "Members", new { OrderType = objMembers.OrderType });

            }
            catch
            {
                TempData["message"] = "<div class=\"error closable\">Failed Transaction</div>";
                return RedirectToAction("AddMember", "Members");
            }

        }

        public ActionResult RenewalAcknowledgement(Int64 MemberId = 0, string PaymentMethod = "PayPal", string OrderType = "")
        {
            try
            {
                Entities.Members objMember = new Entities.Members();
                int status = 0;
                Int64 result = 0;
                int result1 = 0;
                string Username = "";
                objMember = _Members.GetMemberFullDetailsById(MemberId, ref result1);
                if (PaymentMethod == "PayPal")
                {
                    Entities.AppInfo objAppInfo = _appinfo.GetAppInfoDetails(ref status);

                    PayByPayPal.PDTHolder objPDT = new PayByPayPal.PDTHolder();
                    Entities.PaymentSettings objPaymentSettings = _paymentSettings.GetPaymentSettingsDetails(0, "PayPal", ref status);
                    objPayByPayPal.objPaymentSettings.PDTToken = objPaymentSettings.TokenNo;
                    objPayByPayPal.objPaymentSettings.PaymentUrl = objPaymentSettings.PaymentUrl;

                    status = objPayByPayPal.Success();
                    if (status == 0)
                    {
                        objPDT = objPayByPayPal.objPDTHolder;
                        if (objMember.UserName != null && objMember.UserName != "")
                        {
                            Username = objMember.UserName;
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
                                objMembershipOrders.Amount = objMember.MemberAmount;
                                objMembershipOrders.ExpiryDate = objMember.ExpiryDate;
                                objMembershipOrders.PaymentMethod = "PayPal";
                                objMembershipOrders.TransactionId = objPDT.TransactionId;
                                objMembershipOrders.PaymentStatus = objPDT.PaymentStatus;
                                objMembershipOrders.MemberId = objMember.MemberId;
                                objMembershipOrders.MembershipTypeId = objMember.MembershipTypeId;
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
                                    DateTime Date = ConvertPayPalDateTime(objPDT.PaymentDate.ToString());
                                    ViewBag.PaymentDate = Date.ToString("MM/dd/yyyy");
                                }
                            }
                            int _status = 0; Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("Renewal Membership", 0, ref _status);
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
                                body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objMember.UserName));
                                body.Replace("[MEMBERID]", objMember.SpouseCell);
                                body.Replace("[FirstName]", BLL.Common.UppercaseFirst(objMember.FirstName));
                                body.Replace("[MTYPE]", objMember.MembershipType.ToString());
                                body.Replace("[SiteName]", objAppInfo.SiteName);
                                BLL.Common.SendMail(objMember.Email, objTemplates.Subject, body.ToString());
                            }
                            int status2 = 0; Entities.MailTemplates objTemplates2 = _MailTemplates.GetMailTemplateById("Renewal Membership Admin", 0, ref status2);
                            if (objTemplates2 != null)
                            {
                                StringBuilder body = new StringBuilder();
                                body.Append(objTemplates2.Description);
                                body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                                body.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                                body.Replace("[TWUrl]", objAppInfo.TwitterUrl);
                                body.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                                body.Replace("[GUrl]", objAppInfo.SupportEmail);
                                body.Replace("[TPhone]", objAppInfo.CompanyPhone);
                                body.Replace("[TEmail]", objAppInfo.CompanyEmail);
                                body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objMember.FirstName));
                                body.Replace("[PaymentStatus]", objPDT.PaymentStatus.ToString());
                                body.Replace("[TransactionID]", objPDT.TransactionId.ToString());
                                body.Replace("[Amount]", objMember.MemberAmount.ToString());
                                body.Replace("[PType]", "Paypal");
                                body.Replace("[MTYPE]", objMember.MembershipType.ToString());
                                body.Replace("[MEMBERID]", objMember.SpouseCell);
                                body.Replace("[IntrestedArea]", objMember.SpouseSkils);
                                body.Replace("[FirstName]", BLL.Common.UppercaseFirst(objMember.FirstName));
                                body.Replace("[Email]", (objMember.Email));
                                body.Replace("[LastName]", BLL.Common.UppercaseFirst(objMember.LastName));
                                body.Replace("[MembershipType]", BLL.Common.UppercaseFirst(objMember.MembershipType));
                                body.Replace("[Phone]", BLL.Common.UppercaseFirst(objMember.MobilePhone));
                                body.Replace("[SiteName]", objAppInfo.SiteName);

                                BLL.Common.SendMailwithCC(objAppInfo.SecretaryPhone, ConfigurationManager.AppSettings["adminemailid"], objAppInfo.PresidentPhone + "," + objAppInfo.CompanyEmail, objTemplates2.Subject, body.ToString());
                            }
                        }
                    }
                    else
                    {
                        int _status = 0; Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("Renewal Membership", 0, ref _status);
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
                            body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objMember.UserName));
                            body.Replace("[MEMBERID]", objMember.SpouseCell);
                            body.Replace("[FirstName]", BLL.Common.UppercaseFirst(objMember.FirstName));
                            body.Replace("[IntrestedArea]", objMember.SpouseSkils);
                            body.Replace("[SiteName]", objAppInfo.SiteName);
                            BLL.Common.SendMail(objMember.Email, objTemplates.Subject, body.ToString());
                        }
                        return RedirectToAction("Thankyou", "Members");
                    }
                }
                else if (PaymentMethod == "Cash/Cheque")
                {
                    Entities.AppInfo objAppInfo = _appinfo.GetAppInfoDetails(ref status);
                    int _status = 0; Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("Renewal Membership", 0, ref _status);
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
                        body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objMember.UserName));
                        body.Replace("[MEMBERID]", objMember.SpouseCell);
                        body.Replace("[FirstName]", BLL.Common.UppercaseFirst(objMember.FirstName));
                        body.Replace("[IntrestedArea]", objMember.SpouseSkils);
                        BLL.Common.SendMail(objMember.Email, objTemplates.Subject, body.ToString());
                    }
                    int status2 = 0; Entities.MailTemplates objTemplates2 = _MailTemplates.GetMailTemplateById("Renewal Membership Admin", 0, ref status2);
                    if (objTemplates2 != null)
                    {
                        StringBuilder body = new StringBuilder();
                        body.Append(objTemplates2.Description);
                        body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                        body.Replace("[FBUrl]", ConfigurationManager.AppSettings["FBUrl"].ToString());
                        body.Replace("[TWUrl]", ConfigurationManager.AppSettings["TWUrl"].ToString());
                        body.Replace("[YUrl]", ConfigurationManager.AppSettings["YUrl"].ToString());
                        body.Replace("[TPhoneNo]", ConfigurationManager.AppSettings["TPhoneNo"].ToString());
                        body.Replace("[TEmailId]", ConfigurationManager.AppSettings["TEmailId"].ToString());
                        body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objMember.FirstName));
                        body.Replace("[PaymentStatus]", "Pending");
                        body.Replace("[TransactionId]", "N/A");
                        body.Replace("[Amount]", objMember.MemberAmount.ToString());
                        body.Replace("[PType]", "Cash/Cheque");
                        body.Replace("[MTYPE]", objMember.MembershipType.ToString());
                        body.Replace("[MEMBERID]", objMembers.SpouseCell);
                        body.Replace("[IntrestedArea]", objMembers.SpouseSkils);
                        body.Replace("[FirstName]", BLL.Common.UppercaseFirst(objMember.FirstName));
                        body.Replace("[Email]", (objMember.Email));
                        body.Replace("[LastName]", BLL.Common.UppercaseFirst(objMember.LastName));
                        body.Replace("[MembershipType]", BLL.Common.UppercaseFirst(objMember.MembershipType));
                        body.Replace("[Phone]", BLL.Common.UppercaseFirst(objMember.MobilePhone));

                        BLL.Common.SendMailwithCC(objAppInfo.SecretaryPhone, ConfigurationManager.AppSettings["adminemailid"], objAppInfo.PresidentPhone + "," + objAppInfo.CompanyEmail, objTemplates2.Subject, body.ToString());
                    } 
                    ViewBag.UserName = objMember.UserName; 
                }

            }
            catch
            {
                return RedirectToAction("Error404", "Error");
            }
            return View();
        }

        public ActionResult GetAmount(Int64 MembershipTypeId = 0)
        {
            string str = "";
            try
            {
                int _qstatus = 0;
                BLL.MembershipTypes _MembershipTypes = new BLL.MembershipTypes();
                Entities.MembershipTypes _objMembershipTypes = _MembershipTypes.GetMembershipTypeById(MembershipTypeId, ref _qstatus);

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

        [TCAssociationTool.Areas.User.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult ProfileEdit(Entities.Members _objMembers, List<Entities.ChildrenInfo> lstChildrenInfo)
        {
            try
            {
                _objMembers.UpdatedBy = _objMembers.FirstName;                

                Int64 _qstatus = _Members.UpdateMemberProfile(_objMembers);
                if (_qstatus == 1)
                {
                    if (lstChildrenInfo != null)
                    {
                        foreach (Entities.ChildrenInfo objChildrenInfo in lstChildrenInfo)
                        {
                            objChildrenInfo.MemberId = _objMembers.MemberId;
                            if (objChildrenInfo != null && objChildrenInfo.FirstName != null)
                            {
                                Int64 estatus = _Members.InsertChildrenInfo(objChildrenInfo);
                                if (estatus == -1)
                                {
                                    TempData["message"] = "<div class=\"error closable\">Failed inserting member details.</div>";
                                    return RedirectToAction("Profile", "Members", new { MemberId = objMembers.MemberId });
                                }
                            }
                        }
                    }
                    TempData["message"] = "<div class=\"success closable\">Updated your details successfully.</div>";
                }
                else
                {
                    TempData["message"] = "<div class=\"error closable\">Failed editing profile.</div>";
                }
                TempData["message"] = (_qstatus == 1 ? "<div class=\"success closable\">Updated profile details successfully.</div>" : "<div class=\"error closable\">Failed editing profile.</div>");

            }
            catch
            {
                return RedirectToAction("Error404", "Error");
            }
            return RedirectToAction("Profile", "Members");
        }

        [TCAssociationTool.Areas.User.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult ProfileEmailUpdate(string Email = "", Int64 MemberId = 0)
        {
            try
            {
                Int64 _qstatus = _Members.ProfileEmailUpdate(Email, MemberId);
                if (_qstatus == 1)
                {
                    FormsAuthentication.RedirectFromLoginPage(Email, false);
                    TempData["message"] = "<div class=\"success closable\">Updated email successfully.</div>";
                }
                else
                {
                    TempData["message"] = "<div class=\"error closable\">Failed editing email.</div>";
                }

            }
            catch
            {
                TempData["message"] = "<div class=\"error closable\">Failed Transaction</div>";
            }
            return RedirectToAction("Profile", "Members");
        }

        [Authorize]
        [HttpPost]
        [TCAssociationTool.Areas.User.Models.SessionClass.SessionExpireFilter]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            try
            {
                if (model.MemberId != 0)
                {
                    int _qstatus = 0;
                    string oldpass = _Members.GetPassword(model.MemberId, ref _qstatus);

                    if (_qstatus == 1)
                    {
                        if (TCAssociationTool.BLL.Password.VerifyHash(model.OldPassword.Trim(), "SHA512", oldpass) == true)
                        {
                            string newpass = TCAssociationTool.BLL.Password.ComputeHash(model.NewPassword, "SHA512", null);
                            Int64 _pstatus = _Members.ChangePassword(model.MemberId, newpass);
                            if (_pstatus == 1)
                            {
                                TempData["message"] = "<div class=\"success closable\">Password changed successfully.</div>";
                                Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("Change Password", 0, ref _qstatus);
                                if (objTemplates != null)
                                {
                                    objMembers = _Members.GetMemberFullDetailsById(model.MemberId, ref _qstatus);
                                    StringBuilder body = new StringBuilder();
                                    body.Append(objTemplates.Description);
                                    body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                                    body.Replace("[FBUrl]", ConfigurationManager.AppSettings["FBUrl"].ToString());
                                    body.Replace("[TWUrl]", ConfigurationManager.AppSettings["TWUrl"].ToString());
                                    body.Replace("[YUrl]", ConfigurationManager.AppSettings["YUrl"].ToString());
                                    body.Replace("[TPhoneNo]", ConfigurationManager.AppSettings["TPhoneNo"].ToString());
                                    body.Replace("[TEmailId]", ConfigurationManager.AppSettings["TEmailId"].ToString());
                                    body.Replace("[UserName]", BLL.Common.UppercaseFirst(objMembers.FirstName));
                                    body.Replace("[EMAIL]", objMembers.Email);
                                    body.Replace("[PASSWORD]", model.NewPassword);
                                    BLL.Common.SendMail(objMembers.Email, objTemplates.Subject, body.ToString());
                                }
                            }
                            else
                            {
                                TempData["message"] = "<div class=\"error closable\">The current password is incorrect or the new password is invalid.</div>";
                            }
                        }
                        else
                        {
                            TempData["message"] = "<div class=\"error closable\">The current password is incorrect or the new password is invalid.</div>";
                        }
                    }
                    else
                    {
                        TempData["message"] = "<div class=\"error closable\">The current password is incorrect or the new password is invalid.</div>";
                    }
                }
                return RedirectToAction("Profile", "Members");
            }
            catch
            {
                TempData["message"] = "<div class=\"error closable\">Failed Transaction</div>";
                return RedirectToAction("Profile", "Members");
            }
        }


        [HttpPost]
        [TCAssociationTool.Areas.User.Models.SessionClass.SessionExpireFilter]
        public ActionResult ProfilePic(HttpPostedFileBase file, Int64 MemberId)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var image = WebImage.GetImageFromRequest();
                    string imageurl = (image != null ? image.ImageFormat : "NA");
                    Int64 _status = _Members.UpdateUserProfileImage(MemberId, ref imageurl);
                    if (_status == 1)
                    {
                        image.Resize(145, 150, true, false);
                        image.Crop(1, 1, 1, 1);
                        image.Save(ConfigurationManager.AppSettings["uploadPath"] + "\\MemberProfileImages\\" + imageurl);
                        TempData["message"] = "<div class=\"success closable\">Changed your profile picture successfully.</div>";
                    }
                }
            }
            catch 
            {
                TempData["message"] = "<div class=\"error closable\">Failed Transaction</div>";
            }
            return RedirectToAction("Profile", "Members");
        }

        #endregion

        #region CAPTCHA

        public CaptchaImageResult ShowCaptchaImage()
        {
            return new CaptchaImageResult();
        }
        [HttpPost]
        public JsonResult GetCaptcha()
        {
            try
            {
                string str = HttpContext.Session["captchastring"].ToString();

                return Json(new { ok = true, data = str, message = "" });
            }
            catch
            {
                return Json(new { ok = false, message = "<div class=\"error closable\">Failed Transaction</div>" });
            }
        }

        #endregion

        public ActionResult Members()
        {
            int Total = 0;
            ViewBag.MembershipType = _MembershipType.GetMembershipTypesList(ref Total);

            return View();
        }
         
        public ActionResult Volunteers()
        {
            return View();
        }

        //public ActionResult VolunteersList(string Search = "", string SortColumn = "InsertedDate", string SortOrder = "DESC", int PageNo = 1, int Items = 20)
        //{
        //    string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
        //    int Total = 0;
        //    try
        //    {
        //        lstVolunteers = _Volunteers.FEGetVolunteersList(Search, Sort, PageNo, Items, ref Total);
        //    }
        //    catch
        //    {
        //        Total = -1;
        //    }
        //    ViewBag.total = Total;
        //    ViewBag.pageno = PageNo;
        //    ViewBag.items = Items;
        //    ViewBag.lstVolunteers = lstVolunteers;
        //    ViewBag.sortcolumn = SortColumn;
        //    ViewBag.sortorder = SortOrder.ToLower();
        //    return View();
        //}
         
        #region Facebook Login

        //[HttpPost]
        public ActionResult FacebookLogin(string name = "", string first_name = "", string last_name = "", string username = "", string gender = "", string email = "", bool verified = false)
        {
            string msg = "";
            int status = 0;
            try
            {
                Entities.Members objMembers = _Members.GetMemberFullDetailsByEmail(email, ref status);

                if (status != -1)
                {
                    if (objMembers != null && objMembers.MemberId != 0)
                    {
                        if (objMembers != null && objMembers.MemberId != 0)
                        {
                            if (objMembers.IsApproved == true)
                            {
                                Session["username"] = objMembers.UserName;
                                Session["MemberID"] = objMembers.MemberId;
                                FormsAuthentication.RedirectFromLoginPage(objMembers.Email, false);
                                return RedirectToAction("Profile", "Members");
                            }
                            else
                            {
                                ViewBag.Message = "<div class=\"error closable\">Your status has been deactivated.Please contact to admin.</div>";
                            }
                        }
                        else
                        {
                            ViewBag.Message = "<div class=\"error closable\">Email or password is incorrect.</div>";
                        }
                    }
                    else
                    {
                        if (verified)
                        {
                            Int64 MemberId = 0;
                            string ImageUrl = "NA";
                            objMembers.IsApproved = true;
                            objMembers.IsLockedOut = false;
                            objMembers.FailedPasswordAttemptCount = 0;
                            objMembers.LastPasswordChangedDate = DateTime.UtcNow;
                            objMembers.LastActivityDate = DateTime.UtcNow;
                            objMembers.InsertedTime = DateTime.UtcNow;
                            objMembers.UpdatedBy = objMembers.FirstName;
                            objMembers.UpdatedTime = DateTime.UtcNow;
                            objMembers.IsActivated = true;
                            objMembers.DateActivated = DateTime.UtcNow;
                            string _password = BLL.Password.GetUniqueKey(8);
                            string _hashpassword = BLL.Password.ComputeHash(_password, "SHA512", null);
                            objMembers.Password = _hashpassword;
                            objMembers.Email = email;
                            objMembers.FirstName = first_name;
                            objMembers.LastName = last_name;
                            objMembers.MobilePhone = "";

                            Int64 _status = _Members.FEInsertMembers(objMembers, ref MemberId, ref ImageUrl);

                            if (_status == 1)
                            {
                                int status1 = 0;
                                StringBuilder body1 = new StringBuilder();
                                Entities.MailTemplates objadminTemplate = _MailTemplates.GetMailTemplateById("User Registration", 0, ref status1);
                                body1.Append(objadminTemplate.Description);
                                body1.Replace("[baseurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                                body1.Replace("[Email]", email);
                                body1.Replace("[Password]", _password);
                                body1.Replace("[UserName]", first_name);

                                //BLL.Common.SendMail(email.ToString(), objadminTemplate.Subject, body1.ToString());

                                Session["username"] = objMembers.UserName;
                                Session["MemberID"] = objMembers.MemberId;
                                FormsAuthentication.RedirectFromLoginPage(objMembers.Email, false);
                                return RedirectToAction("Profile", "Members");
                            }
                            else
                            {
                                msg = "Failed processing your request. Please try later,..";
                            }
                        }
                        else
                        {
                            msg = "Sorry,.. Facebook account is not yet verified.";
                        }
                    }
                }
                else
                {
                    msg = "Failed processing your request. Please try later,..";
                }
            }
            catch
            { return RedirectToAction("Error404", "Error"); }

            ViewBag.msg = msg;
            return View();
        }

        #endregion Facebook Login

        # region Google Login

        public ActionResult ReturnFromGoogle(string name = "", string first_name = "", string last_name = "", string username = "", string gender = "", string email = "", string TransferGUID = "")
        {
            string msg = "";
            int status = 0;
            try
            {
                Entities.Members _objMembers = _Members.GetMemberFullDetailsByEmail(email, ref status);
                if (_objMembers == null || _objMembers.MemberId == 0)
                {
                    _objMembers = _Members.GetMemberFullDetailsByEmail(email, ref status);
                }

                if (status != -1)
                {
                    if (_objMembers != null && _objMembers.MemberId != 0)
                    {
                        if (_objMembers != null && _objMembers.MemberId != 0)
                        {

                            if (_objMembers.IsApproved == true)
                            {
                                Session["username"] = objMembers.UserName;
                                Session["MemberID"] = objMembers.MemberId;
                                FormsAuthentication.RedirectFromLoginPage(_objMembers.Email, false); 
                                string url = ConfigurationManager.AppSettings["baseurl"];
                                if (Request.UrlReferrer != null && Request.UrlReferrer.ToString() != "" && Request.UrlReferrer.ToString() != ConfigurationManager.AppSettings["baseurl"] + "acknowledgement.html")
                                {
                                    url = Request.UrlReferrer.ToString();
                                }
                                ViewBag.returnurl = url;
                                return new RedirectResult(url);
                            }
                            else
                            {
                                TempData["message"] = "<div class=\"error closable\">Your status has been deactivated.Please contact to admin.</div>";
                            }
                        }
                        else
                        {
                            TempData["message"] = "<div class=\"error closable\">Email or password is incorrect.</div>";
                        }
                    }
                    else
                    {
                        Entities.Members _objMembersInsert = new Entities.Members(); 
                        _objMembersInsert.Email = email;
                        _objMembersInsert.IsApproved = true;
                        _objMembersInsert.UpdatedTime = DateTime.UtcNow;
                        _objMembersInsert.UserName = first_name + " " + last_name;
                        _objMembersInsert.IsLockedOut = false;
                        string _password = BLL.Password.GetUniqueKey(8);
                        string _hashpassword = BLL.Password.ComputeHash(_password, "SHA512", null);
                        _objMembersInsert.Password = _hashpassword;
                        _objMembersInsert.IsActivated = true; 

                        _objMembersInsert.DateActivated = DateTime.MinValue;
                        _objMembersInsert.LastPasswordChangedDate = DateTime.MinValue;
                        _objMembersInsert.UpdatedBy = first_name; 
                        Int64 MemberId = 0;
                        string ImageUrl = "NA";
                        Int64 _status = _Members.FEInsertMembers(_objMembersInsert, ref MemberId, ref ImageUrl);

                        if (_status == 1)
                        {
                            int status1 = 0;
                            Entities.MailTemplates objadminTemplate = _MailTemplates.GetMailTemplateById("Thank you for Registration", 0, ref status1);
                            StringBuilder body = new StringBuilder();
                            body.Append(objadminTemplate.Description);
                            body.Replace("[UserName]", name);
                            body.Replace("[Email]", email);
                            body.Replace("[MemberId]", MemberId.ToString());
                            body.Replace("[Password]", _password);
                            body.Replace("[TPhoneNo]", ConfigurationManager.AppSettings["TPhoneNo"].ToString());
                            body.Replace("[TEmailId]", ConfigurationManager.AppSettings["TEmailId"].ToString());
                            body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                            body.Replace("[Link]", "N/A");
                            BLL.Common.SendMail(email, objadminTemplate.Subject, body.ToString());


                            Session["username"] = objMembers.UserName;
                            Session["MemberID"] = objMembers.MemberId;
                            FormsAuthentication.RedirectFromLoginPage(email, false);
                             
                            string url = ConfigurationManager.AppSettings["baseurl"];
                            if (Request.UrlReferrer != null && Request.UrlReferrer.ToString() != "" && Request.UrlReferrer.ToString() != ConfigurationManager.AppSettings["baseurl"] + "acknowledgement.html")
                            {
                                url = Request.UrlReferrer.ToString();
                            }
                            ViewBag.returnurl = url;
                            return new RedirectResult(url);
                        }
                        else
                        {
                            TempData["message"] = "Failed processing your request. Please try later,..";
                        }
                    }
                }
                else
                {
                    TempData["message"] = "Failed processing your request. Please try later,..";
                }
            }
            catch
            { return RedirectToAction("Error404", "Error"); }

            ViewBag.msg = msg;
            return View();
        }

        # endregion Google Login

        public ActionResult MemberList()
        {
            Entities.InnerPages objLifeMembers = new Entities.InnerPages();
            int status = 0;

            objLifeMembers = _InnerPages.GetInnerPagesListById(0, "TCA Life Members 2018", ref status);

            int _qstatus = 0;
            objMembers = _Members.AddMembershipRequirement(ref _qstatus);

            ViewBag.objLifeMembers = objLifeMembers;
            ViewBag.objMembers = objMembers;
            return View();
        }

        public ActionResult FEMembersList(Int64 MembershipTypeId = 0, string Search = "", string SortColumn = "MemberId", string SortOrder = "ASC", int PageNo = 1, int Items = 20)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;
            try
            {
                lstMembers = _Members.FEGetMembersListByVariable(MembershipTypeId, Search, Sort, PageNo, Items, ref Total);

            }
            catch
            {
                ViewBag.message = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }

            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstMembers = lstMembers;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();
            return View();
        }
    } 
}


