using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TCAssociationTool.BLL
{
   public class News
    {
        string logourl = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "/news/";
        TCAssociationTool.DAL.News _News = new TCAssociationTool.DAL.News();

        #region Methods

       public Int64 InsertNews(Entities.News objNews, ref string ImageUrl)
        {
            Int64 _status = 0;
            if (objNews != null )
            {
                _status = _News.InsertNews(objNews, ref ImageUrl);

            }
            return _status;
        }

       public Int64 DeleteNews(Int64 NewsId)
        {
            Int64 _status = 0;
            _status = _News.DeleteNews(NewsId);
            return _status;
        }

       public Int64 UpdateNewsStatus(Int64 NewsId)
       {
           Int64 _status = 0;
           _status = _News.UpdateNewsStatus(NewsId);
           return _status;
       }

       public Int64 UpdateNewsDisplayOrder(int OrderNo, Int64 NewsId)
       {
           Int64 _status = 0;
           _status = _News.UpdateNewsDisplayOrder(OrderNo, NewsId);
           return _status;
       }

        #endregion

        #region Entities filling

       public List<TCAssociationTool.Entities.News> GetNewsList(ref int status)
        {
            List<TCAssociationTool.Entities.News> lstNews = new List<Entities.News>();
            DataTable dt = _News.GetNewsList(ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.News objlstNews = new TCAssociationTool.Entities.News();

                    objlstNews.NewsId = Convert.ToInt64(dr["NewsId"].ToString());
                    objlstNews.Title = dr["Title"].ToString();
                    objlstNews.NewsText = dr["NewsText"].ToString();
                    objlstNews.ImageUrl = (dr["ImageUrl"] != DBNull.Value ? dr["ImageUrl"].ToString() : "");
                    objlstNews.PostDate = Convert.ToDateTime(dr["PostDate"].ToString());
                    objlstNews.OrderNo = (dr["OrderNo"] != DBNull.Value ? Convert.ToInt64(dr["OrderNo"]) : 0);
                    objlstNews.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    objlstNews.UpdatedBy = dr["UpdatedBy"].ToString();
                    objlstNews.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"].ToString());

                    lstNews.Add(objlstNews);
                }

            }
            return lstNews;
        }

       public TCAssociationTool.Entities.News GetNewsById(Int64 NewsId, ref int status)
        {
            TCAssociationTool.Entities.News objNews = new TCAssociationTool.Entities.News();
            DataTable dt = new DataTable();
            if (NewsId != 0)
            {
                dt = _News.GetNewsById(NewsId, ref status);
                if (dt.Rows.Count == 1)
                {
                    objNews.NewsId = Convert.ToInt64(dt.Rows[0]["NewsId"].ToString());
                    objNews.Title = dt.Rows[0]["Title"].ToString();
                    objNews.NewsText = dt.Rows[0]["NewsText"].ToString();
                    objNews.ImageUrl = (dt.Rows[0]["ImageUrl"] != DBNull.Value ? dt.Rows[0]["ImageUrl"].ToString() : "");
                    objNews.PostDate =Convert.ToDateTime(dt.Rows[0]["PostDate"].ToString());
                    objNews.OrderNo = (dt.Rows[0]["OrderNo"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["OrderNo"]) : 0);
                    objNews.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                    objNews.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                    objNews.UpdatedTime = Convert.ToDateTime(dt.Rows[0]["UpdatedTime"].ToString());

                }
            }
            return objNews;
        }

        public List<TCAssociationTool.Entities.News> GetNewsListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.News> lstNews = new List<TCAssociationTool.Entities.News>();
            DataTable dt = _News.GetNewsListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _News.GetNewsListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.News objNews = new TCAssociationTool.Entities.News();

                    objNews.RId = Convert.ToInt64(dr["RId"].ToString());
                    objNews.NewsId = Convert.ToInt64(dr["NewsId"].ToString());
                    objNews.Title = dr["Title"].ToString();
                    objNews.NewsText = dr["NewsText"].ToString();
                    objNews.ImageUrl = (dr["ImageUrl"] != DBNull.Value ? dr["ImageUrl"].ToString() : "");
                    objNews.PostDate = Convert.ToDateTime(dr["PostDate"].ToString());
                    objNews.OrderNo = (dr["OrderNo"] != DBNull.Value ?Convert.ToInt64(dr["OrderNo"]) : 0);
                    objNews.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    objNews.UpdatedBy = dr["UpdatedBy"].ToString();
                    objNews.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"].ToString());

                    lstNews.Add(objNews);
                }
            }
            return lstNews;
        }

        #endregion

        #region Front-end

        public List<TCAssociationTool.Entities.News> FEGetNewsList(ref int status)
        {
            List<TCAssociationTool.Entities.News> lstNews = new List<Entities.News>();
            DataTable dt = _News.FEGetNewsList(ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.News objlstNews = new TCAssociationTool.Entities.News();

                    objlstNews.NewsId = Convert.ToInt64(dr["NewsId"].ToString());
                    objlstNews.Title = dr["Title"].ToString();
                    objlstNews.NewsText = dr["NewsText"].ToString();
                    objlstNews.ImageUrl = (dr["ImageUrl"] != DBNull.Value ? dr["ImageUrl"].ToString() : "");
                    objlstNews.PostDate = Convert.ToDateTime(dr["PostDate"].ToString());
                    objlstNews.OrderNo = (dr["OrderNo"] != DBNull.Value ? Convert.ToInt64(dr["OrderNo"]) : 0);
                    objlstNews.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    objlstNews.UpdatedBy = dr["UpdatedBy"].ToString();
                    objlstNews.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"].ToString());

                    lstNews.Add(objlstNews);
                }

            }
            return lstNews;
        }

        public List<Entities.News> FENewsGetListByVariable(Int64 NewsId, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<Entities.News> lstNews = new List<Entities.News>();
            DataTable dt = _News.FENewsGetListByVariable(NewsId, Search, Sort, PageNo, Items, ref Total);
            //if (dt.Rows.Count == 0 && PageNo != 0)
            //{
            //    dt = _News.GetNewsList(NewsId, Search, Sort,  (PageNo - 1), Items, ref Total);
            //}
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.News objNews = new Entities.News();
                    objNews.NewsId = Convert.ToInt64(dr["NewsId"]);
                    objNews.Title = dr["Title"].ToString();
                    objNews.NewsText = dr["NewsText"].ToString();
                    objNews.ImageUrl = (dr["ImageUrl"] == DBNull.Value ? "" : dr["ImageUrl"].ToString());
                    objNews.PostDate = Convert.ToDateTime(dr["PostDate"]);
                    objNews.UpdatedBy = dr["UpdatedBy"].ToString();
                    objNews.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
                    objNews.RId = Convert.ToInt64(dr["RId"]);

                    lstNews.Add(objNews);
                }
            }
            return lstNews;
        }

        #endregion


        #region API
        public List<Entities.News> APIFENewsGetListByVariable(Int64 NewsId, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<Entities.News> lstNews = new List<Entities.News>();
            DataTable dt = _News.FENewsGetListByVariable(NewsId, Search, Sort, PageNo, Items, ref Total);
            //if (dt.Rows.Count == 0 && PageNo != 0)
            //{
            //    dt = _News.GetNewsList(NewsId, Search, Sort,  (PageNo - 1), Items, ref Total);
            //}
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.News objNews = new Entities.News();
                    objNews.NewsId = Convert.ToInt64(dr["NewsId"]);
                    objNews.Title = dr["Title"].ToString();
                    objNews.NewsText = dr["NewsText"].ToString();
                    objNews.ImageUrl = (dr["ImageUrl"] != DBNull.Value ? logourl + dr["ImageUrl"].ToString() : "");
                    //objNews.ImageUrl = (dr["ImageUrl"] == DBNull.Value ? "" : dr["ImageUrl"].ToString());
                    objNews.PostDate = Convert.ToDateTime(dr["PostDate"]);
                    objNews.UpdatedBy = dr["UpdatedBy"].ToString();
                    objNews.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
                    objNews.RId = Convert.ToInt64(dr["RId"]);

                    lstNews.Add(objNews);
                }
            }
            return lstNews;
        }
        #endregion
    }
}
