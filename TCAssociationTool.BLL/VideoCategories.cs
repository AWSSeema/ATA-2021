using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TCAssociationTool.BLL
{
   public  class VideoCategories
    {
       TCAssociationTool.DAL.VideoCategories _VideoCategories = new TCAssociationTool.DAL.VideoCategories();

        #region Methods

       public Int64 InsertVideoCategories(Entities.VideoCategories objVideoCategories)
        {
            Int64 _status = 0;
            if (objVideoCategories != null )
            {
                _status = _VideoCategories.InsertVideoCategories(objVideoCategories);

            }
            return _status;
        }

       public Int64 DeleteVideoCategory(Int64 VideoCategoryId)
        {
            Int64 _status = 0;
            _status = _VideoCategories.DeleteVideoCategory(VideoCategoryId);
            return _status;
        }

        #endregion

        #region Entities filling

       public List<TCAssociationTool.Entities.VideoCategories> GetVideoCategoriesList(ref int status)
        {
            List<TCAssociationTool.Entities.VideoCategories> lstVideoCategories = new List<Entities.VideoCategories>();
            System.Data.DataTable dt = _VideoCategories.GetVideoCategoriesList(ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.VideoCategories objlstVideoCategories = new TCAssociationTool.Entities.VideoCategories();

                    objlstVideoCategories.VideoCategoryId = Convert.ToInt64(dr["VideoCategoryId"].ToString());
                    objlstVideoCategories.CategoryName = (dr["CategoryName"] != DBNull.Value ? dr["CategoryName"].ToString() : "");
                    objlstVideoCategories.Year = Convert.ToInt64(dr["Year"].ToString());
                    objlstVideoCategories.UpdatedBy = dr["UpdatedBy"].ToString();
                    objlstVideoCategories.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"].ToString());


                    lstVideoCategories.Add(objlstVideoCategories);
                }

            }
            return lstVideoCategories;
        }

       public TCAssociationTool.Entities.VideoCategories GetVideoCategoryById(Int64 VideoCategoryId, ref int status)
        {
            TCAssociationTool.Entities.VideoCategories objVideoCategories = new TCAssociationTool.Entities.VideoCategories();
            DataTable dt = new DataTable();
            if (VideoCategoryId != 0)
            {
                dt = _VideoCategories.GetVideoCategoryById(VideoCategoryId, ref status);
                if (dt.Rows.Count == 1)
                {
                    objVideoCategories.VideoCategoryId = Convert.ToInt64(dt.Rows[0]["VideoCategoryId"].ToString());
                    objVideoCategories.CategoryName = (dt.Rows[0]["CategoryName"] != DBNull.Value ? dt.Rows[0]["CategoryName"].ToString() : "");
                    objVideoCategories.Year = Convert.ToInt64(dt.Rows[0]["Year"].ToString());
                    objVideoCategories.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                    objVideoCategories.UpdatedTime = Convert.ToDateTime(dt.Rows[0]["UpdatedTime"].ToString());

                }
            }
            return objVideoCategories;
        }

       public List<TCAssociationTool.Entities.VideoCategories> GetVideoCategoriesListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.VideoCategories> lstVideoCategories = new List<TCAssociationTool.Entities.VideoCategories>();
            DataTable dt = _VideoCategories.GetVideoCategoryListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _VideoCategories.GetVideoCategoryListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.VideoCategories objVideoCategories = new TCAssociationTool.Entities.VideoCategories();

                    objVideoCategories.RId = Convert.ToInt64(dr["RId"].ToString());
                    objVideoCategories.VideoCategoryId = Convert.ToInt64(dr["VideoCategoryId"].ToString());
                    objVideoCategories.CategoryName = dr["CategoryName"].ToString();
                    objVideoCategories.Year = Convert.ToInt64(dr["Year"].ToString());
                    objVideoCategories.UpdatedBy = dr["UpdatedBy"].ToString();
                    objVideoCategories.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"].ToString());
                    objVideoCategories.VideosCount = Convert.ToInt64(dr["VideosCount"].ToString());

                    lstVideoCategories.Add(objVideoCategories);
                }
            }
            return lstVideoCategories;
        }

        #endregion
    }
}
