using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TCAssociationTool.Areas.API.Controllers
{
    public class NewsAPIController : ApiController
    {
        BLL.News _News = new BLL.News();

        [HttpGet]
        public HttpResponseMessage NewsListAPI(Int64 NewsId = 0, string Search = "", string SortColumn = "E.UpdatedTime", string SortOrder = "DESC", int PageNo = 1, int Items = 15)
        {
            int status = 0;
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");

            string msg = "";

            List<Entities.News> NewsList = new List<Entities.News>();

            string imgurl = System.Configuration.ConfigurationManager.AppSettings["baseurl"] + "Content/news/";

            try
            {
                msg = "success";

                NewsList = _News.FENewsGetListByVariable(NewsId, Search, Sort, PageNo, Items, ref status);

                var News = NewsList.Select(N => new { N.NewsId, N.Title, N.NewsText, ImageUrl = (BLL.Common.ValidateImage(imgurl + N.ImageUrl, imgurl + "no-image.jpg")), PostDate = Convert.ToDateTime(N.PostDate).ToString("dd MMM, yyyy hh:mm tt"), PostedBy = N.UpdatedBy, UpdatedTime = Convert.ToDateTime(N.UpdatedTime).ToString("dd MMM, yyyy hh:mm tt") });

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, News });
            }
            catch
            {
                msg = "failed";
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, msg);
            }
           
        }

        [HttpGet]
        public HttpResponseMessage NewsDetailsAPI(Int64 id = 0)
        {
            string msg = "";
            int status = 0;

            TCAssociationTool.Entities.News objNews = new TCAssociationTool.Entities.News();

            string imgurl = System.Configuration.ConfigurationManager.AppSettings["baseurl"] + "Content/news/";
            try
            {
                objNews = _News.GetNewsById(id, ref status);

                if (objNews.NewsId != 0)
                {
                    msg = "Success";
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, objNews.NewsId, objNews.Title, Description = System.Text.RegularExpressions.Regex.Replace(objNews.NewsText, @"<[^>]+>|&nbsp;", "").Trim(), ImageUrl = (BLL.Common.ValidateImage(imgurl + objNews.ImageUrl, imgurl + "no-image.jpg")), PostDate = Convert.ToDateTime(objNews.PostDate).ToString("dd MMM, yyyy hh:mm tt"), objNews.OrderNo, PostedBy = objNews.UpdatedBy, UpdatedTime = Convert.ToDateTime(objNews.UpdatedTime).ToString("dd MMM, yyyy hh:mm tt") });
                }
                else
                {
                    msg = "No page found";
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg });
                }
            }
            catch
            {
                msg = "failed";
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, msg);
            }
        }

    }
}
