using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TCAssociationTool.BLL
{
    public class Users
    {
        TCAssociationTool.DAL.Users _users = new TCAssociationTool.DAL.Users();

        #region Methods

        public Int64 InsertUserProfile(TCAssociationTool.Entities.Users objUser)
        {
            Int64 _status = 0;
            if (objUser != null)
            {
                _status = _users.InsertUserProfile(objUser);
            }
            return _status;
        }

        public Int64 UpdateUserAccess(TCAssociationTool.Entities.UserRoles objUser)
        {
            Int64 _status = 0;
            if (objUser != null)
            {
                _status = _users.UpdateUserAccess(objUser);
            }
            return _status;
        }

        public Int64 DeleteUser(Int64 UserId)
        {
            Int64 _status = 0;
            if (UserId != 0)
            {
                _status = _users.DeleteUser(UserId);
            }
            return _status;
        }

        public Int64 DeleteAllUser()
        {
            Int64 _status = 0;
              _status = _users.DeleteAllUser();            
            return _status;
        }

        public Int64 UpdateUserStatus(Int64 UserId)
        {
            Int64 _status = 0;
            if (UserId != 0)
            {
                _status = _users.UpdateUserStatus(UserId);
            }
            return _status;
        }

        public Int64 UnlockUser(Int64 UserId)
        {
            Int64 _status = 0;
            if (UserId != 0)
            {
                _status = _users.UnlockUser(UserId);
            }
            return _status;
        }

        public Int64 ChangePassword(Int64 UserId, string Password)
        {
            Int64 _status = 0;
            if (UserId != 0 && Password != null && Password.Trim() != "")
            {
                _status = _users.ChangePassword(UserId, Password);
            }
            return _status;
        }

        public string GetPassword(Int64 _userid, ref int _qstatus)
        {
            string _password = "";
            DataTable dt = _users.GetPassword(_userid, ref _qstatus);
            if (dt.Rows.Count == 1)
            {
                _password = dt.Rows[0]["Password"].ToString();
            }
            return _password;
        }

        public Int64 UpdateRegistrationGUID(Int64 UserId, string IsActivated, Guid RegistrationGUID)
        {
            Int64 _status = 0;
            if (UserId != 0)
            {
                _status = _users.UpdateRegistrationGUID(UserId, IsActivated, RegistrationGUID);
            }
            return _status;
        }

        public Int64 UpdateUser(TCAssociationTool.Entities.Users objUser)
        {
            Int64 _status = 0;
            if (objUser != null && objUser.UserId != 0)
            {
                _status = _users.UpdateUser(objUser);
            }
            return _status;
        }

        public Int64 UpdateUserProfileImage(Int64 UserId, ref string ProfileImage)
        {
            Int64 _status = 0;
            if (UserId != 0)
            {
                _status = _users.UpdateUserProfileImage(UserId, ref ProfileImage);
            }
            return _status;
        }

        #endregion

        #region Entities filling

        public List<TCAssociationTool.Entities.Roles> GetUserRolesList(ref int Total)
        {
            List<TCAssociationTool.Entities.Roles> lstRoles = new List<TCAssociationTool.Entities.Roles>();
            DataTable dt = _users.GetUserRolesList(ref Total);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Roles objRoles = new TCAssociationTool.Entities.Roles();
                    objRoles.RoleId = Convert.ToInt64(dr["RoleId"].ToString());
                    objRoles.RoleName =dr["RoleName"].ToString();
                    lstRoles.Add(objRoles);
                }

            }
            return lstRoles;
        }

        public List<TCAssociationTool.Entities.Roles> GetUserRolesListById(Int64 UserId, ref int Total)
        {
            List<TCAssociationTool.Entities.Roles> lstRoles = new List<TCAssociationTool.Entities.Roles>();
            DataTable dt = _users.GetUserRolesListById(UserId,ref Total);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Roles objRoles = new TCAssociationTool.Entities.Roles();
                    objRoles.RoleId = Convert.ToInt64(dr["RoleId"].ToString());
                    objRoles.RoleName = dr["RoleName"].ToString();
                    lstRoles.Add(objRoles);
                }

            }
            return lstRoles;
        }

        public Entities.Users GetUserByUserName(string UserName, ref int status)
        {
            TCAssociationTool.Entities.Users _objuser = new TCAssociationTool.Entities.Users();
            DataTable dt = new DataTable();

            if (UserName != null && UserName.Trim() != "")
            {
                dt = _users.GetUserByUserName(UserName, ref status);
                if (dt.Rows.Count == 1)
                {
                    _objuser.UserId = Convert.ToInt64(dt.Rows[0]["UserId"]);
                    _objuser.UserName = dt.Rows[0]["UserName"].ToString();
                    _objuser.Email = dt.Rows[0]["Email"].ToString();
                    _objuser.Designation = (dt.Rows[0]["Designation"] != DBNull.Value ?dt.Rows[0]["Designation"].ToString() : null);
                    _objuser.MobilePhone = (dt.Rows[0]["MobilePhone"] != DBNull.Value ? dt.Rows[0]["MobilePhone"].ToString() : null);
                    _objuser.IsApproved = Convert.ToBoolean(dt.Rows[0]["IsApproved"]);
                    _objuser.IsLockedOut = Convert.ToBoolean(dt.Rows[0]["IsLockedOut"]);
                    _objuser.FailedPasswordAttemptCount = (dt.Rows[0]["FailedPasswordAttemptCount"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["FailedPasswordAttemptCount"]) : 0);
                    _objuser.LastPasswordChangedDate = (dt.Rows[0]["LastPasswordChangedDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastPasswordChangedDate"]) : DateTime.MinValue);
                    _objuser.LastLoginDate = (dt.Rows[0]["LastLoginDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastLoginDate"]) : DateTime.MinValue);
                    _objuser.IsActivated = Convert.ToBoolean(dt.Rows[0]["IsActivated"]);
                    _objuser.DateActivated = (dt.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["DateActivated"]) : DateTime.MinValue);
                    _objuser.InsertedBy = dt.Rows[0]["InsertedBy"].ToString();
                    _objuser.InsertedTime = Convert.ToDateTime(dt.Rows[0]["InsertedTime"]);
                    _objuser.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                    _objuser.UpdatedTime = Convert.ToDateTime(dt.Rows[0]["UpdatedTime"]);
                    _objuser.RoleName = (dt.Rows[0]["RoleName"] != DBNull.Value ? dt.Rows[0]["RoleName"].ToString() : "");
                    _objuser.RoleIds = (dt.Rows[0]["RoleIds"] != DBNull.Value ? dt.Rows[0]["RoleIds"].ToString() : "");
                    _objuser.LastLoginDate = (dt.Rows[0]["LastLoginDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastLoginDate"]) : DateTime.MinValue);

                    if (dt.Rows[0]["RegistrationGUID"] != DBNull.Value)
                    {
                        _objuser.RegistrationGUID = Guid.Parse(dt.Rows[0]["RegistrationGUID"].ToString());
                    }
                }
            }
            return _objuser;
        }

        public Entities.Users GetUserByEmail(string Email, ref int status)
        {
            TCAssociationTool.Entities.Users _objuser = new TCAssociationTool.Entities.Users();
            DataTable dt = _users.GetUserByEmail(Email, ref status);

            if (Email != null && Email.Trim() != "")
            {
                dt = _users.GetUserByEmail(Email, ref status);
                if (dt.Rows.Count == 1)
                {
                    _objuser.UserId = Convert.ToInt64(dt.Rows[0]["UserId"]);
                    _objuser.UserName = dt.Rows[0]["UserName"].ToString();
                    _objuser.Email = dt.Rows[0]["Email"].ToString();
                    _objuser.IsApproved = Convert.ToBoolean(dt.Rows[0]["IsApproved"]);
                    _objuser.IsLockedOut = Convert.ToBoolean(dt.Rows[0]["IsLockedOut"]);
                    _objuser.LastPasswordChangedDate = (dt.Rows[0]["LastPasswordChangedDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastPasswordChangedDate"]) : DateTime.MinValue);
                    _objuser.LastLoginDate = (dt.Rows[0]["LastLoginDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastLoginDate"]) : DateTime.MinValue);
                    _objuser.IsActivated = Convert.ToBoolean(dt.Rows[0]["IsActivated"]);
                    _objuser.DateActivated = (dt.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["DateActivated"]) : DateTime.MinValue);
                    _objuser.LastLoginDate = (dt.Rows[0]["LastLoginDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastLoginDate"]) : DateTime.MinValue);

                    if (dt.Rows[0]["RegistrationGUID"] != DBNull.Value)
                    {
                        _objuser.RegistrationGUID = Guid.Parse(dt.Rows[0]["RegistrationGUID"].ToString());
                    }
                    _objuser.ProfileImage = (dt.Rows[0]["ProfileImage"] != DBNull.Value ? dt.Rows[0]["ProfileImage"].ToString() : "");
                    _objuser.InsertedBy = (dt.Rows[0]["InsertedBy"] != DBNull.Value ? dt.Rows[0]["InsertedBy"].ToString() : null);
                    _objuser.InsertedTime = (dt.Rows[0]["InsertedTime"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["InsertedTime"]) : DateTime.MinValue);
                    _objuser.UpdatedBy = (dt.Rows[0]["UpdatedBy"] != DBNull.Value ? dt.Rows[0]["UpdatedBy"].ToString() : null);
                    _objuser.UpdatedTime = (dt.Rows[0]["UpdatedTime"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["UpdatedTime"]) : DateTime.MinValue);
                    _objuser.RoleName = (dt.Rows[0]["RoleName"] != DBNull.Value ? dt.Rows[0]["RoleName"].ToString() : "");
                    _objuser.RoleIds = (dt.Rows[0]["RoleIds"] != DBNull.Value ? dt.Rows[0]["RoleIds"].ToString() : "");
                }
            }
            return _objuser;
        }

        public Entities.Users GetUserByPhoneNo(string MobilePhone, ref int status)
        {
            TCAssociationTool.Entities.Users _objuser = new TCAssociationTool.Entities.Users();
            DataTable dt = _users.GetUserByPhoneNo(MobilePhone, ref status);

            if (MobilePhone != null && MobilePhone.Trim() != "")
            {
                dt = _users.GetUserByPhoneNo(MobilePhone, ref status);
                if (dt.Rows.Count == 1)
                {
                    _objuser.UserId = Convert.ToInt64(dt.Rows[0]["UserId"]);
                    _objuser.UserName = dt.Rows[0]["UserName"].ToString();
                    _objuser.Email = dt.Rows[0]["Email"].ToString();
                    _objuser.IsApproved = Convert.ToBoolean(dt.Rows[0]["IsApproved"]);
                    _objuser.IsLockedOut = Convert.ToBoolean(dt.Rows[0]["IsLockedOut"]);
                    _objuser.LastPasswordChangedDate = (dt.Rows[0]["LastPasswordChangedDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastPasswordChangedDate"]) : DateTime.MinValue);
                    _objuser.LastLoginDate = (dt.Rows[0]["LastLoginDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastLoginDate"]) : DateTime.MinValue);
                    _objuser.IsActivated = Convert.ToBoolean(dt.Rows[0]["IsActivated"]);
                    _objuser.DateActivated = (dt.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["DateActivated"]) : DateTime.MinValue);
                    _objuser.LastLoginDate = (dt.Rows[0]["LastLoginDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastLoginDate"]) : DateTime.MinValue);

                    if (dt.Rows[0]["RegistrationGUID"] != DBNull.Value)
                    {
                        _objuser.RegistrationGUID = Guid.Parse(dt.Rows[0]["RegistrationGUID"].ToString());
                    }
                    _objuser.ProfileImage = (dt.Rows[0]["ProfileImage"] != DBNull.Value ? dt.Rows[0]["ProfileImage"].ToString() : "");
                    _objuser.InsertedBy = (dt.Rows[0]["InsertedBy"] != DBNull.Value ? dt.Rows[0]["InsertedBy"].ToString() : null);
                    _objuser.InsertedTime = (dt.Rows[0]["InsertedTime"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["InsertedTime"]) : DateTime.MinValue);
                    _objuser.UpdatedBy = (dt.Rows[0]["UpdatedBy"] != DBNull.Value ? dt.Rows[0]["UpdatedBy"].ToString() : null);
                    _objuser.UpdatedTime = (dt.Rows[0]["UpdatedTime"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["UpdatedTime"]) : DateTime.MinValue);
                    _objuser.RoleName = (dt.Rows[0]["RoleName"] != DBNull.Value ? dt.Rows[0]["RoleName"].ToString() : "");
                    _objuser.RoleIds = (dt.Rows[0]["RoleIds"] != DBNull.Value ? dt.Rows[0]["RoleIds"].ToString() : "");
                }
            }
            return _objuser;
        }

        public List<Entities.Users> GetUserListByVariable(Int64 UserId, string RoleIds, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<Entities.Users> lstUsers = new List<Entities.Users>();
            DataTable dt = _users.GetUserListByVariable(UserId,RoleIds,Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo > 1)
            {
                dt = _users.GetUserListByVariable(UserId, RoleIds, Search, Sort, PageNo, Items, ref Total);
            }

            if (dt.Rows.Count != 0 && Total != -1)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.Users _objuser = new Entities.Users();

                    _objuser.UserId = Convert.ToInt64(dr["UserId"]);
                    _objuser.UserName = dr["UserName"].ToString();
                    _objuser.Email = dr["Email"].ToString();
                    _objuser.Designation = dr["Designation"].ToString();
                    _objuser.IsApproved = Convert.ToBoolean(dr["IsApproved"]);
                    _objuser.IsLockedOut = Convert.ToBoolean(dr["IsLockedOut"]);
                    _objuser.RId = Convert.ToInt64(dr["RId"]);
                    _objuser.IsActivated = Convert.ToBoolean(dr["IsActivated"]);
                    _objuser.DateActivated = (dr["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dr["DateActivated"]) : DateTime.MinValue);
                    _objuser.LastLoginDate = (dr["LastLoginDate"] != DBNull.Value ? Convert.ToDateTime(dr["LastLoginDate"]) : DateTime.MinValue);

                    if (dr["RegistrationGUID"] != DBNull.Value)
                    {
                        _objuser.RegistrationGUID = Guid.Parse(dr["RegistrationGUID"].ToString());
                    }

                    _objuser.ProfileImage = (dr["ProfileImage"] != DBNull.Value ? dr["ProfileImage"].ToString() : "");
                    _objuser.RoleName = (dr["RoleName"] != DBNull.Value ? dr["RoleName"].ToString() : "");
                    if (_objuser.RoleName != "")
                    {
                        _objuser.RoleName = _objuser.RoleName.Remove(_objuser.RoleName.Length-1, 1);
                    }
                    _objuser.InsertedTime = (dr["InsertedTime"] != DBNull.Value ? Convert.ToDateTime(dr["InsertedTime"]) : DateTime.MinValue);
                    _objuser.UpdatedTime = (dr["UpdatedTime"] != DBNull.Value ? Convert.ToDateTime(dr["UpdatedTime"]) : DateTime.MinValue);
                    _objuser.MobilePhone = (dr["MobilePhone"] != DBNull.Value ? dr["MobilePhone"].ToString() : "");

                    lstUsers.Add(_objuser);
                }
            }
            return lstUsers;
        }

        public Entities.Users GetUserDetailsById(Int64 UserId, ref Int64 _qStatus)
        {
            TCAssociationTool.Entities.Users _objuser = new TCAssociationTool.Entities.Users();
            DataTable dt = new DataTable();

            if (UserId != 0)
            {
                dt = _users.GetUserDetailsById(UserId, ref _qStatus);
                if (dt.Rows.Count == 1)
                {
                    _objuser.UserId = Convert.ToInt64(dt.Rows[0]["UserId"]);
                    _objuser.UserName = dt.Rows[0]["UserName"].ToString();
                    _objuser.Email = dt.Rows[0]["Email"].ToString();
                    _objuser.IsApproved = Convert.ToBoolean(dt.Rows[0]["IsApproved"]);
                    _objuser.IsLockedOut = Convert.ToBoolean(dt.Rows[0]["IsLockedOut"]);
                    _objuser.IsActivated = Convert.ToBoolean(dt.Rows[0]["IsActivated"]);
                    _objuser.DateActivated = (dt.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["DateActivated"]) : DateTime.MinValue);

                    if (dt.Rows[0]["RegistrationGUID"] != DBNull.Value)
                    {
                        _objuser.RegistrationGUID = Guid.Parse(dt.Rows[0]["RegistrationGUID"].ToString());
                    }

                    _objuser.ProfileImage = (dt.Rows[0]["ProfileImage"] != DBNull.Value ? dt.Rows[0]["ProfileImage"].ToString() : "");
                    _objuser.Designation = (dt.Rows[0]["Designation"] != DBNull.Value ? dt.Rows[0]["Designation"].ToString() : "");
                    _objuser.InsertedTime = (dt.Rows[0]["InsertedTime"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["InsertedTime"]) : DateTime.MinValue);
                    _objuser.UpdatedTime = (dt.Rows[0]["UpdatedTime"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["UpdatedTime"]) : DateTime.MinValue);
                    _objuser.RoleName = (dt.Rows[0]["RoleName"] != DBNull.Value ? dt.Rows[0]["RoleName"].ToString() : "");
                    _objuser.RoleIds = (dt.Rows[0]["RoleIds"] != DBNull.Value ? dt.Rows[0]["RoleIds"].ToString() : "");
                    _objuser.MobilePhone = (dt.Rows[0]["MobilePhone"] != DBNull.Value ? dt.Rows[0]["MobilePhone"].ToString() : "");
                }
            }
            return _objuser;
        }
        
        #endregion

    }
}
