using PaymentProcess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TCAssociationTool.Areas.User.Models;
using TCAssociationTool.Entities;
using TCAssociationTool.BLL;
using BluePayLibrary;
using System.Net;
using System.IO;

namespace TCAssociationTool.Areas.User.Controllers
{
    public class DonationController : Controller
    {

        BLL.Donors _Donors = new BLL.Donors();
        BLL.DonationCategories _DonationCategories = new BLL.DonationCategories();
        BLL.PaymentSettings _paymentSettings = new BLL.PaymentSettings();
        PayByPayPal objPayByPayPal = new PayByPayPal();
        BLL.MailTemplates _MailTemplates = new BLL.MailTemplates();
        BLL.Members _Members = new BLL.Members();
        Entities.Members _objMember = new Entities.Members();
        TCAssociationTool.BLL.AppInfo _appinfo = new BLL.AppInfo();


        public string action1 = string.Empty;
        public string hash1 = string.Empty;
        public string txnid1 = string.Empty;

        public ActionResult Index()
        {
            List<Entities.DonationCategories> lstDonationCategories = new List<Entities.DonationCategories>();
            try
            {
                int status = 0;
                int QStatus = 0;                
                lstDonationCategories = _DonationCategories.FEDonationCategoriesGetList(ref status);
                if (this.User.Identity.IsAuthenticated && this.User.Identity.Name != null && HttpContext.Session != null && Session["username"] != null)
                {
                    _objMember = _Members.GetMemberFullDetailsByEmail(HttpContext.User.Identity.Name.ToString(), ref QStatus);
                }
            }
            catch
            {
                return RedirectToAction("Error404", "Error");
            }
            ViewBag.lstDonationCategories = lstDonationCategories;
            ViewBag.Memberdetails = _objMember;
            
            return View();
        }

