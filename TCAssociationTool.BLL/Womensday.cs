using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TCAssociationTool.BLL
{
 public   class Womensday
    {
        TCAssociationTool.DAL.Womensday _Womensday = new TCAssociationTool.DAL.Womensday();
        #region Methods

        public Int64 InsertWomensday(Entities.Womensday objWomensdays)
        {
            Int64 _status = 0;
            if (objWomensdays != null)
            {
                _status = _Womensday.InsertWomensday(objWomensdays);

            }
            return _status;
        }

        public Int64 WomensdayUpdateComments(Entities.Womensday objWomensdays)
        {
            Int64 _status = 0;
            if (objWomensdays != null)
            {
                _status = _Womensday.WomensdayUpdateComments(objWomensdays);

            }
            return _status;
        }

        public Int64 DeleteWomensday(Int64 Id)
        {
            Int64 _status = 0;
            _status = _Womensday.DeleteWomensday(Id);
            return _status;
        }



        #endregion

        #region Entities filling



        public Entities.Womensday GetWomensdayById(Int64 Id, ref List<Entities.Womensdayguests> lstWomensdayguests, ref int status)
        {
            DataSet ds = _Womensday.GetWomensdayById(Id, ref status);
            Entities.Womensday objWomensday = new Entities.Womensday();

            if (ds.Tables[0].Rows.Count == 1)
            {
                objWomensday.Id = Convert.ToInt64(ds.Tables[0].Rows[0]["Id"].ToString());
                objWomensday.firstname = (ds.Tables[0].Rows[0]["firstname"] != DBNull.Value ? ds.Tables[0].Rows[0]["firstname"].ToString() : "");
                objWomensday.lastname = (ds.Tables[0].Rows[0]["lastname"] != DBNull.Value ? ds.Tables[0].Rows[0]["lastname"].ToString() : "");
                objWomensday.email = (ds.Tables[0].Rows[0]["email"] != DBNull.Value ? ds.Tables[0].Rows[0]["email"].ToString() : "");
                objWomensday.phone = (ds.Tables[0].Rows[0]["phone"] != DBNull.Value ? ds.Tables[0].Rows[0]["phone"].ToString() : "");
                objWomensday.datesent = (ds.Tables[0].Rows[0]["datesent"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["datesent"]) : DateTime.MinValue);
                objWomensday.amount = (ds.Tables[0].Rows[0]["amount"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"]) : 0);
                objWomensday.address = (ds.Tables[0].Rows[0]["address"] != DBNull.Value ? ds.Tables[0].Rows[0]["address"].ToString() : "");
                objWomensday.location = (ds.Tables[0].Rows[0]["location"] != DBNull.Value ? ds.Tables[0].Rows[0]["location"].ToString() : "");
                objWomensday.comments = (ds.Tables[0].Rows[0]["comments"] != DBNull.Value ? ds.Tables[0].Rows[0]["comments"].ToString() : "");
                objWomensday.city = (ds.Tables[0].Rows[0]["city"] != DBNull.Value ? ds.Tables[0].Rows[0]["city"].ToString() : "");
                objWomensday.state = (ds.Tables[0].Rows[0]["state"] != DBNull.Value ? ds.Tables[0].Rows[0]["state"].ToString() : "");
                objWomensday.event_id = (ds.Tables[0].Rows[0]["event_id"] != DBNull.Value ? Convert.ToInt64(ds.Tables[0].Rows[0]["event_id"]) : 0);
            }

            if (ds.Tables[1].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    TCAssociationTool.Entities.Womensdayguests objWomensdayguests = new TCAssociationTool.Entities.Womensdayguests();
                    objWomensdayguests.Id = Convert.ToInt64(dr["Id"].ToString());
                    objWomensdayguests.wid = Convert.ToInt64(dr["wid"].ToString());
                    objWomensdayguests.firstname = dr["firstname"].ToString();
                    objWomensdayguests.lastname = dr["lastname"].ToString();
                    objWomensdayguests.email = dr["email"].ToString();

                    lstWomensdayguests.Add(objWomensdayguests);

                   
          }
            }

            return objWomensday;
        }



        public List<TCAssociationTool.Entities.Womensday> GetWomensdayListByVariable(string location,string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.Womensday> lstWomensday = new List<TCAssociationTool.Entities.Womensday>();
            DataTable dt = _Womensday.GetWomensdayListByVariable(location,Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _Womensday.GetWomensdayListByVariable(location,Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Womensday objWomensday = new TCAssociationTool.Entities.Womensday();

                    objWomensday.RId = Convert.ToInt64(dr["RId"].ToString());
                    objWomensday.Id = Convert.ToInt64(dr["Id"].ToString());
                    objWomensday.firstname = (dr["firstname"] != DBNull.Value ? dr["firstname"].ToString() : "");
                    objWomensday.lastname = (dr["lastname"] != DBNull.Value ? dr["lastname"].ToString() : "");
                    objWomensday.email = (dr["email"] != DBNull.Value ? dr["email"].ToString() : "");
                    objWomensday.phone = (dr["phone"] != DBNull.Value ? dr["phone"].ToString() : "");
                    objWomensday.amount = (dr["amount"] != DBNull.Value ? Convert.ToDecimal(dr["amount"]) : 0);
                    objWomensday.datesent = (dr["datesent"] != DBNull.Value ? Convert.ToDateTime(dr["datesent"]) : DateTime.MinValue);
                    objWomensday.address = (dr["address"] != DBNull.Value ? dr["address"].ToString() : "");
                    objWomensday.location = (dr["location"] != DBNull.Value ? dr["location"].ToString() : "");
                    objWomensday.comments = (dr["comments"] != DBNull.Value ? dr["comments"].ToString() : "");
                    objWomensday.city = (dr["city"] != DBNull.Value ? dr["city"].ToString() : "");
                    objWomensday.state = (dr["state"] != DBNull.Value ? dr["state"].ToString() : "");
                    objWomensday.event_id = (dr["event_id"] != DBNull.Value ? Convert.ToInt64(dr["event_id"]) : 0);


                    lstWomensday.Add(objWomensday);
                }
            }
            return lstWomensday;
        }

        #endregion
    }
}
