using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.User.Controllers
{
    public class SponsorsController : Controller
    {
        TCAssociationTool.BLL.Sponsors _Sponsors = new TCAssociationTool.BLL.Sponsors();

        public ActionResult Index(string Type = "Sponsors")
        {
            int status = 0;

            List<TCAssociationTool.Entities.SponsorCategories> lstSponsorCategories = new List<TCAssociationTool.Entities.SponsorCategories>();
            List<TCAssociationTool.Entities.Sponsors> lstSponsors = new List<TCAssociationTool.Entities.Sponsors>();
            try
            {
                lstSponsors = _Sponsors.FEGetListLeftBlock(ref lstSponsorCategories, ref status);
            }
            catch
            {
                return RedirectToAction("Error404", "Error");
            }
            ViewBag.lstSponsorCategories = lstSponsorCategories;
            ViewBag.lstSponsors = lstSponsors;
            ViewBag.Type = Type;
            return View();            
        }

        public ActionResult Media()
        {
            return View();
        }

    }
}