        #region PayPal
        // Donation By PayPal
        [HttpPost]
        public ActionResult AddDonor(Entities.Donors objdonar)
        {
            try
            {
                Int64 DonarId = 0;
                objdonar.UpdatedBy = objdonar.FirstName;
                objdonar.InsertedBy = objdonar.FirstName;
                Int64 status1 = _Donors.InsertDonors(objdonar, ref DonarId);
                if (DonarId != 0)
                {
                    if (objdonar.PaymentMethod == "CreditCard")
                    {
                        int status = 0;
                        Entities.PaymentSettings objPaymentSettings = _paymentSettings.GetPaymentSettingsDetails(0, "PayPal", ref status);
                        if (objPaymentSettings != null)
                        {
                            List<PayByPayPal.CartItems> lstCartItems = new List<PayByPayPal.CartItems>();

                            PayByPayPal.CartItems objCartItems = new PayByPayPal.CartItems();
                            objCartItems.ItemName = "Donation";
                            objCartItems.ItemPrice = objdonar.Amount;
                            lstCartItems.Add(objCartItems);

                            objPayByPayPal.lstCartItems = lstCartItems;

                            PayByPayPal.PaymentSettings objpaymentsettings = new PayByPayPal.PaymentSettings();
                            objpaymentsettings.BusinessEmail = objPaymentSettings.PaymentEmail;
                            objpaymentsettings.BusinessPassword = objPaymentSettings.PaymentPassword;
                            objpaymentsettings.CurrencyCode = objPaymentSettings.CurrencyCode;
                            objpaymentsettings.NotifyUrl = ConfigurationManager.AppSettings["baseurl"] + "donation/ipn.html" + "?DonarId=" + DonarId;
                            objpaymentsettings.PaymentUrl = objPaymentSettings.PaymentUrl;
                            objpaymentsettings.ReturnUrl = ConfigurationManager.AppSettings["baseurl"].ToString() + "donation-acknowledgement.html?DonarId=" + DonarId;
                            objpaymentsettings.Cmd = "_cart";
                            objpaymentsettings.PDTToken = objPaymentSettings.TokenNo;
                            objpaymentsettings.CBT = "Click here to complete your transaction";
                            objpaymentsettings.CancelUrl = ConfigurationManager.AppSettings["baseurl"] + "index.html";

                            objPayByPayPal.objPaymentSettings = objpaymentsettings;

                            string returnurl = objPayByPayPal.CheckOut();

                            return new RedirectResult(returnurl);
                        }
                    }
                    else if (objdonar.PaymentMethod == "CreditCard")
                    {
                        action1 = ConfigurationManager.AppSettings["PaymentUrl"];
                        ViewBag.action1 = action1;
                        //ViewBag.hash = hash1;
                        //ViewBag.txnid = txnid1;
                        //ViewBag.key = ConfigurationManager.AppSettings["TokenNo"];
                        ViewBag.firstname = objdonar.FirstName.Trim();
                        ViewBag.lastname = objdonar.LastName.Trim();
                        ViewBag.email = objdonar.Email.Trim();
                        ViewBag.phone = (objdonar.PhoneNo != null ? objdonar.PhoneNo + " " + objdonar.PhoneNo.Trim() : objdonar.PhoneNo);
                        ViewBag.surl = ConfigurationManager.AppSettings["baseurl"].ToString() + "donation-acknowledgement.html?DonarId=" + DonarId + "&PaymentMethod=" + objdonar.PaymentMethod;
                        ViewBag.furl = ConfigurationManager.AppSettings["baseurl"];
                        ViewBag.service_provider = "first_data";
                        ViewBag.address = objdonar.Address;
                        ViewBag.city = objdonar.City;
                        ViewBag.state = objdonar.State;
                        ViewBag.CardNumber = objdonar.CardNumber;
                        ViewBag.cardtype = objdonar.CardType;
                        ViewBag.holderfirstname = objdonar.CardHolderFirstName;
                        ViewBag.holderlastname = objdonar.CardHolderLastName;
                        ViewBag.CSVMonth = objdonar.CSVMonth;
                        ViewBag.CSVYear = objdonar.CSVYear;
                        ViewBag.Cvv = objdonar.Cvv;


                        ViewBag.amount = objdonar.Amount;
                    }
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"error closable\">Failed Transaction</div>";
                return RedirectToAction("Index", "Donation");
            }

            return View();
        }
         
        // Acknowledgement for PayPal
        public ActionResult Acknowledgement(Int64 DonarId = 0, string PaymentMethod = "PayPal")
        {
            try
            {
                Entities.Donors objDonors = new Entities.Donors();
                int status1 = 0;
                Int64 result = 0;
                int result1 = 0;
                int status = 0;
                string Username = "";
                if (PaymentMethod == "PayPal")
                {
                    Entities.AppInfo objAppInfo = _appinfo.GetAppInfoDetails(ref status);

                    objDonors = _Donors.GetDonorsById(DonarId, ref result1);
                    PayByPayPal.PDTHolder objPDT = new PayByPayPal.PDTHolder();
                    Entities.PaymentSettings objPaymentSettings = _paymentSettings.GetPaymentSettingsDetails(0, "PayPal", ref status1);
                    objPayByPayPal.objPaymentSettings.PDTToken = objPaymentSettings.TokenNo;
                    objPayByPayPal.objPaymentSettings.PaymentUrl = objPaymentSettings.PaymentUrl;

                    status1 = objPayByPayPal.Success();
                    if (status1 == 0)
                    {
                        objPDT = objPayByPayPal.objPDTHolder;
                        if (objDonors.FirstName != null)
                        {
                            Username = objDonors.FirstName + " " + objDonors.LastName;
                        }
                        else
                        {
                            Username = HttpContext.User.Identity.Name.ToString();
                        }
                        if (Username != null)
                        {
                            if (result1 == 1)
                            {
                                objDonors.Amount = Convert.ToDecimal(objPDT.GrossTotal);
                                objDonors.PaymentMethod = "PayPal";
                                objDonors.TransactionId = objPDT.TransactionId;
                                objDonors.PaymentStatus = objPDT.PaymentStatus;
                                objDonors.DonorId = objDonors.DonorId;
                                objDonors.OrderDate = DateTime.UtcNow;
                                objDonors.UpdatedBy = Username;

                                result = _Donors.UpdateDonors(objDonors);
                                if (result == 1)
                                {
                                    ViewBag.PayerEmail = objDonors.Email;
                                    ViewBag.UserName = Username;
                                    ViewBag.ReceiverEmail = objPDT.ReceiverEmail;
                                    ViewBag.AddressCity = objPDT.AddressCity;
                                    ViewBag.AddressCountry = objPDT.AddressCountry;
                                    ViewBag.AddressName = objPDT.AddressName;
                                    ViewBag.AddressState = objPDT.AddressState;
                                    ViewBag.AddressStreet = objPDT.AddressStreet;
                                    ViewBag.AddressZip = objPDT.AddressZip;
                                    ViewBag.GrossTotal = objDonors.Amount;
                                    ViewBag.PaymentStatus = objPDT.PaymentStatus;
                                    ViewBag.TransactionId = objPDT.TransactionId;
                                    ViewBag.DonorId = DonarId;
                                    DateTime paydate = DateTime.UtcNow;
                                    DateTime Date = paydate = DateTime.UtcNow;
                                    ViewBag.PaymentDate = Date.ToString("MM/dd/yyyy");
                                }
                            }
                            int _status = 0;
                            Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("Thank you for giving donation", 0, ref _status);
                            if (objTemplates != null)
                            {
                                StringBuilder body = new StringBuilder();
                                body.Append(objTemplates.Description);
                                body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                                body.Replace("[UserName]", BLL.Common.UppercaseFirst(objDonors.FirstName + " " + objDonors.LastName));
                                body.Replace("[FirstName]", objDonors.FirstName);
                                body.Replace("[LastName]", objDonors.LastName);
                                body.Replace("[Email]", objDonors.Email);
                                body.Replace("[PhoneNo]", objDonors.PhoneNo);
                                body.Replace("[Address]", (objDonors.Address == "" ? "N/A" : objDonors.Address));
                                body.Replace("[DonationCause]", objDonors.DonationCause);
                                body.Replace("[DonationProgram]", objDonors.DonationProgram);
                                body.Replace("[DonationAmount]", objPDT.GrossTotal.ToString());
                                body.Replace("[TransactionId]", objDonors.TransactionId);
                                body.Replace("[PaymentStatus]", objDonors.PaymentStatus);
                                body.Replace("[PaymentDate]", objDonors.OrderDate.ToShortDateString());
                                body.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                                body.Replace("[TWUrl]", objAppInfo.TwitterUrl);
                                body.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                                body.Replace("[GUrl]", objAppInfo.SupportEmail);
                                body.Replace("[TPhone]", objAppInfo.CompanyPhone);
                                body.Replace("[TEmail]", objAppInfo.CompanyEmail);
                                body.Replace("[ApplicationName]", objAppInfo.SiteName);
                                body.Replace("[SiteName]", objAppInfo.SiteName);
                                BLL.Common.SendMail(objDonors.Email, objTemplates.Subject, body.ToString());
                            }
                            Entities.MailTemplates objTemplates1 = _MailTemplates.GetMailTemplateById("Donate for Admin", 0, ref _status);
                            if (objTemplates1 != null)
                            {
                                StringBuilder body1 = new StringBuilder();
                                body1.Append(objTemplates1.Description);
                                body1.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                                body1.Replace("[UserName]", BLL.Common.UppercaseFirst(objDonors.FirstName + " " + objDonors.LastName));
                                body1.Replace("[FirstName]", objDonors.FirstName);
                                body1.Replace("[LastName]", objDonors.LastName);
                                body1.Replace("[Email]", objDonors.Email);
                                body1.Replace("[PhoneNo]", objDonors.PhoneNo);
                                body1.Replace("[Address]", (objDonors.Address == "" ? "N/A" : objDonors.Address));
                                body1.Replace("[DonationCause]", objDonors.DonationCause);
                                body1.Replace("[DonationProgram]", objDonors.DonationProgram);
                                body1.Replace("[DonationAmount]", objPDT.GrossTotal.ToString());
                                body1.Replace("[TransactionId]", objDonors.TransactionId);
                                body1.Replace("[PaymentStatus]", objDonors.PaymentStatus); 
                                body1.Replace("[PaymentDate]", objDonors.OrderDate.ToShortDateString());
                                body1.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                                body1.Replace("[TWUrl]", objAppInfo.TwitterUrl);
                                body1.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                                body1.Replace("[GUrl]", objAppInfo.SupportEmail);
                                body1.Replace("[TPhone]", objAppInfo.CompanyPhone);
                                body1.Replace("[TEmail]", objAppInfo.CompanyEmail);
                                body1.Replace("[ApplicationName]", objAppInfo.SiteName);
                                body1.Replace("[SiteName]", objAppInfo.SiteName);

                                BLL.Common.SendMail(objAppInfo.CompanyEmail, objTemplates1.Subject, body1.ToString());
                            }
                        }
                    }
                    else 
                    {
                        int _status = 0;
                        Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("Thank you for giving donation", 0, ref _status);
                        if (objTemplates != null)
                        {
                            StringBuilder body = new StringBuilder();
                            body.Append(objTemplates.Description);
                            body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                            body.Replace("[UserName]", BLL.Common.UppercaseFirst(objDonors.FirstName + " " + objDonors.LastName));
                            body.Replace("[FirstName]", objDonors.FirstName);
                            body.Replace("[LastName]", objDonors.LastName);
                            body.Replace("[Email]", objDonors.Email);
                            body.Replace("[PhoneNo]", objDonors.PhoneNo);
                            body.Replace("[Address]", (objDonors.Address == null ? "N/A" : objDonors.Address));
                            body.Replace("[DonationCause]", objDonors.DonationCause);
                            body.Replace("[DonationProgram]", objDonors.DonationProgram);
                            body.Replace("[DonationAmount]", objPDT.GrossTotal.ToString());
                            body.Replace("[TransactionId]", objDonors.TransactionId);
                            body.Replace("[PaymentStatus]", objDonors.PaymentStatus);
                            body.Replace("[PaymentDate]", objDonors.OrderDate.ToShortDateString());
                            body.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                            body.Replace("[TWUrl]", objAppInfo.TwitterUrl);
                            body.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                            body.Replace("[GUrl]", objAppInfo.SupportEmail);
                            body.Replace("[TPhone]", objAppInfo.CompanyPhone);
                            body.Replace("[TEmail]", objAppInfo.CompanyEmail);
                            body.Replace("[SiteName]", objAppInfo.SiteName);
                            BLL.Common.SendMail(objDonors.Email, objTemplates.Subject, body.ToString());
                        }
                        Entities.MailTemplates objTemplates1 = _MailTemplates.GetMailTemplateById("Donate for Admin", 0, ref _status);
                        if (objTemplates1 != null)
                        {
                            StringBuilder body1 = new StringBuilder();
                            body1.Append(objTemplates1.Description);
                            body1.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                            body1.Replace("[UserName]", BLL.Common.UppercaseFirst(objDonors.FirstName + " " + objDonors.LastName));
                            body1.Replace("[FirstName]", objDonors.FirstName);
                            body1.Replace("[LastName]", objDonors.LastName);
                            body1.Replace("[Email]", objDonors.Email);
                            body1.Replace("[PhoneNo]", objDonors.PhoneNo);
                            body1.Replace("[Address]", (objDonors.Address == null ? "N/A" : objDonors.Address));
                            body1.Replace("[DonationCause]", objDonors.DonationCause);
                            body1.Replace("[DonationProgram]", objDonors.DonationProgram);
                            body1.Replace("[DonationAmount]", objPDT.GrossTotal.ToString());
                            body1.Replace("[TransactionId]", objDonors.TransactionId);
                            body1.Replace("[PaymentStatus]", objDonors.PaymentStatus); 
                            body1.Replace("[PaymentDate]", objDonors.OrderDate.ToShortDateString());
                            body1.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                            body1.Replace("[TWUrl]", objAppInfo.TwitterUrl);
                            body1.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                            body1.Replace("[GUrl]", objAppInfo.SupportEmail);
                            body1.Replace("[TPhone]", objAppInfo.CompanyPhone);
                            body1.Replace("[TEmail]", objAppInfo.CompanyEmail);
                            body1.Replace("[SiteName]", objAppInfo.SiteName);
                            BLL.Common.SendMail(objAppInfo.CompanyEmail, objTemplates1.Subject, body1.ToString());
                        }
                        return RedirectToAction("Thankyou", "Donation");
                    }
                }
                else if (PaymentMethod == "CreditCard")
                {
                    Entities.AppInfo objAppInfo = _appinfo.GetAppInfoDetails(ref status);

                    objDonors = _Donors.GetDonorsById(DonarId, ref result1);

                     if (objDonors.FirstName != null)
                        {
                            Username = objDonors.FirstName + " " + objDonors.LastName;
                        }
                        else
                        {
                            Username = HttpContext.User.Identity.Name.ToString();
                        }
                     if (Username != null)
                     {
                         objDonors.Amount = Convert.ToDecimal(objDonors.Amount);
                         objDonors.PaymentMethod = "PayPal";
                         objDonors.TransactionId = "sale";
                         objDonors.PaymentStatus = "Completed";
                         objDonors.DonorId = objDonors.DonorId;
                         objDonors.OrderDate = DateTime.UtcNow;
                         objDonors.UpdatedBy = Username;

                         result = _Donors.UpdateDonors(objDonors);
                         if (result == 1)
                         {
                             ViewBag.PayerEmail = objDonors.Email;
                             ViewBag.UserName = Username;
                             ViewBag.ReceiverEmail = "info@ataworld.org";
                             ViewBag.AddressCity = objDonors.City;                            
                             ViewBag.Address1 = objDonors.Address;
                             ViewBag.AddressState = objDonors.State;
                             ViewBag.Address2 = objDonors.Address2;
                             ViewBag.AddressZip = objDonors.ZipCode;
                             ViewBag.GrossTotal = objDonors.Amount;
                             ViewBag.PaymentStatus = "Completed";
                             ViewBag.TransactionId = "sale";
                             ViewBag.DonorId = DonarId;
                             DateTime paydate = DateTime.UtcNow;
                             DateTime Date = paydate = DateTime.UtcNow;
                             ViewBag.PaymentDate = Date.ToString("MM/dd/yyyy");
                         }

                         int _status = 0;
                         Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("Thank you for giving donation", 0, ref _status);
                         if (objTemplates != null)
                         {
                             StringBuilder body = new StringBuilder();
                             body.Append(objTemplates.Description);
                             body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                             body.Replace("[UserName]", BLL.Common.UppercaseFirst(objDonors.FirstName + " " + objDonors.LastName));
                             body.Replace("[FirstName]", objDonors.FirstName);
                             body.Replace("[LastName]", objDonors.LastName);
                             body.Replace("[Email]", objDonors.Email);
                             body.Replace("[PhoneNo]", objDonors.PhoneNo);
                             body.Replace("[Address]", (objDonors.Address == "" ? "N/A" : objDonors.Address));
                             body.Replace("[DonationCause]", objDonors.DonationCause);
                             body.Replace("[DonationProgram]", objDonors.DonationProgram);
                             body.Replace("[DonationAmount]", objDonors.Amount.ToString());
                             body.Replace("[TransactionId]", objDonors.TransactionId);
                             body.Replace("[PaymentStatus]", objDonors.PaymentStatus);
                             body.Replace("[PaymentDate]", objDonors.OrderDate.ToShortDateString());
                             body.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                             body.Replace("[TWUrl]", objAppInfo.TwitterUrl);
                             body.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                             body.Replace("[GUrl]", objAppInfo.SupportEmail);
                             body.Replace("[TPhone]", objAppInfo.CompanyPhone);
                             body.Replace("[TEmail]", objAppInfo.CompanyEmail);
                             body.Replace("[ApplicationName]", objAppInfo.SiteName);
                             body.Replace("[SiteName]", objAppInfo.SiteName);
                             BLL.Common.SendMail(objDonors.Email, objTemplates.Subject, body.ToString());
                         }
                         Entities.MailTemplates objTemplates1 = _MailTemplates.GetMailTemplateById("Donate for Admin", 0, ref _status);
                         if (objTemplates1 != null)
                         {
                             StringBuilder body1 = new StringBuilder();
                             body1.Append(objTemplates1.Description);
                             body1.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                             body1.Replace("[UserName]", BLL.Common.UppercaseFirst(objDonors.FirstName + " " + objDonors.LastName));
                             body1.Replace("[FirstName]", objDonors.FirstName);
                             body1.Replace("[LastName]", objDonors.LastName);
                             body1.Replace("[Email]", objDonors.Email);
                             body1.Replace("[PhoneNo]", objDonors.PhoneNo);
                             body1.Replace("[Address]", (objDonors.Address == "" ? "N/A" : objDonors.Address));
                             body1.Replace("[DonationCause]", objDonors.DonationCause);
                             body1.Replace("[DonationProgram]", objDonors.DonationProgram);
                             body1.Replace("[DonationAmount]", objDonors.Amount.ToString());
                             body1.Replace("[TransactionId]", objDonors.TransactionId);
                             body1.Replace("[PaymentStatus]", objDonors.PaymentStatus);
                             body1.Replace("[PaymentDate]", objDonors.OrderDate.ToShortDateString());
                             body1.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                             body1.Replace("[TWUrl]", objAppInfo.TwitterUrl);
                             body1.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                             body1.Replace("[GUrl]", objAppInfo.SupportEmail);
                             body1.Replace("[TPhone]", objAppInfo.CompanyPhone);
                             body1.Replace("[TEmail]", objAppInfo.CompanyEmail);
                             body1.Replace("[ApplicationName]", objAppInfo.SiteName);
                             body1.Replace("[SiteName]", objAppInfo.SiteName);

                             BLL.Common.SendMail(objAppInfo.CompanyEmail, objTemplates1.Subject, body1.ToString());
                         }
                     }
                }
            }
            catch
            {
                return RedirectToAction("Error404", "Error");
            }
            return View();
        }

        // Paypal IPN
        public ActionResult DonationIPN(Int64 DonarId = 0)
        {
            int result1 = 0;
            Entities.Donors objDonors = new Entities.Donors();
            PayByPayPal.PDTHolder objPDT = new PayByPayPal.PDTHolder();
            Entities.MembershipOrders objMembershipOrders = new Entities.MembershipOrders();
            objDonors = _Donors.GetDonorsById(DonarId, ref result1);
            string status = "";
            try
            {
                int qstatus = 0;
                Entities.PaymentSettings objPaymentSettings = _paymentSettings.GetPaymentSettingsDetails(0, "PayPal", ref qstatus);
                objPayByPayPal.objPaymentSettings.PaymentUrl = objPaymentSettings.PaymentUrl;
                status = objPayByPayPal.ipn();

                // For Testing the Action
                //BLL.Common.SendMailwithfrom("y.sekhar@innovateindia.in", ConfigurationManager.AppSettings["adminemailid"].ToString(), "testIPN", status.ToString());

                if (status == "VERIFIED")
                {
                    objPDT = objPayByPayPal.objPDTHolder;

                    objDonors.Amount = Convert.ToDecimal(objPDT.GrossTotal);
                    objDonors.PaymentMethod = "PayPal";
                    objDonors.TransactionId = objPDT.TransactionId;
                    objDonors.PaymentStatus = objPDT.PaymentStatus;
                    objDonors.DonorId = objDonors.DonorId;
                    objDonors.OrderDate = ConvertPayPalDateTime(objPDT.PaymentDate.ToString());
                    objDonors.UpdatedBy = objDonors.FirstName;
                    Int64 result = 0;
                    result = _Donors.UpdateDonors(objDonors);
                }
            }
            catch
            {
                TempData["message"] = "Your payment did not go through please contact admin for assistance.";
            }

            ViewBag.status = status;
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

        #endregion

        #region BluePay

        // Donation By BluePay
        [HttpPost]
        public ActionResult AddDonorBluePay(Entities.Donors objdonar)
        {
            try
            {
                Int64 DonarId = 0;
                objdonar.UpdatedBy = objdonar.FirstName;
                objdonar.InsertedBy = objdonar.FirstName;
                Int64 status1 = _Donors.InsertDonors(objdonar, ref DonarId);
                if (DonarId != 0)
                {
                    string accountID = ConfigurationManager.AppSettings["accountID"].ToString();
                    string secretKey = ConfigurationManager.AppSettings["secretKey"].ToString();
                    string mode = ConfigurationManager.AppSettings["mode"].ToString();

                    BluePay payment = new BluePay
                    (
                        accountID,
                        secretKey,
                        mode
                    );

                    payment.SetCustomerInformation
                    (
                        firstName: objdonar.FirstName,
                        lastName: objdonar.LastName,
                        address1: objdonar.Address,
                        address2: objdonar.Address,
                        city: "",
                        state: "",
                        zip: "",
                        country: "USA",
                        phone: objdonar.PhoneNo,
                        email: objdonar.Email
                    );

                    payment.SetCCInformation
                    (
                        ccNumber: objdonar.CardNumber,
                        ccExpiration: objdonar.CSVMonth + objdonar.CSVYear,
                        cvv2: objdonar.Cvv
                    );

                    // Card Authorization Amount: $0.00
                    payment.Sale(amount: objdonar.Amount.ToString());

                    // Makes the API Request with BluePay
                    payment.Process();

                    ViewBag.Status = payment.GetStatus();
                    ViewBag.GetTransID = payment.GetTransID();
                    ViewBag.GetMessage = payment.GetMessage();
                    ViewBag.GetCardType = payment.GetCardType();
                    ViewBag.GetMaskedPaymentAccount = payment.GetMaskedPaymentAccount();
                    ViewBag.GetAuthCode = payment.GetAuthCode();

                    return RedirectToAction("Acknowledgement", "Donation", new { DonarId = DonarId, amt = objdonar.Amount, status = payment.GetStatus(), transid = payment.GetTransID(), message = payment.GetMessage(), avs = payment.GetAVS(), cvv2 = payment.GetCVV2(), cardtype = payment.GetCardType(), authcode = payment.GetAuthCode() });
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"error closable\">Failed Transaction</div>";
                return RedirectToAction("Index", "Donation");
            }

            return View();
        }

        // Bluepay Acknowledgement
        public ActionResult AcknowledgementBluepay(string PaymentMethod = "bluepay", Int64 DonarId = 0, decimal amt = 0, string OrderType = "", string pstatus = "", string transid = "", string message = "", string avs = "", string cvv2 = "", string cardtype = "", string authcode = "", string CouponCode = "")
        {
            try
            {
                Entities.Donors objDonors = new Entities.Donors();
                int status = 0;
                Int64 result = 0;
                objDonors = _Donors.GetDonorsById(DonarId, ref status);

                if (status == 1)
                {
                    objDonors.Amount = Convert.ToDecimal(amt);
                    objDonors.PaymentMethod = "Bluepay";
                    objDonors.TransactionId = transid;
                    objDonors.PaymentStatus = pstatus;
                    objDonors.OrderDate = DateTime.UtcNow;
                    objDonors.UpdatedBy = objDonors.FirstName;
                    objDonors.InsertedBy = objDonors.FirstName;

                    result = _Donors.UpdateDonors(objDonors);
                    if (result == 1)
                    {
                        int _status = 0;
                        Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("Thank you for giving donation", 0, ref _status);
                        if (objTemplates != null)
                        {
                            StringBuilder body = new StringBuilder();
                            body.Append(objTemplates.Description);
                            body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                            body.Replace("[UserName]", BLL.Common.UppercaseFirst(objDonors.FirstName + " " + objDonors.LastName));
                            body.Replace("[FirstName]", objDonors.FirstName);
                            body.Replace("[LastName]", objDonors.LastName);
                            body.Replace("[Email]", objDonors.Email);
                            body.Replace("[PhoneNo]", objDonors.PhoneNo);
                            body.Replace("[Address]", (objDonors.Address == "" ? "N/A" : objDonors.Address));
                            body.Replace("[DonationCause]", objDonors.DonationCause);
                            body.Replace("[DonationProgram]", objDonors.DonationProgram);
                            body.Replace("[DonationAmount]", amt.ToString());
                            body.Replace("[TransactionId]", objDonors.TransactionId);
                            body.Replace("[PaymentStatus]", objDonors.PaymentStatus);
                            body.Replace("[PaymentDate]", objDonors.OrderDate.ToShortDateString());
                            body.Replace("[FBUrl]", ConfigurationManager.AppSettings["FBUrl"].ToString());
                            body.Replace("[TWUrl]", ConfigurationManager.AppSettings["TWUrl"].ToString());
                            body.Replace("[YUrl]", ConfigurationManager.AppSettings["YUrl"].ToString());
                            body.Replace("[TPhoneNo]", ConfigurationManager.AppSettings["TPhoneNo"].ToString());
                            body.Replace("[TEmailId]", ConfigurationManager.AppSettings["TEmailId"].ToString());
                            BLL.Common.SendMail(objDonors.Email, objTemplates.Subject, body.ToString());
                        }
                        Entities.MailTemplates objTemplates1 = _MailTemplates.GetMailTemplateById("Donate for Admin", 0, ref _status);
                        if (objTemplates1 != null)
                        {
                            StringBuilder body1 = new StringBuilder();
                            body1.Append(objTemplates1.Description);
                            body1.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                            body1.Replace("[UserName]", BLL.Common.UppercaseFirst(objDonors.FirstName + " " + objDonors.LastName));
                            body1.Replace("[FirstName]", objDonors.FirstName);
                            body1.Replace("[LastName]", objDonors.LastName);
                            body1.Replace("[Email]", objDonors.Email);
                            body1.Replace("[PhoneNo]", objDonors.PhoneNo);
                            body1.Replace("[Address]", (objDonors.Address == "" ? "N/A" : objDonors.Address));
                            body1.Replace("[DonationCause]", objDonors.DonationCause);
                            body1.Replace("[DonationProgram]", objDonors.DonationProgram);
                            body1.Replace("[DonationAmount]", amt.ToString());
                            body1.Replace("[TransactionId]", objDonors.TransactionId);
                            body1.Replace("[PaymentStatus]", objDonors.PaymentStatus);
                            body1.Replace("[PaymentDate]", objDonors.OrderDate.ToShortDateString());
                            body1.Replace("[FBUrl]", ConfigurationManager.AppSettings["FBUrl"].ToString());
                            body1.Replace("[TWUrl]", ConfigurationManager.AppSettings["TWUrl"].ToString());
                            body1.Replace("[YUrl]", ConfigurationManager.AppSettings["YUrl"].ToString());
                            body1.Replace("[TPhoneNo]", ConfigurationManager.AppSettings["TPhoneNo"].ToString());
                            body1.Replace("[TEmailId]", ConfigurationManager.AppSettings["TEmailId"].ToString());

                            BLL.Common.SendMail(ConfigurationManager.AppSettings["adminemailid"].ToString(), objTemplates1.Subject, body1.ToString());
                        }

                        ViewBag.PayerEmail = objDonors.Email; 
                        ViewBag.GrossTotal = amt;
                        ViewBag.PaymentStatus = pstatus;
                        ViewBag.TransactionId = transid;
                        ViewBag.DonorId = DonarId;
                        DateTime paydate = DateTime.UtcNow;
                        DateTime Date = paydate = DateTime.UtcNow;
                        ViewBag.PaymentDate = Date.ToString("MM/dd/yyyy");
                    }
                }
            }
            catch
            {
            }
            return View();
        }

        #endregion

        #region Authorized.net

        // Donation By Authorized.net
        [HttpPost]
        public ActionResult AddDonorByAuth(Entities.Donors objdonar)
        {
            try
            {
                Int64 DonarId = 0;
                objdonar.UpdatedBy = objdonar.FirstName;
                objdonar.InsertedBy = objdonar.FirstName;
                Int64 status1 = _Donors.InsertDonors(objdonar, ref DonarId);
                if (DonarId != 0)
                {
                    String post_url = ConfigurationManager.AppSettings["payurl"].ToString();

                    Dictionary<string, string> post_values = new Dictionary<string, string>(); 
                    post_values.Add("x_login", ConfigurationManager.AppSettings["apiid"].ToString());
                    post_values.Add("x_tran_key", ConfigurationManager.AppSettings["transactionkey"].ToString());
                    post_values.Add("x_delim_data", "TRUE");
                    post_values.Add("x_delim_char", "|");
                    post_values.Add("x_relay_response", "FALSE");

                    post_values.Add("x_type", "AUTH_CAPTURE");
                    post_values.Add("x_method", "CC");
                    post_values.Add("x_card_num", objdonar.CardNumber);
                    post_values.Add("x_exp_date", objdonar.CSVMonth + objdonar.CSVYear.Substring(2, 2));

                    post_values.Add("x_amount", objdonar.Amount.ToString());
                    post_values.Add("x_description", "Sample Transaction");

                    String post_string = "";

                    foreach (KeyValuePair<string, string> post_value in post_values)
                    {
                        post_string += post_value.Key + "=" + HttpUtility.UrlEncode(post_value.Value) + "&";
                    }
                    post_string = post_string.TrimEnd('&');

                    HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(post_url);
                    objRequest.Method = "POST";
                    objRequest.ContentLength = post_string.Length;
                    objRequest.ContentType = "application/x-www-form-urlencoded";

                    StreamWriter myWriter = null;
                    myWriter = new StreamWriter(objRequest.GetRequestStream());
                    myWriter.Write(post_string);
                    myWriter.Close();

                    String post_response;
                    HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
                    using (StreamReader responseStream = new StreamReader(objResponse.GetResponseStream()))
                    {
                        post_response = responseStream.ReadToEnd();
                        responseStream.Close();
                    }
                    Array response_array = post_response.Split('|');

                    if (response_array.GetValue(3).ToString() == "This transaction has been approved.")
                    {
                        objdonar.PaymentMethod = "Authorize.Net"; 
                        objdonar.TransactionId = response_array.GetValue(6).ToString();
                        objdonar.PaymentStatus = "Completed";
                        objdonar.DonorId = DonarId;
                        objdonar.OrderDate = DateTime.UtcNow;
                        objdonar.UpdatedBy = objdonar.FirstName;
                        objdonar.Amount = objdonar.Amount;
                        Int64 result = _Donors.UpdateDonors(objdonar);
                        return RedirectToAction("Acknowledgement", "Donation", new { id = DonarId });
                    }
                    else
                    {
                        return RedirectToAction("PaymentFailed", "Donation", new { str = response_array.GetValue(3).ToString() });
                    } 
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"error closable\">" + ex.Message + "</div>";
                return RedirectToAction("AddDonor", "Donation");
            }

            return View();
        }

        // Authorized.net Payment Failed
        public ActionResult PaymentFailed(string str = "")
        {
            ViewBag.str = str;
            return View();
        }

        // Authorized.net Acknowledgement
        public ActionResult AcknowledgementByAuth(Int64 id = 0, string PaymentMethod = "Authorize.Net", string OrderType = "")
        {
            try
            {
                Entities.Donors objDonors = new Entities.Donors(); 
                int result1 = 0;
                if (PaymentMethod == "Authorize.Net")
                {

                    objDonors = _Donors.GetDonorsById(id, ref result1); 
                    int _status = 0;
                    Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("Thank you for giving donation", 0, ref _status);
                    if (objTemplates != null)
                    {
                        StringBuilder body = new StringBuilder();
                        body.Append(objTemplates.Description);
                        body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                        body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objDonors.FirstName));
                        body.Replace("[FBUrl]", ConfigurationManager.AppSettings["FBUrl"].ToString());
                        body.Replace("[TWUrl]", ConfigurationManager.AppSettings["TWUrl"].ToString());
                        body.Replace("[YUrl]", ConfigurationManager.AppSettings["YUrl"].ToString());
                        body.Replace("[TPhoneNo]", ConfigurationManager.AppSettings["TPhoneNo"].ToString());
                        body.Replace("[TEmailId]", ConfigurationManager.AppSettings["TEmailId"].ToString());
                        body.Replace("[Name]", objDonors.FirstName);
                        body.Replace("[Email]", objDonors.Email);
                        body.Replace("[PhoneNo]", objDonors.PhoneNo); 
                        body.Replace("[Address]", objDonors.Address);
                        body.Replace("[DonationCause]", objDonors.DonationCause);
                        body.Replace("[DonationProgram]", objDonors.DonationProgram);
                        body.Replace("[Amount]", "$ " + objDonors.Amount.ToString());
                        body.Replace("[TransactionId]", objDonors.TransactionId);
                        body.Replace("[PaymentStatus]", objDonors.PaymentStatus);
                        body.Replace("[PaymentMethod]", objDonors.PaymentMethod);
                        body.Replace("[PaymentDate]", objDonors.OrderDate.ToShortDateString());
                        BLL.Common.SendMail(objDonors.Email, objTemplates.Subject, body.ToString());
                    }
                    Entities.MailTemplates objTemplates1 = _MailTemplates.GetMailTemplateById("Donate for Admin", 0, ref _status);
                    if (objTemplates1 != null)
                    {
                        StringBuilder body1 = new StringBuilder();
                        body1.Append(objTemplates1.Description);
                        body1.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                        body1.Replace("[FBUrl]", ConfigurationManager.AppSettings["FBUrl"].ToString());
                        body1.Replace("[TWUrl]", ConfigurationManager.AppSettings["TWUrl"].ToString());
                        body1.Replace("[YUrl]", ConfigurationManager.AppSettings["YUrl"].ToString());
                        body1.Replace("[TPhoneNo]", ConfigurationManager.AppSettings["TPhoneNo"].ToString());
                        body1.Replace("[TEmailId]", ConfigurationManager.AppSettings["TEmailId"].ToString());
                        body1.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objDonors.FirstName));
                        body1.Replace("[Name]", objDonors.FirstName);
                        body1.Replace("[Email]", objDonors.Email);
                        body1.Replace("[PhoneNo]", objDonors.PhoneNo); 
                        body1.Replace("[Address]", objDonors.Address);
                        body1.Replace("[DonationCause]", objDonors.DonationCause);
                        body1.Replace("[DonationProgram]", objDonors.DonationProgram);
                        body1.Replace("[Amount]", "$ " + objDonors.Amount.ToString());
                        body1.Replace("[TransactionId]", objDonors.TransactionId);
                        body1.Replace("[PaymentStatus]", objDonors.PaymentStatus);
                        body1.Replace("[PaymentMethod]", objDonors.PaymentMethod);
                        body1.Replace("[PaymentDate]", objDonors.OrderDate.ToShortDateString());

                        BLL.Common.SendMail(ConfigurationManager.AppSettings["adminemailid"].ToString(), objTemplates1.Subject, body1.ToString());
                    }
                    ViewBag.objDonors = objDonors; 
                }
            }
            catch
            {
                return RedirectToAction("Error404", "Error");
            }

            return View();
        }

        #endregion

        public ActionResult Thankyou()
        {
            return View();
        }
         
        public ActionResult DonarsList(string type="")
        {
            int status = 0;
            List<Entities.Donors> lstDonors = new List<Entities.Donors>();
            try
            {
                lstDonors = _Donors.FEDonorsGetList("",type,"UpdatedDate DESC",1,1000, ref status);
            }
            catch
            {
                return RedirectToAction("Error404", "Error");
            }
            ViewBag.type = type;
            ViewBag.lstDonors = lstDonors;
            return View();
        }
    }
}
