using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace TCAssociationTool.DAL
{
  public  class EventForm
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;



        public Int64 InsertEventForm(Entities.EventForm objEventForm)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@Id",objEventForm.Id),
                    new SqlParameter("@firstname",(objEventForm.firstname == null ?DBNull.Value:(object)objEventForm.firstname.Trim())),
                    new SqlParameter("@lastname",(objEventForm.lastname == null ?DBNull.Value:(object)objEventForm.lastname.Trim())),
                    new SqlParameter("@email",(objEventForm.email == null ?DBNull.Value:(object)objEventForm.email.Trim())),
                    new SqlParameter("@phone",(objEventForm.phone == null ?DBNull.Value:(object)objEventForm.phone.Trim())),
                    new SqlParameter("@members",(objEventForm.members == 0 ?DBNull.Value:(object)objEventForm.members)),
                    new SqlParameter("@address",(objEventForm.address == null ?DBNull.Value:(object)objEventForm.address.Trim())),
                    new SqlParameter("@city",(objEventForm.city == null ?DBNull.Value:(object)objEventForm.city.Trim())),
                    new SqlParameter("@state",(objEventForm.state == null ?DBNull.Value:(object)objEventForm.state.Trim())),
                    new SqlParameter("@zip",(objEventForm.zip == null ?DBNull.Value:(object)objEventForm.zip.Trim())),
                    new SqlParameter("@ismember",(objEventForm.ismember == null ?DBNull.Value:(object)objEventForm.ismember.Trim())),
                    new SqlParameter("@datesent",(objEventForm.datesent == DateTime.MinValue ?DBNull.Value:(object)objEventForm.datesent)),
                    new SqlParameter("@status2",objEventForm.status2),
                    new SqlParameter("@comments",(objEventForm.comments == null ?DBNull.Value:(object)objEventForm.comments.Trim())),
                    new SqlParameter("@eventname",(objEventForm.eventname == null ?DBNull.Value:(object)objEventForm.eventname.Trim())),
                    new SqlParameter("@eventid",(objEventForm.eventid == 0 ?DBNull.Value:(object)objEventForm.eventid)),
                    new SqlParameter("@QStatus",0)
                    };

               

                _sqlP[16].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("Event_FormInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[16].Value);

            
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }



        public Int64 EventFormCommentUpdate(Entities.EventForm objEventForm)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@Id",objEventForm.Id),
                    new SqlParameter("@comments",(objEventForm.comments == null ?DBNull.Value:(object)objEventForm.comments.Trim())),
                    new SqlParameter("@QStatus",0)
                    };



                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("Event_FormCommentUpdate", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }





        public DataTable GetEventFormListByVariable(Int64 eventid, string Search, string Sort, int PageNo, int Items, ref int Total)
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
                dt = _dbAccess.GetDataTable("Event_FormGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[5].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public DataTable GetEventFormById(Int64 Id, ref int status)
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
                dt = _dbAccess.GetDataTable("Event_FormGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteEventForm(Int64 Id)
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
                _dbAccess.SP_ExecuteScalar("Event_FormDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateEventFormStatus(Int64 id)
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
                _dbAccess.SP_ExecuteScalar("Event_FormUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 EventFormDeleteAll(string Id)
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
                _dbAccess.SP_ExecuteScalar("Event_formDeleteAll", ref _sqlP);
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
