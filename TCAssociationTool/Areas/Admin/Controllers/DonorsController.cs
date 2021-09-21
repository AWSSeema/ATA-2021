using TCAssociationTool.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.IO;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using ClosedXML.Excel;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,")]
    public class DonorsController : Controller
    {

        TCAssociationTool.BLL.Donors _Donor = new TCAssociationTool.BLL.Donors();
        DAL.Donors _DonorsD = new DAL.Donors();
        BLL.Members _Members = new BLL.Members();
        Entities.Members objMembers = new Entities.Members();
        BLL.DonationCategories _DonationCategories = new BLL.DonationCategories();
        TCAssociationTool.Entities.Donors objRequestCalls = new TCAssociationTool.Entities.Donors();

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index(Int64 DonationCategoryId = 0)
        {
            try
            {
                int qstatus = 0;
                objMembers = _Members.AddMembershipRequirement(ref qstatus);
                ViewBag.objMembers = objMembers;

                int _qstatus = 0;
                List<Entities.DonationCategories> _lstDonationCategories = _DonationCategories.GetDonationCategoriesList(ref _qstatus);
                if (_qstatus == 1)
                {
                    ViewBag.lstDonationCategories = _lstDonationCategories;
                    ViewBag.status = _qstatus;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "Donors");
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.DonationCategoryId = DonationCategoryId;
            return View();
        }

        [HttpGet]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult DonorsList(Int64 PaymentMethodId = 0, Int64 PaymentStatusId = 0, string StartDate = "", string EndDate = "", string search = "", string SortColumn = "UpdatedDate", string SortOrder = "Desc", int PageNo = 1, int PageItems = 10)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;
            List<Entities.Donors> lstDonors = new List<Entities.Donors>();
            try
            {
                lstDonors = _Donor.GetDonorsListByVariable(PaymentMethodId, PaymentStatusId, StartDate, EndDate, HttpUtility.UrlDecode(search.Trim()), Sort, PageNo, PageItems, ref Total);
               
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message+"</div>";
            }
            ViewBag.Total = Total;
            ViewBag.items = PageItems;
            ViewBag.PageNo = PageNo;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();
            ViewBag.lstDonors = lstDonors;
            return View();
        }

        public JsonResult DeleteDonor(Int64 DonorId)
        {
            string str = "";
            try
            {
                Int64 _status = _Donor.DeleteDonor(DonorId);
                if (_status == 1)
                {
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

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult ViewDonor(Int64 DonorId)
        {
            Entities.Donors objDonors = new Entities.Donors();
            int status = 0;
            try
            {
                objDonors = _Donor.GetDonorsById(DonorId, ref status);

                if (objDonors == null)
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Sorry, failed processing your request.</div>";
                    return RedirectToAction("Index", "Donors");
                }

            }
            catch(Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex .Message+ "</div>";
                return RedirectToAction("Index", "Donors");
            }

            ViewBag.objDonors = objDonors;
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult EditDonor(Int64 DonorId)
        {
            Entities.Donors objDonors = new Entities.Donors();
            Entities.Members objMembers = new Entities.Members();
            List<Entities.DonationCategories> lstDonationCategories = new List<Entities.DonationCategories>();

            int status = 0;
            int Donationstatus = 0;
            try
            {
                int _qstatus = 0;
                objMembers = _Members.AddMembershipRequirement(ref _qstatus);
                objDonors = _Donor.GetDonorsById(DonorId, ref status);
                lstDonationCategories = _DonationCategories.FEDonationCategoriesGetList(ref Donationstatus);

                if (objDonors == null && objMembers == null)
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Sorry, failed processing your request.</div>";
                    return RedirectToAction("Index", "Donors");
                }

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Sorry, failed processing your request.</div>";
                return RedirectToAction("Index", "Donors");
            }
            ViewBag.lstDonationCategories = lstDonationCategories;
            ViewBag.objDonors = objDonors;
            ViewBag.objMembers = objMembers;
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult AddDonor(Entities.Donors objDonors)
        {
            try
            {
                Int64 DonorId = 0;

                objDonors.UpdatedBy = HttpContext.User.Identity.Name.ToString();
                objDonors.InsertedBy = HttpContext.User.Identity.Name.ToString();
                Int64 _status = _Donor.InsertDonors(objDonors, ref DonorId);
                if (_status == 1)
                {
                    TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully</div>";
                    return RedirectToAction("Index", "Donors");
                }
                if (_status == 2)
                {
                    TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                    return RedirectToAction("EditDonor", "Donors", new { DonorId = objDonors.DonorId });
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed uploading page.</div>";
                    return RedirectToAction("Index", "Donors");
                }
            }
            catch(Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message+"</div>";
                return RedirectToAction("Index", "Donors");
           
            }

            return View();
        }

        [Models.SessionClass.SessionExpireFilter]

        public ActionResult AddDonor(Int64 DonationCategoryId = 0)
        {
            Entities.Members objMembers = new Entities.Members();
            List<Entities.DonationCategories> lstDonationCategories = new List<Entities.DonationCategories>();

            int Donationstatus = 0;
            try
            {
                int _qstatus = 0;
                objMembers = _Members.AddMembershipRequirement(ref _qstatus);
                lstDonationCategories = _DonationCategories.GetDonationCategoriesList(ref Donationstatus);
                //List<Entities.DonationCategories> lstDonationCategories = _DonationCategories.GetDonationCategoriesList(ref _qstatus);
                if (_qstatus == 1)
                {
                    ViewBag.lstDonationCategories = lstDonationCategories;
                    ViewBag.status = _qstatus;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "Donors");
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.DonationCategoryId = DonationCategoryId;
            ViewBag.objMembers = objMembers;
            return View();
        }

        //{
        //    int _qstatus = 0;
        //    try
        //    {
        //        List<Entities.DonationCategories> lstDonationCategories = _DonationCategories.GetDonationCategoriesList(ref _qstatus);

        //        if (_qstatus == 1)
        //        {
        //            ViewBag.lstDonationCategories = lstDonationCategories;
        //            ViewBag.status = _qstatus;
        //        }
        //        else
        //        {
        //            TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
        //            return RedirectToAction("Index", "Donors");
        //        }
        //    }
        //    catch
        //    {
        //        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
        //    }
        //    ViewBag.CategoryName = CategoryName;
        //    return View();
        //}
       

        [HttpPost]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public JsonResult DonorsStatus(Int64 DonorId)
        {
            string str = "";
            try
            {
                Int64 _status = _Donor.UpdateDonorsStatus(DonorId);
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


        #region export to excel

        public void DonorRegistrationsExportToExcel(Int64 PaymentStatusId = 0, Int64 PaymentMethodId = 0, string Search = "", string SortColumn = "InsertedDate", string SortOrder = "DESC")
        {
            try
            {
                string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
                DataTable dtUni = _DonorsD.DonorRegistrationsExportToExcel(PaymentStatusId, PaymentMethodId, Search, Sort);
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
                    Response.AddHeader("content-disposition", "attachment;filename=Donor-Registration-Export-" + DateTime.UtcNow.ToString("dd-MM-yyyy") + ".xlsx");
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


    }
}
