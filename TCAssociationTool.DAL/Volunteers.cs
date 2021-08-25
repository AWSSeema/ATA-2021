﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace TCAssociationTool.DAL
{
    public class Volunteers
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

        #region Admin
        
        public Int64 InsertVolunteers(Entities.Volunteers objVolunteers)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@VolunteerId",objVolunteers.VolunteerId),
                    new SqlParameter("@VolunteerCategoryId",(objVolunteers.VolunteerCategoryId==0?(object)DBNull.Value:objVolunteers.VolunteerCategoryId)),
                    new SqlParameter("@EventId",(objVolunteers.EventId==0?(object)DBNull.Value:objVolunteers.EventId)),
                    new SqlParameter("@FirstName",objVolunteers.FirstName),
                    new SqlParameter("@LastName",objVolunteers.LastName),
                    new SqlParameter("@Email",objVolunteers.Email),
                    new SqlParameter("@PhoneNo",(objVolunteers.PhoneNo == null ?DBNull.Value:(object)objVolunteers.PhoneNo)),
                    new SqlParameter("@Address",(objVolunteers.Address == null ?DBNull.Value:(object)objVolunteers.Address)),
                    new SqlParameter("@Comments",(objVolunteers.Comments == null ?DBNull.Value:(object)objVolunteers.Comments)),
                    new SqlParameter("@HoursSpent",(objVolunteers.HoursSpent == null ?DBNull.Value:(object)objVolunteers.HoursSpent)),
                    new SqlParameter("@Field1",(objVolunteers.Field1 == null ?DBNull.Value:(object)objVolunteers.Field1)),
                    new SqlParameter("@Field2",(objVolunteers.Field2 == null ?DBNull.Value:(object)objVolunteers.Field2)),
                    new SqlParameter("@IsActive",objVolunteers.IsActive),
                    new SqlParameter("@InsertedBy",objVolunteers.InsertedBy),
                    new SqlParameter("@InsertedDate",DateTime.UtcNow),
                    new SqlParameter("@UpdatedBy",objVolunteers.UpdatedBy),
                    new SqlParameter("@UpdatedDate",DateTime.UtcNow),
                     new SqlParameter("@MemberId",(objVolunteers.MemberId==0?(object)DBNull.Value:objVolunteers.MemberId)),
                    new SqlParameter("@QStatus",0)
                    };

                _sqlP[18].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("VolunteersInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[18].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetVolunteersListByVariable(Int64 VolunteerCategoryId, Int64 EventId, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@VolunteerCategoryId",VolunteerCategoryId),
                    new SqlParameter("@EventId",EventId),
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),                    
                    new SqlParameter("@Total",Total)
                };

                _sqlP[6].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("VolunteersGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[6].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetVolunteerById(Int64 VolunteerId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@VolunteerId",VolunteerId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("VolunteersGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetVolunteerInfoById(Int64 VolunteerId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@VolunteerId",VolunteerId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("VolunteersGetinfoById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteVolunteer(Int64 VolunteerId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@VolunteerId",VolunteerId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("VolunteersDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateVolunteerstatus(Int64 VolunteerId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@VolunteerId",VolunteerId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("VolunteersUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }
        
        public DataTable VolunteerValidationByEmail(string Email, ref int status)
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
                dt = _dbAccess.GetDataTable("VolunteersValidationByEmail", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 UpdateVolunteerHours(string HoursSpent, Int64 VolunteerId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@VolunteerId",VolunteerId),
                    new SqlParameter("@HoursSpent",HoursSpent),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UpdateVolunteerHours", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable ExportToVolunteers(string Search, Int64 VolunteerCategoryId, Int64 EventId, string Sort)
        {
            DataTable dt = null;
            int Total = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@VolunteerCategoryId",VolunteerCategoryId),
                    new SqlParameter("@EventId",EventId),
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@Total",Total)
                };

                _sqlP[4].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("ExportVolunteersGetList", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        
        #endregion

        #region Front-end

        public DataTable FEGetVolunteersList(Int64 VolunteerCategoryId, Int64 EventId, string Email, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@VolunteerCategoryId",VolunteerCategoryId),
                    new SqlParameter("@EventId",EventId),
                    new SqlParameter("@Email",Email),
                     new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),                    
                    new SqlParameter("@Total",Total)
                };
                _sqlP[7].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("FEGetVolunteersList", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[7].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 FEVolunteerInsert(Entities.Volunteers objVolunteers)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@VolunteerId",objVolunteers.VolunteerId),
                    new SqlParameter("@VolunteerCategoryId",(objVolunteers.VolunteerCategoryId==0?(object)DBNull.Value:objVolunteers.VolunteerCategoryId)),
                    new SqlParameter("@EventId",(objVolunteers.EventId==0?(object)DBNull.Value:objVolunteers.EventId)),
                    new SqlParameter("@FirstName",objVolunteers.FirstName),
                    new SqlParameter("@LastName",objVolunteers.LastName),
                    new SqlParameter("@Email",objVolunteers.Email),
                    new SqlParameter("@PhoneNo",(objVolunteers.PhoneNo == null ?DBNull.Value:(object)objVolunteers.PhoneNo)),
                    new SqlParameter("@Address",(objVolunteers.Address == null ?DBNull.Value:(object)objVolunteers.Address)),
                    new SqlParameter("@Comments",(objVolunteers.Comments == null ?DBNull.Value:(object)objVolunteers.Comments)),
                    new SqlParameter("@HoursSpent",(objVolunteers.HoursSpent == null ?DBNull.Value:(object)objVolunteers.HoursSpent)),
                    new SqlParameter("@Field1",(objVolunteers.Field1 == null ?DBNull.Value:(object)objVolunteers.Field1)),
                    new SqlParameter("@Field2",(objVolunteers.Field2 == null ?DBNull.Value:(object)objVolunteers.Field2)),
                    new SqlParameter("@IsActive",objVolunteers.IsActive),
                    new SqlParameter("@InsertedBy",objVolunteers.InsertedBy),
                    new SqlParameter("@InsertedDate",DateTime.UtcNow),
                    new SqlParameter("@UpdatedBy",objVolunteers.UpdatedBy),
                    new SqlParameter("@UpdatedDate",DateTime.UtcNow),
                    new SqlParameter("@MemberId",(objVolunteers.MemberId==0?(object)DBNull.Value:objVolunteers.MemberId)),
                    new SqlParameter("@QStatus",0)
                    };

                _sqlP[18].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("FEVolunteerInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[18].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable FEVolunteersList(Int64 MemberId, ref int status)
        {
            DataTable ds = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@MemberId",MemberId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataTable("FEVolunteersList", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        #endregion
    }
}
