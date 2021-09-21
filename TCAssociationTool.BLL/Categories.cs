using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TCAssociationTool.BLL
{
   public class Categories
    {
        TCAssociationTool.DAL.Categories _Categories = new TCAssociationTool.DAL.Categories();

        #region Methods

        public Int64 InsertCategories(Entities.Categories objCategories)
        {
            Int64 _status = 0;
            if (objCategories != null)
            {
                _status = _Categories.InsertCategories(objCategories);

            }
            return _status;
        }

        public Int64 DeleteCategories(Int64 id)
        {
            Int64 _status = 0;
            _status = _Categories.DeleteCategories(id);
            return _status;
        }

       
        #endregion

        #region Entities filling

        public List<TCAssociationTool.Entities.Categories> GetCategoriesList(ref int status)
        {
            List<TCAssociationTool.Entities.Categories> lstCategories = new List<Entities.Categories>();
            DataTable dt = _Categories.GetCategoriesList(ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Categories objlstCategories = new TCAssociationTool.Entities.Categories();

                    objlstCategories.id = Convert.ToInt64(dr["id"].ToString());
                    objlstCategories.catname = dr["catname"].ToString();
                    objlstCategories.datemodified = Convert.ToDateTime(dt.Rows[0]["datemodified"].ToString());

                    lstCategories.Add(objlstCategories);
                }

            }
            return lstCategories;
        }

        public TCAssociationTool.Entities.Categories GetCategoryById(Int64 id, ref int status)
        {
            TCAssociationTool.Entities.Categories objCategories = new TCAssociationTool.Entities.Categories();
            DataTable dt = new DataTable();
            if (id != 0)
            {
                dt = _Categories.GetCategoriesById(id, ref status);
                if (dt.Rows.Count == 1)
                {
                    objCategories.id = Convert.ToInt64(dt.Rows[0]["id"].ToString());
                    objCategories.catname = dt.Rows[0]["catname"].ToString();
                    objCategories.datemodified = Convert.ToDateTime(dt.Rows[0]["datemodified"].ToString());
                }
            }
            return objCategories;
        }

       
       public List<TCAssociationTool.Entities.Categories> GetCategoriesListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.Categories> lstCategories = new List<TCAssociationTool.Entities.Categories>();
            DataTable dt = _Categories.GetCategoriesListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _Categories.GetCategoriesListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Categories objCategories = new TCAssociationTool.Entities.Categories();

                    objCategories.RId = Convert.ToInt64(dr["RId"].ToString());
                    objCategories.id = Convert.ToInt64(dr["id"].ToString());
                    objCategories.catname = dr["catname"].ToString();
                    objCategories.datemodified = Convert.ToDateTime(dr["datemodified"].ToString());

                    lstCategories.Add(objCategories);
                }
            }
            return lstCategories;
        }

        #endregion
    }
}
