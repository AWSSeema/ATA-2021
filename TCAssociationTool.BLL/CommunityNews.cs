using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TCAssociationTool.BLL
{
   public class CommunityNews
    {
        TCAssociationTool.DAL.CommunityNews _CommunityNews = new TCAssociationTool.DAL.CommunityNews();

        #region Methods

        public Int64 InsertCommunityNews(Entities.CommunityNews objCommunityNews, ref string imgsrc)
        {
            Int64 _status = 0;
            if (objCommunityNews != null)
            {
                _status = _CommunityNews.InsertCommunityNews(objCommunityNews, ref imgsrc);

            }
            return _status;
        }

        public Int64 DeleteCommunityNews(Int64 id)
        {
            Int64 _status = 0;
            _status = _CommunityNews.DeleteCommunityNews(id);
            return _status;
        }

        public Int64 UpdateCommunityNewsStatus(Int64 id)
        {
            Int64 _status = 0;
            _status = _CommunityNews.UpdateCommunityNewsStatus(id);
            return _status;
        }

        public Int64 UpdateCommunityNewsOrderNo(int orderno, Int64 id)
        {
            Int64 _status = 0;
            _status = _CommunityNews.UpdateCommunityNewsOrderNo(orderno, id);
            return _status;
        }
        #endregion

        #region Entities filling

        public List<TCAssociationTool.Entities.CommunityNews> GetCommunityNewsList(ref int status)
        {
            List<TCAssociationTool.Entities.CommunityNews> lstCommunityNews = new List<Entities.CommunityNews>();
            DataTable dt = _CommunityNews.GetCommunityNewsList(ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.CommunityNews objCommunityNews = new TCAssociationTool.Entities.CommunityNews();

                    objCommunityNews.id = Convert.ToInt64(dr["id"].ToString());
                    objCommunityNews.heading = dr["heading"].ToString();
                    objCommunityNews.status2 = Convert.ToBoolean(dr["IsActive"]);
                    objCommunityNews.datemodified = Convert.ToDateTime(dr["UpdatedDate"].ToString());
                    objCommunityNews.imgsrc = (dr["imgsrc"] != DBNull.Value ? dr["imgsrc"].ToString() : "");
                    objCommunityNews.shortdesc = (dr["shortdesc"] != DBNull.Value ? dr["shortdesc"].ToString() : "");
                    objCommunityNews.pageurl = (dr["pageurl"] != DBNull.Value ? dr["pageurl"].ToString() : "");
                    objCommunityNews.orderno = (dr["orderno"] != DBNull.Value ? Convert.ToInt32(dr["orderno"].ToString()) : 0);
                    objCommunityNews.target = (dr["target"] != DBNull.Value ? dr["target"].ToString() : "");

                    lstCommunityNews.Add(objCommunityNews);
                }

            }
            return lstCommunityNews;
        }

        public TCAssociationTool.Entities.CommunityNews GetCommunityNewsById(Int64 id, ref int status)
        {
            TCAssociationTool.Entities.CommunityNews objCommunityNews = new TCAssociationTool.Entities.CommunityNews();
            DataTable dt = new DataTable();
            if (id != 0)
            {
                dt = _CommunityNews.GetCommunityNewsById(id, ref status);
                if (dt.Rows.Count == 1)
                {
                    objCommunityNews.id = Convert.ToInt64(dt.Rows[0]["id"].ToString());
                    objCommunityNews.heading = dt.Rows[0]["heading"].ToString();
                    objCommunityNews.status2 = Convert.ToBoolean(dt.Rows[0]["status2"]);
                    objCommunityNews.datemodified = Convert.ToDateTime(dt.Rows[0]["datemodified"].ToString());
                    objCommunityNews.imgsrc = (dt.Rows[0]["imgsrc"] != DBNull.Value ? dt.Rows[0]["imgsrc"].ToString() : "");
                    objCommunityNews.shortdesc = (dt.Rows[0]["shortdesc"] != DBNull.Value ? dt.Rows[0]["shortdesc"].ToString() : "");
                    objCommunityNews.pageurl = (dt.Rows[0]["pageurl"] != DBNull.Value ? dt.Rows[0]["pageurl"].ToString() : "");
                    objCommunityNews.orderno = (dt.Rows[0]["orderno"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["orderno"].ToString()) : 0);
                    objCommunityNews.target = (dt.Rows[0]["target"] != DBNull.Value ? dt.Rows[0]["target"].ToString() : "");

                }
            }
            return objCommunityNews;
        }

        public List<TCAssociationTool.Entities.CommunityNews> GetCommunityNewsListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.CommunityNews> lstCommunityNews = new List<TCAssociationTool.Entities.CommunityNews>();

            DataTable dt = _CommunityNews.GetCommunityNewsListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _CommunityNews.GetCommunityNewsListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.CommunityNews objCommunityNews = new TCAssociationTool.Entities.CommunityNews();

                    objCommunityNews.RId = Convert.ToInt32(dr["RId"].ToString());
                    objCommunityNews.id = Convert.ToInt64(dr["id"].ToString());
                    objCommunityNews.heading = dr["heading"].ToString();
                    objCommunityNews.status2 = Convert.ToBoolean(dr["status2"]);
                    objCommunityNews.datemodified = Convert.ToDateTime(dr["datemodified"].ToString());
                    objCommunityNews.imgsrc = (dr["imgsrc"] != DBNull.Value ? dr["imgsrc"].ToString() : "");
                    objCommunityNews.shortdesc = (dr["shortdesc"] != DBNull.Value ? dr["shortdesc"].ToString() : "");
                    objCommunityNews.pageurl = (dr["pageurl"] != DBNull.Value ? dr["pageurl"].ToString() : "");
                    objCommunityNews.orderno = (dr["orderno"] != DBNull.Value ? Convert.ToInt32(dr["orderno"].ToString()) : 0);
                    objCommunityNews.target = (dr["target"] != DBNull.Value ? dr["target"].ToString() : "");

                    lstCommunityNews.Add(objCommunityNews);
                }
            }
            return lstCommunityNews;
        }

        #endregion
    }
}
