using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace TCAssociationTool.Areas.API.Controllers
{
    public class MemberAPIController : ApiController
    {
        TCAssociationTool.BLL.Users _user = new TCAssociationTool.BLL.Users();
        TCAssociationTool.BLL.Members _Members = new TCAssociationTool.BLL.Members();

        [HttpGet]
        public HttpResponseMessage LogOn(string pno = "", string pass = "", string returnurl = "")
        {
            string msg = "";
            int _status = 0;
            string str = "";
            try
            {
                Entities.Users model = new Entities.Users();
                model.MobilePhone = pno;
                model.Password = pass;


                Entities.Members objMembers = _Members.GetMemberFullDetailsByPhoneNo(model.MobilePhone, ref _status);
                if (objMembers != null && objMembers.MemberId != 0)
                {
                    if (objMembers.IsApproved == true)
                    {
                        int _qstatus = 0;
                        string password = _Members.GetPassword(objMembers.MemberId, ref _qstatus);
                        if (_qstatus == 1)
                        {
                            //if (TCAssociationTool.BLL.Password.VerifyHash(pass.Trim(), "SHA512", password) == true)
                            //{
                                msg = "Success";
                                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, MemberId = objMembers.MemberId, objMembers.Email, objMembers.UserName, MobileNo = objMembers.MobilePhone });
                            //}
                            //else
                            //{
                            //    msg = "Failed";
                            //    str = "Username or Password is incorrect.";
                            //    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, str });
                            //}
                        }
                        else
                        {
                            msg = "Failed";
                            str = "Username or Password is incorrect.";
                            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, str });
                        }
                    }
                    else
                    {
                        msg = "Failed";
                        str = "Your status has been deactivated.Please contact to admin.";
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, str });
                    }
                }
                else
                {
                    msg = "Failed";
                    msg = "Username or Password is incorrect.";
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, str });
                }

            }
            catch (Exception ex)
            {
                msg = "Failed";
                str = "Username or Password is incorrect.";
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, str });
            }

        }
    }
}
