using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TCAssociationTool.Areas.AIP.Controllers
{
    public class BoardMembersAPIController : ApiController
    {
        BLL.Committees _Committees = new BLL.Committees();
        BLL.CommitteeCategories _CommitteeCategories = new BLL.CommitteeCategories();
        List<Entities.Committees> lstCommittees = new List<Entities.Committees>();
        BLL.CommitteeMembers _CommitteeMembers = new BLL.CommitteeMembers();

        #region Board_Members

        [HttpGet]
        public HttpResponseMessage Index(string CType = "board-members")
        {
            string Ctype = BLL.Common.EncodeURL(CType);
            List<Entities.CommitteeCategories> lstCommitteeCategories = new List<Entities.CommitteeCategories>();
            List<Entities.CommitteeCategories> lstCommitteeCategories1 = new List<Entities.CommitteeCategories>();

            string imgurl = System.Configuration.ConfigurationManager.AppSettings["APIUrl"] + "Content/Committees/thumbimages/";
            try
            {
                string msg = "success";
                int status = 0;
                lstCommittees = _Committees.FECommitteesGetList(ref lstCommitteeCategories, CType, ref status);
                lstCommitteeCategories1 = _CommitteeCategories.GetCommitteeCategoriesList(ref status);

                var CommitteeList = lstCommittees.Select(C => new { C.CommitteeId, C.Name, C.PhoneNo, C.Address, C.City, C.State, C.Email, C.ImageUrl, C.DisplayOrder, C.IsActive, C.Description, C.UpdatedBy, InsertedDate = Convert.ToDateTime(C.UpdatedTime).ToString("dd MMM, yyyy"), C.CommitteeCategoryId, C.CategoryName, C.Designation, C.Type, NewsImage = (BLL.Common.ValidateImage(imgurl + C.ImageUrl, imgurl + "no-image-small-" + BLL.Common.GetRandomNumber(1) + ".jpg")) });
                var CommitteeCategories = lstCommitteeCategories.Select(CC => new { CC.CommitteeCategoryId, CC.CategoryName, CC.Type, CC.DisplayOrder, CC.IsActive, CC.UpdatedBy, CC.UpdatedTime, CC.CommitteeMemberCount });
                var CommitteeCategoriesList2 = lstCommitteeCategories.Select(CC => new { CC.CommitteeCategoryId, CC.CategoryName, CC.Type, CC.DisplayOrder, CC.IsActive, CC.Year, CC.UpdatedBy, CC.UpdatedTime });
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, CommitteeList, CommitteeCategories, CommitteeCategoriesList2 });
            }
            catch
            {
                string msg = "failed";
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, msg);
            }
        }

        #endregion

        #region Committees

        [HttpGet]
        public HttpResponseMessage PastPresidents(string CType = "", string Year = "")
        {
            List<Entities.CommitteeCategories> lstCommitteeCategories = new List<Entities.CommitteeCategories>();
            List<Entities.CommitteeCategories> lstCommitteeCategories1 = new List<Entities.CommitteeCategories>();

            string imgurl = System.Configuration.ConfigurationManager.AppSettings["usersiteurl"] + "Content/committees/normalphoto/";
            try
            {
                int status = 0;
                string msg = "success";
                lstCommittees = _Committees.FEPastCommitteesGetList(ref lstCommitteeCategories, CType, Year, ref status);
                var CommitteeList = lstCommittees.Select(C => new { C.CommitteeId, C.Name, PhoneNo = (C.PhoneNo != null && C.PhoneNo != "" ? C.PhoneNo : " "), Address = (C.Address != null && C.Address != "" ? C.Address : " "), City = (C.City != null && C.City != "" ? C.City : " "), State = (C.State != null && C.State != "" ? C.State : " "), Email = (C.Email != null && C.Email != "" ? C.Email : " "), ImageUrl = (BLL.Common.ValidateImage(imgurl + C.ImageUrl, imgurl + "no-image.jpeg")), C.DisplayOrder, C.IsActive, C.Description, C.UpdatedBy, InsertedDate = Convert.ToDateTime(C.UpdatedTime).ToString("dd MMM, yyyy"), C.CommitteeCategoryId, C.CategoryName, C.Designation, C.Type });
                var CommitteeCategories = lstCommitteeCategories.Select(CC => new { CC.CommitteeCategoryId, CC.CategoryName, CC.Type, CC.DisplayOrder, CC.IsActive, CC.UpdatedBy, CC.UpdatedTime, CC.CommitteeMemberCount });
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, CommitteeList, CommitteeCategories });
            }
            catch
            {
                string msg = "failed";
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, msg);
            }
        }

        [HttpGet]
        public HttpResponseMessage Committeeinfo(Int64 CommitteeId = 0)
        {
            string str = "";
            try
            {

                int _qstatus = 0;
                Entities.Committees _objCommittees = _Committees.GetCommitteeById(CommitteeId, ref _qstatus);

                string msg = "success";
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, _objCommittees });
                
            }
            catch
            {
                string msg = "failed";
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, msg);
            }
        }

        [HttpGet]
        public HttpResponseMessage CommitteeYears(string CType = "")
        {
            string Ctype = BLL.Common.EncodeURL(CType);
            List<Entities.CommitteeCategories> lstCommitteeCategories = new List<Entities.CommitteeCategories>();

            string imgurl = System.Configuration.ConfigurationManager.AppSettings["usersiteurl"] + "Content/committees/normalphoto/";
            try
            {
                string msg = "success";
                int status = 0;
                lstCommitteeCategories = _Committees.GetCommitteeYears(ref status);
                
                var CommitteeYears = lstCommitteeCategories.Select(CC => new { CC.Year });                
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, CommitteeYears });
            }
            catch
            {
                string msg = "failed";
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, msg);
            }
        }

        [HttpGet]
        public HttpResponseMessage CommitteesList(string CType = "")
        {
            string Ctype = BLL.Common.EncodeURL(CType);
            List<Entities.CommitteeCategories> lstCommitteeCategories = new List<Entities.CommitteeCategories>();
            List<Entities.CommitteeCategories> lstCommitteeCategories1 = new List<Entities.CommitteeCategories>();

            string imgurl = System.Configuration.ConfigurationManager.AppSettings["usersiteurl"] + "Content/committees/normalphoto/";

            try
            {
                int status = 0;
                string msg = "success";
                lstCommittees = _Committees.FECommitteesGetList(ref lstCommitteeCategories, CType, ref status);
                lstCommitteeCategories1 = _CommitteeCategories.GetCommitteeCategoriesList(ref status);

                var CommitteeList = lstCommittees.Select(C => new { C.CommitteeId, C.Name, C.PhoneNo, C.Address, C.City, C.State, C.Email, ImageUrl = (BLL.Common.ValidateImage(imgurl + C.ImageUrl, imgurl + "no-image.jpeg")), C.DisplayOrder, C.IsActive, C.Description, C.UpdatedBy, InsertedDate = Convert.ToDateTime(C.UpdatedTime).ToString("dd MMM, yyyy"), C.CommitteeCategoryId, C.CategoryName, C.Designation, C.Type, NewsImage = (BLL.Common.ValidateImage(imgurl + C.ImageUrl, imgurl + "no-image-small-" + BLL.Common.GetRandomNumber(1) + ".jpg")) });
                var CommitteeCategories = lstCommitteeCategories.Select(CC => new { CC.CommitteeCategoryId, CC.CategoryName, CC.Type, CC.DisplayOrder, CC.IsActive, CC.UpdatedBy, CC.UpdatedTime, CC.CommitteeMemberCount });
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, CommitteeList, CommitteeCategories });
            }
            catch
            {
                string msg = "failed";
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, msg);
            }
        }

        [HttpGet]
        public HttpResponseMessage CommitteeCategory()
        {
            int status = 0;
            string msg = "";

            List<Entities.CommitteeCategories> lstCommitteeCategories = new List<Entities.CommitteeCategories>();
            try
            {
                msg = "Success";

                lstCommitteeCategories = _CommitteeCategories.GetCommitteeCategoriesList(ref status);

                var CommitteeCategory = lstCommitteeCategories.Select(CC => new { CC.CommitteeCategoryId, CC.CategoryName, CC.Type, CC.DisplayOrder, CC.Year, CC.UpdatedBy, PostedDate = Convert.ToDateTime(CC.UpdatedTime).ToString("dd MMM,yyyy") });

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, CommitteeCategory });

            }
            catch (Exception ex)
            {
                msg = "failed";
                string str = ex.Message;
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, msg, str);
            }
        }

        #endregion

    }
}
