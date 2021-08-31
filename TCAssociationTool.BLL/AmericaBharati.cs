using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TCAssociationTool.BLL
{
   public class AmericaBharati
    {
        DAL.AmericaBharati _AmericaBharati = new DAL.AmericaBharati();

        #region Methods

        public Int64 DeleteAmericaBharati(Int64 id)
        {
            Int64 _status = 0;
            if (id != 0)
            {
                _status = _AmericaBharati.DeleteAmericaBharati(id);
            }
            return _status;
        }

        public Int64 InsertAmericaBharati(Entities.AmericaBharati objAmericaBharati)
        {
            Int64 _status = 0;
            if (objAmericaBharati != null)
            {
                _status = _AmericaBharati.InsertAmericaBharati(objAmericaBharati);
            }
            return _status;
        }

        public Int64 UpdateAmericaBharatiStatus(Int64 id)
        {
            Int64 _status = 0;
            _status = _AmericaBharati.UpdateAmericaBharatiStatus(id);
            return _status;
        }

        #endregion

        #region Entity Loading

        public List<Entities.AmericaBharati> GetAmericaBharatiListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = _AmericaBharati.GetAmericaBharatiListByVariable(Search, Sort, PageNo, Items, ref Total);
            List<Entities.AmericaBharati> lstAmericaBharati = new List<Entities.AmericaBharati>();

            if (dt.Rows.Count == 0 && PageNo > 1)
            {
                dt = _AmericaBharati.GetAmericaBharatiListByVariable(Search, Sort, PageNo, Items, ref Total);
            }

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.AmericaBharati objAmericaBharati = new Entities.AmericaBharati();

                    objAmericaBharati.RId = Convert.ToInt64(dr["Rid"]);
                    objAmericaBharati.id = Convert.ToInt64(dr["id"]);
                    objAmericaBharati.heading = dr["heading"].ToString();
                    objAmericaBharati.description = dr["description"].ToString();
                    objAmericaBharati.pagetitle = (dr["pagetitle"] != DBNull.Value ? dr["pagetitle"].ToString() : null);
                    objAmericaBharati.metakeywords = (dr["metakeywords"] != DBNull.Value ? dr["metakeywords"].ToString() : null);
                    objAmericaBharati.metadesc = (dr["metadesc"] != DBNull.Value ? dr["metadesc"].ToString() : null);
                    objAmericaBharati.pageurl = (dr["pageurl"] != DBNull.Value ? dr["pageurl"].ToString() : null);
                    objAmericaBharati.filename = (dr["filename"] != DBNull.Value ? dr["filename"].ToString() : null);
                    objAmericaBharati.status2 = (dr["status2"] != DBNull.Value ? Convert.ToBoolean(dr["status2"].ToString()) : false);
                    objAmericaBharati.datemodified = Convert.ToDateTime(dr["datemodified"]);
                  

                    lstAmericaBharati.Add(objAmericaBharati);
                }
            }
            return lstAmericaBharati;
        }

        public Entities.AmericaBharati GetAmericaBharatiById(Int64 id, ref int status)
        {
            DataTable dt = _AmericaBharati.GetAmericaBharatiById(id, ref status);
            Entities.AmericaBharati objAmericaBharati = new Entities.AmericaBharati();

            if (dt.Rows.Count == 1)
            {
              
                objAmericaBharati.id = Convert.ToInt64(dt.Rows[0]["id"]);
                objAmericaBharati.heading = dt.Rows[0]["heading"].ToString();
                objAmericaBharati.description = dt.Rows[0]["description"].ToString();
                objAmericaBharati.pagetitle = (dt.Rows[0]["pagetitle"] != DBNull.Value ? dt.Rows[0]["pagetitle"].ToString() : null);
                objAmericaBharati.metakeywords = (dt.Rows[0]["metakeywords"] != DBNull.Value ? dt.Rows[0]["metakeywords"].ToString() : null);
                objAmericaBharati.metadesc = (dt.Rows[0]["metadesc"] != DBNull.Value ? dt.Rows[0]["metadesc"].ToString() : null);
                objAmericaBharati.pageurl = (dt.Rows[0]["pageurl"] != DBNull.Value ? dt.Rows[0]["pageurl"].ToString() : null);
                objAmericaBharati.filename = (dt.Rows[0]["filename"] != DBNull.Value ? dt.Rows[0]["filename"].ToString() : null);
                objAmericaBharati.status2 = (dt.Rows[0]["status2"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["status2"].ToString()) : false);
                objAmericaBharati.datemodified = Convert.ToDateTime(dt.Rows[0]["datemodified"]);

            }

            return objAmericaBharati;
        }


        public List<TCAssociationTool.Entities.AmericaBharati> GetAmericaBharatiList(ref int status)
        {
            List<TCAssociationTool.Entities.AmericaBharati> lstAmericaBharati = new List<Entities.AmericaBharati>();
            DataTable dt = _AmericaBharati.GetAmericaBharatiList(ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.AmericaBharati objAmericaBharati = new TCAssociationTool.Entities.AmericaBharati();

                    objAmericaBharati.id = Convert.ToInt64(dr["id"]);
                    objAmericaBharati.heading = dr["heading"].ToString();
                    objAmericaBharati.description = dr["description"].ToString();
                    objAmericaBharati.pagetitle = (dr["pagetitle"] != DBNull.Value ? dr["pagetitle"].ToString() : null);
                    objAmericaBharati.metakeywords = (dr["metakeywords"] != DBNull.Value ? dr["metakeywords"].ToString() : null);
                    objAmericaBharati.metadesc = (dr["metadesc"] != DBNull.Value ? dr["metadesc"].ToString() : null);
                    objAmericaBharati.pageurl = (dr["pageurl"] != DBNull.Value ? dr["pageurl"].ToString() : null);
                    objAmericaBharati.filename = (dr["filename"] != DBNull.Value ? dr["filename"].ToString() : null);
                    objAmericaBharati.status2 = (dr["status2"] != DBNull.Value ? Convert.ToBoolean(dr["status2"].ToString()) : false);
                    objAmericaBharati.datemodified = Convert.ToDateTime(dr["datemodified"]);

                    lstAmericaBharati.Add(objAmericaBharati);
                }

            }
            return lstAmericaBharati;
        }
        #endregion
    }
}
