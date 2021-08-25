using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.User.Controllers
{
    public class NewsController : Controller
    {
        BLL.News _News = new BLL.News();
        BLL.Users _users = new BLL.Users();

        public ActionResult Index(Int64 nid = 0)
        {
            ViewBag.NewsId = nid;
            return View();
        }

        public ActionResult NewsList(Int64 NewsId = 0, string Search = "", string SortColumn = "E.UpdatedTime", string SortOrder = "DESC", int PageNo = 1, int Items = 15)
        {
            int status = 0;
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            try
            {
                ViewBag.NewsList = _News.FENewsGetListByVariable(NewsId, Search, Sort, PageNo, Items, ref status);
            }
            catch
            {
                status = -1;
            }
            ViewBag.PageNo = PageNo;
            ViewBag.Items = Items;
            ViewBag.Total = status;
            ViewBag.NewsId = NewsId;
            return View();
        }

    }
}
