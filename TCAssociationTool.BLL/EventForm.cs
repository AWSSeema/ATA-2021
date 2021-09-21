using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TCAssociationTool.BLL
{
  public  class EventForm
    {
        TCAssociationTool.DAL.EventForm _EventForm = new TCAssociationTool.DAL.EventForm();
        #region Methods

        public Int64 InsertEventForm(Entities.EventForm objEventForm)
        {
            Int64 _status = 0;
            if (objEventForm != null)
            {
                _status = _EventForm.InsertEventForm(objEventForm);

            }
            return _status;
        }

        public Int64 EventFormCommentUpdate(Entities.EventForm objEventForm)
        {
            Int64 _status = 0;
            if (objEventForm != null)
            {
                _status = _EventForm.EventFormCommentUpdate(objEventForm);

            }
            return _status;
        }


        public Int64 DeleteEventForm(Int64 Id)
        {
            Int64 _status = 0;
            _status = _EventForm.DeleteEventForm(Id);
            return _status;
        }   
        public Int64 EventFormDeleteAll(string Id)
        {
            Int64 _status = 0;
            _status = _EventForm.EventFormDeleteAll(Id);
            return _status;
        }

        public Int64 UpdateEventFormStatus(Int64 id)
        {
            Int64 _status = 0;
            _status = _EventForm.UpdateEventFormStatus(id);
            return _status;
        }



        #endregion

        #region Entities filling



        public TCAssociationTool.Entities.EventForm GetEventFormById(Int64 Id, ref int status)
        {
            TCAssociationTool.Entities.EventForm objEventForm = new TCAssociationTool.Entities.EventForm();
            DataTable dt = new DataTable();
            if (Id != 0)
            {
                dt = _EventForm.GetEventFormById(Id, ref status);
                if (dt.Rows.Count == 1)
                {
                    objEventForm.Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                    objEventForm.firstname = (dt.Rows[0]["firstname"] != DBNull.Value ? dt.Rows[0]["firstname"].ToString() : "");
                    objEventForm.lastname = (dt.Rows[0]["lastname"] != DBNull.Value ? dt.Rows[0]["lastname"].ToString() : "");
                    objEventForm.email = (dt.Rows[0]["email"] != DBNull.Value ? dt.Rows[0]["email"].ToString() : "");
                    objEventForm.phone = (dt.Rows[0]["phone"] != DBNull.Value ? dt.Rows[0]["phone"].ToString() : "");
                    objEventForm.address = (dt.Rows[0]["address"] != DBNull.Value ? dt.Rows[0]["address"].ToString() : "");
                    objEventForm.members = (dt.Rows[0]["members"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["members"]) : 0);
                    objEventForm.city = (dt.Rows[0]["city"] != DBNull.Value ? dt.Rows[0]["city"].ToString() : "");
                    objEventForm.state = (dt.Rows[0]["state"] != DBNull.Value ? dt.Rows[0]["state"].ToString() : "");
                    objEventForm.zip = (dt.Rows[0]["zip"] != DBNull.Value ? dt.Rows[0]["zip"].ToString() : "");
                    objEventForm.ismember = (dt.Rows[0]["ismember"] != DBNull.Value ? dt.Rows[0]["ismember"].ToString() : "");
                    objEventForm.datesent = (dt.Rows[0]["datesent"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["datesent"]) : DateTime.MinValue);
                    objEventForm.comments = (dt.Rows[0]["comments"] != DBNull.Value ? dt.Rows[0]["comments"].ToString() : "");
                    objEventForm.eventname = (dt.Rows[0]["eventname"] != DBNull.Value ? dt.Rows[0]["eventname"].ToString() : "");
                    objEventForm.eventid = (dt.Rows[0]["eventid"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["eventid"]) : 0);
                    objEventForm.status2 = (dt.Rows[0]["status2"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["status2"]) : false);



                }
            }
            return objEventForm;
        }



        public List<TCAssociationTool.Entities.EventForm> GetEventFormListByVariable(Int64 eventid,string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.EventForm> lstEventForm = new List<TCAssociationTool.Entities.EventForm>();
            DataTable dt = _EventForm.GetEventFormListByVariable(eventid, Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _EventForm.GetEventFormListByVariable(eventid, Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.EventForm objEventForm = new TCAssociationTool.Entities.EventForm();

                    objEventForm.RId = Convert.ToInt64(dr["RId"].ToString());
                    objEventForm.Id = Convert.ToInt64(dr["Id"].ToString());
                    objEventForm.firstname = (dr["firstname"] != DBNull.Value ? dr["firstname"].ToString() : "");
                    objEventForm.lastname = (dr["lastname"] != DBNull.Value ? dr["lastname"].ToString() : "");
                    objEventForm.email = (dr["email"] != DBNull.Value ? dr["email"].ToString() : "");
                    objEventForm.phone = (dr["phone"] != DBNull.Value ? dr["phone"].ToString() : "");
                    objEventForm.address = (dr["address"] != DBNull.Value ? dr["address"].ToString() : "");
                    objEventForm.members = (dr["members"] != DBNull.Value ? Convert.ToInt64(dr["members"]) : 0);
                    objEventForm.city = (dr["city"] != DBNull.Value ? dr["city"].ToString() : "");
                    objEventForm.state = (dr["state"] != DBNull.Value ? dr["state"].ToString() : "");
                    objEventForm.zip = (dr["zip"] != DBNull.Value ? dr["zip"].ToString() : "");
                    objEventForm.ismember = (dr["ismember"] != DBNull.Value ? dr["ismember"].ToString() : "");
                    objEventForm.datesent = (dr["datesent"] != DBNull.Value ? Convert.ToDateTime(dr["datesent"]) : DateTime.MinValue);
                    objEventForm.comments = (dr["comments"] != DBNull.Value ? dr["comments"].ToString() : "");
                    objEventForm.eventname = (dr["eventname"] != DBNull.Value ? dr["eventname"].ToString() : "");
                    objEventForm.eventid = (dr["eventid"] != DBNull.Value ? Convert.ToInt64(dr["eventid"]) : 0);
                    objEventForm.status2 = (dr["status2"] != DBNull.Value ? Convert.ToBoolean(dr["status2"]) : false);

                  

                    lstEventForm.Add(objEventForm);
                }
            }
            return lstEventForm;
        }

        #endregion
    }
}
