using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TCAssociationTool.BLL
{
  public  class CulturalRegistrations
    {
        TCAssociationTool.DAL.CulturalRegistrations _CulturalRegistrations = new TCAssociationTool.DAL.CulturalRegistrations();
        #region Methods

        public Int64 InsertCulturalRegistrations(Entities.CulturalRegistrations objCulturalRegistrations)
        {
            Int64 _status = 0;
            if (objCulturalRegistrations != null)
            {
                _status = _CulturalRegistrations.InsertCulturalRegistrations(objCulturalRegistrations);

            }
            return _status;
        }

        public Int64 CulturalRegistrationsCommentUpdate(Entities.CulturalRegistrations objCulturalRegistrations)
        {
            Int64 _status = 0;
            if (objCulturalRegistrations != null)
            {
                _status = _CulturalRegistrations.CulturalRegistrationsCommentUpdate(objCulturalRegistrations);

            }
            return _status;
        }


        public Int64 DeleteCulturalRegistrations(Int64 Id)
        {
            Int64 _status = 0;
            _status = _CulturalRegistrations.DeleteCulturalRegistrations(Id);
            return _status;
        }
        public Int64 CulturalRegistrationsDeleteAll(string Id)
        {
            Int64 _status = 0;
            _status = _CulturalRegistrations.CulturalRegistrationsDeleteAll(Id);
            return _status;
        }

        public Int64 UpdateCulturalRegistrationsStatus(Int64 id)
        {
            Int64 _status = 0;
            _status = _CulturalRegistrations.UpdateCulturalRegistrationsStatus(id);
            return _status;
        }



        #endregion

        #region Entities filling



        public TCAssociationTool.Entities.CulturalRegistrations GetCulturalRegistrationsById(Int64 Id, ref int status)
        {
            TCAssociationTool.Entities.CulturalRegistrations objCulturalRegistrations = new TCAssociationTool.Entities.CulturalRegistrations();
            DataTable dt = new DataTable();
            if (Id != 0)
            {
                dt = _CulturalRegistrations.GetCulturalRegistrationsById(Id, ref status);
                if (dt.Rows.Count == 1)
                {
                    objCulturalRegistrations.Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                    objCulturalRegistrations.item_title = (dt.Rows[0]["item_title"] != DBNull.Value ? dt.Rows[0]["item_title"].ToString() : "");
                    objCulturalRegistrations.item_type = (dt.Rows[0]["item_type"] != DBNull.Value ? dt.Rows[0]["item_type"].ToString() : "");
                    objCulturalRegistrations.item_description = (dt.Rows[0]["item_description"] != DBNull.Value ? dt.Rows[0]["item_description"].ToString() : "");
                    objCulturalRegistrations.minutes = (dt.Rows[0]["minutes"] != DBNull.Value ? dt.Rows[0]["minutes"].ToString() : "");
                    objCulturalRegistrations.group_name = (dt.Rows[0]["group_name"] != DBNull.Value ? dt.Rows[0]["adgroup_namedress"].ToString() : "");
                    objCulturalRegistrations.name = (dt.Rows[0]["name"] != DBNull.Value ? dt.Rows[0]["name"].ToString() : "");
                    objCulturalRegistrations.choreographer = (dt.Rows[0]["choreographer"] != DBNull.Value ? dt.Rows[0]["choreographer"].ToString() : "");
                    objCulturalRegistrations.email = (dt.Rows[0]["email"] != DBNull.Value ? dt.Rows[0]["email"].ToString() : "");
                    objCulturalRegistrations.mobile = (dt.Rows[0]["mobile"] != DBNull.Value ? dt.Rows[0]["mobile"].ToString() : "");
                    objCulturalRegistrations.address1 = (dt.Rows[0]["address1"] != DBNull.Value ? dt.Rows[0]["address1"].ToString() : "");
                    objCulturalRegistrations.address2 = (dt.Rows[0]["address2"] != DBNull.Value ? dt.Rows[0]["address2"].ToString() : "");
                    objCulturalRegistrations.city = (dt.Rows[0]["city"] != DBNull.Value ? dt.Rows[0]["city"].ToString() : "");
                    objCulturalRegistrations.address1 = (dt.Rows[0]["address1"] != DBNull.Value ? dt.Rows[0]["address1"].ToString() : "");
                    objCulturalRegistrations.address2 = (dt.Rows[0]["address2"] != DBNull.Value ? dt.Rows[0]["address2"].ToString() : "");
                    objCulturalRegistrations.city = (dt.Rows[0]["city"] != DBNull.Value ? dt.Rows[0]["city"].ToString() : "");
                    objCulturalRegistrations.state = (dt.Rows[0]["state"] != DBNull.Value ? dt.Rows[0]["state"].ToString() : "");
                    objCulturalRegistrations.zip = (dt.Rows[0]["zip"] != DBNull.Value ? dt.Rows[0]["zip"].ToString() : "");
                    objCulturalRegistrations.url = (dt.Rows[0]["url"] != DBNull.Value ? dt.Rows[0]["url"].ToString() : "");
                    objCulturalRegistrations.date_created = (dt.Rows[0]["date_created"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["date_created"]) : DateTime.MinValue);
                    objCulturalRegistrations.status2 = (dt.Rows[0]["status2"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["status2"]) : false);
                    objCulturalRegistrations.date_modified = (dt.Rows[0]["date_modified"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["date_modified"]) : DateTime.MinValue);
                    objCulturalRegistrations.comments = (dt.Rows[0]["comments"] != DBNull.Value ? dt.Rows[0]["comments"].ToString() : "");

              

                }
            }
            return objCulturalRegistrations;
        }



        public List<TCAssociationTool.Entities.CulturalRegistrations> GetCulturalRegistrationsListByVariable(string ItemType,string StartDate, string EndDate, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.CulturalRegistrations> lstCulturalRegistrations = new List<TCAssociationTool.Entities.CulturalRegistrations>();
            DataTable dt = _CulturalRegistrations.GetCulturalRegistrationsListByVariable(ItemType,StartDate, EndDate, Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _CulturalRegistrations.GetCulturalRegistrationsListByVariable(ItemType,StartDate, EndDate, Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.CulturalRegistrations objCulturalRegistrations = new TCAssociationTool.Entities.CulturalRegistrations();

                    objCulturalRegistrations.RId = Convert.ToInt64(dr["RId"].ToString());
                    objCulturalRegistrations.Id = Convert.ToInt64(dr["Id"].ToString());
                    objCulturalRegistrations.item_title = (dr["item_title"] != DBNull.Value ? dr["item_title"].ToString() : "");
                    objCulturalRegistrations.item_type = (dr["item_type"] != DBNull.Value ? dr["item_type"].ToString() : "");
                    objCulturalRegistrations.item_description = (dr["item_description"] != DBNull.Value ? dr["item_description"].ToString() : "");
                    objCulturalRegistrations.minutes = (dr["minutes"] != DBNull.Value ? dr["minutes"].ToString() : "");
                    objCulturalRegistrations.group_name = (dr["group_name"] != DBNull.Value ? dr["group_name"].ToString() : "");
                    objCulturalRegistrations.name = (dr["name"] != DBNull.Value ? dr["name"].ToString() : "");
                    objCulturalRegistrations.choreographer = (dr["choreographer"] != DBNull.Value ? dr["choreographer"].ToString() : "");
                    objCulturalRegistrations.email = (dr["email"] != DBNull.Value ? dr["email"].ToString() : "");
                    objCulturalRegistrations.mobile = (dr["mobile"] != DBNull.Value ? dr["mobile"].ToString() : "");
                    objCulturalRegistrations.address1 = (dr["address1"] != DBNull.Value ? dr["address1"].ToString() : "");
                    objCulturalRegistrations.address2 = (dr["address2"] != DBNull.Value ? dr["address2"].ToString() : "");
                    objCulturalRegistrations.city = (dr["city"] != DBNull.Value ? dr["city"].ToString() : "");
                    objCulturalRegistrations.state = (dr["state"] != DBNull.Value ? dr["state"].ToString() : "");
                    objCulturalRegistrations.zip = (dr["zip"] != DBNull.Value ? dr["zip"].ToString() : "");
                    objCulturalRegistrations.url = (dr["url"] != DBNull.Value ? dr["url"].ToString() : "");
                    objCulturalRegistrations.date_created = (dr["date_created"] != DBNull.Value ? Convert.ToDateTime(dr["date_created"]) : DateTime.MinValue);
                    objCulturalRegistrations.status2 = (dr["status2"] != DBNull.Value ? Convert.ToBoolean(dr["status2"]) : false);
                    objCulturalRegistrations.date_modified = (dr["date_modified"] != DBNull.Value ? Convert.ToDateTime(dr["date_modified"]) : DateTime.MinValue);
                    objCulturalRegistrations.comments = (dr["comments"] != DBNull.Value ? dr["comments"].ToString() : "");

      

                    lstCulturalRegistrations.Add(objCulturalRegistrations);
                }
            }
            return lstCulturalRegistrations;
        }

        #endregion
    }
}
