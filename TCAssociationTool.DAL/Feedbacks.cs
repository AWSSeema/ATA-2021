using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TCAssociationTool.DAL
{
   public class Feedbacks
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;



        public Int64 InsertFeedbacks(Entities.Feedbacks objFeedbacks)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@Id",objFeedbacks.Id),
                    new SqlParameter("@firstname",(objFeedbacks.firstname == null ?DBNull.Value:(object)objFeedbacks.firstname.Trim())),
                    new SqlParameter("@lastname",(objFeedbacks.lastname == null ?DBNull.Value:(object)objFeedbacks.lastname.Trim())),
                    new SqlParameter("@email",(objFeedbacks.email == null ?DBNull.Value:(object)objFeedbacks.email.Trim())),
                    new SqlParameter("@message",(objFeedbacks.message == null ?DBNull.Value:(object)objFeedbacks.message.Trim())),
                    new SqlParameter("@datesent",(objFeedbacks.datesent == DateTime.MinValue ?(object)DBNull.Value:objFeedbacks.datesent)),
                    new SqlParameter("@admincomments",(objFeedbacks.admincomments == null ?DBNull.Value:(object)objFeedbacks.admincomments.Trim())),
                    new SqlParameter("@QStatus",0)
                    };


                _sqlP[7].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("FeedbacksInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[7].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }


        public Int64 FeedbacksCommentUpdate(Entities.Feedbacks objFeedbacks)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@Id",objFeedbacks.Id),
                    new SqlParameter("@admincomments",(objFeedbacks.admincomments == null ?DBNull.Value:(object)objFeedbacks.admincomments.Trim())),
                    new SqlParameter("@QStatus",0)
                    };


                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("FeedbacksUpdateComments", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }


        public DataTable GetFeedbacksListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
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
                dt = _dbAccess.GetDataTable("FeedbacksGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public DataTable GetFeedbacksById(Int64 Id, ref int status)
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
                dt = _dbAccess.GetDataTable("FeedbacksGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteFeedbacks(Int64 Id)
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
                _dbAccess.SP_ExecuteScalar("FeedbacksDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }


        public Int64 FeedbacksDeleteAll(string Id)
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
                _dbAccess.SP_ExecuteScalar("FeedbacksDeleteAll", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }


        

        public DataTable ExportFeedbacksList(string Search, string Sort)
        {
            DataTable dt = null;
            int Total = 0;
            try
            {
                _sqlP = new[]
                {

               
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@Total",Total)
                };

                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("ExportFeedbacksGetList", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}
