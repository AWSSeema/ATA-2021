using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TCAssociationTool.DAL
{
  public  class Ticket
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;



        public Int64 InsertTicket(Entities.Ticket objTicket)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@Id",objTicket.Id),
                    new SqlParameter("@firstname",(objTicket.firstname == null ?DBNull.Value:(object)objTicket.firstname.Trim())),
                    new SqlParameter("@lastname",(objTicket.lastname == null ?DBNull.Value:(object)objTicket.lastname.Trim())),
                    new SqlParameter("@age",(objTicket.age == null ?DBNull.Value:(object)objTicket.age.Trim())),
                    new SqlParameter("@category",(objTicket.category == null ?DBNull.Value:(object)objTicket.category.Trim())),
                    new SqlParameter("@amount",(objTicket.amount == 0 ?DBNull.Value:(object)objTicket.amount)),
                    new SqlParameter("@ticketid",(objTicket.ticketid == 0 ?DBNull.Value:(object)objTicket.ticketid)),
                    new SqlParameter("@QStatus",0)
                    };

     

                _sqlP[7].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("TicketInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[7].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }


        //public Int64 TicketCommentUpdate(Entities.Ticket objTicket)
        //{
        //    Int64 _status = 0;
        //    try
        //    {
        //        _sqlP = new[]
        //            {
        //            new SqlParameter("@Id",objTicket.Id),
        //            new SqlParameter("@comments",(objTicket.comments == null ?DBNull.Value:(object)objTicket.comments.Trim())),
        //            new SqlParameter("@QStatus",0)
        //            };


        //        _sqlP[2].Direction = System.Data.ParameterDirection.Output;
        //        _dbAccess.SP_ExecuteScalar("TicketCommentUpdate", ref _sqlP);
        //        _status = Convert.ToInt64(_sqlP[2].Value);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return _status;
        //}



        public DataTable GetTicketListByVariable(Int64 eventid,string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                     new SqlParameter("@eventid",eventid),
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@Total",Total)
                };

                _sqlP[5].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("TicketGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[5].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetTicketById(Int64 Id, ref int status)
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
                dt = _dbAccess.GetDataTable("TicketGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteTicket(Int64 Id)
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
                _dbAccess.SP_ExecuteScalar("TicketDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 TicketDeleteAll(string Id)
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
                _dbAccess.SP_ExecuteScalar("TicketDeleteAll", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable ExportTicketList(string Search, Int64 eventid, string Sort)
        {
            DataTable dt = null;
            int Total = 0;
            try
            {
                _sqlP = new[]
                {

                    new SqlParameter("@eventid",eventid),
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@Total",Total)
                };

                _sqlP[3].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("ExportTicketGetList", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[3].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}
