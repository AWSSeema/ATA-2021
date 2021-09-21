using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TCAssociationTool.BLL
{
   public class SubCategories
    {
        TCAssociationTool.DAL.SubCategories _SubCategories = new TCAssociationTool.DAL.SubCategories();

        #region Methods

        public Int64 InsertSubCategories(Entities.SubCategories objSubCategories)
        {
            Int64 _status = 0;
            if (objSubCategories != null)
            {
                _status = _SubCategories.InsertSubCategories(objSubCategories);

            }
            return _status;
        }

        public Int64 DeleteSubCategories(Int64 id)
        {
            Int64 _status = 0;
            _status = _SubCategories.DeleteSubCategories(id);
            return _status;
        }
        public Int64 UpdateSubCategoryDisplayOrder(int orderno, Int64 id)
        {
            Int64 _status = 0;
            _status = _SubCategories.UpdateSubCategoryDisplayOrder(orderno, id);
            return _status;
        }

        #endregion

        #region Entities filling

        public List<TCAssociationTool.Entities.SubCategories> GetSubCategoriesList(Int64 id, ref int status)
        {
            List<TCAssociationTool.Entities.SubCategories> lstSubCategories = new List<Entities.SubCategories>();
            DataTable dt = _SubCategories.GetSubCategoriesList(id,ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.SubCategories objlstSubCategories = new TCAssociationTool.Entities.SubCategories();

                    objlstSubCategories.id = Convert.ToInt64(dr["id"].ToString());
                    objlstSubCategories.scname = dr["scname"].ToString();
                    objlstSubCategories.catid = Convert.ToInt64(dr["catid"].ToString());
                    objlstSubCategories.datemodified = Convert.ToDateTime(dt.Rows[0]["datemodified"].ToString());
                    objlstSubCategories.orderno = (dr["orderno"] != DBNull.Value ? Convert.ToInt32(dr["orderno"]) : 0);
                    objlstSubCategories.pageurl = (dr["pageurl"] != DBNull.Value ?(dr["orderno"]).ToString() : null);

                    lstSubCategories.Add(objlstSubCategories);
                }

            }
            return lstSubCategories;
        }

        public TCAssociationTool.Entities.SubCategories GetSubCategoryById(Int64 id, ref int status)
        {
            TCAssociationTool.Entities.SubCategories objSubCategories = new TCAssociationTool.Entities.SubCategories();
            DataTable dt = new DataTable();
            if (id != 0)
            {
                dt = _SubCategories.GetSubCategoriesById(id, ref status);
                if (dt.Rows.Count == 1)
                {
                    objSubCategories.id = Convert.ToInt64(dt.Rows[0]["id"].ToString());
                    objSubCategories.scname = dt.Rows[0]["scname"].ToString();
                    objSubCategories.catid = Convert.ToInt64(dt.Rows[0]["catid"].ToString());
                    objSubCategories.datemodified = Convert.ToDateTime(dt.Rows[0]["datemodified"].ToString());
                    objSubCategories.orderno = (dt.Rows[0]["orderno"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["orderno"]) : 0);
                    objSubCategories.pageurl = (dt.Rows[0]["pageurl"] != DBNull.Value ? (dt.Rows[0]["pageurl"]).ToString() : null);
                }
            }
            return objSubCategories;
        }

        public List<TCAssociationTool.Entities.SubCategories> GetSubCategoriesListByVariable(string Search,Int64 catid, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.SubCategories> lstSubCategories = new List<TCAssociationTool.Entities.SubCategories>();
            DataTable dt = _SubCategories.GetSubCategoriesListByVariable(Search, catid, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _SubCategories.GetSubCategoriesListByVariable(Search, catid, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.SubCategories objSubCategories = new TCAssociationTool.Entities.SubCategories();

                    objSubCategories.RId = Convert.ToInt64(dr["RId"].ToString());
                    objSubCategories.id = Convert.ToInt64(dr["id"].ToString());
                    objSubCategories.scname = dr["scname"].ToString();
                    objSubCategories.catid = Convert.ToInt64(dr["catid"].ToString());
                    objSubCategories.datemodified = Convert.ToDateTime(dt.Rows[0]["datemodified"].ToString());
                    objSubCategories.orderno = (dr["orderno"] != DBNull.Value ? Convert.ToInt32(dr["orderno"]) : 0);
                    objSubCategories.pageurl = (dr["pageurl"] != DBNull.Value ? (dr["orderno"]).ToString() : null);
                    objSubCategories.catname = (dr["catname"] != DBNull.Value ? (dr["catname"]).ToString() : null);

                    lstSubCategories.Add(objSubCategories);
                }
            }
            return lstSubCategories;
        }

        #endregion
    }
}
