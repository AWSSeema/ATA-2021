using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TCAssociationTool.BLL
{
    public class Members
    {
        DAL.Members _Members = new DAL.Members();
        DAL.Members _Members1 = new DAL.Members();

        #region Methods

        public Int64 DeleteMembers(Int64 MemberId)
        {
            Int64 _status = 0;
            if (MemberId != 0)
            {
                _status = _Members.DeleteMember(MemberId);
            }
            return _status;
        }

        public Int64 DeleteMemberOrder(Int64 MemberOrderId)
        {
            Int64 _status = 0;
            if (MemberOrderId != 0)
            {
                _status = _Members.DeleteMemberOrder(MemberOrderId);
            }
            return _status;
        }

        public Int64 DeleteChildInfo(Int64 ChildInfoId)
        {
            Int64 _status = 0;
            if (ChildInfoId != 0)
            {
                _status = _Members.DeleteChildInfo(ChildInfoId);
            }
            return _status;
        }

        public Int64 InsertMembers(Entities.Members objMembers,ref Int64 MemberId,ref string imageurl)
        {
            Int64 _status = 0;
            if (objMembers != null)
            {
                _status = _Members.InsertMember(objMembers,ref MemberId,ref imageurl);
            }
            return _status;
        }

        public Int64 UpdateMemberStatus(Int64 MemberId)
        {
            Int64 _status = 0;
            if (MemberId != 0)
            {
                _status = _Members.UpdateMemberStatus(MemberId);
            }
            return _status;
        }

        public Int64 FEInsertMembers(Entities.Members objMembers, ref Int64 MemberId, ref string imageurl)
        {
            Int64 _status = 0;
            if (objMembers != null)
            {
                _status = _Members.FEInsertMember(objMembers, ref MemberId, ref imageurl);
            }
            return _status;
        }

        public Int64 UpdateMembers(Entities.Members objMembers)
        {
            Int64 _status = 0;
            if (objMembers != null)
            {
                _status = _Members.UpdateMember(objMembers);
            }
            return _status;
        }

        public Int64 ProfileEmailUpdate(string Email, Int64 UserId)
        {
            Int64 _status = 0;

            _status = _Members.ProfileEmailUpdate(Email, UserId);

            return _status;
        }

        public Int64 UpdateUserProfileImage(Int64 MemberId, ref string ProfileImage)
        {
            Int64 _status = 0;
            if (MemberId != 0)
            {
                _status = _Members.UpdateUserProfileImage(MemberId, ref ProfileImage);
            }
            return _status;
        }

        public Int64 UpdateMemberProfile(Entities.Members objMembers)
        {
            Int64 _status = 0;
            if (objMembers != null)
            {
                _status = _Members.UpdateMemberProfile(objMembers);
            }
            return _status;
        }

        public Int64 DeleteAllMembers(string MemberId)
        {
            Int64 _status = 0;
            _status = _Members.DeleteAllMembers(MemberId);
            return _status;
        }

        public Int64 InsertChildrenInfo(Entities.ChildrenInfo objChildrenInfo)
        {
            Int64 _status = 0;
            if (objChildrenInfo != null)
            {
                _status = _Members.InsertChildrenInfo(objChildrenInfo);
            }
            return _status;
        }

        public Int64 InsertMemberOrder(Entities.MembershipOrders objMembershipOrders)
        {
            Int64 _status = 0;
            if (objMembershipOrders != null)
            {
                _status = _Members.InsertMemberOrder(objMembershipOrders);
            }
            return _status;
        }

        public Int64 ChangePassword(Int64 MemberId, string Password)
        {
            Int64 _status = 0;
            if (MemberId != 0 && Password != null && Password.Trim() != "")
            {
                _status = _Members.ChangePassword(MemberId, Password);
            }
            return _status;
        }

        public string GetPassword(Int64 _Memberid, ref int _qstatus)
        {
            string _password = "";
            DataTable dt = _Members.GetPassword(_Memberid, ref _qstatus);
            if (dt.Rows.Count == 1)
            {
                _password = dt.Rows[0]["Password"].ToString();
            }
            return _password;
        }

        public Int64 UpdateMemberProfileImage(Int64 MemberId, ref string ProfileImage)
        {
            Int64 _status = 0;
            if (MemberId != 0)
            {
                _status = _Members.UpdateMemberProfileImage(MemberId, ref ProfileImage);
            }
            return _status;
        }

        #endregion

        #region Entity Loading

        public List<Entities.Members> GetMembersListByVariable(string Search,Int64 MembershipTypeId,Int64 PaymentStatusId,string StartDate,string EndDate,string ExpiryDate,string IsVolunteer, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = _Members.GetMembersListByVariable(Search, MembershipTypeId, PaymentStatusId, StartDate, EndDate, ExpiryDate, IsVolunteer, Sort, PageNo, Items, ref Total);
            List<Entities.Members> lstMembers = new List<Entities.Members>();

            if (dt.Rows.Count == 0 && PageNo > 1)
            {
                dt = _Members.GetMembersListByVariable(Search, MembershipTypeId, PaymentStatusId, StartDate, EndDate, ExpiryDate, IsVolunteer, Sort, PageNo, Items, ref Total);
            }

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.Members objMembers = new Entities.Members();

                    objMembers.RId = Convert.ToInt64(dr["Rid"]);
                    objMembers.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objMembers.UserName = dr["UserName"].ToString();
                    objMembers.Email = dr["Email"].ToString();
                    objMembers.FirstName = dr["FirstName"].ToString();
                    objMembers.LastName = dr["LastName"].ToString();
                    objMembers.ProfileImage = (dr["ProfileImage"] != DBNull.Value ? dr["ProfileImage"].ToString() : null);
                    objMembers.Occupation = (dr["Occupation"] != DBNull.Value ? dr["Occupation"].ToString() : null);
                    objMembers.SpouseFirstName = (dr["SpouseFirstName"] != DBNull.Value ? dr["SpouseFirstName"].ToString() : null);
                    objMembers.SpouseLastName = (dr["SpouseLastName"] != DBNull.Value ? dr["SpouseLastName"].ToString() : null);
                    objMembers.SpouseOccupation = (dr["SpouseOccupation"] != DBNull.Value ? dr["SpouseOccupation"].ToString() : null);
                    objMembers.SpouseEmail = (dr["SpouseEmail"] != DBNull.Value ? dr["SpouseEmail"].ToString() : null);
                    objMembers.SpouseCell = (dr["SpouseCell"] != DBNull.Value ? dr["SpouseCell"].ToString() : null);
                    objMembers.Address = (dr["Address"] != DBNull.Value ? dr["Address"].ToString() : null);
                    objMembers.City = (dr["City"] != DBNull.Value ? dr["City"].ToString() : null);
                    objMembers.State = (dr["State"] != DBNull.Value ? dr["State"].ToString() : null);
                    objMembers.ZipCode = (dr["ZipCode"] != DBNull.Value ? dr["ZipCode"].ToString() : null);
                    objMembers.HomePhone = (dr["HomePhone"] != DBNull.Value ? dr["HomePhone"].ToString() : null);
                    objMembers.MobilePhone = (dr["MobilePhone"] != DBNull.Value ? dr["MobilePhone"].ToString() : null);
                    objMembers.IsApproved = Convert.ToBoolean(dr["IsApproved"]);
                    objMembers.IsLockedOut = (dr["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dr["IsLockedOut"]) : false);
                    objMembers.IsActivated = (dr["IsActivated"] != DBNull.Value ?Convert.ToBoolean(dr["IsActivated"]) : false);
                    objMembers.DateActivated = (dr["DateActivated"] != DBNull.Value ?Convert.ToDateTime(dr["DateActivated"]) : DateTime.MinValue);
                    objMembers.MembershipTypeId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembers.MembershipType = dr["MembershipType"].ToString();
                    objMembers.IsVolunteer = (dr["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dr["IsVolunteer"]) : false);
                    objMembers.IsTeluguorigin = (dr["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dr["IsTeluguorigin"]) : false);
                    objMembers.Comments = (dr["Comments"] != DBNull.Value ? dr["Comments"].ToString() : null);
                    objMembers.ReferredBy = (dr["ReferredBy"] != DBNull.Value ? dr["ReferredBy"].ToString() : null);
                    objMembers.MobilePhone = (dr["MobilePhone"] != DBNull.Value ? dr["MobilePhone"].ToString() : null);
                    objMembers.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);
                    objMembers.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
                    objMembers.objMembershipOrder.Amount = (dr["Amount"] != DBNull.Value ? Convert.ToDecimal(dr["Amount"]) : 0);
                    objMembers.objMembershipOrder.TransactionId = (dr["TransactionId"] != DBNull.Value ? dr["TransactionId"].ToString() : null);
                    objMembers.objMembershipOrder.MembershipTypeId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembers.objMembershipOrder.PaymentStatusId = (dr["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentStatusId"]) : 0);
                    objMembers.objMembershipOrder.PaymentMethodId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembers.objMembershipOrder.MembershipTypeId = (dr["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentMethodId"]) : 0);
                    objMembers.objMembershipOrder.AdminComment = (dr["AdminComment"] != DBNull.Value ? dr["AdminComment"].ToString() : null);
                    objMembers.objMembershipOrder.UserComment = (dr["UserComment"] != DBNull.Value ? dr["UserComment"].ToString() : null);
                    objMembers.objMembershipOrder.PaymentStatus = (dr["PaymentStatus"] != DBNull.Value ? dr["PaymentStatus"].ToString() : null);
                    objMembers.objMembershipOrder.PaymentMethod = (dr["PaymentMethod"] != DBNull.Value ? dr["PaymentMethod"].ToString() : null);
                    objMembers.objMembershipOrder.OrderDate = (dr["OrderDate"] != DBNull.Value ? Convert.ToDateTime(dr["OrderDate"]) : DateTime.MinValue);
                    objMembers.objMembershipOrder.ExpiryDate = (dr["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dr["ExpiryDate"]) : DateTime.MinValue);
                    objMembers.Fax = (dr["Fax"] != DBNull.Value ? dr["Fax"].ToString() : null);
                    objMembers.WebsiteAddress = (dr["WebsiteAddress"] != DBNull.Value ? dr["WebsiteAddress"].ToString() : null);
                    objMembers.Address2 = (dr["Address2"] != DBNull.Value ? dr["Address2"].ToString() : null);
                    lstMembers.Add(objMembers);
                }
            }
            return lstMembers;
        }

        public List<Entities.Members> GetMembersOrderDetailsListByVariable(string Search,string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = _Members.GetMembersOrderDetailsListByVariable(Search,Sort, PageNo, Items, ref Total);
            List<Entities.Members> lstMembers = new List<Entities.Members>();

            if (dt.Rows.Count == 0 && PageNo > 1)
            {
                dt = _Members.GetMembersOrderDetailsListByVariable(Search, Sort, PageNo, Items, ref Total);
            }

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.Members objMembers = new Entities.Members();

                    objMembers.RId = Convert.ToInt64(dr["Rid"]);
                    objMembers.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objMembers.SpouseCell = (dr["SpouseCell"] != DBNull.Value ? dr["SpouseCell"].ToString() : "");
                    objMembers.objMembershipOrder.MembershipOrderId = Convert.ToInt64(dr["MembershipOrderId"]);
                    objMembers.FirstName = dr["FirstName"].ToString();
                    objMembers.MembershipTypeId = Convert.ToInt64(dr["MembershipTypeId"]);
                    objMembers.MembershipType = dr["MembershipType"].ToString();
                    objMembers.objMembershipOrder.Amount = (dr["Amount"] != DBNull.Value ? Convert.ToDecimal(dr["Amount"]) : 0);
                    objMembers.objMembershipOrder.TransactionId = (dr["TransactionId"] != DBNull.Value ? dr["TransactionId"].ToString() : "");
                    objMembers.objMembershipOrder.MembershipTypeId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembers.objMembershipOrder.PaymentStatusId = (dr["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentStatusId"]) : 0);
                    objMembers.objMembershipOrder.PaymentMethodId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembers.objMembershipOrder.MembershipTypeId = (dr["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentMethodId"]) : 0);
                    objMembers.objMembershipOrder.AdminComment = (dr["AdminComment"] != DBNull.Value ? dr["AdminComment"].ToString() : "");
                    objMembers.objMembershipOrder.PaymentBy = (dr["PaymentBy"] != DBNull.Value ? dr["PaymentBy"].ToString() : "");
                    objMembers.objMembershipOrder.UserComment = (dr["UserComment"] != DBNull.Value ? dr["UserComment"].ToString() : "");
                    objMembers.objMembershipOrder.PaymentStatus = (dr["PaymentStatus"] != DBNull.Value ? dr["PaymentStatus"].ToString() : "");
                    objMembers.objMembershipOrder.PaymentMethod = (dr["PaymentMethod"] != DBNull.Value ? dr["PaymentMethod"].ToString() : "");
                    objMembers.objMembershipOrder.OrderDate = (dr["OrderDate"] != DBNull.Value ? Convert.ToDateTime(dr["OrderDate"]) : DateTime.MinValue);
                    objMembers.objMembershipOrder.ExpiryDate = (dr["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dr["ExpiryDate"]) : DateTime.UtcNow);
                    objMembers.objMembershipOrder.UpdatedTime = (dr["UpdatedTime"] != DBNull.Value ? Convert.ToDateTime(dr["UpdatedTime"]) : DateTime.MinValue);

                    lstMembers.Add(objMembers);
                }
            }
            return lstMembers;
        }

        public Entities.Members GetMembersById(Int64 MembersId, ref int status)
        {
            DataTable dt = _Members.GetMemberById(MembersId, ref status);
            Entities.Members objMembers = new Entities.Members();

            if (dt.Rows.Count == 1)
            {
                    objMembers.MemberId = Convert.ToInt64(dt.Rows[0]["MemberId"]);
                    objMembers.UserName = dt.Rows[0]["UserName"].ToString();
                    objMembers.Email = dt.Rows[0]["Email"].ToString();
                    objMembers.FirstName = dt.Rows[0]["FirstName"].ToString();
                    objMembers.LastName = dt.Rows[0]["LastName"].ToString();
                    objMembers.ProfileImage = (dt.Rows[0]["ProfileImage"] != DBNull.Value ? dt.Rows[0]["ProfileImage"].ToString() : null);
                    objMembers.Occupation = (dt.Rows[0]["Occupation"] != DBNull.Value ? dt.Rows[0]["Occupation"].ToString() : null);
                    objMembers.MemberAge = (dt.Rows[0]["MemberAge"] != DBNull.Value ? dt.Rows[0]["MemberAge"].ToString() : null);
                    objMembers.MemberSkils = (dt.Rows[0]["Occupation"] != DBNull.Value ? dt.Rows[0]["MemberSkils"].ToString() : null);
                    objMembers.SpouseSkils = (dt.Rows[0]["SpouseSkils"] != DBNull.Value ? dt.Rows[0]["SpouseSkils"].ToString() : null);
                    objMembers.SpouseFirstName = (dt.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt.Rows[0]["SpouseFirstName"].ToString() : null);
                    objMembers.SpouseLastName = (dt.Rows[0]["SpouseLastName"] != DBNull.Value ? dt.Rows[0]["SpouseLastName"].ToString() : null);
                    objMembers.SpouseOccupation = (dt.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt.Rows[0]["SpouseOccupation"].ToString() : null);
                    objMembers.SpouseEmail = (dt.Rows[0]["SpouseEmail"] != DBNull.Value ? dt.Rows[0]["SpouseEmail"].ToString() : null);
                    objMembers.SpouseCell = (dt.Rows[0]["SpouseCell"] != DBNull.Value ? dt.Rows[0]["SpouseCell"].ToString() : null);
                    objMembers.Address = (dt.Rows[0]["Address"] != DBNull.Value ? dt.Rows[0]["Address"].ToString() : null);
                    objMembers.City = (dt.Rows[0]["City"] != DBNull.Value ? dt.Rows[0]["City"].ToString() : null);
                    objMembers.State = (dt.Rows[0]["State"] != DBNull.Value ? dt.Rows[0]["State"].ToString() : null);
                    objMembers.ZipCode = (dt.Rows[0]["ZipCode"] != DBNull.Value ? dt.Rows[0]["ZipCode"].ToString() : null);
                    objMembers.HomePhone = (dt.Rows[0]["HomePhone"] != DBNull.Value ? dt.Rows[0]["HomePhone"].ToString() : null);
                    objMembers.MobilePhone = (dt.Rows[0]["MobilePhone"] != DBNull.Value ? dt.Rows[0]["MobilePhone"].ToString() : null);
                    objMembers.IsApproved = Convert.ToBoolean(dt.Rows[0]["IsApproved"]);
                    objMembers.IsLockedOut = Convert.ToBoolean(dt.Rows[0]["IsLockedOut"]);
                    objMembers.IsActivated = Convert.ToBoolean(dt.Rows[0]["IsActivated"]);
                    objMembers.DateActivated = (dt.Rows[0]["DateActivated"] != DBNull.Value ?Convert.ToDateTime(dt.Rows[0]["DateActivated"]) : DateTime.MinValue);
                    objMembers.MembershipTypeId = Convert.ToInt64(dt.Rows[0]["MembershipTypeId"]);
                    objMembers.MembershipType = dt.Rows[0]["MembershipType"].ToString();
                    objMembers.IsVolunteer = Convert.ToBoolean(dt.Rows[0]["IsVolunteer"]);
                    objMembers.IsTeluguorigin = Convert.ToBoolean(dt.Rows[0]["IsTeluguorigin"]);
                    objMembers.Comments = (dt.Rows[0]["Comments"] != DBNull.Value ? dt.Rows[0]["Comments"].ToString() : null);
                    objMembers.ReferredBy = (dt.Rows[0]["ReferredBy"] != DBNull.Value ? dt.Rows[0]["ReferredBy"].ToString() : null);
                    objMembers.MobilePhone = (dt.Rows[0]["MobilePhone"] != DBNull.Value ? dt.Rows[0]["MobilePhone"].ToString() : null);
                    objMembers.InsertedTime = Convert.ToDateTime(dt.Rows[0]["InsertedTime"]);
                    objMembers.UpdatedTime = Convert.ToDateTime(dt.Rows[0]["UpdatedTime"]);
                    objMembers.Fax = (dt.Rows[0]["Fax"] != DBNull.Value ? dt.Rows[0]["Fax"].ToString() : null);
                    objMembers.WebsiteAddress = (dt.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt.Rows[0]["WebsiteAddress"].ToString() : null);
                    objMembers.Address2 = (dt.Rows[0]["Address2"] != DBNull.Value ? dt.Rows[0]["Address2"].ToString() : null);
            }

            return objMembers;
        }

        public List<Entities.Members> GetMembersList(ref int status)
        {
            DataTable dt = _Members.GetMembersList(ref status);
            List<Entities.Members> lstMembers = new List<Entities.Members>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.Members objMembers = new Entities.Members();

                    objMembers.RId = Convert.ToInt64(dr["Rid"]);
                    objMembers.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objMembers.UserName = dr["UserName"].ToString();
                    objMembers.Email = dr["Email"].ToString();
                    objMembers.FirstName = dr["FirstName"].ToString();
                    objMembers.LastName = dr["LastName"].ToString();
                    objMembers.ProfileImage = (dr["ProfileImage"] != DBNull.Value ? dr["ProfileImage"].ToString() : null);
                    objMembers.Occupation = (dr["Occupation"] != DBNull.Value ? dr["Occupation"].ToString() : null);
                    objMembers.SpouseFirstName = (dr["SpouseFirstName"] != DBNull.Value ? dr["SpouseFirstName"].ToString() : null);
                    objMembers.SpouseLastName = (dr["SpouseLastName"] != DBNull.Value ? dr["SpouseLastName"].ToString() : null);
                    objMembers.SpouseOccupation = (dr["SpouseOccupation"] != DBNull.Value ? dr["SpouseOccupation"].ToString() : null);
                    objMembers.SpouseEmail = (dr["SpouseEmail"] != DBNull.Value ? dr["SpouseEmail"].ToString() : null);
                    objMembers.SpouseCell = (dr["SpouseCell"] != DBNull.Value ? dr["SpouseCell"].ToString() : null);
                    objMembers.Address = (dr["Address"] != DBNull.Value ? dr["Address"].ToString() : null);
                    objMembers.City = (dr["City"] != DBNull.Value ? dr["City"].ToString() : null);
                    objMembers.State = (dr["State"] != DBNull.Value ? dr["State"].ToString() : null);
                    objMembers.ZipCode = (dr["ZipCode"] != DBNull.Value ? dr["ZipCode"].ToString() : null);
                    objMembers.HomePhone = (dr["HomePhone"] != DBNull.Value ? dr["HomePhone"].ToString() : null);
                    objMembers.MobilePhone = (dr["MobilePhone"] != DBNull.Value ? dr["MobilePhone"].ToString() : null);
                    objMembers.IsApproved = Convert.ToBoolean(dr["IsApproved"]);
                    objMembers.IsLockedOut = Convert.ToBoolean(dr["IsLockedOut"]);
                    objMembers.IsActivated = Convert.ToBoolean(dr["IsActivated"]);
                    objMembers.DateActivated = (dr["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dr["DateActivated"]) : DateTime.MinValue);
                    objMembers.MembershipTypeId = Convert.ToInt64(dr["MembershipTypeId"]);
                    objMembers.MembershipType = dr["MembershipType"].ToString();
                    objMembers.IsVolunteer = Convert.ToBoolean(dr["IsVolunteer"]);
                    objMembers.IsTeluguorigin = Convert.ToBoolean(dr["IsTeluguorigin"]);
                    objMembers.Comments = (dr["Comments"] != DBNull.Value ? dr["Comments"].ToString() : null);
                    objMembers.ReferredBy = (dr["ReferredBy"] != DBNull.Value ? dr["ReferredBy"].ToString() : null);
                    objMembers.MobilePhone = (dr["MobilePhone"] != DBNull.Value ? dr["MobilePhone"].ToString() : null);
                    objMembers.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);
                    objMembers.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
                    objMembers.Fax = (dr["Fax"] != DBNull.Value ? dr["Fax"].ToString() : null);
                    objMembers.WebsiteAddress = (dr["WebsiteAddress"] != DBNull.Value ? dr["WebsiteAddress"].ToString() : null);
                    objMembers.Address2 = (dr["Address2"] != DBNull.Value ? dr["Address2"].ToString() : null);
                    lstMembers.Add(objMembers);
                }
            }

            return lstMembers;
        }

        public Entities.Members GetMembersFullDetailsById(Int64 MembersId, ref int status)
        {
            DataSet ds1 = _Members.GetMembersFullDetailsById(MembersId, ref status);

            DataTable dt_Members = ds1.Tables[0];
            DataTable dt_ChildrenInfo = ds1.Tables[1];
            DataTable dt_MembershipOrder = ds1.Tables[2];
             

            Entities.Members objMembers = new Entities.Members();
            List<Entities.ChildrenInfo> lstChildrenInfo = new List<Entities.ChildrenInfo>();
            List<Entities.MembershipOrders> lstMembershipOrder = new List<Entities.MembershipOrders>();

            if (dt_Members.Rows.Count ==1)
            {
               
                objMembers.MemberId = Convert.ToInt64(dt_Members.Rows[0]["MemberId"]);
                objMembers.UserName = dt_Members.Rows[0]["UserName"].ToString();
                objMembers.Email = dt_Members.Rows[0]["Email"].ToString();
                objMembers.FirstName = dt_Members.Rows[0]["FirstName"].ToString();
                objMembers.LastName = dt_Members.Rows[0]["LastName"].ToString();
                objMembers.ProfileImage = (dt_Members.Rows[0]["ProfileImage"] != DBNull.Value ? dt_Members.Rows[0]["ProfileImage"].ToString() : null);
                objMembers.Occupation = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["Occupation"].ToString() : null);
                objMembers.MemberAge = (dt_Members.Rows[0]["MemberAge"] != DBNull.Value ? dt_Members.Rows[0]["MemberAge"].ToString() : null);
                objMembers.MemberSkils = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["MemberSkils"].ToString() : null);
                objMembers.SpouseSkils = (dt_Members.Rows[0]["SpouseSkils"] != DBNull.Value ? dt_Members.Rows[0]["SpouseSkils"].ToString() : null);
                objMembers.SpouseFirstName = (dt_Members.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseFirstName"].ToString() : null);
                objMembers.SpouseLastName = (dt_Members.Rows[0]["SpouseLastName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseLastName"].ToString() : null);
                objMembers.SpouseOccupation = (dt_Members.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseOccupation"].ToString() : null);
                objMembers.SpouseEmail = (dt_Members.Rows[0]["SpouseEmail"] != DBNull.Value ? dt_Members.Rows[0]["SpouseEmail"].ToString() : null);
                objMembers.SpouseCell = (dt_Members.Rows[0]["SpouseCell"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCell"].ToString() : null);
                objMembers.Address = (dt_Members.Rows[0]["Address"] != DBNull.Value ? dt_Members.Rows[0]["Address"].ToString() : null);
                objMembers.City = (dt_Members.Rows[0]["City"] != DBNull.Value ? dt_Members.Rows[0]["City"].ToString() : null);
                objMembers.State = (dt_Members.Rows[0]["State"] != DBNull.Value ? dt_Members.Rows[0]["State"].ToString() : null);
                objMembers.ZipCode = (dt_Members.Rows[0]["ZipCode"] != DBNull.Value ? dt_Members.Rows[0]["ZipCode"].ToString() : null);
                objMembers.HomePhone = (dt_Members.Rows[0]["HomePhone"] != DBNull.Value ? dt_Members.Rows[0]["HomePhone"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.IsApproved = Convert.ToBoolean(dt_Members.Rows[0]["IsApproved"]);
                objMembers.IsLockedOut = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.IsActivated = (dt_Members.Rows[0]["IsActivated"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsActivated"]) : false);
                objMembers.DateActivated = (dt_Members.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["DateActivated"]) : DateTime.MinValue);
                objMembers.MembershipTypeId = Convert.ToInt64(dt_Members.Rows[0]["MembershipTypeId"]);
                objMembers.MembershipType = dt_Members.Rows[0]["MembershipType"].ToString();
                objMembers.IsVolunteer = Convert.ToBoolean(dt_Members.Rows[0]["IsVolunteer"]);
                objMembers.IsTeluguorigin = (dt_Members.Rows[0]["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsTeluguorigin"]) : false);
                objMembers.Comments = (dt_Members.Rows[0]["Comments"] != DBNull.Value ? dt_Members.Rows[0]["Comments"].ToString() : null);
                objMembers.ReferredBy = (dt_Members.Rows[0]["ReferredBy"] != DBNull.Value ? dt_Members.Rows[0]["ReferredBy"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.InsertedTime = Convert.ToDateTime(dt_Members.Rows[0]["InsertedTime"]);
                objMembers.UpdatedTime = Convert.ToDateTime(dt_Members.Rows[0]["UpdatedTime"]);
                objMembers.Fax = (dt_Members.Rows[0]["Fax"] != DBNull.Value ? dt_Members.Rows[0]["Fax"].ToString() : null);
                objMembers.WebsiteAddress = (dt_Members.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt_Members.Rows[0]["WebsiteAddress"].ToString() : null);
                objMembers.Address2 = (dt_Members.Rows[0]["Address2"] != DBNull.Value ? dt_Members.Rows[0]["Address2"].ToString() : null);
            }

            if (dt_ChildrenInfo.Rows.Count != 0)
            {               

                foreach (DataRow dr in dt_ChildrenInfo.Rows)
                {
                    Entities.ChildrenInfo objChildrenInfo = new Entities.ChildrenInfo();

                    objChildrenInfo.ChildrenInfoId = Convert.ToInt64(dr["ChildrenInfoId"]);
                    objChildrenInfo.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objChildrenInfo.FirstName = dr["FirstName"].ToString();
                    objChildrenInfo.LastName = dr["LastName"].ToString();
                    objChildrenInfo.Age = (dr["Age"] != DBNull.Value ? Convert.ToInt32(dr["Age"].ToString()) : 0);
                    objChildrenInfo.Relationship = dr["Relationship"].ToString();

                    lstChildrenInfo.Add(objChildrenInfo);
                }
            }

            objMembers.lstChildrenInfo = lstChildrenInfo;

            if (dt_MembershipOrder.Rows.Count != 0)
            {

                foreach (DataRow dr in dt_MembershipOrder.Rows)
                {
                    Entities.MembershipOrders objMembershipOrder = new Entities.MembershipOrders();

                    objMembershipOrder.MembershipOrderId = Convert.ToInt64(dr["MembershipOrderId"]);
                    objMembershipOrder.MemberId = (dr["MemberId"] != DBNull.Value ? Convert.ToInt64(dr["MemberId"]) : 0);
                    objMembershipOrder.MembershipTypeId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembershipOrder.Amount = (dr["Amount"] != DBNull.Value ? Convert.ToDecimal(dr["Amount"]) : 0);
                    objMembershipOrder.TransactionId = (dr["TransactionId"] != DBNull.Value ?dr["TransactionId"].ToString() : null);
                    objMembershipOrder.MembershipTypeId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembershipOrder.PaymentStatusId = (dr["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentStatusId"]) : 0);
                    objMembershipOrder.PaymentMethodId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembershipOrder.MembershipTypeId = (dr["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentMethodId"]) : 0);
                    objMembershipOrder.PaymentBy = (dr["PaymentBy"] != DBNull.Value ? dr["PaymentBy"].ToString() : null);
                    objMembershipOrder.AdminComment = (dr["AdminComment"] != DBNull.Value ? dr["AdminComment"].ToString() : null);
                    objMembershipOrder.UserComment = (dr["UserComment"] != DBNull.Value ? dr["UserComment"].ToString() : null);
                    objMembershipOrder.OrderDate = (dr["OrderDate"] != DBNull.Value ?Convert.ToDateTime(dr["OrderDate"]) : DateTime.MinValue);
                    objMembershipOrder.ExpiryDate = (dr["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dr["ExpiryDate"]) : DateTime.MinValue);
                    objMembershipOrder.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
                    objMembershipOrder.UpdatedBy = dr["UpdatedBy"].ToString();

                    lstMembershipOrder.Add(objMembershipOrder);
                }
            }

            objMembers.lstMembershipOrder = lstMembershipOrder;

            return objMembers;
        }

        public Entities.Members GetMemberFullDetailsById(Int64 MembersId, ref int status)
        {
            DataSet ds0 = _Members1.GetMemberFullDetailsById(MembersId, ref status);

            DataTable dt_Members = ds0.Tables[0];
            DataTable dt_ChildrenInfo = ds0.Tables[1];
            DataTable dt_MembershipOrder = ds0.Tables[2];


            Entities.Members objMembers = new Entities.Members();
            List<Entities.ChildrenInfo> lstChildrenInfo = new List<Entities.ChildrenInfo>();
            List<Entities.MembershipOrders> lstMembershipOrder = new List<Entities.MembershipOrders>();

            if (dt_Members.Rows.Count == 1)
            {

                objMembers.MemberId = Convert.ToInt64(dt_Members.Rows[0]["MemberId"]);
                objMembers.UserName = dt_Members.Rows[0]["UserName"].ToString();
                objMembers.Email = dt_Members.Rows[0]["Email"].ToString();
                objMembers.FirstName = dt_Members.Rows[0]["FirstName"].ToString();
                objMembers.LastName = dt_Members.Rows[0]["LastName"].ToString();
                objMembers.ProfileImage = (dt_Members.Rows[0]["ProfileImage"] != DBNull.Value ? dt_Members.Rows[0]["ProfileImage"].ToString() : null);
                objMembers.Occupation = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["Occupation"].ToString() : null);
                objMembers.MemberAge = (dt_Members.Rows[0]["MemberAge"] != DBNull.Value ? dt_Members.Rows[0]["MemberAge"].ToString() : null);
                objMembers.MemberSkils = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["MemberSkils"].ToString() : null);
                objMembers.SpouseSkils = (dt_Members.Rows[0]["SpouseSkils"] != DBNull.Value ? dt_Members.Rows[0]["SpouseSkils"].ToString() : null);
                objMembers.SpouseFirstName = (dt_Members.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseFirstName"].ToString() : null);
                objMembers.SpouseLastName = (dt_Members.Rows[0]["SpouseLastName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseLastName"].ToString() : null);
                objMembers.SpouseOccupation = (dt_Members.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseOccupation"].ToString() : null);
                objMembers.SpouseEmail = (dt_Members.Rows[0]["SpouseEmail"] != DBNull.Value ? dt_Members.Rows[0]["SpouseEmail"].ToString() : null);
                objMembers.SpouseCell = (dt_Members.Rows[0]["SpouseCell"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCell"].ToString() : null);
                objMembers.Address = (dt_Members.Rows[0]["Address"] != DBNull.Value ? dt_Members.Rows[0]["Address"].ToString() : null);
                objMembers.City = (dt_Members.Rows[0]["City"] != DBNull.Value ? dt_Members.Rows[0]["City"].ToString() : null);
                objMembers.State = (dt_Members.Rows[0]["State"] != DBNull.Value ? dt_Members.Rows[0]["State"].ToString() : null);
                objMembers.ZipCode = (dt_Members.Rows[0]["ZipCode"] != DBNull.Value ? dt_Members.Rows[0]["ZipCode"].ToString() : null);
                objMembers.HomePhone = (dt_Members.Rows[0]["HomePhone"] != DBNull.Value ? dt_Members.Rows[0]["HomePhone"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.IsApproved = Convert.ToBoolean(dt_Members.Rows[0]["IsApproved"]);
                objMembers.IsLockedOut = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.MemberAmount = (dt_Members.Rows[0]["MemberAmount"] != DBNull.Value ? Convert.ToDecimal(dt_Members.Rows[0]["MemberAmount"]) : 0);
                objMembers.IsActivated = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.DateActivated = (dt_Members.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["DateActivated"]) : DateTime.MinValue);
                objMembers.MembershipTypeId = Convert.ToInt64(dt_Members.Rows[0]["MembershipTypeId"]);
                objMembers.MembershipType = dt_Members.Rows[0]["MembershipType"].ToString();
                objMembers.IsVolunteer = (dt_Members.Rows[0]["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsVolunteer"]) : false);
                objMembers.IsTeluguorigin = (dt_Members.Rows[0]["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsTeluguorigin"]) : false);
                objMembers.Comments = (dt_Members.Rows[0]["Comments"] != DBNull.Value ? dt_Members.Rows[0]["Comments"].ToString() : null);
                objMembers.ReferredBy = (dt_Members.Rows[0]["ReferredBy"] != DBNull.Value ? dt_Members.Rows[0]["ReferredBy"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.InsertedTime = Convert.ToDateTime(dt_Members.Rows[0]["InsertedTime"]);
                objMembers.UpdatedTime = Convert.ToDateTime(dt_Members.Rows[0]["UpdatedTime"]);
                objMembers.Fax = (dt_Members.Rows[0]["Fax"] != DBNull.Value ? dt_Members.Rows[0]["Fax"].ToString() : null);
                objMembers.WebsiteAddress = (dt_Members.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt_Members.Rows[0]["WebsiteAddress"].ToString() : null);
                objMembers.Address2 = (dt_Members.Rows[0]["Address2"] != DBNull.Value ? dt_Members.Rows[0]["Address2"].ToString() : null);
            }

            if (dt_ChildrenInfo.Rows.Count != 0)
            {

                foreach (DataRow dr in dt_ChildrenInfo.Rows)
                {
                    Entities.ChildrenInfo objChildrenInfo = new Entities.ChildrenInfo();

                    objChildrenInfo.ChildrenInfoId = Convert.ToInt64(dr["ChildrenInfoId"]);
                    objChildrenInfo.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objChildrenInfo.FirstName = dr["FirstName"].ToString();
                    objChildrenInfo.LastName = dr["LastName"].ToString();
                    objChildrenInfo.Age = (dr["Age"] != DBNull.Value ?Convert.ToInt32(dr["Age"].ToString()) : 0);
                    objChildrenInfo.Relationship = dr["Relationship"].ToString();

                    lstChildrenInfo.Add(objChildrenInfo);
                }
            }

            objMembers.lstChildrenInfo = lstChildrenInfo;

            if (dt_MembershipOrder.Rows.Count == 1)
            {
                objMembers.objMembershipOrder.MembershipOrderId = Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipOrderId"]);
                objMembers.objMembershipOrder.MemberId = (dt_MembershipOrder.Rows[0]["MemberId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MemberId"]) : 0);
                objMembers.objMembershipOrder.MembershipTypeId = (dt_MembershipOrder.Rows[0]["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipTypeId"]) : 0);
                objMembers.objMembershipOrder.Amount = (dt_MembershipOrder.Rows[0]["Amount"] != DBNull.Value ? Convert.ToDecimal(dt_MembershipOrder.Rows[0]["Amount"]) : 0);
                objMembers.objMembershipOrder.TransactionId = (dt_MembershipOrder.Rows[0]["TransactionId"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["TransactionId"].ToString() : null);
                objMembers.objMembershipOrder.PaymentStatusId = (dt_MembershipOrder.Rows[0]["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentStatusId"]) : 0);
                objMembers.objMembershipOrder.PaymentMethodId = (dt_MembershipOrder.Rows[0]["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentMethodId"]) : 0);
                objMembers.objMembershipOrder.PaymentBy = (dt_MembershipOrder.Rows[0]["PaymentBy"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentBy"].ToString() : null);
                objMembers.objMembershipOrder.AdminComment = (dt_MembershipOrder.Rows[0]["AdminComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["AdminComment"].ToString() : null);
                objMembers.objMembershipOrder.PaymentStatus = (dt_MembershipOrder.Rows[0]["PaymentStatus"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentStatus"].ToString() : null);
                objMembers.objMembershipOrder.PaymentMethod = (dt_MembershipOrder.Rows[0]["PaymentMethod"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentMethod"].ToString() : null);
                objMembers.objMembershipOrder.MembershipType = (dt_MembershipOrder.Rows[0]["MembershipType"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["MembershipType"].ToString() : null);
                objMembers.objMembershipOrder.UserComment = (dt_MembershipOrder.Rows[0]["UserComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["UserComment"].ToString() : null);
                objMembers.objMembershipOrder.OrderDate = (dt_MembershipOrder.Rows[0]["OrderDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["OrderDate"]) : DateTime.MinValue);
                objMembers.objMembershipOrder.ExpiryDate = (dt_MembershipOrder.Rows[0]["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["ExpiryDate"]) : DateTime.MinValue);
                objMembers.objMembershipOrder.ChequeDate = (dt_MembershipOrder.Rows[0]["ChequeDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["ChequeDate"]) : DateTime.MinValue);
                objMembers.objMembershipOrder.ChequeNo = dt_MembershipOrder.Rows[0]["ChequeNo"].ToString();
                objMembers.objMembershipOrder.BankName = dt_MembershipOrder.Rows[0]["BankName"].ToString();
                objMembers.objMembershipOrder.UpdatedTime = Convert.ToDateTime(dt_MembershipOrder.Rows[0]["UpdatedTime"]);
                objMembers.objMembershipOrder.UpdatedBy = dt_MembershipOrder.Rows[0]["UpdatedBy"].ToString();

            }

            objMembers.lstMembershipOrder = lstMembershipOrder;

            return objMembers;
        }

        public Entities.Members GetMemberFullDetailsBySpouseCell(string SpouseCell, string LastName, ref int status)
        {
            DataSet ds0 = _Members1.GetMemberFullDetailsBySpouseCell(SpouseCell, LastName, ref status);

            DataTable dt_Members = ds0.Tables[0];
            DataTable dt_ChildrenInfo = ds0.Tables[1];
            DataTable dt_MembershipOrder = ds0.Tables[2];


            Entities.Members objMembers = new Entities.Members();
            List<Entities.ChildrenInfo> lstChildrenInfo = new List<Entities.ChildrenInfo>();
            List<Entities.MembershipOrders> lstMembershipOrder = new List<Entities.MembershipOrders>();

            if (dt_Members.Rows.Count == 1)
            {

                objMembers.MemberId = Convert.ToInt64(dt_Members.Rows[0]["MemberId"]);
                objMembers.UserName = dt_Members.Rows[0]["UserName"].ToString();
                objMembers.Email = dt_Members.Rows[0]["Email"].ToString();
                objMembers.FirstName = dt_Members.Rows[0]["FirstName"].ToString();
                objMembers.LastName = dt_Members.Rows[0]["LastName"].ToString();
                objMembers.ProfileImage = (dt_Members.Rows[0]["ProfileImage"] != DBNull.Value ? dt_Members.Rows[0]["ProfileImage"].ToString() : null);
                objMembers.Occupation = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["Occupation"].ToString() : null);
                objMembers.MemberAge = (dt_Members.Rows[0]["MemberAge"] != DBNull.Value ? dt_Members.Rows[0]["MemberAge"].ToString() : null);
                objMembers.MemberSkils = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["MemberSkils"].ToString() : null);
                objMembers.SpouseSkils = (dt_Members.Rows[0]["SpouseSkils"] != DBNull.Value ? dt_Members.Rows[0]["SpouseSkils"].ToString() : null);
                objMembers.SpouseFirstName = (dt_Members.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseFirstName"].ToString() : null);
                objMembers.SpouseLastName = (dt_Members.Rows[0]["SpouseLastName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseLastName"].ToString() : null);
                objMembers.SpouseOccupation = (dt_Members.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseOccupation"].ToString() : null);
                objMembers.SpouseEmail = (dt_Members.Rows[0]["SpouseEmail"] != DBNull.Value ? dt_Members.Rows[0]["SpouseEmail"].ToString() : null);
                objMembers.SpouseCell = (dt_Members.Rows[0]["SpouseCell"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCell"].ToString() : null);
                objMembers.Address = (dt_Members.Rows[0]["Address"] != DBNull.Value ? dt_Members.Rows[0]["Address"].ToString() : null);
                objMembers.City = (dt_Members.Rows[0]["City"] != DBNull.Value ? dt_Members.Rows[0]["City"].ToString() : null);
                objMembers.State = (dt_Members.Rows[0]["State"] != DBNull.Value ? dt_Members.Rows[0]["State"].ToString() : null);
                objMembers.ZipCode = (dt_Members.Rows[0]["ZipCode"] != DBNull.Value ? dt_Members.Rows[0]["ZipCode"].ToString() : null);
                objMembers.HomePhone = (dt_Members.Rows[0]["HomePhone"] != DBNull.Value ? dt_Members.Rows[0]["HomePhone"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.IsApproved = Convert.ToBoolean(dt_Members.Rows[0]["IsApproved"]);
                objMembers.IsLockedOut = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.MemberAmount = (dt_Members.Rows[0]["MemberAmount"] != DBNull.Value ? Convert.ToDecimal(dt_Members.Rows[0]["MemberAmount"]) : 0);
                objMembers.IsActivated = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.DateActivated = (dt_Members.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["DateActivated"]) : DateTime.MinValue);
                objMembers.MembershipTypeId = Convert.ToInt64(dt_Members.Rows[0]["MembershipTypeId"]);
                objMembers.MembershipType = dt_Members.Rows[0]["MembershipType"].ToString();
                objMembers.IsVolunteer = (dt_Members.Rows[0]["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsVolunteer"]) : false);
                objMembers.IsTeluguorigin = (dt_Members.Rows[0]["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsTeluguorigin"]) : false);
                objMembers.Comments = (dt_Members.Rows[0]["Comments"] != DBNull.Value ? dt_Members.Rows[0]["Comments"].ToString() : null);
                objMembers.ReferredBy = (dt_Members.Rows[0]["ReferredBy"] != DBNull.Value ? dt_Members.Rows[0]["ReferredBy"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.InsertedTime = Convert.ToDateTime(dt_Members.Rows[0]["InsertedTime"]);
                objMembers.UpdatedTime = Convert.ToDateTime(dt_Members.Rows[0]["UpdatedTime"]);
                objMembers.Fax = (dt_Members.Rows[0]["Fax"] != DBNull.Value ? dt_Members.Rows[0]["Fax"].ToString() : null);
                objMembers.WebsiteAddress = (dt_Members.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt_Members.Rows[0]["WebsiteAddress"].ToString() : null);
                objMembers.Address2 = (dt_Members.Rows[0]["Address2"] != DBNull.Value ? dt_Members.Rows[0]["Address2"].ToString() : null);
            }

            if (dt_ChildrenInfo.Rows.Count != 0)
            {

                foreach (DataRow dr in dt_ChildrenInfo.Rows)
                {
                    Entities.ChildrenInfo objChildrenInfo = new Entities.ChildrenInfo();

                    objChildrenInfo.ChildrenInfoId = Convert.ToInt64(dr["ChildrenInfoId"]);
                    objChildrenInfo.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objChildrenInfo.FirstName = dr["FirstName"].ToString();
                    objChildrenInfo.LastName = dr["LastName"].ToString();
                    objChildrenInfo.Age = (dr["Age"] != DBNull.Value ? Convert.ToInt32(dr["Age"].ToString()) : 0);
                    objChildrenInfo.Relationship = dr["Relationship"].ToString();

                    lstChildrenInfo.Add(objChildrenInfo);
                }
            }

            objMembers.lstChildrenInfo = lstChildrenInfo;

            if (dt_MembershipOrder.Rows.Count == 1)
            {
                objMembers.objMembershipOrder.MembershipOrderId = Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipOrderId"]);


            }

            objMembers.lstMembershipOrder = lstMembershipOrder;

            return objMembers;
        }

        public Entities.Members GetMemberFullDetailsBySpouse(string SpouseCell, ref int status)
        {
            DataSet ds0 = _Members1.GetMemberFullDetailsBySpouse(SpouseCell, ref status);

            DataTable dt_Members = ds0.Tables[0];
            DataTable dt_ChildrenInfo = ds0.Tables[1];
            DataTable dt_MembershipOrder = ds0.Tables[2];


            Entities.Members objMembers = new Entities.Members();
            List<Entities.ChildrenInfo> lstChildrenInfo = new List<Entities.ChildrenInfo>();
            List<Entities.MembershipOrders> lstMembershipOrder = new List<Entities.MembershipOrders>();

            if (dt_Members.Rows.Count == 1)
            {

                objMembers.MemberId = Convert.ToInt64(dt_Members.Rows[0]["MemberId"]);
                objMembers.UserName = dt_Members.Rows[0]["UserName"].ToString();
                objMembers.Email = dt_Members.Rows[0]["Email"].ToString();
                objMembers.FirstName = dt_Members.Rows[0]["FirstName"].ToString();
                objMembers.LastName = dt_Members.Rows[0]["LastName"].ToString();
                objMembers.ProfileImage = (dt_Members.Rows[0]["ProfileImage"] != DBNull.Value ? dt_Members.Rows[0]["ProfileImage"].ToString() : null);
                objMembers.Occupation = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["Occupation"].ToString() : null);
                objMembers.MemberAge = (dt_Members.Rows[0]["MemberAge"] != DBNull.Value ? dt_Members.Rows[0]["MemberAge"].ToString() : null);
                objMembers.MemberSkils = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["MemberSkils"].ToString() : null);
                objMembers.SpouseSkils = (dt_Members.Rows[0]["SpouseSkils"] != DBNull.Value ? dt_Members.Rows[0]["SpouseSkils"].ToString() : null);
                objMembers.SpouseFirstName = (dt_Members.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseFirstName"].ToString() : null);
                objMembers.SpouseLastName = (dt_Members.Rows[0]["SpouseLastName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseLastName"].ToString() : null);
                objMembers.SpouseOccupation = (dt_Members.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseOccupation"].ToString() : null);
                objMembers.SpouseEmail = (dt_Members.Rows[0]["SpouseEmail"] != DBNull.Value ? dt_Members.Rows[0]["SpouseEmail"].ToString() : null);
                objMembers.SpouseCell = (dt_Members.Rows[0]["SpouseCell"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCell"].ToString() : null);
                objMembers.Address = (dt_Members.Rows[0]["Address"] != DBNull.Value ? dt_Members.Rows[0]["Address"].ToString() : null);
                objMembers.City = (dt_Members.Rows[0]["City"] != DBNull.Value ? dt_Members.Rows[0]["City"].ToString() : null);
                objMembers.State = (dt_Members.Rows[0]["State"] != DBNull.Value ? dt_Members.Rows[0]["State"].ToString() : null);
                objMembers.ZipCode = (dt_Members.Rows[0]["ZipCode"] != DBNull.Value ? dt_Members.Rows[0]["ZipCode"].ToString() : null);
                objMembers.HomePhone = (dt_Members.Rows[0]["HomePhone"] != DBNull.Value ? dt_Members.Rows[0]["HomePhone"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.IsApproved = Convert.ToBoolean(dt_Members.Rows[0]["IsApproved"]);
                objMembers.IsLockedOut = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.MemberAmount = (dt_Members.Rows[0]["MemberAmount"] != DBNull.Value ? Convert.ToDecimal(dt_Members.Rows[0]["MemberAmount"]) : 0);
                objMembers.IsActivated = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.DateActivated = (dt_Members.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["DateActivated"]) : DateTime.MinValue);
                objMembers.MembershipTypeId = Convert.ToInt64(dt_Members.Rows[0]["MembershipTypeId"]);
                objMembers.MembershipType = dt_Members.Rows[0]["MembershipType"].ToString();
                objMembers.IsVolunteer = (dt_Members.Rows[0]["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsVolunteer"]) : false);
                objMembers.IsTeluguorigin = (dt_Members.Rows[0]["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsTeluguorigin"]) : false);
                objMembers.Comments = (dt_Members.Rows[0]["Comments"] != DBNull.Value ? dt_Members.Rows[0]["Comments"].ToString() : null);
                objMembers.ReferredBy = (dt_Members.Rows[0]["ReferredBy"] != DBNull.Value ? dt_Members.Rows[0]["ReferredBy"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.InsertedTime = Convert.ToDateTime(dt_Members.Rows[0]["InsertedTime"]);
                objMembers.UpdatedTime = Convert.ToDateTime(dt_Members.Rows[0]["UpdatedTime"]);
                objMembers.Fax = (dt_Members.Rows[0]["Fax"] != DBNull.Value ? dt_Members.Rows[0]["Fax"].ToString() : null);
                objMembers.WebsiteAddress = (dt_Members.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt_Members.Rows[0]["WebsiteAddress"].ToString() : null);
                objMembers.Address2 = (dt_Members.Rows[0]["Address2"] != DBNull.Value ? dt_Members.Rows[0]["Address2"].ToString() : null);
            }

            if (dt_ChildrenInfo.Rows.Count != 0)
            {

                foreach (DataRow dr in dt_ChildrenInfo.Rows)
                {
                    Entities.ChildrenInfo objChildrenInfo = new Entities.ChildrenInfo();

                    objChildrenInfo.ChildrenInfoId = Convert.ToInt64(dr["ChildrenInfoId"]);
                    objChildrenInfo.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objChildrenInfo.FirstName = dr["FirstName"].ToString();
                    objChildrenInfo.LastName = dr["LastName"].ToString();
                    objChildrenInfo.Age = (dr["Age"] != DBNull.Value ? Convert.ToInt32(dr["Age"].ToString()) : 0);
                    objChildrenInfo.Relationship = dr["Relationship"].ToString();

                    lstChildrenInfo.Add(objChildrenInfo);
                }
            }

            objMembers.lstChildrenInfo = lstChildrenInfo;

            if (dt_MembershipOrder.Rows.Count == 1)
            {
                objMembers.objMembershipOrder.MembershipOrderId = Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipOrderId"]);


            }

            objMembers.lstMembershipOrder = lstMembershipOrder;

            return objMembers;
        }

        public Entities.Members GetMemberFullDetailsByLastName(string LastName, ref int status)
        {
            DataSet ds0 = _Members1.GetMemberFullDetailsByLastName(LastName, ref status);

            DataTable dt_Members = ds0.Tables[0];
            DataTable dt_ChildrenInfo = ds0.Tables[1];
            DataTable dt_MembershipOrder = ds0.Tables[2];


            Entities.Members objMembers = new Entities.Members();
            List<Entities.ChildrenInfo> lstChildrenInfo = new List<Entities.ChildrenInfo>();
            List<Entities.MembershipOrders> lstMembershipOrder = new List<Entities.MembershipOrders>();

            if (dt_Members.Rows.Count == 1)
            {

                objMembers.MemberId = Convert.ToInt64(dt_Members.Rows[0]["MemberId"]);
                objMembers.UserName = dt_Members.Rows[0]["UserName"].ToString();
                objMembers.Email = dt_Members.Rows[0]["Email"].ToString();
                objMembers.FirstName = dt_Members.Rows[0]["FirstName"].ToString();
                objMembers.LastName = dt_Members.Rows[0]["LastName"].ToString();
                objMembers.ProfileImage = (dt_Members.Rows[0]["ProfileImage"] != DBNull.Value ? dt_Members.Rows[0]["ProfileImage"].ToString() : null);
                objMembers.Occupation = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["Occupation"].ToString() : null);
                objMembers.MemberAge = (dt_Members.Rows[0]["MemberAge"] != DBNull.Value ? dt_Members.Rows[0]["MemberAge"].ToString() : null);
                objMembers.MemberSkils = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["MemberSkils"].ToString() : null);
                objMembers.SpouseSkils = (dt_Members.Rows[0]["SpouseSkils"] != DBNull.Value ? dt_Members.Rows[0]["SpouseSkils"].ToString() : null);
                objMembers.SpouseFirstName = (dt_Members.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseFirstName"].ToString() : null);
                objMembers.SpouseLastName = (dt_Members.Rows[0]["SpouseLastName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseLastName"].ToString() : null);
                objMembers.SpouseOccupation = (dt_Members.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseOccupation"].ToString() : null);
                objMembers.SpouseEmail = (dt_Members.Rows[0]["SpouseEmail"] != DBNull.Value ? dt_Members.Rows[0]["SpouseEmail"].ToString() : null);
                objMembers.SpouseCell = (dt_Members.Rows[0]["SpouseCell"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCell"].ToString() : null);
                objMembers.Address = (dt_Members.Rows[0]["Address"] != DBNull.Value ? dt_Members.Rows[0]["Address"].ToString() : null);
                objMembers.City = (dt_Members.Rows[0]["City"] != DBNull.Value ? dt_Members.Rows[0]["City"].ToString() : null);
                objMembers.State = (dt_Members.Rows[0]["State"] != DBNull.Value ? dt_Members.Rows[0]["State"].ToString() : null);
                objMembers.ZipCode = (dt_Members.Rows[0]["ZipCode"] != DBNull.Value ? dt_Members.Rows[0]["ZipCode"].ToString() : null);
                objMembers.HomePhone = (dt_Members.Rows[0]["HomePhone"] != DBNull.Value ? dt_Members.Rows[0]["HomePhone"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.IsApproved = Convert.ToBoolean(dt_Members.Rows[0]["IsApproved"]);
                objMembers.IsLockedOut = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.MemberAmount = (dt_Members.Rows[0]["MemberAmount"] != DBNull.Value ? Convert.ToDecimal(dt_Members.Rows[0]["MemberAmount"]) : 0);
                objMembers.IsActivated = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.DateActivated = (dt_Members.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["DateActivated"]) : DateTime.MinValue);
                objMembers.MembershipTypeId = Convert.ToInt64(dt_Members.Rows[0]["MembershipTypeId"]);
                objMembers.MembershipType = dt_Members.Rows[0]["MembershipType"].ToString();
                objMembers.IsVolunteer = (dt_Members.Rows[0]["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsVolunteer"]) : false);
                objMembers.IsTeluguorigin = (dt_Members.Rows[0]["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsTeluguorigin"]) : false);
                objMembers.Comments = (dt_Members.Rows[0]["Comments"] != DBNull.Value ? dt_Members.Rows[0]["Comments"].ToString() : null);
                objMembers.ReferredBy = (dt_Members.Rows[0]["ReferredBy"] != DBNull.Value ? dt_Members.Rows[0]["ReferredBy"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.InsertedTime = Convert.ToDateTime(dt_Members.Rows[0]["InsertedTime"]);
                objMembers.UpdatedTime = Convert.ToDateTime(dt_Members.Rows[0]["UpdatedTime"]);
                objMembers.Fax = (dt_Members.Rows[0]["Fax"] != DBNull.Value ? dt_Members.Rows[0]["Fax"].ToString() : null);
                objMembers.WebsiteAddress = (dt_Members.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt_Members.Rows[0]["WebsiteAddress"].ToString() : null);
                objMembers.Address2 = (dt_Members.Rows[0]["Address2"] != DBNull.Value ? dt_Members.Rows[0]["Address2"].ToString() : null);
            }

            if (dt_ChildrenInfo.Rows.Count != 0)
            {

                foreach (DataRow dr in dt_ChildrenInfo.Rows)
                {
                    Entities.ChildrenInfo objChildrenInfo = new Entities.ChildrenInfo();

                    objChildrenInfo.ChildrenInfoId = Convert.ToInt64(dr["ChildrenInfoId"]);
                    objChildrenInfo.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objChildrenInfo.FirstName = dr["FirstName"].ToString();
                    objChildrenInfo.LastName = dr["LastName"].ToString();
                    objChildrenInfo.Age = (dr["Age"] != DBNull.Value ? Convert.ToInt32(dr["Age"].ToString()) : 0);
                    objChildrenInfo.Relationship = dr["Relationship"].ToString();

                    lstChildrenInfo.Add(objChildrenInfo);
                }
            }

            objMembers.lstChildrenInfo = lstChildrenInfo;

            if (dt_MembershipOrder.Rows.Count == 1)
            {
                objMembers.objMembershipOrder.MembershipOrderId = Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipOrderId"]);


            }

            objMembers.lstMembershipOrder = lstMembershipOrder;

            return objMembers;
        }

        public Entities.MembershipOrders GetMemberOrderById(Int64 MemberOrderId, ref int status)
        {
            DataTable dt_MembershipOrder = _Members1.GetMemberOrderById(MemberOrderId, ref status);

            Entities.MembershipOrders objMembershipOrder = new Entities.MembershipOrders();
           
            if (dt_MembershipOrder.Rows.Count == 1)
            {
                objMembershipOrder.MembershipOrderId = Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipOrderId"]);
                objMembershipOrder.MemberId = (dt_MembershipOrder.Rows[0]["MemberId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MemberId"]) : 0);
                objMembershipOrder.MembershipTypeId = (dt_MembershipOrder.Rows[0]["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipTypeId"]) : 0);
                objMembershipOrder.Amount = (dt_MembershipOrder.Rows[0]["Amount"] != DBNull.Value ? Convert.ToDecimal(dt_MembershipOrder.Rows[0]["Amount"]) : 0);
                objMembershipOrder.TransactionId = (dt_MembershipOrder.Rows[0]["TransactionId"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["TransactionId"].ToString() : null);
                objMembershipOrder.PaymentStatusId = (dt_MembershipOrder.Rows[0]["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentStatusId"]) : 0);
                objMembershipOrder.PaymentMethodId = (dt_MembershipOrder.Rows[0]["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentMethodId"]) : 0);
                objMembershipOrder.PaymentBy = (dt_MembershipOrder.Rows[0]["PaymentBy"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentBy"].ToString() : null);
                objMembershipOrder.AdminComment = (dt_MembershipOrder.Rows[0]["AdminComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["AdminComment"].ToString() : null);
                objMembershipOrder.PaymentStatus = (dt_MembershipOrder.Rows[0]["PaymentStatus"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentStatus"].ToString() : null);
                objMembershipOrder.PaymentMethod = (dt_MembershipOrder.Rows[0]["PaymentMethod"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentMethod"].ToString() : null);
                objMembershipOrder.MembershipType = (dt_MembershipOrder.Rows[0]["MembershipType"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["MembershipType"].ToString() : null);
                objMembershipOrder.UserComment = (dt_MembershipOrder.Rows[0]["UserComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["UserComment"].ToString() : null);
                objMembershipOrder.OrderDate = (dt_MembershipOrder.Rows[0]["OrderDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["OrderDate"]) : DateTime.MinValue);
                objMembershipOrder.ExpiryDate = (dt_MembershipOrder.Rows[0]["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["ExpiryDate"]) : DateTime.MinValue);
                objMembershipOrder.BankName = (dt_MembershipOrder.Rows[0]["BankName"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["BankName"].ToString() : null);
                objMembershipOrder.ChequeNo = (dt_MembershipOrder.Rows[0]["ChequeNo"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["ChequeNo"].ToString() : null);
                objMembershipOrder.ChequeDate = (dt_MembershipOrder.Rows[0]["ChequeDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["ChequeDate"]) : DateTime.MinValue);
                objMembershipOrder.UpdatedTime = Convert.ToDateTime(dt_MembershipOrder.Rows[0]["UpdatedTime"]);
                objMembershipOrder.UpdatedBy = dt_MembershipOrder.Rows[0]["UpdatedBy"].ToString();

            }
            return objMembershipOrder;
        }

        public Entities.Members GetMemberFullDetailsByUserName(string UserName, ref int status)
        {
            DataSet ds0 = _Members1.GetMemberFullDetailsByUserName(UserName, ref status);

            DataTable dt_Members = ds0.Tables[0];
            DataTable dt_ChildrenInfo = ds0.Tables[1];
            DataTable dt_MembershipOrder = ds0.Tables[2];


            Entities.Members objMembers = new Entities.Members();
            List<Entities.ChildrenInfo> lstChildrenInfo = new List<Entities.ChildrenInfo>();
            List<Entities.MembershipOrders> lstMembershipOrder = new List<Entities.MembershipOrders>();

            if (dt_Members.Rows.Count == 1)
            {

                objMembers.MemberId = Convert.ToInt64(dt_Members.Rows[0]["MemberId"]);
                objMembers.UserName = dt_Members.Rows[0]["UserName"].ToString();
                objMembers.Email = dt_Members.Rows[0]["Email"].ToString();
                objMembers.FirstName = dt_Members.Rows[0]["FirstName"].ToString();
                objMembers.LastName = dt_Members.Rows[0]["LastName"].ToString();
                objMembers.ProfileImage = (dt_Members.Rows[0]["ProfileImage"] != DBNull.Value ? dt_Members.Rows[0]["ProfileImage"].ToString() : null);
                objMembers.Occupation = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["Occupation"].ToString() : null);
                objMembers.SpouseFirstName = (dt_Members.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseFirstName"].ToString() : null);
                objMembers.SpouseLastName = (dt_Members.Rows[0]["SpouseLastName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseLastName"].ToString() : null);
                objMembers.SpouseOccupation = (dt_Members.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseOccupation"].ToString() : null);
                objMembers.SpouseEmail = (dt_Members.Rows[0]["SpouseEmail"] != DBNull.Value ? dt_Members.Rows[0]["SpouseEmail"].ToString() : null);
                objMembers.SpouseCell = (dt_Members.Rows[0]["SpouseCell"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCell"].ToString() : null);
                objMembers.Address = (dt_Members.Rows[0]["Address"] != DBNull.Value ? dt_Members.Rows[0]["Address"].ToString() : null);
                objMembers.City = (dt_Members.Rows[0]["City"] != DBNull.Value ? dt_Members.Rows[0]["City"].ToString() : null);
                objMembers.State = (dt_Members.Rows[0]["State"] != DBNull.Value ? dt_Members.Rows[0]["State"].ToString() : null);
                objMembers.ZipCode = (dt_Members.Rows[0]["ZipCode"] != DBNull.Value ? dt_Members.Rows[0]["ZipCode"].ToString() : null);
                objMembers.HomePhone = (dt_Members.Rows[0]["HomePhone"] != DBNull.Value ? dt_Members.Rows[0]["HomePhone"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.IsApproved = Convert.ToBoolean(dt_Members.Rows[0]["IsApproved"]);
                objMembers.IsLockedOut = Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]);
                objMembers.IsActivated = Convert.ToBoolean(dt_Members.Rows[0]["IsActivated"]);
                objMembers.DateActivated = (dt_Members.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["DateActivated"]) : DateTime.MinValue);
                objMembers.MembershipTypeId = Convert.ToInt64(dt_Members.Rows[0]["MembershipTypeId"]);
                objMembers.MembershipType = dt_Members.Rows[0]["MembershipType"].ToString();
                objMembers.IsVolunteer = (dt_Members.Rows[0]["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsVolunteer"].ToString()):false);
                objMembers.MemberAmount = (dt_Members.Rows[0]["MemberAmount"] != DBNull.Value ? Convert.ToDecimal(dt_Members.Rows[0]["MemberAmount"]) : 0);
                objMembers.IsTeluguorigin = Convert.ToBoolean(dt_Members.Rows[0]["IsTeluguorigin"]);
                objMembers.Comments = (dt_Members.Rows[0]["Comments"] != DBNull.Value ? dt_Members.Rows[0]["Comments"].ToString() : null);
                objMembers.ReferredBy = (dt_Members.Rows[0]["ReferredBy"] != DBNull.Value ? dt_Members.Rows[0]["ReferredBy"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.InsertedTime = Convert.ToDateTime(dt_Members.Rows[0]["InsertedTime"]);
                objMembers.UpdatedTime = Convert.ToDateTime(dt_Members.Rows[0]["UpdatedTime"]);
                objMembers.Fax = (dt_Members.Rows[0]["Fax"] != DBNull.Value ? dt_Members.Rows[0]["Fax"].ToString() : null);
                objMembers.WebsiteAddress = (dt_Members.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt_Members.Rows[0]["WebsiteAddress"].ToString() : null);
                objMembers.Address2 = (dt_Members.Rows[0]["Address2"] != DBNull.Value ? dt_Members.Rows[0]["Address2"].ToString() : null);
            }

            if (dt_ChildrenInfo.Rows.Count != 0)
            {

                foreach (DataRow dr in dt_ChildrenInfo.Rows)
                {
                    Entities.ChildrenInfo objChildrenInfo = new Entities.ChildrenInfo();

                    objChildrenInfo.ChildrenInfoId = Convert.ToInt64(dr["ChildrenInfoId"]);
                    objChildrenInfo.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objChildrenInfo.FirstName = dr["FirstName"].ToString();
                    objChildrenInfo.LastName = dr["LastName"].ToString();
                    objChildrenInfo.Age = (dr["Age"] != DBNull.Value ? Convert.ToInt32(dr["Age"].ToString()) : 0);
                    objChildrenInfo.Relationship = dr["Relationship"].ToString();

                    lstChildrenInfo.Add(objChildrenInfo);
                }
            }

            objMembers.lstChildrenInfo = lstChildrenInfo;

            if (dt_MembershipOrder.Rows.Count == 1)
            {
                objMembers.objMembershipOrder.MembershipOrderId = Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipOrderId"]);
                objMembers.objMembershipOrder.MemberId = (dt_MembershipOrder.Rows[0]["MemberId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MemberId"]) : 0);
                objMembers.objMembershipOrder.MembershipTypeId = (dt_MembershipOrder.Rows[0]["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipTypeId"]) : 0);
                objMembers.objMembershipOrder.Amount = (dt_MembershipOrder.Rows[0]["Amount"] != DBNull.Value ? Convert.ToDecimal(dt_MembershipOrder.Rows[0]["Amount"]) : 0);
                objMembers.objMembershipOrder.TransactionId = (dt_MembershipOrder.Rows[0]["TransactionId"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["TransactionId"].ToString() : null);
                objMembers.objMembershipOrder.PaymentStatusId = (dt_MembershipOrder.Rows[0]["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentStatusId"]) : 0);
                objMembers.objMembershipOrder.PaymentMethodId = (dt_MembershipOrder.Rows[0]["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentMethodId"]) : 0);
                objMembers.objMembershipOrder.PaymentBy = (dt_MembershipOrder.Rows[0]["PaymentBy"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentBy"].ToString() : null);
                objMembers.objMembershipOrder.AdminComment = (dt_MembershipOrder.Rows[0]["AdminComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["AdminComment"].ToString() : null);
                objMembers.objMembershipOrder.PaymentStatus = (dt_MembershipOrder.Rows[0]["PaymentStatus"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentStatus"].ToString() : null);
                objMembers.objMembershipOrder.PaymentMethod = (dt_MembershipOrder.Rows[0]["PaymentMethod"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentMethod"].ToString() : null);
                objMembers.objMembershipOrder.MembershipType = (dt_MembershipOrder.Rows[0]["MembershipType"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["MembershipType"].ToString() : null);
                objMembers.objMembershipOrder.UserComment = (dt_MembershipOrder.Rows[0]["UserComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["UserComment"].ToString() : null);
                objMembers.objMembershipOrder.OrderDate = (dt_MembershipOrder.Rows[0]["OrderDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["OrderDate"]) : DateTime.MinValue);
                objMembers.objMembershipOrder.ExpiryDate = (dt_MembershipOrder.Rows[0]["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["ExpiryDate"]) : DateTime.MinValue);
                objMembers.objMembershipOrder.UpdatedTime = Convert.ToDateTime(dt_MembershipOrder.Rows[0]["UpdatedTime"]);
                objMembers.objMembershipOrder.UpdatedBy = dt_MembershipOrder.Rows[0]["UpdatedBy"].ToString();

            }

            return objMembers;
        }

        public Entities.Members GetMemberFullDetailsByEmail(string Email, ref int status)
        {
            DataSet ds0 = _Members1.GetMemberFullDetailsByEmail(Email, ref status);

            DataTable dt_Members = ds0.Tables[0];
            DataTable dt_ChildrenInfo = ds0.Tables[1];
            DataTable dt_MembershipOrder = ds0.Tables[2];


            Entities.Members objMembers = new Entities.Members();
            List<Entities.ChildrenInfo> lstChildrenInfo = new List<Entities.ChildrenInfo>();
            List<Entities.MembershipOrders> lstMembershipOrder = new List<Entities.MembershipOrders>();

            if (dt_Members.Rows.Count == 1)
            {

                objMembers.MemberId = Convert.ToInt64(dt_Members.Rows[0]["MemberId"]);
                objMembers.UserName = dt_Members.Rows[0]["UserName"].ToString();
                objMembers.Email = dt_Members.Rows[0]["Email"].ToString();
                objMembers.FirstName = dt_Members.Rows[0]["FirstName"].ToString();
                objMembers.LastName = dt_Members.Rows[0]["LastName"].ToString();
                objMembers.ProfileImage = (dt_Members.Rows[0]["ProfileImage"] != DBNull.Value ? dt_Members.Rows[0]["ProfileImage"].ToString() : null);
                objMembers.Occupation = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["Occupation"].ToString() : null);
                objMembers.MemberAge = (dt_Members.Rows[0]["MemberAge"] != DBNull.Value ? dt_Members.Rows[0]["MemberAge"].ToString() : null);
                objMembers.MemberSkils = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["MemberSkils"].ToString() : null);
                objMembers.SpouseSkils = (dt_Members.Rows[0]["SpouseSkils"] != DBNull.Value ? dt_Members.Rows[0]["SpouseSkils"].ToString() : null);
                objMembers.SpouseFirstName = (dt_Members.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseFirstName"].ToString() : null);
                objMembers.SpouseLastName = (dt_Members.Rows[0]["SpouseLastName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseLastName"].ToString() : null);
                objMembers.SpouseOccupation = (dt_Members.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseOccupation"].ToString() : null);
                objMembers.SpouseEmail = (dt_Members.Rows[0]["SpouseEmail"] != DBNull.Value ? dt_Members.Rows[0]["SpouseEmail"].ToString() : null);
                objMembers.SpouseCell = (dt_Members.Rows[0]["SpouseCell"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCell"].ToString() : null);
                objMembers.Address = (dt_Members.Rows[0]["Address"] != DBNull.Value ? dt_Members.Rows[0]["Address"].ToString() : null);
                objMembers.City = (dt_Members.Rows[0]["City"] != DBNull.Value ? dt_Members.Rows[0]["City"].ToString() : null);
                objMembers.State = (dt_Members.Rows[0]["State"] != DBNull.Value ? dt_Members.Rows[0]["State"].ToString() : null);
                objMembers.ZipCode = (dt_Members.Rows[0]["ZipCode"] != DBNull.Value ? dt_Members.Rows[0]["ZipCode"].ToString() : null);
                objMembers.HomePhone = (dt_Members.Rows[0]["HomePhone"] != DBNull.Value ? dt_Members.Rows[0]["HomePhone"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.IsApproved = Convert.ToBoolean(dt_Members.Rows[0]["IsApproved"]);
                objMembers.IsLockedOut = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.IsActivated = (dt_Members.Rows[0]["IsActivated"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsActivated"]) : false);
                objMembers.DateActivated = (dt_Members.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["DateActivated"]) : DateTime.MinValue);
                objMembers.MembershipTypeId = (dt_Members.Rows[0]["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dt_Members.Rows[0]["MembershipTypeId"]) : 0);
                objMembers.MembershipType = dt_Members.Rows[0]["MembershipType"].ToString();
                objMembers.IsVolunteer = (dt_Members.Rows[0]["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsVolunteer"]) : false);
                objMembers.MemberAmount = (dt_Members.Rows[0]["MemberAmount"] != DBNull.Value ? Convert.ToDecimal(dt_Members.Rows[0]["MemberAmount"]) : 0);
                objMembers.IsTeluguorigin = (dt_Members.Rows[0]["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsTeluguorigin"]) : false);
                objMembers.Comments = (dt_Members.Rows[0]["Comments"] != DBNull.Value ? dt_Members.Rows[0]["Comments"].ToString() : null);
                objMembers.ReferredBy = (dt_Members.Rows[0]["ReferredBy"] != DBNull.Value ? dt_Members.Rows[0]["ReferredBy"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.InsertedTime = Convert.ToDateTime(dt_Members.Rows[0]["InsertedTime"]);
                objMembers.UpdatedTime = Convert.ToDateTime(dt_Members.Rows[0]["UpdatedTime"]);
                objMembers.Fax = (dt_Members.Rows[0]["Fax"] != DBNull.Value ? dt_Members.Rows[0]["Fax"].ToString() : null);
                objMembers.WebsiteAddress = (dt_Members.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt_Members.Rows[0]["WebsiteAddress"].ToString() : null);
                objMembers.Address2 = (dt_Members.Rows[0]["Address2"] != DBNull.Value ? dt_Members.Rows[0]["Address2"].ToString() : null);
                objMembers.ExpiryDate = (dt_Members.Rows[0]["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["ExpiryDate"]) : DateTime.MinValue);
            }

            if (dt_ChildrenInfo.Rows.Count != 0)
            {

                foreach (DataRow dr in dt_ChildrenInfo.Rows)
                {
                    Entities.ChildrenInfo objChildrenInfo = new Entities.ChildrenInfo();

                    objChildrenInfo.ChildrenInfoId = Convert.ToInt64(dr["ChildrenInfoId"]);
                    objChildrenInfo.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objChildrenInfo.FirstName = dr["FirstName"].ToString();
                    objChildrenInfo.LastName = dr["LastName"].ToString();
                    objChildrenInfo.Age = (dr["Age"] != DBNull.Value ? Convert.ToInt32(dr["Age"].ToString()) : 0);
                    objChildrenInfo.Relationship = dr["Relationship"].ToString();

                    lstChildrenInfo.Add(objChildrenInfo);
                }
            }

            objMembers.lstChildrenInfo = lstChildrenInfo;

            if (dt_MembershipOrder.Rows.Count == 1)
            {
                objMembers.objMembershipOrder.MembershipOrderId = Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipOrderId"]);
                objMembers.objMembershipOrder.MemberId = (dt_MembershipOrder.Rows[0]["MemberId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MemberId"]) : 0);
                objMembers.objMembershipOrder.MembershipTypeId = (dt_MembershipOrder.Rows[0]["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipTypeId"]) : 0);
                objMembers.objMembershipOrder.Amount = (dt_MembershipOrder.Rows[0]["Amount"] != DBNull.Value ? Convert.ToDecimal(dt_MembershipOrder.Rows[0]["Amount"]) : 0);
                objMembers.objMembershipOrder.TransactionId = (dt_MembershipOrder.Rows[0]["TransactionId"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["TransactionId"].ToString() : null);
                objMembers.objMembershipOrder.PaymentStatusId = (dt_MembershipOrder.Rows[0]["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentStatusId"]) : 0);
                objMembers.objMembershipOrder.PaymentMethodId = (dt_MembershipOrder.Rows[0]["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentMethodId"]) : 0);
                objMembers.objMembershipOrder.PaymentBy = (dt_MembershipOrder.Rows[0]["PaymentBy"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentBy"].ToString() : null);
                objMembers.objMembershipOrder.AdminComment = (dt_MembershipOrder.Rows[0]["AdminComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["AdminComment"].ToString() : null);
                objMembers.objMembershipOrder.PaymentStatus = (dt_MembershipOrder.Rows[0]["PaymentStatus"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentStatus"].ToString() : null);
                objMembers.objMembershipOrder.PaymentMethod = (dt_MembershipOrder.Rows[0]["PaymentMethod"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentMethod"].ToString() : null);
                objMembers.objMembershipOrder.MembershipType = (dt_MembershipOrder.Rows[0]["MembershipType"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["MembershipType"].ToString() : null);
                objMembers.objMembershipOrder.UserComment = (dt_MembershipOrder.Rows[0]["UserComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["UserComment"].ToString() : null);
                objMembers.objMembershipOrder.OrderDate = (dt_MembershipOrder.Rows[0]["OrderDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["OrderDate"]) : DateTime.MinValue);
                objMembers.objMembershipOrder.ExpiryDate = (dt_MembershipOrder.Rows[0]["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["ExpiryDate"]) : DateTime.MinValue);
                objMembers.objMembershipOrder.Expiry = (dt_MembershipOrder.Rows[0]["Expiry"] != DBNull.Value ? Convert.ToInt32(dt_MembershipOrder.Rows[0]["Expiry"]) : 0);
                objMembers.objMembershipOrder.UpdatedTime = Convert.ToDateTime(dt_MembershipOrder.Rows[0]["UpdatedTime"]);
                objMembers.objMembershipOrder.UpdatedBy = dt_MembershipOrder.Rows[0]["UpdatedBy"].ToString();

            }

            objMembers.lstMembershipOrder = lstMembershipOrder;

            return objMembers;
        }

        public Entities.Members GetMemberFullDetailsByPhoneNo(string MobilePhone, ref int status)
        {
            DataSet ds0 = _Members1.GetMemberFullDetailsByPhoneNo(MobilePhone, ref status);

            DataTable dt_Members = ds0.Tables[0];
            DataTable dt_ChildrenInfo = ds0.Tables[1];
            DataTable dt_MembershipOrder = ds0.Tables[2];


            Entities.Members objMembers = new Entities.Members();
            List<Entities.ChildrenInfo> lstChildrenInfo = new List<Entities.ChildrenInfo>();
            List<Entities.MembershipOrders> lstMembershipOrder = new List<Entities.MembershipOrders>();

            if (dt_Members.Rows.Count == 1)
            {

                objMembers.MemberId = Convert.ToInt64(dt_Members.Rows[0]["MemberId"]);
                objMembers.UserName = dt_Members.Rows[0]["UserName"].ToString();
                objMembers.Email = dt_Members.Rows[0]["Email"].ToString();
                objMembers.FirstName = dt_Members.Rows[0]["FirstName"].ToString();
                objMembers.LastName = dt_Members.Rows[0]["LastName"].ToString();
                objMembers.ProfileImage = (dt_Members.Rows[0]["ProfileImage"] != DBNull.Value ? dt_Members.Rows[0]["ProfileImage"].ToString() : null);
                objMembers.Occupation = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["Occupation"].ToString() : null);
                objMembers.MemberAge = (dt_Members.Rows[0]["MemberAge"] != DBNull.Value ? dt_Members.Rows[0]["MemberAge"].ToString() : null);
                objMembers.MemberSkils = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["MemberSkils"].ToString() : null);
                objMembers.SpouseSkils = (dt_Members.Rows[0]["SpouseSkils"] != DBNull.Value ? dt_Members.Rows[0]["SpouseSkils"].ToString() : null);
                objMembers.SpouseFirstName = (dt_Members.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseFirstName"].ToString() : null);
                objMembers.SpouseLastName = (dt_Members.Rows[0]["SpouseLastName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseLastName"].ToString() : null);
                objMembers.SpouseOccupation = (dt_Members.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseOccupation"].ToString() : null);
                objMembers.SpouseEmail = (dt_Members.Rows[0]["SpouseEmail"] != DBNull.Value ? dt_Members.Rows[0]["SpouseEmail"].ToString() : null);
                objMembers.SpouseCell = (dt_Members.Rows[0]["SpouseCell"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCell"].ToString() : null);
                objMembers.Address = (dt_Members.Rows[0]["Address"] != DBNull.Value ? dt_Members.Rows[0]["Address"].ToString() : null);
                objMembers.City = (dt_Members.Rows[0]["City"] != DBNull.Value ? dt_Members.Rows[0]["City"].ToString() : null);
                objMembers.State = (dt_Members.Rows[0]["State"] != DBNull.Value ? dt_Members.Rows[0]["State"].ToString() : null);
                objMembers.ZipCode = (dt_Members.Rows[0]["ZipCode"] != DBNull.Value ? dt_Members.Rows[0]["ZipCode"].ToString() : null);
                objMembers.HomePhone = (dt_Members.Rows[0]["HomePhone"] != DBNull.Value ? dt_Members.Rows[0]["HomePhone"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.IsApproved = Convert.ToBoolean(dt_Members.Rows[0]["IsApproved"]);
                objMembers.IsLockedOut = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.IsActivated = (dt_Members.Rows[0]["IsActivated"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsActivated"]) : false);
                objMembers.DateActivated = (dt_Members.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["DateActivated"]) : DateTime.MinValue);
                objMembers.MembershipTypeId = (dt_Members.Rows[0]["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dt_Members.Rows[0]["MembershipTypeId"]) : 0);
                objMembers.MembershipType = dt_Members.Rows[0]["MembershipType"].ToString();
                objMembers.IsVolunteer = (dt_Members.Rows[0]["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsVolunteer"]) : false);
                objMembers.MemberAmount = (dt_Members.Rows[0]["MemberAmount"] != DBNull.Value ? Convert.ToDecimal(dt_Members.Rows[0]["MemberAmount"]) : 0);
                objMembers.IsTeluguorigin = (dt_Members.Rows[0]["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsTeluguorigin"]) : false);
                objMembers.Comments = (dt_Members.Rows[0]["Comments"] != DBNull.Value ? dt_Members.Rows[0]["Comments"].ToString() : null);
                objMembers.ReferredBy = (dt_Members.Rows[0]["ReferredBy"] != DBNull.Value ? dt_Members.Rows[0]["ReferredBy"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.InsertedTime = Convert.ToDateTime(dt_Members.Rows[0]["InsertedTime"]);
                objMembers.UpdatedTime = Convert.ToDateTime(dt_Members.Rows[0]["UpdatedTime"]);
                objMembers.Fax = (dt_Members.Rows[0]["Fax"] != DBNull.Value ? dt_Members.Rows[0]["Fax"].ToString() : null);
                objMembers.WebsiteAddress = (dt_Members.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt_Members.Rows[0]["WebsiteAddress"].ToString() : null);
                objMembers.Address2 = (dt_Members.Rows[0]["Address2"] != DBNull.Value ? dt_Members.Rows[0]["Address2"].ToString() : null);
                objMembers.ExpiryDate = (dt_Members.Rows[0]["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["ExpiryDate"]) : DateTime.MinValue);
            }

            if (dt_ChildrenInfo.Rows.Count != 0)
            {

                foreach (DataRow dr in dt_ChildrenInfo.Rows)
                {
                    Entities.ChildrenInfo objChildrenInfo = new Entities.ChildrenInfo();

                    objChildrenInfo.ChildrenInfoId = Convert.ToInt64(dr["ChildrenInfoId"]);
                    objChildrenInfo.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objChildrenInfo.FirstName = dr["FirstName"].ToString();
                    objChildrenInfo.LastName = dr["LastName"].ToString();
                    objChildrenInfo.Age = (dr["Age"] != DBNull.Value ? Convert.ToInt32(dr["Age"].ToString()) : 0);
                    objChildrenInfo.Relationship = dr["Relationship"].ToString();

                    lstChildrenInfo.Add(objChildrenInfo);
                }
            }

            objMembers.lstChildrenInfo = lstChildrenInfo;

            if (dt_MembershipOrder.Rows.Count == 1)
            {
                objMembers.objMembershipOrder.MembershipOrderId = Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipOrderId"]);
                objMembers.objMembershipOrder.MemberId = (dt_MembershipOrder.Rows[0]["MemberId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MemberId"]) : 0);
                objMembers.objMembershipOrder.MembershipTypeId = (dt_MembershipOrder.Rows[0]["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipTypeId"]) : 0);
                objMembers.objMembershipOrder.Amount = (dt_MembershipOrder.Rows[0]["Amount"] != DBNull.Value ? Convert.ToDecimal(dt_MembershipOrder.Rows[0]["Amount"]) : 0);
                objMembers.objMembershipOrder.TransactionId = (dt_MembershipOrder.Rows[0]["TransactionId"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["TransactionId"].ToString() : null);
                objMembers.objMembershipOrder.PaymentStatusId = (dt_MembershipOrder.Rows[0]["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentStatusId"]) : 0);
                objMembers.objMembershipOrder.PaymentMethodId = (dt_MembershipOrder.Rows[0]["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentMethodId"]) : 0);
                objMembers.objMembershipOrder.PaymentBy = (dt_MembershipOrder.Rows[0]["PaymentBy"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentBy"].ToString() : null);
                objMembers.objMembershipOrder.AdminComment = (dt_MembershipOrder.Rows[0]["AdminComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["AdminComment"].ToString() : null);
                objMembers.objMembershipOrder.PaymentStatus = (dt_MembershipOrder.Rows[0]["PaymentStatus"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentStatus"].ToString() : null);
                objMembers.objMembershipOrder.PaymentMethod = (dt_MembershipOrder.Rows[0]["PaymentMethod"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentMethod"].ToString() : null);
                objMembers.objMembershipOrder.MembershipType = (dt_MembershipOrder.Rows[0]["MembershipType"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["MembershipType"].ToString() : null);
                objMembers.objMembershipOrder.UserComment = (dt_MembershipOrder.Rows[0]["UserComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["UserComment"].ToString() : null);
                objMembers.objMembershipOrder.OrderDate = (dt_MembershipOrder.Rows[0]["OrderDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["OrderDate"]) : DateTime.MinValue);
                objMembers.objMembershipOrder.ExpiryDate = (dt_MembershipOrder.Rows[0]["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["ExpiryDate"]) : DateTime.MinValue);
                objMembers.objMembershipOrder.Expiry = (dt_MembershipOrder.Rows[0]["Expiry"] != DBNull.Value ? Convert.ToInt32(dt_MembershipOrder.Rows[0]["Expiry"]) : 0);
                objMembers.objMembershipOrder.UpdatedTime = Convert.ToDateTime(dt_MembershipOrder.Rows[0]["UpdatedTime"]);
                objMembers.objMembershipOrder.UpdatedBy = dt_MembershipOrder.Rows[0]["UpdatedBy"].ToString();

            }

            objMembers.lstMembershipOrder = lstMembershipOrder;

            return objMembers;
        }

        public Entities.Members FEGetMemberFullDetailsByEmail(string Email, ref int status)
        {
            DataSet ds0 = _Members1.FEGetMemberFullDetailsByEmail(Email, ref status);

            DataTable dt_Members = ds0.Tables[0];
            DataTable dt_ChildrenInfo = ds0.Tables[1];
            DataTable dt_MembershipOrder = ds0.Tables[2];


            Entities.Members objMembers = new Entities.Members();
            List<Entities.ChildrenInfo> lstChildrenInfo = new List<Entities.ChildrenInfo>();
            List<Entities.MembershipOrders> lstMembershipOrder = new List<Entities.MembershipOrders>();

            if (dt_Members.Rows.Count == 1)
            {

                objMembers.MemberId = Convert.ToInt64(dt_Members.Rows[0]["MemberId"]);
                objMembers.UserName = dt_Members.Rows[0]["UserName"].ToString();
                objMembers.Email = dt_Members.Rows[0]["Email"].ToString();
                objMembers.FirstName = dt_Members.Rows[0]["FirstName"].ToString();
                objMembers.LastName = dt_Members.Rows[0]["LastName"].ToString();
                objMembers.ProfileImage = (dt_Members.Rows[0]["ProfileImage"] != DBNull.Value ? dt_Members.Rows[0]["ProfileImage"].ToString() : null);
                objMembers.Occupation = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["Occupation"].ToString() : null);
                objMembers.MemberAge = (dt_Members.Rows[0]["MemberAge"] != DBNull.Value ? dt_Members.Rows[0]["MemberAge"].ToString() : null);
                objMembers.MemberSkils = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["MemberSkils"].ToString() : null);
                objMembers.SpouseSkils = (dt_Members.Rows[0]["SpouseSkils"] != DBNull.Value ? dt_Members.Rows[0]["SpouseSkils"].ToString() : null);
                objMembers.SpouseFirstName = (dt_Members.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseFirstName"].ToString() : null);
                objMembers.SpouseLastName = (dt_Members.Rows[0]["SpouseLastName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseLastName"].ToString() : null);
                objMembers.SpouseOccupation = (dt_Members.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseOccupation"].ToString() : null);
                objMembers.SpouseEmail = (dt_Members.Rows[0]["SpouseEmail"] != DBNull.Value ? dt_Members.Rows[0]["SpouseEmail"].ToString() : null);
                objMembers.SpouseCell = (dt_Members.Rows[0]["SpouseCell"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCell"].ToString() : null);
                objMembers.Address = (dt_Members.Rows[0]["Address"] != DBNull.Value ? dt_Members.Rows[0]["Address"].ToString() : null);
                objMembers.City = (dt_Members.Rows[0]["City"] != DBNull.Value ? dt_Members.Rows[0]["City"].ToString() : null);
                objMembers.State = (dt_Members.Rows[0]["State"] != DBNull.Value ? dt_Members.Rows[0]["State"].ToString() : null);
                objMembers.ZipCode = (dt_Members.Rows[0]["ZipCode"] != DBNull.Value ? dt_Members.Rows[0]["ZipCode"].ToString() : null);
                objMembers.HomePhone = (dt_Members.Rows[0]["HomePhone"] != DBNull.Value ? dt_Members.Rows[0]["HomePhone"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.IsApproved = Convert.ToBoolean(dt_Members.Rows[0]["IsApproved"]);
                objMembers.IsLockedOut = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.IsActivated = (dt_Members.Rows[0]["IsActivated"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsActivated"]) : false);
                objMembers.DateActivated = (dt_Members.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["DateActivated"]) : DateTime.MinValue);
                objMembers.MembershipTypeId = (dt_Members.Rows[0]["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dt_Members.Rows[0]["MembershipTypeId"]) : 0);
                objMembers.MembershipType = dt_Members.Rows[0]["MembershipType"].ToString();
                objMembers.IsVolunteer = (dt_Members.Rows[0]["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsVolunteer"]) : false);
                objMembers.MemberAmount = (dt_Members.Rows[0]["MemberAmount"] != DBNull.Value ? Convert.ToDecimal(dt_Members.Rows[0]["MemberAmount"]) : 0);
                objMembers.IsTeluguorigin = (dt_Members.Rows[0]["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsTeluguorigin"]) : false);
                objMembers.Comments = (dt_Members.Rows[0]["Comments"] != DBNull.Value ? dt_Members.Rows[0]["Comments"].ToString() : null);
                objMembers.ReferredBy = (dt_Members.Rows[0]["ReferredBy"] != DBNull.Value ? dt_Members.Rows[0]["ReferredBy"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.InsertedTime = Convert.ToDateTime(dt_Members.Rows[0]["InsertedTime"]);
                objMembers.UpdatedTime = Convert.ToDateTime(dt_Members.Rows[0]["UpdatedTime"]);
                objMembers.Fax = (dt_Members.Rows[0]["Fax"] != DBNull.Value ? dt_Members.Rows[0]["Fax"].ToString() : null);
                objMembers.WebsiteAddress = (dt_Members.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt_Members.Rows[0]["WebsiteAddress"].ToString() : null);
                objMembers.Address2 = (dt_Members.Rows[0]["Address2"] != DBNull.Value ? dt_Members.Rows[0]["Address2"].ToString() : null);
                objMembers.ExpiryDate = (dt_Members.Rows[0]["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["ExpiryDate"]) : DateTime.MinValue);
            }

            if (dt_ChildrenInfo.Rows.Count != 0)
            {

                foreach (DataRow dr in dt_ChildrenInfo.Rows)
                {
                    Entities.ChildrenInfo objChildrenInfo = new Entities.ChildrenInfo();

                    objChildrenInfo.ChildrenInfoId = Convert.ToInt64(dr["ChildrenInfoId"]);
                    objChildrenInfo.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objChildrenInfo.FirstName = dr["FirstName"].ToString();
                    objChildrenInfo.LastName = dr["LastName"].ToString();
                    objChildrenInfo.Age = (dr["Age"] != DBNull.Value ? Convert.ToInt32(dr["Age"].ToString()) : 0);
                    objChildrenInfo.Relationship = dr["Relationship"].ToString();

                    lstChildrenInfo.Add(objChildrenInfo);
                }
            }

            objMembers.lstChildrenInfo = lstChildrenInfo;

            if (dt_MembershipOrder.Rows.Count == 1)
            {
                objMembers.objMembershipOrder.MembershipOrderId = Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipOrderId"]);
                objMembers.objMembershipOrder.MemberId = (dt_MembershipOrder.Rows[0]["MemberId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MemberId"]) : 0);
                objMembers.objMembershipOrder.MembershipTypeId = (dt_MembershipOrder.Rows[0]["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipTypeId"]) : 0);
                objMembers.objMembershipOrder.Amount = (dt_MembershipOrder.Rows[0]["Amount"] != DBNull.Value ? Convert.ToDecimal(dt_MembershipOrder.Rows[0]["Amount"]) : 0);
                objMembers.objMembershipOrder.TransactionId = (dt_MembershipOrder.Rows[0]["TransactionId"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["TransactionId"].ToString() : null);
                objMembers.objMembershipOrder.PaymentStatusId = (dt_MembershipOrder.Rows[0]["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentStatusId"]) : 0);
                objMembers.objMembershipOrder.PaymentMethodId = (dt_MembershipOrder.Rows[0]["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentMethodId"]) : 0);
                objMembers.objMembershipOrder.PaymentBy = (dt_MembershipOrder.Rows[0]["PaymentBy"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentBy"].ToString() : null);
                objMembers.objMembershipOrder.AdminComment = (dt_MembershipOrder.Rows[0]["AdminComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["AdminComment"].ToString() : null);
                objMembers.objMembershipOrder.PaymentStatus = (dt_MembershipOrder.Rows[0]["PaymentStatus"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentStatus"].ToString() : null);
                objMembers.objMembershipOrder.PaymentMethod = (dt_MembershipOrder.Rows[0]["PaymentMethod"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentMethod"].ToString() : null);
                objMembers.objMembershipOrder.MembershipType = (dt_MembershipOrder.Rows[0]["MembershipType"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["MembershipType"].ToString() : null);
                objMembers.objMembershipOrder.UserComment = (dt_MembershipOrder.Rows[0]["UserComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["UserComment"].ToString() : null);
                objMembers.objMembershipOrder.OrderDate = (dt_MembershipOrder.Rows[0]["OrderDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["OrderDate"]) : DateTime.MinValue);
                objMembers.objMembershipOrder.ExpiryDate = (dt_MembershipOrder.Rows[0]["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["ExpiryDate"]) : DateTime.MinValue);
                objMembers.objMembershipOrder.Expiry = (dt_MembershipOrder.Rows[0]["Expiry"] != DBNull.Value ? Convert.ToInt32(dt_MembershipOrder.Rows[0]["Expiry"]) : 0);
                objMembers.objMembershipOrder.UpdatedTime = Convert.ToDateTime(dt_MembershipOrder.Rows[0]["UpdatedTime"]);
                objMembers.objMembershipOrder.UpdatedBy = dt_MembershipOrder.Rows[0]["UpdatedBy"].ToString();

            }

            objMembers.lstMembershipOrder = lstMembershipOrder;

            return objMembers;
        }

        public Entities.Members AddMembershipRequirement(ref int status)
        {
            DataSet ds = _Members.AddMembershipRequirement(ref status);
            Entities.Members objMembers = new Entities.Members();
            List<Entities.PaymentMethods> lstPaymentMethod = new List<Entities.PaymentMethods>();
            List<Entities.MembershipTypes> lstMembershipType = new List<Entities.MembershipTypes>();
            List<Entities.PaymentStatus> lstPaymentStatus = new List<Entities.PaymentStatus>();

            if (ds.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Entities.MembershipTypes objMembershipType = new Entities.MembershipTypes();

                    objMembershipType.MembershipTypeId = Convert.ToInt64(dr["MembershipTypeId"]);                   
                    objMembershipType.MembershipType = dr["MembershipType"].ToString();
                    objMembershipType.Price = Convert.ToDecimal(dr["Price"]);
                    lstMembershipType.Add(objMembershipType);
                }
            }
            objMembers.lstMembershipType = lstMembershipType;

            if (ds.Tables[1].Rows.Count != 0)
            {

                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    Entities.PaymentStatus objPaymentStatus = new Entities.PaymentStatus();

                    objPaymentStatus.PaymentStatusId = Convert.ToInt64(dr["PaymentStatusId"]);
                    objPaymentStatus.PaymentStatus1 = dr["PaymentStatus"].ToString();

                    lstPaymentStatus.Add(objPaymentStatus);
                }
            }
            objMembers.lstPaymentStatus = lstPaymentStatus;

            if (ds.Tables[2].Rows.Count != 0)
            {

                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    Entities.PaymentMethods objPaymentMethod = new Entities.PaymentMethods();

                    objPaymentMethod.PaymentMethodId = Convert.ToInt64(dr["PaymentMethodId"]);
                    objPaymentMethod.PaymentMethod = dr["PaymentMethod"].ToString();

                    lstPaymentMethod.Add(objPaymentMethod);
                }
            }
            objMembers.lstPaymentMethod = lstPaymentMethod;

            return objMembers;
        }

        public List<Entities.Members> FEGetMembersListByVariable( Int64 MembershipTypeId, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = _Members.FEGetMembersListByVariable(MembershipTypeId, Search, Sort, PageNo, Items, ref Total);
            List<Entities.Members> lstMembers = new List<Entities.Members>();

            if (dt.Rows.Count == 0 && PageNo > 1)
            {
                dt = _Members.FEGetMembersListByVariable(MembershipTypeId, Search, Sort, PageNo, Items, ref Total);
            }

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.Members objMembers = new Entities.Members();

                    objMembers.RId = Convert.ToInt64(dr["Rid"]);
                    objMembers.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objMembers.UserName = dr["UserName"].ToString();
                    objMembers.Email = dr["Email"].ToString();
                    objMembers.FirstName = dr["FirstName"].ToString();
                    objMembers.LastName = dr["LastName"].ToString();
                    objMembers.ProfileImage = (dr["ProfileImage"] != DBNull.Value ? dr["ProfileImage"].ToString() : null);
                    objMembers.Occupation = (dr["Occupation"] != DBNull.Value ? dr["Occupation"].ToString() : null);
                    objMembers.SpouseFirstName = (dr["SpouseFirstName"] != DBNull.Value ? dr["SpouseFirstName"].ToString() : null);
                    objMembers.SpouseLastName = (dr["SpouseLastName"] != DBNull.Value ? dr["SpouseLastName"].ToString() : null);
                    objMembers.SpouseOccupation = (dr["SpouseOccupation"] != DBNull.Value ? dr["SpouseOccupation"].ToString() : null);
                    objMembers.SpouseEmail = (dr["SpouseEmail"] != DBNull.Value ? dr["SpouseEmail"].ToString() : null);
                    objMembers.SpouseCell = (dr["SpouseCell"] != DBNull.Value ? dr["SpouseCell"].ToString() : null);
                    objMembers.Address = (dr["Address"] != DBNull.Value ? dr["Address"].ToString() : null);
                    objMembers.City = (dr["City"] != DBNull.Value ? dr["City"].ToString() : null);
                    objMembers.State = (dr["State"] != DBNull.Value ? dr["State"].ToString() : null);
                    objMembers.ZipCode = (dr["ZipCode"] != DBNull.Value ? dr["ZipCode"].ToString() : null);
                    objMembers.HomePhone = (dr["HomePhone"] != DBNull.Value ? dr["HomePhone"].ToString() : null);
                    objMembers.MobilePhone = (dr["MobilePhone"] != DBNull.Value ? dr["MobilePhone"].ToString() : null);
                    objMembers.IsApproved = Convert.ToBoolean(dr["IsApproved"]);
                    objMembers.IsLockedOut = (dr["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dr["IsLockedOut"]) : false);
                    objMembers.IsActivated = (dr["IsActivated"] != DBNull.Value ? Convert.ToBoolean(dr["IsActivated"]) : false);
                    objMembers.DateActivated = (dr["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dr["DateActivated"]) : DateTime.MinValue);
                    objMembers.MembershipTypeId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembers.MembershipType = dr["MembershipType"].ToString();
                    objMembers.IsVolunteer = (dr["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dr["IsVolunteer"]) : false);
                    objMembers.IsTeluguorigin = (dr["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dr["IsTeluguorigin"]) : false);
                    objMembers.Comments = (dr["Comments"] != DBNull.Value ? dr["Comments"].ToString() : null);
                    objMembers.ReferredBy = (dr["ReferredBy"] != DBNull.Value ? dr["ReferredBy"].ToString() : null);
                    objMembers.MobilePhone = (dr["MobilePhone"] != DBNull.Value ? dr["MobilePhone"].ToString() : null);
                    objMembers.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);
                    objMembers.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
                    objMembers.Amount = (dr["Amount"] != DBNull.Value ? Convert.ToDecimal(dr["Amount"]) : 0);
                    objMembers.objMembershipOrder.TransactionId = (dr["TransactionId"] != DBNull.Value ? dr["TransactionId"].ToString() : null);
                    objMembers.objMembershipOrder.MembershipTypeId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembers.objMembershipOrder.PaymentStatusId = (dr["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentStatusId"]) : 0);
                    objMembers.objMembershipOrder.PaymentMethodId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembers.objMembershipOrder.MembershipTypeId = (dr["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentMethodId"]) : 0);
                    objMembers.objMembershipOrder.AdminComment = (dr["AdminComment"] != DBNull.Value ? dr["AdminComment"].ToString() : null);
                    objMembers.objMembershipOrder.UserComment = (dr["UserComment"] != DBNull.Value ? dr["UserComment"].ToString() : null);
                    objMembers.PaymentStatus = (dr["PaymentStatus"] != DBNull.Value ? dr["PaymentStatus"].ToString() : null);
                    objMembers.objMembershipOrder.PaymentMethod = (dr["PaymentMethod"] != DBNull.Value ? dr["PaymentMethod"].ToString() : null);
                    objMembers.objMembershipOrder.OrderDate = (dr["OrderDate"] != DBNull.Value ? Convert.ToDateTime(dr["OrderDate"]) : DateTime.MinValue);
                    objMembers.objMembershipOrder.ExpiryDate = (dr["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dr["ExpiryDate"]) : DateTime.MinValue);
                    objMembers.Fax = (dr["Fax"] != DBNull.Value ? dr["Fax"].ToString() : null);
                    objMembers.WebsiteAddress = (dr["WebsiteAddress"] != DBNull.Value ? dr["WebsiteAddress"].ToString() : null);
                    objMembers.Address2 = (dr["Address2"] != DBNull.Value ? dr["Address2"].ToString() : null);
                    lstMembers.Add(objMembers);
                }
            }
            return lstMembers;
        }

        public Entities.Members FEMemberGetFullDetails(Int64 MemberId, ref int status)
        {
            DataTable dt_Members = _Members1.FEMemberGetFullDetails(MemberId, ref status);

            Entities.Members objMembers = new Entities.Members();
            if (dt_Members.Rows.Count == 1)
            {

                objMembers.MemberId = Convert.ToInt64(dt_Members.Rows[0]["MemberId"]);
                objMembers.UserName = dt_Members.Rows[0]["UserName"].ToString();
                objMembers.Email = dt_Members.Rows[0]["Email"].ToString();
                objMembers.FirstName = dt_Members.Rows[0]["FirstName"].ToString();
                objMembers.LastName = dt_Members.Rows[0]["LastName"].ToString();
                objMembers.ProfileImage = (dt_Members.Rows[0]["ProfileImage"] != DBNull.Value ? dt_Members.Rows[0]["ProfileImage"].ToString() : null);
                objMembers.Occupation = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["Occupation"].ToString() : null);
                objMembers.MemberAge = (dt_Members.Rows[0]["MemberAge"] != DBNull.Value ? dt_Members.Rows[0]["MemberAge"].ToString() : null);
                objMembers.MemberSkils = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["MemberSkils"].ToString() : null);
                objMembers.SpouseSkils = (dt_Members.Rows[0]["SpouseSkils"] != DBNull.Value ? dt_Members.Rows[0]["SpouseSkils"].ToString() : null);
                objMembers.SpouseFirstName = (dt_Members.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseFirstName"].ToString() : null);
                objMembers.SpouseLastName = (dt_Members.Rows[0]["SpouseLastName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseLastName"].ToString() : null);
                objMembers.SpouseOccupation = (dt_Members.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseOccupation"].ToString() : null);
                objMembers.SpouseEmail = (dt_Members.Rows[0]["SpouseEmail"] != DBNull.Value ? dt_Members.Rows[0]["SpouseEmail"].ToString() : null);
                objMembers.SpouseCell = (dt_Members.Rows[0]["SpouseCell"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCell"].ToString() : null);
                objMembers.Address = (dt_Members.Rows[0]["Address"] != DBNull.Value ? dt_Members.Rows[0]["Address"].ToString() : null);
                objMembers.City = (dt_Members.Rows[0]["City"] != DBNull.Value ? dt_Members.Rows[0]["City"].ToString() : null);
                objMembers.State = (dt_Members.Rows[0]["State"] != DBNull.Value ? dt_Members.Rows[0]["State"].ToString() : null);
                objMembers.ZipCode = (dt_Members.Rows[0]["ZipCode"] != DBNull.Value ? dt_Members.Rows[0]["ZipCode"].ToString() : null);
                objMembers.HomePhone = (dt_Members.Rows[0]["HomePhone"] != DBNull.Value ? dt_Members.Rows[0]["HomePhone"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.IsApproved = Convert.ToBoolean(dt_Members.Rows[0]["IsApproved"]);
                objMembers.IsLockedOut = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.IsActivated = (dt_Members.Rows[0]["IsActivated"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsActivated"]) : false);
                objMembers.DateActivated = (dt_Members.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["DateActivated"]) : DateTime.MinValue);
                objMembers.MembershipTypeId = (dt_Members.Rows[0]["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dt_Members.Rows[0]["MembershipTypeId"]) : 0);
                objMembers.IsVolunteer = (dt_Members.Rows[0]["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsVolunteer"]) : false);
                objMembers.IsTeluguorigin = (dt_Members.Rows[0]["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsTeluguorigin"]) : false);
                objMembers.Comments = (dt_Members.Rows[0]["Comments"] != DBNull.Value ? dt_Members.Rows[0]["Comments"].ToString() : null);
                objMembers.ReferredBy = (dt_Members.Rows[0]["ReferredBy"] != DBNull.Value ? dt_Members.Rows[0]["ReferredBy"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.InsertedTime = Convert.ToDateTime(dt_Members.Rows[0]["InsertedTime"]);
                objMembers.UpdatedTime = Convert.ToDateTime(dt_Members.Rows[0]["UpdatedTime"]);
                objMembers.Fax = (dt_Members.Rows[0]["Fax"] != DBNull.Value ? dt_Members.Rows[0]["Fax"].ToString() : null);
                objMembers.WebsiteAddress = (dt_Members.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt_Members.Rows[0]["WebsiteAddress"].ToString() : null);
                objMembers.Address2 = (dt_Members.Rows[0]["Address2"] != DBNull.Value ? dt_Members.Rows[0]["Address2"].ToString() : null);
            }

            return objMembers;
        }

        #endregion
    }
}
