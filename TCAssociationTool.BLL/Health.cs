using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TCAssociationTool.BLL
{
   public class Health
    {
        TCAssociationTool.DAL.Health _Health = new TCAssociationTool.DAL.Health();
        #region Methods

        public Int64 InsertHealth(Entities.Health objHealths)
        {
            Int64 _status = 0;
            if (objHealths != null)
            {
                _status = _Health.InsertHealth(objHealths);

            }
            return _status;
        }

  

        public Int64 DeleteHealth(Int64 Id)
        {
            Int64 _status = 0;
            _status = _Health.DeleteHealth(Id);
            return _status;
        }

        public Int64 HealthUpdateStatus(Int64 Id)
        {
            Int64 _status = 0;
            _status = _Health.HealthUpdateStatus(Id);
            return _status;
        }



        #endregion

        #region Entities filling



        public TCAssociationTool.Entities.Health GetHealthById(Int64 Id, ref int status)
        {
            TCAssociationTool.Entities.Health objHealth = new TCAssociationTool.Entities.Health();
            DataTable dt = new DataTable();
            if (Id != 0)
            {
                dt = _Health.GetHealthById(Id, ref status);
                if (dt.Rows.Count == 1)
                {
                    objHealth.Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                    objHealth.heading = (dt.Rows[0]["heading"] != DBNull.Value ? dt.Rows[0]["heading"].ToString() : "");
                    objHealth.description = (dt.Rows[0]["description"] != DBNull.Value ? dt.Rows[0]["description"].ToString() : "");
                    objHealth.datemodified = (dt.Rows[0]["datemodified"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["datemodified"]) : DateTime.MinValue);
                    objHealth.pagetitle = (dt.Rows[0]["pagetitle"] != DBNull.Value ? dt.Rows[0]["pagetitle"].ToString() : "");
                    objHealth.metakeywords = (dt.Rows[0]["metakeywords"] != DBNull.Value ? dt.Rows[0]["metakeywords"].ToString() : "");
                    objHealth.metadesc = (dt.Rows[0]["metadesc"] != DBNull.Value ? dt.Rows[0]["metadesc"].ToString() : "");
                    objHealth.pageurl = (dt.Rows[0]["pageurl"] != DBNull.Value ? dt.Rows[0]["pageurl"].ToString() : "");
                    objHealth.topics = (dt.Rows[0]["topics"] != DBNull.Value ? dt.Rows[0]["topics"].ToString() : "");
                    objHealth.issuedate = (dt.Rows[0]["issuedate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["issuedate"]) : DateTime.MinValue);

                }
            }
            return objHealth;
        }



        public List<TCAssociationTool.Entities.Health> GetHealthListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.Health> lstHealth = new List<TCAssociationTool.Entities.Health>();
            DataTable dt = _Health.GetHealthListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _Health.GetHealthListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Health objHealth = new TCAssociationTool.Entities.Health();

                    objHealth.RId = Convert.ToInt64(dr["RId"].ToString());
                    objHealth.Id = Convert.ToInt64(dr["Id"].ToString());
                    objHealth.heading = (dr["heading"] != DBNull.Value ? dr["heading"].ToString() : "");
                    objHealth.status2 = (dr["status2"] != DBNull.Value ? Convert.ToBoolean(dr["status2"]) : false);
                    objHealth.issuedate = (dr["issuedate"] != DBNull.Value ? Convert.ToDateTime(dr["issuedate"]) : DateTime.MinValue);


                    lstHealth.Add(objHealth);
                }
            }
            return lstHealth;
        }

        #endregion
    }
}
