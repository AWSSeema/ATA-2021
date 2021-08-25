using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TCAssociationTool.Areas.AIP.Controllers
{
    public class WebpagesAPIController : ApiController
    {

        BLL.InnerPages _innerpage = new BLL.InnerPages();

        [HttpGet]
        public HttpResponseMessage PageDetails(string Heading = "")
        {
            int status = 0;
            string msg = "";
            try
            {
                Entities.InnerPages objInnerPages = new Entities.InnerPages();
                objInnerPages = _innerpage.GetInnerPagesListById(0, Heading, ref status);

                if (objInnerPages.InnerPageId != 0)
                {
                    msg = "Success";
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, objInnerPages.InnerPageId, objInnerPages.Heading, Description = System.Text.RegularExpressions.Regex.Replace(objInnerPages.Description, @"<[^>]+>|&nbsp;", "").Trim() });
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
