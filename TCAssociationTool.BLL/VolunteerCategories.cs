using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TCAssociationTool.BLL
{
   public class VolunteerCategories
    {
        TCAssociationTool.DAL.VolunteerCategories _VolunteerCategories = new TCAssociationTool.DAL.VolunteerCategories();

        #region Methods

        public Int64 InsertVolunteerCategories(Entities.VolunteerCategories objVolunteerCategories)
        {
            Int64 _status = 0;
            if (objVolunteerCategories != null)
            {
                _status = _VolunteerCategories.InsertVolunteerCategories(objVolunteerCategories);

            }
            return _status;
        }

        public Int64 DeleteVolunteerCategory(Int64 VolunteerCategoryId)
        {
            Int64 _status = 0;
            _status = _VolunteerCategories.DeleteVolunteerCategory(VolunteerCategoryId);
            return _status;
        }

        public Int64 UpdateVolunteerCategoryStatus(Int64 VolunteerCategoryId)
        {
            Int64 _status = 0;
            _status = _VolunteerCategories.UpdateVolunteerCategoryStatus(VolunteerCategoryId);
            return _status;
        }

        public Int64 UpdateVolunteerCategoryDisplayOrder(int DisplayOrder, Int64 VolunteerCategoryId)
        {
            Int64 _status = 0;
            _status = _VolunteerCategories.UpdateVolunteerCategoryDisplayOrder(DisplayOrder, VolunteerCategoryId);
            return _status;
        }

        #endregion

        #region Entities filling

        public List<TCAssociationTool.Entities.VolunteerCategories> GetVolunteerCategoriesList(ref int status)
        {
            List<TCAssociationTool.Entities.VolunteerCategories> lstVolunteerCategories = new List<Entities.VolunteerCategories>();
            DataTable dt = _VolunteerCategories.GetVolunteerCategoriesList(ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.VolunteerCategories objlstVolunteerCategories = new TCAssociationTool.Entities.VolunteerCategories();

                    objlstVolunteerCategories.VolunteerCategoryId = Convert.ToInt64(dr["VolunteerCategoryId"].ToString());
                    objlstVolunteerCategories.CategoryName = dr["CategoryName"].ToString();
                    objlstVolunteerCategories.OrderNo = (dr["OrderNo"] != DBNull.Value ? Convert.ToInt32(dr["OrderNo"]) : 0);
                    objlstVolunteerCategories.IsActive = Convert.ToBoolean(dr["IsActive"]);

                    lstVolunteerCategories.Add(objlstVolunteerCategories);
                }

            }
            return lstVolunteerCategories;
        }

        public TCAssociationTool.Entities.VolunteerCategories GetVolunteerCategoryById(Int64 VolunteerCategoryId, ref int status)
        {
            TCAssociationTool.Entities.VolunteerCategories objVolunteerCategories = new TCAssociationTool.Entities.VolunteerCategories();
            DataTable dt = new DataTable();
            if (VolunteerCategoryId != 0)
            {
                dt = _VolunteerCategories.GetVolunteerCategoryById(VolunteerCategoryId, ref status);
                if (dt.Rows.Count == 1)
                {
                    objVolunteerCategories.VolunteerCategoryId = Convert.ToInt64(dt.Rows[0]["VolunteerCategoryId"].ToString());
                    objVolunteerCategories.CategoryName = dt.Rows[0]["CategoryName"].ToString();
                    objVolunteerCategories.OrderNo = (dt.Rows[0]["OrderNo"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["OrderNo"]) : 0);
                    objVolunteerCategories.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                }
            }
            return objVolunteerCategories;
        }

        public List<TCAssociationTool.Entities.VolunteerCategories> GetVolunteerCategoriesListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.VolunteerCategories> lstVolunteerCategories = new List<TCAssociationTool.Entities.VolunteerCategories>();
            DataTable dt = _VolunteerCategories.GetVolunteerCategoriesListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _VolunteerCategories.GetVolunteerCategoriesListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.VolunteerCategories objVolunteerCategories = new TCAssociationTool.Entities.VolunteerCategories();

                    objVolunteerCategories.RId = Convert.ToInt64(dr["RId"].ToString());
                    objVolunteerCategories.VolunteerCategoryId = Convert.ToInt64(dr["VolunteerCategoryId"].ToString());
                    objVolunteerCategories.CategoryName = dr["CategoryName"].ToString();
                    objVolunteerCategories.OrderNo = (dr["OrderNo"] != DBNull.Value ? Convert.ToInt32(dr["OrderNo"]) : 0);
                    objVolunteerCategories.IsActive = Convert.ToBoolean(dr["IsActive"]);

                    lstVolunteerCategories.Add(objVolunteerCategories);
                }
            }
            return lstVolunteerCategories;
        }

        #endregion

    }
}
