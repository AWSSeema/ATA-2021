using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TCAssociationTool.DAL
{
   public class Health
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;



        public Int64 InsertHealth(Entities.Health objHealth)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@Id",objHealth.Id),
                    new SqlParameter("@heading",(objHealth.heading == null ?DBNull.Value:(object)objHealth.heading.Trim())),
                    new SqlParameter("@description",(objHealth.description == null ?DBNull.Value:(object)objHealth.description.Trim())),
                    new SqlParameter("@datemodified",(objHealth.datemodified == DateTime.MinValue ?(object)DBNull.Value:objHealth.datemodified)),
                    new SqlParameter("@status2",(objHealth.status2 )),
                    new SqlParameter("@pagetitle",(objHealth.pagetitle == null ?DBNull.Value:(object)objHealth.pagetitle.Trim())),
                    new SqlParameter("@metakeywords",(objHealth.metakeywords == null ?DBNull.Value:(object)objHealth.metakeywords.Trim())),
                    new SqlParameter("@metadesc",(objHealth.metadesc == null ?DBNull.Value:(object)objHealth.metadesc.Trim())),
                    new SqlParameter("@pageurl",(objHealth.pageurl == null ?DBNull.Value:(object)objHealth.pageurl.Trim())),
                    new SqlParameter("@topics",(objHealth.topics == null ?DBNull.Value:(object)objHealth.topics.Trim())),
                    new SqlParameter("@issuedate",(objHealth.issuedate == DateTime.MinValue ?(object)DBNull.Value:objHealth.issuedate)),
                    new SqlParameter("@QStatus",0)
                    };
                 
                _sqlP[11].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("HealthInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[11].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }


       

        public DataTable GetHealthListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
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
                dt = _dbAccess.GetDataTable("HealthGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public DataTable GetHealthById(Int64 Id, ref int status)
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
                dt = _dbAccess.GetDataTable("HealthGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteHealth(Int64 Id)
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
                _dbAccess.SP_ExecuteScalar("HealthDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 HealthUpdateStatus(Int64 Id)
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
                _dbAccess.SP_ExecuteScalar("healthUpdateStatus", ref _sqlP);
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
