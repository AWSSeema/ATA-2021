using TCAssociationTool.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ClosedXML.Excel;


namespace TCAssociationTool.Areas.Admin.Controllers
{
    public class TicketController : Controller
    {
        BLL.Ticket _Ticket = new BLL.Ticket();
        Entities.Ticket objTicket = new Entities.Ticket();
        DAL.Ticket _DTicket = new DAL.Ticket();
        List<Entities.Events> lstEvents = new List<Entities.Events>();
        BLL.Events _Events = new BLL.Events();

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
            int status = 0;
            try
            {
                lstEvents = _Events.GetEventsList(ref status);
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.lstEvents = lstEvents;

            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult TicketList(Int64 eventid = 0, string Search = "", string SortColumn = "Id", string SortOrder = "Desc", int PageNo = 1, int Items = 20)
        {
            int Total = 0;
            List<Entities.Ticket> lstTicket = new List<Entities.Ticket>();
            try
            {
                string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
                lstTicket = _Ticket.GetTicketListByVariable(eventid, Search, Sort, PageNo, Items, ref Total);
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.total = Total;
            ViewBag.lstTicket = lstTicket;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder;
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult EditTicket(Int64 Id)
        {
            try
            {
                int status = 0;
                objTicket = _Ticket.GetTicketById(Id, ref status);
                if (status != -1 && objTicket != null)
                {
                    ViewBag.objTicket = objTicket;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed processing your request.</div>";
                }

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return View();
        }


        [Models.SessionClass.SessionExpireFilter]
        public ActionResult ViewTicket(Int64 Id)
        {
            try
            {
                int status = 0;
                objTicket = _Ticket.GetTicketById(Id, ref status);
                if (status != -1 && objTicket != null)
                {
                    ViewBag.objTicket = objTicket;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed processing your request.</div>";
                }

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return View();
        }



        [Models.SessionClass.SessionExpireFilter]
        public ActionResult AddTicket()
        {
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult InsertTicket(Entities.Ticket objTicket)
        {
            try
            {


                Int64 _status = _Ticket.InsertTicket(objTicket);
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
                    return RedirectToAction("Index", "Ticket");

                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return RedirectToAction("Index", "Ticket");
        }

        //[Models.SessionClass.SessionExpireFilter]
        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult TicketCommentUpdate(Entities.Ticket objTicket)
        //{
        //    try
        //    {


        //        Int64 _status = _Ticket.TicketUpdateComments(objTicket);
        //        if (_status != -1)
        //        {

        //            if (_status == 2)
        //            {
        //                TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Updated Comment Successfully</div>";
        //            }
        //            return RedirectToAction("Index", "Ticket");

        //        }
        //    }
        //    catch
        //    {
        //        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
        //    }
        //    return RedirectToAction("Index", "Ticket");
        //}


        [HttpPost]
        [Models.SessionClass.SessionExpireFilter]
        public JsonResult Delete(Int64 Id)
        {
            string str = "";
            bool _bool = true;

            try
            {
                Int64 _status = _Ticket.DeleteTicket(Id);
                switch (_status)
                {
                    case 1:


                        str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                        _bool = true;
                        break;
                    case -1:
                        str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting Ticket.</div>";
                        _bool = false;
                        break;
                }
            }
            catch
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                _bool = false;
            }
            return Json(new { ok = _bool, data = str });
        }


        [HttpPost]
        public JsonResult TicketDeleteAll(string Id)
        {
            string str = "";
            try
            {
                Int64 _status = _Ticket.TicketDeleteAll(Id);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Records Deleted Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"error-alert closable\">Failed deleting members.</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }

        public void TicketExporttoExcel(string Search = "", Int64 eventid =0, string SortColumn = "Id", string SortOrder = "DESC")
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            try
            {
                DataTable dtUni = _DTicket.ExportTicketList(Search, eventid, Sort);

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dtUni, "Ticket-Registration-Export");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Ticket-Registration-Export-" + DateTime.UtcNow.ToString("MM-dd-yyyy") + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
        }

    }
}
