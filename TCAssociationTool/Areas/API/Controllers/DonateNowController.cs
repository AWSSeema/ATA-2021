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

namespace TCAssociationTool.Areas.API.Controllers
{
    public class DonateNowController : Controller
    {
        BLL.Donors _Donors = new BLL.Donors();
        BLL.DonationCategories _DonationCategories = new BLL.DonationCategories();
        BLL.PaymentSettings _paymentSettings = new BLL.PaymentSettings();
        PayByPayPal objPayByPayPal = new PayByPayPal();
        BLL.MailTemplates _MailTemplates = new BLL.MailTemplates();
        BLL.Members _Members = new BLL.Members();
        Entities.Members _objMember = new Entities.Members();
        TCAssociationTool.BLL.AppInfo _appinfo = new BLL.AppInfo();

        [HttpGet]
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
        public ActionResult AddDonors(Entities.Donors objdonar)
        {
            try
            {
                Int64 DonarId = 0;
                objdonar.UpdatedBy = objdonar.FirstName;
                objdonar.InsertedBy = objdonar.FirstName;
                Int64 status1 = _Donors.InsertDonors(objdonar, ref DonarId);
                if (DonarId != 0)
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

                        objPayByPayPal.objPaymentSettings = objpaymentsettings;

                        string returnurl = objPayByPayPal.CheckOut();

                        return new RedirectResult(returnurl);
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

                                BLL.Common.SendMailwithCC(objAppInfo.PresidentPhone, objAppInfo.CompanyEmail, objAppInfo.CompanyEmail, objTemplates1.Subject, body1.ToString());
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
                            BLL.Common.SendMailwithCC(objAppInfo.PresidentPhone, objAppInfo.CompanyEmail, objAppInfo.CompanyEmail, objTemplates1.Subject, body1.ToString());
                        }
                        return RedirectToAction("Thankyou", "Donation");
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

    }
}
