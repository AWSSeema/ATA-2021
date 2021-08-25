using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace TCAssociationTool.DAL
{
    public class Users
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

        public Int64 InsertUserProfile(TCAssociationTool.Entities.Users objUser)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@UserId",objUser.UserId),
                    new SqlParameter("@UserName",(objUser.UserName == null ?DBNull.Value:(object)objUser.UserName.Trim())),
                    new SqlParameter("@RoleName",(objUser.RoleName == null ?DBNull.Value:(object)objUser.RoleName.Trim())),
                    new SqlParameter("@Email",(objUser.Email == null ?DBNull.Value:(object)objUser.Email.Trim())),
                    new SqlParameter("@Designation",(objUser.Designation == null ?DBNull.Value:(object)objUser.Designation)),
                    new SqlParameter("@MobilePhone",(objUser.MobilePhone == null ?DBNull.Value:(object)objUser.MobilePhone)),
                    new SqlParameter("@IsApproved",objUser.IsApproved),
                    new SqlParameter("@IsLockedOut",objUser.IsLockedOut),
                    new SqlParameter("@IsActivated",objUser.IsActivated),
                    new SqlParameter("@DateActivated",DBNull.Value),
                    new SqlParameter("@RegistrationGUID",objUser.RegistrationGUID),
                    new SqlParameter("@FailedPasswordAttemptCount",objUser.FailedPasswordAttemptCount),
                    new SqlParameter("@LastPasswordChangedDate",DBNull.Value),
                    new SqlParameter("@LastLoginDate",DBNull.Value),  
                    new SqlParameter("@InsertedBy",objUser.InsertedBy),
                    new SqlParameter("@InsertedTime",DateTime.UtcNow),
                    new SqlParameter("@UpdatedBy",objUser.UpdatedBy),
                    new SqlParameter("@UpdatedTime",DateTime.UtcNow),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[18].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UsersProfileInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[18].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateUserAccess(TCAssociationTool.Entities.UserRoles objUser)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@UserId",objUser.UserId),
                     new SqlParameter("@RoleIds",objUser.RoleIds),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UpdateUserAccess", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetUserListByVariable(Int64 UserId, string RoleId, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@UserId",UserId),
                    new SqlParameter("@RoleId",RoleId),
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@Total",Total)
                };

                _sqlP[6].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("UsersGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[6].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 ChangePassword(Int64 UserId, string Password)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@UserId",UserId),
                    new SqlParameter("@Password",Password),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UsersChangePassword", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetUserRolesList(ref int Total)
        {
            DataTable ds = null;
            try
            {
                _sqlP = new[] 
                {             
                    new SqlParameter("@Qstatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataTable("UserRolesGetList", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataTable GetUserRolesListById(Int64 UserId, ref int Total)
        {
            DataTable ds = null;
            try
            {
                _sqlP = new[] 
                {   
                    new SqlParameter("@UserId",UserId),
                    new SqlParameter("@Qstatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataTable("UserRolesGetById", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public Int64 DeleteUser(Int64 UserId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@UserId",UserId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UsersDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 DeleteAllUser()
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UsersDeleteAll", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetUserByEmail(string Email, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@Email",Email),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("UsersGetByEmail", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetUserByPhoneNo(string MobilePhone, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@MobilePhone",MobilePhone),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("UsersGetByPhoneNo", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetUserDetailsById(Int64 UserId, ref Int64 _QStatus)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@UserId", UserId),
                    new SqlParameter("@QStatus", _QStatus)
                };

                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("UsersGetById", ref _sqlP);
                _QStatus = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetUserByUserName(string UserName, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@UserName",UserName),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("UsersGetByUserName", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetPassword(Int64 _userid, ref int _QStatus)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] {
                new SqlParameter("@UserId",_userid),
                new SqlParameter("@QStatus",_QStatus)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;

                dt = _dbAccess.GetDataTable("UsersGetPassword", ref _sqlP);
                _QStatus = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 UnlockUser(Int64 UserId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@UserId",UserId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UsersUnlock", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateUserStatus(Int64 UserId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@UserId",UserId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UsersUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateUserProfileImage(Int64 UserId, ref string ProfileImage)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@UserId",UserId),
                    new SqlParameter("@ProfileImage",ProfileImage),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[1].SqlDbType = SqlDbType.NVarChar;
                _sqlP[1].Size = 256;
                _sqlP[1].Direction = System.Data.ParameterDirection.InputOutput;
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UsersProfileImage", ref _sqlP);
                ProfileImage = _sqlP[1].Value.ToString();
                _status = Convert.ToInt64(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateUser(TCAssociationTool.Entities.Users objUser)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@UserId",objUser.UserId),
                    new SqlParameter("@UserName",(objUser.UserName==null?(object)DBNull.Value:objUser.UserName)),
                    new SqlParameter("@Email",(objUser.Email == null ?DBNull.Value:(object)objUser.Email.Trim())),
                    new SqlParameter("@Designation",(objUser.Designation == null ?DBNull.Value:(object)objUser.Designation)),
                    new SqlParameter("@MobilePhone",(objUser.MobilePhone == null ?DBNull.Value:(object)objUser.MobilePhone)),
                    new SqlParameter("@UpdatedBy",objUser.UpdatedBy),
                    new SqlParameter("@UpdatedTime",DateTime.UtcNow),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[7].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UpdateUser", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[7].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateRegistrationGUID(Int64 UserId, string IsActivated, Guid RegistrationGUID)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@UserId",UserId),
                    new SqlParameter("@IsActivated",IsActivated),
                    new SqlParameter("@DateActivated",DateTime.UtcNow),
                    new SqlParameter("@RegistrationGUID",RegistrationGUID),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[4].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UserUpdateRegistrationGUID", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }
    }
}
