using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TCAssociationTool.BLL
{
   public class CulturalLocations
    {
        TCAssociationTool.DAL.CulturalLocations _CulturalLocations = new TCAssociationTool.DAL.CulturalLocations();
        #region Methods

        public Int64 InsertCulturalLocations(Entities.CulturalLocations objCulturalLocationss)
        {
            Int64 _status = 0;
            if (objCulturalLocationss != null)
            {
                _status = _CulturalLocations.InsertCulturalLocations(objCulturalLocationss);

            }
            return _status;
        }

      
        public Int64 DeleteCulturalLocations(Int64 Id)
        {
            Int64 _status = 0;
            _status = _CulturalLocations.DeleteCulturalLocations(Id);
            return _status;
        }

        public Int64 CulturalLocationsStatus(Int64 Id)
        {
            Int64 _status = 0;
            _status = _CulturalLocations.CulturalLocationsStatus(Id);
            return _status;
        }



        #endregion

        #region Entities filling



        public TCAssociationTool.Entities.CulturalLocations GetCulturalLocationsById(Int64 Id, ref int status)
        {
            TCAssociationTool.Entities.CulturalLocations objCulturalLocations = new TCAssociationTool.Entities.CulturalLocations();
            DataTable dt = new DataTable();
            if (Id != 0)
            {
                dt = _CulturalLocations.GetCulturalLocationsById(Id, ref status);
                if (dt.Rows.Count == 1)
                {
                    objCulturalLocations.Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                    objCulturalLocations.location = (dt.Rows[0]["location"] != DBNull.Value ? dt.Rows[0]["location"].ToString() : "");
                    objCulturalLocations.adminemail = (dt.Rows[0]["adminemail"] != DBNull.Value ? dt.Rows[0]["adminemail"].ToString() : "");
                   

                }
            }
            return objCulturalLocations;
        }

        public List<TCAssociationTool.Entities.CulturalLocations> GetCulturalLocationslist(ref int status)
        {
            List<TCAssociationTool.Entities.CulturalLocations> lstCulturalLocations = new List<TCAssociationTool.Entities.CulturalLocations>();
            DataTable dt = _CulturalLocations.GetCulturalLocationslist( ref status);
            if (dt.Rows.Count == 0)
            {
                dt = _CulturalLocations.GetCulturalLocationslist( ref status);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.CulturalLocations objCulturalLocations = new TCAssociationTool.Entities.CulturalLocations();

                    objCulturalLocations.Id = Convert.ToInt64(dr["Id"].ToString());
                    objCulturalLocations.location = (dr["location"] != DBNull.Value ? dr["location"].ToString() : "");
                  

                    lstCulturalLocations.Add(objCulturalLocations);
                }
            }
            return lstCulturalLocations;
        }


        public List<TCAssociationTool.Entities.CulturalLocations> GetCulturalLocationsListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.CulturalLocations> lstCulturalLocations = new List<TCAssociationTool.Entities.CulturalLocations>();
            DataTable dt = _CulturalLocations.GetCulturalLocationsListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _CulturalLocations.GetCulturalLocationsListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.CulturalLocations objCulturalLocations = new TCAssociationTool.Entities.CulturalLocations();

                    objCulturalLocations.RId = Convert.ToInt64(dr["RId"].ToString());
                    objCulturalLocations.Id = Convert.ToInt64(dr["Id"].ToString());
                    objCulturalLocations.location = (dr["location"] != DBNull.Value ? dr["location"].ToString() : "");
                    objCulturalLocations.adminemail = (dr["adminemail"] != DBNull.Value ? dr["adminemail"].ToString() : "");
                    objCulturalLocations.status2 = (dr["status2"] != DBNull.Value ? Convert.ToBoolean(dr["status2"]) : false);


                    lstCulturalLocations.Add(objCulturalLocations);
                }
            }
            return lstCulturalLocations;
        }

        #endregion
    }
}
