using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace TCAssociationTool.DAL
{
 public   class CulturalRegistrations
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;



        public Int64 InsertCulturalRegistrations(Entities.CulturalRegistrations objCulturalRegistrations)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@Id",objCulturalRegistrations.Id),
                    new SqlParameter("@item_title",(objCulturalRegistrations.item_title == null ?DBNull.Value:(object)objCulturalRegistrations.item_title.Trim())),
                    new SqlParameter("@item_type",(objCulturalRegistrations.item_type == null ?DBNull.Value:(object)objCulturalRegistrations.item_type.Trim())),
                    new SqlParameter("@item_description",(objCulturalRegistrations.item_description == null ?DBNull.Value:(object)objCulturalRegistrations.item_description.Trim())),
                    new SqlParameter("@minutes",(objCulturalRegistrations.minutes == null ?DBNull.Value:(object)objCulturalRegistrations.minutes.Trim())),
                    new SqlParameter("@group_name",(objCulturalRegistrations.group_name == null ?DBNull.Value:(object)objCulturalRegistrations.group_name.Trim())),
                    new SqlParameter("@name",(objCulturalRegistrations.name == null ?DBNull.Value:(object)objCulturalRegistrations.name.Trim())),
                    new SqlParameter("@choreographer",(objCulturalRegistrations.choreographer == null ?DBNull.Value:(object)objCulturalRegistrations.choreographer.Trim())),
                    new SqlParameter("@email",(objCulturalRegistrations.email == null ?DBNull.Value:(object)objCulturalRegistrations.email.Trim())),
                    new SqlParameter("@mobile",(objCulturalRegistrations.mobile == null ?DBNull.Value:(object)objCulturalRegistrations.mobile.Trim())),
                    new SqlParameter("@address1",(objCulturalRegistrations.address1 == null ?DBNull.Value:(object)objCulturalRegistrations.address1.Trim())),
                    new SqlParameter("@address2",(objCulturalRegistrations.address2 == null ?DBNull.Value:(object)objCulturalRegistrations.address2.Trim())),
                    new SqlParameter("@city",(objCulturalRegistrations.city == null ?DBNull.Value:(object)objCulturalRegistrations.city.Trim())),
                    new SqlParameter("@state",(objCulturalRegistrations.state == null ?DBNull.Value:(object)objCulturalRegistrations.state.Trim())),
                    new SqlParameter("@zip",(objCulturalRegistrations.zip == null ?DBNull.Value:(object)objCulturalRegistrations.zip.Trim())),
                    new SqlParameter("@url",(objCulturalRegistrations.url == null ?DBNull.Value:(object)objCulturalRegistrations.url.Trim())),
                    new SqlParameter("@date_created",(objCulturalRegistrations.date_created == DateTime.MinValue ?DBNull.Value:(object)objCulturalRegistrations.date_created)),
                    new SqlParameter("@status2",objCulturalRegistrations.status2),
                    new SqlParameter("@date_modified",(objCulturalRegistrations.date_modified == DateTime.MinValue ?DBNull.Value:(object)objCulturalRegistrations.date_modified)),
                    new SqlParameter("@comments",(objCulturalRegistrations.comments == null ?DBNull.Value:(object)objCulturalRegistrations.comments.Trim())),
                    new SqlParameter("@QStatus",0)
                    };

 




                _sqlP[20].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("cultural_registrationsInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[20].Value);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }



        public Int64 CulturalRegistrationsCommentUpdate(Entities.CulturalRegistrations objCulturalRegistrations)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@Id",objCulturalRegistrations.Id),
                    new SqlParameter("@comments",(objCulturalRegistrations.comments == null ?DBNull.Value:(object)objCulturalRegistrations.comments.Trim())),
                    new SqlParameter("@QStatus",0)
                    };



                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("cultural_registrationsCommentUpdate", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }





        public DataTable GetCulturalRegistrationsListByVariable(string ItemType,string StartDate,string EndDate, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ItemType",ItemType),
                    new SqlParameter("@StartDate",StartDate),
                    new SqlParameter("@EndDate",EndDate),
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@Total",Total)
                };

                _sqlP[7].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("cultural_registrationsGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[7].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public DataTable GetCulturalRegistrationsById(Int64 Id, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@Id",Id),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("cultural_registrationsGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteCulturalRegistrations(Int64 Id)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@Id",Id),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("cultural_registrationsDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateCulturalRegistrationsStatus(Int64 id)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@id",id),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("cultural_registrationsUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 CulturalRegistrationsDeleteAll(string Id)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@Id",Id),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("cultural_registrationsDeleteAll", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable ExportCulturalRegistrationsList(string Search,string ItemType, string StartDate,string EndDate, string Sort)
        {
            DataTable dt = null;
            int Total = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ItemType",ItemType),
                    new SqlParameter("@StartDate",StartDate),
                    new SqlParameter("@EndDate",EndDate),
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@Total",Total)
                };

                _sqlP[5].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("Exportcultural_registrationsGetList", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[5].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

    }
}
