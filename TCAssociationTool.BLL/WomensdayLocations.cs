using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TCAssociationTool.BLL
{
   public class WomensdayLocations
    {
        TCAssociationTool.DAL.WomensdayLocations _WomensdayLocations = new TCAssociationTool.DAL.WomensdayLocations();
        #region Methods

        public Int64 InsertWomensdayLocations(Entities.WomensdayLocations objWomensdayLocationss)
        {
            Int64 _status = 0;
            if (objWomensdayLocationss != null)
            {
                _status = _WomensdayLocations.InsertWomensdayLocations(objWomensdayLocationss);

            }
            return _status;
        }

        public Int64 WomensdayLocationsCommentUpdate(Int64 Id)
        {
            Int64 _status = 0;
            if (Id != 0)
            {
                _status = _WomensdayLocations.WomensdayLocationsCommentUpdate(Id);

            }
            return _status;
        }

        public Int64 DeleteWomensdayLocations(Int64 Id)
        {
            Int64 _status = 0;
            _status = _WomensdayLocations.DeleteWomensdayLocations(Id);
            return _status;
        }



        #endregion

        #region Entities filling



        public TCAssociationTool.Entities.WomensdayLocations GetWomensdayLocationsById(Int64 Id, ref int status)
        {
            TCAssociationTool.Entities.WomensdayLocations objWomensdayLocations = new TCAssociationTool.Entities.WomensdayLocations();
            DataTable dt = new DataTable();
            if (Id != 0)
            {
                dt = _WomensdayLocations.GetWomensdayLocationsById(Id, ref status);
                if (dt.Rows.Count == 1)
                {
                    objWomensdayLocations.Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                    objWomensdayLocations.location = (dt.Rows[0]["location"] != DBNull.Value ? dt.Rows[0]["location"].ToString() : "");
                    objWomensdayLocations.adminemail = (dt.Rows[0]["adminemail"] != DBNull.Value ? dt.Rows[0]["adminemail"].ToString() : "");
                    objWomensdayLocations.datemodified = (dt.Rows[0]["datemodified"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["datemodified"]) : DateTime.MinValue);
                    objWomensdayLocations.amount = (dt.Rows[0]["amount"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[0]["amount"]) : 0);
                    objWomensdayLocations.status2 = (dt.Rows[0]["status2"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["status2"]) : false);
                    objWomensdayLocations.event_details = (dt.Rows[0]["event_details"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["event_details"]) : 0);

    
                }
            }
            return objWomensdayLocations;
        }



        public List<TCAssociationTool.Entities.WomensdayLocations> GetWomensdayLocationsListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.WomensdayLocations> lstWomensdayLocations = new List<TCAssociationTool.Entities.WomensdayLocations>();
            DataTable dt = _WomensdayLocations.GetWomensdayLocationsListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _WomensdayLocations.GetWomensdayLocationsListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.WomensdayLocations objWomensdayLocations = new TCAssociationTool.Entities.WomensdayLocations();

                    objWomensdayLocations.RId = Convert.ToInt64(dr["RId"].ToString());
                    objWomensdayLocations.Id = Convert.ToInt64(dr["Id"].ToString());
                    objWomensdayLocations.location = (dr["location"] != DBNull.Value ? dr["location"].ToString() : "");
                    objWomensdayLocations.adminemail = (dr["adminemail"] != DBNull.Value ? dr["adminemail"].ToString() : "");
                    objWomensdayLocations.amount = (dr["amount"] != DBNull.Value ? Convert.ToDecimal(dr["amount"]) : 0);
                    objWomensdayLocations.datemodified = (dr["datemodified"] != DBNull.Value ? Convert.ToDateTime(dr["datemodified"]) : DateTime.MinValue);
                    objWomensdayLocations.status2 = (dr["status2"] != DBNull.Value ? Convert.ToBoolean(dr["status2"]) : false);
                    objWomensdayLocations.event_details = (dr["event_details"] != DBNull.Value ? Convert.ToInt64(dr["event_details"]) : 0);

                   
                    lstWomensdayLocations.Add(objWomensdayLocations);
                }
            }
            return lstWomensdayLocations;
        }

        #endregion

    }
}
