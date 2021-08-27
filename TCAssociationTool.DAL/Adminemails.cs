using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace TCAssociationTool.DAL
{
 public   class Adminemails
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;



        public Int64 InsertAdminemails(Entities.Adminemails objAdminemails)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@Id",objAdminemails.Id),
                    new SqlParameter("@name",(objAdminemails.name == null ?DBNull.Value:(object)objAdminemails.name.Trim())),
                    new SqlParameter("@designation",(objAdminemails.designation == null ?DBNull.Value:(object)objAdminemails.designation.Trim())),
                    new SqlParameter("@email",(objAdminemails.email == null ?DBNull.Value:(object)objAdminemails.email.Trim())),
                    new SqlParameter("@isdonation",(objAdminemails.isdonation == false ?DBNull.Value:(object)objAdminemails.isdonation)),
                    new SqlParameter("@ismembership",(objAdminemails.ismembership == false ?DBNull.Value:(object)objAdminemails.ismembership)),
                    new SqlParameter("@datemodified",(objAdminemails.datemodified == DateTime.MinValue ?DBNull.Value:(object)objAdminemails.datemodified)),
                    new SqlParameter("@defaultmembership",(objAdminemails.defaultmembership == false ?DBNull.Value:(object)objAdminemails.defaultmembership)),
                    new SqlParameter("@defaultdonation",(objAdminemails.defaultdonation == false ?DBNull.Value:(object)objAdminemails.defaultdonation)),
                               new SqlParameter("@QStatus",0)
                    };


                _sqlP[9].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("AdminemailsInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[9].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }



        
        public DataTable GetAdminemailsListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {

                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@Total",Total)
                };

                _sqlP[4].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("AdminemailsGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }




        public DataTable GetAdminemailsById(Int64 Id, ref int status)
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
                dt = _dbAccess.GetDataTable("AdminemailsGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public Int64 DeleteAdminemails(Int64 Id)
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
                _dbAccess.SP_ExecuteScalar("AdminemailsDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }



        public Int64 AdminemailsUpdatedefaultmembership(Int64 Id)
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
                _dbAccess.SP_ExecuteScalar("AdminemailsUpdatedefaultmembership", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }


        public Int64 AdminemailsUpdatedefaultdonation(Int64 Id)
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
                _dbAccess.SP_ExecuteScalar("AdminemailsUpdatedefaultdonation", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }
    }
}
