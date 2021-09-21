using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TCAssociationTool.DAL
{
   public class AmericaBharati
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

        #region Method

        public Int64 DeleteAmericaBharati(Int64 id)
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
                _dbAccess.SP_ExecuteScalar("america_bharatiDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 InsertAmericaBharati(Entities.AmericaBharati objAmericaBharati)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@id",objAmericaBharati.id),
                    new SqlParameter("@heading",objAmericaBharati.heading),
                    new SqlParameter("@description",objAmericaBharati.description),
                    new SqlParameter("@pagetitle",(objAmericaBharati.pagetitle!= null?(object)objAmericaBharati.pagetitle:DBNull.Value.ToString())),
                    new SqlParameter("@metakeywords",(objAmericaBharati.metakeywords!= null?(object)objAmericaBharati.metakeywords:DBNull.Value.ToString())),
                    new SqlParameter("@metadesc",(objAmericaBharati.metadesc!= null?(object)objAmericaBharati.metadesc:DBNull.Value.ToString())),
                    new SqlParameter("@pageurl",(objAmericaBharati.pageurl!= null?(object)objAmericaBharati.pageurl:DBNull.Value.ToString())),
                    new SqlParameter("@filename",(objAmericaBharati.filename!= null?(object)objAmericaBharati.filename:DBNull.Value.ToString())),
                    new SqlParameter("@datemodified",DateTime.UtcNow),
                    new SqlParameter("@status2",objAmericaBharati.status2),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[10].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("america_bharatiInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[10].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateAmericaBharatiStatus(Int64 id)
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
                _dbAccess.SP_ExecuteScalar("america_bharatiUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        #endregion

        #region Entity Filling

        public DataTable GetAmericaBharatiById(Int64 id, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@id",id),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("america_bharatiGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetAmericaBharatiListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
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
                    new SqlParameter("@Total",0)
                };
                _sqlP[4].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("america_bharatiGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetAmericaBharatiList(ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                  new SqlParameter("@QStatus",0)
                
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("america_bharatiGetList", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        #endregion 
    }
}
