using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TCAssociationTool.BLL
{
    public class DonationCategories
    {
        TCAssociationTool.DAL.DonationCategories _DonationCategories = new TCAssociationTool.DAL.DonationCategories();

        #region Methods

        public Int64 InsertDonationCategories(Entities.DonationCategories objDonationCategories)
        {
            Int64 _status = 0;
            if (objDonationCategories != null)
            {
                _status = _DonationCategories.InsertDonationCategories(objDonationCategories);

            }
            return _status;
        }

        public Int64 DeleteDonationCategory(Int64 DonationCategoryId)
        {
            Int64 _status = 0;
            _status = _DonationCategories.DeleteDonationCategory(DonationCategoryId);
            return _status;
        }

        public Int64 UpdateDonationCategoryStatus(Int64 DonationCategoryId)
        {
            Int64 _status = 0;
            _status = _DonationCategories.UpdateDonationCategoryStatus(DonationCategoryId);
            return _status;
        }

        public Int64 UpdateDonationCategoryDisplayOrder(int DisplayOrder, Int64 DonationCategoryId)
        {
            Int64 _status = 0;
            _status = _DonationCategories.UpdateDonationCategoryDisplayOrder(DisplayOrder, DonationCategoryId);
            return _status;
        }

        #endregion

        #region Entities filling

        public List<TCAssociationTool.Entities.DonationCategories> GetDonationCategoriesList(ref int status)
        {
            List<TCAssociationTool.Entities.DonationCategories> lstDonationCategories = new List<Entities.DonationCategories>();
            DataTable dt = _DonationCategories.GetDonationCategoriesList(ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.DonationCategories objlstDonationCategories = new TCAssociationTool.Entities.DonationCategories();

                    objlstDonationCategories.DonationCategoryId = Convert.ToInt64(dr["DonationCategoryId"].ToString());
                    objlstDonationCategories.CategoryName = dr["CategoryName"].ToString();
                    objlstDonationCategories.OrderNo = (dr["OrderNo"] != DBNull.Value ? Convert.ToInt32(dr["OrderNo"]) : 0);
                    objlstDonationCategories.IsActive = Convert.ToBoolean(dr["IsActive"]);

                    lstDonationCategories.Add(objlstDonationCategories);
                }

            }
            return lstDonationCategories;
        }

        public TCAssociationTool.Entities.DonationCategories GetDonationCategoryById(Int64 DonationCategoryId, ref int status)
        {
            TCAssociationTool.Entities.DonationCategories objDonationCategories = new TCAssociationTool.Entities.DonationCategories();
            DataTable dt = new DataTable();
            if (DonationCategoryId != 0)
            {
                dt = _DonationCategories.GetDonationCategoryById(DonationCategoryId, ref status);
                if (dt.Rows.Count == 1)
                {
                    objDonationCategories.DonationCategoryId = Convert.ToInt64(dt.Rows[0]["DonationCategoryId"].ToString());
                    objDonationCategories.CategoryName = dt.Rows[0]["CategoryName"].ToString();
                    objDonationCategories.OrderNo = (dt.Rows[0]["OrderNo"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["OrderNo"]) : 0);
                    objDonationCategories.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                }
            }
            return objDonationCategories;
        }

        public List<TCAssociationTool.Entities.DonationCategories> GetDonationCategoriesListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.DonationCategories> lstDonationCategories = new List<TCAssociationTool.Entities.DonationCategories>();
            DataTable dt = _DonationCategories.GetDonationCategoriesListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _DonationCategories.GetDonationCategoriesListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.DonationCategories objDonationCategories = new TCAssociationTool.Entities.DonationCategories();

                    objDonationCategories.RId = Convert.ToInt64(dr["RId"].ToString());
                    objDonationCategories.DonationCategoryId = Convert.ToInt64(dr["DonationCategoryId"].ToString());
                    objDonationCategories.CategoryName = dr["CategoryName"].ToString();
                    objDonationCategories.OrderNo = (dr["OrderNo"] != DBNull.Value ? Convert.ToInt32(dr["OrderNo"]) : 0);
                    objDonationCategories.IsActive = Convert.ToBoolean(dr["IsActive"]);

                    lstDonationCategories.Add(objDonationCategories);
                }
            }
            return lstDonationCategories;
        }
        
        #endregion

        #region Front-end

        public List<TCAssociationTool.Entities.DonationCategories> FEDonationCategoriesGetList(ref int status)
        {
            List<TCAssociationTool.Entities.DonationCategories> lstDonationCategories = new List<Entities.DonationCategories>();
            DataTable dt = _DonationCategories.FEDonationCategoriesGetList(ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.DonationCategories objlstDonationCategories = new TCAssociationTool.Entities.DonationCategories();

                    objlstDonationCategories.DonationCategoryId = Convert.ToInt64(dr["DonationCategoryId"].ToString());
                    objlstDonationCategories.CategoryName = dr["CategoryName"].ToString();
                    objlstDonationCategories.OrderNo = (dr["OrderNo"] != DBNull.Value ? Convert.ToInt32(dr["OrderNo"]) : 0);
                    objlstDonationCategories.IsActive = Convert.ToBoolean(dr["IsActive"]);

                    lstDonationCategories.Add(objlstDonationCategories);
                }

            }
            return lstDonationCategories;
        }

        #endregion
    }
}
