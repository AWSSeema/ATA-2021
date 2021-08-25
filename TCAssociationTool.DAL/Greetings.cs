using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace TCAssociationTool.DAL
{
   public class Greetings
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

        public DataTable GetGreetingsList(ref int qstatus)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("greetingsGetList", ref _sqlP);
                qstatus = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 UpdateGreetingsStatus(Int64 id)
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
                _dbAccess.SP_ExecuteScalar("greetingsUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 InsertGreetings(Entities.Greetings objGreetings, ref string imgsrc)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@id",objGreetings.id),
                    new SqlParameter("@title",objGreetings.title),
                    new SqlParameter("@imgsrc",imgsrc),
                    new SqlParameter("@imgwidth",(objGreetings.imgwidth == 0 ?DBNull.Value:(object)objGreetings.imgwidth)),
                    new SqlParameter("@status2",objGreetings.status2),
                    new SqlParameter("@date_modified",DateTime.UtcNow),
                    new SqlParameter("@top_padding",(objGreetings.top_padding == 0 ?DBNull.Value:(object)objGreetings.top_padding)),
                    new SqlParameter("@link",(objGreetings.link == null ?DBNull.Value:(object)objGreetings.link)),
                    new SqlParameter("@target",(objGreetings.target == null ?DBNull.Value:(object)objGreetings.target)),
                    new SqlParameter("@lastdate",(objGreetings.lastdate == DateTime.MinValue ?(object)DBNull.Value:objGreetings.lastdate)),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[2].SqlDbType = SqlDbType.NVarChar;
                _sqlP[2].Size = 512;
                _sqlP[2].Direction = System.Data.ParameterDirection.InputOutput;

                _sqlP[10].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("greetingsInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[10].Value);

                imgsrc = _sqlP[2].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetGreetingsListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
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
                dt = _dbAccess.GetDataTable("greetingsGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetGreetingsById(Int64 id, ref int status)
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
                dt = _dbAccess.GetDataTable("greetingsGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteGreetings(Int64 id)
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
                _dbAccess.SP_ExecuteScalar("greetingsDelete", ref _sqlP);
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
