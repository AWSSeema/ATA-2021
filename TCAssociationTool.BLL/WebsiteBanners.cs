using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TCAssociationTool.BLL
{
  public  class WebsiteBanners
    {
      TCAssociationTool.DAL.WebsiteBanners _WebsiteBanners = new TCAssociationTool.DAL.WebsiteBanners();

        #region Methods

          public Int64 InsertWebsiteBanners(Entities.WebsiteBanners objWebsiteBanners, ref string BannerUrl)
            {
                Int64 _status = 0;
                if (objWebsiteBanners != null)
                {
                    _status = _WebsiteBanners.InsertWebsiteBanners(objWebsiteBanners, ref BannerUrl);

                }
                return _status;
            }

          public Int64 DeleteWebsiteBanner(Int64 WebsiteBannerId)
        {
            Int64 _status = 0;
            _status = _WebsiteBanners.DeleteWebsiteBanner(WebsiteBannerId);
            return _status;
        }

          public Int64 UpdateWebsiteBannersStatus(Int64 WebsiteBannerId)
          {
              Int64 _status = 0;
              _status = _WebsiteBanners.UpdateWebsiteBannersStatus(WebsiteBannerId);
              return _status;
          }

          public Int64 UpdateWebsiteBannersOrderNo(int OrderNo, Int64 WebsiteBannerId)
          {
              Int64 _status = 0;
              _status = _WebsiteBanners.UpdateWebsiteBannersOrderNo(OrderNo, WebsiteBannerId);
              return _status;
          }

        #endregion

        #region Entities filling

          public List<TCAssociationTool.Entities.WebsiteBanners> GetWebsiteBannersList(ref int status)
            {
                List<TCAssociationTool.Entities.WebsiteBanners> lstWebsiteBanners = new List<Entities.WebsiteBanners>();
                DataTable dt = _WebsiteBanners.GetWebsiteBannersList(ref status);

                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TCAssociationTool.Entities.WebsiteBanners objlstWebsiteBanners = new TCAssociationTool.Entities.WebsiteBanners();

                        objlstWebsiteBanners.WebsiteBannerId = Convert.ToInt64(dr["WebsiteBannerId"].ToString());
                        objlstWebsiteBanners.BannerTitle =dr["BannerTitle"].ToString();
                        objlstWebsiteBanners.BannerUrl = (dr["BannerUrl"] != DBNull.Value ? dr["BannerUrl"].ToString() : "");
                        objlstWebsiteBanners.RedirectUrl = (dr["RedirectUrl"] != DBNull.Value ? dr["RedirectUrl"].ToString() : "");
                        objlstWebsiteBanners.OrderNo = (dr["OrderNo"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["OrderNo"].ToString()) : 0);
                        objlstWebsiteBanners.Target = (dt.Rows[0]["Target"] != DBNull.Value ? dt.Rows[0]["Target"].ToString() : "");
                        objlstWebsiteBanners.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objlstWebsiteBanners.UpdatedBy = dr["UpdatedBy"].ToString();
                        objlstWebsiteBanners.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"].ToString());
                        lstWebsiteBanners.Add(objlstWebsiteBanners);
                    }

                }
                return lstWebsiteBanners;
            }

          public TCAssociationTool.Entities.WebsiteBanners GetWebsiteBannerById(Int64 PhotoId, ref int status)
            {
                TCAssociationTool.Entities.WebsiteBanners objWebsiteBanners = new TCAssociationTool.Entities.WebsiteBanners();
                DataTable dt = new DataTable();
                if (PhotoId != 0)
                {
                    dt = _WebsiteBanners.GetWebsiteBannerById(PhotoId, ref status);
                    if (dt.Rows.Count == 1)
                    {
                        objWebsiteBanners.WebsiteBannerId = Convert.ToInt64(dt.Rows[0]["WebsiteBannerId"].ToString());
                        objWebsiteBanners.BannerTitle = dt.Rows[0]["BannerTitle"].ToString();
                        objWebsiteBanners.BannerUrl = (dt.Rows[0]["BannerUrl"] != DBNull.Value ? dt.Rows[0]["BannerUrl"].ToString() : "");
                        objWebsiteBanners.RedirectUrl = (dt.Rows[0]["RedirectUrl"] != DBNull.Value ? dt.Rows[0]["RedirectUrl"].ToString() : "");
                        objWebsiteBanners.OrderNo = (dt.Rows[0]["OrderNo"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["OrderNo"].ToString()) : 0);
                        objWebsiteBanners.Target = (dt.Rows[0]["Target"] != DBNull.Value ? dt.Rows[0]["Target"].ToString() : "");
                        objWebsiteBanners.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
                        objWebsiteBanners.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                        objWebsiteBanners.UpdatedTime = Convert.ToDateTime(dt.Rows[0]["UpdatedTime"].ToString());

                    }
                }
                return objWebsiteBanners;
            }

          public List<TCAssociationTool.Entities.WebsiteBanners> GetWebsiteBannerListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.WebsiteBanners> lstWebsiteBanners = new List<TCAssociationTool.Entities.WebsiteBanners>();

            DataTable dt = _WebsiteBanners.GetWebsiteBannerListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _WebsiteBanners.GetWebsiteBannerListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.WebsiteBanners objWebsiteBanners = new TCAssociationTool.Entities.WebsiteBanners();

                    objWebsiteBanners.RId = Convert.ToInt32(dr["RId"].ToString());
                    objWebsiteBanners.WebsiteBannerId = Convert.ToInt32(dr["WebsiteBannerId"].ToString());
                    objWebsiteBanners.BannerTitle = dr["BannerTitle"].ToString();
                    objWebsiteBanners.BannerUrl = (dr["BannerUrl"] != DBNull.Value ? dr["BannerUrl"].ToString() : "");
                    objWebsiteBanners.RedirectUrl = (dr["RedirectUrl"] != DBNull.Value ? dr["RedirectUrl"].ToString() : "");
                    objWebsiteBanners.OrderNo = (dr["OrderNo"] != DBNull.Value ?Convert.ToInt32(dr["OrderNo"].ToString()) : 0);
                    objWebsiteBanners.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
                    objWebsiteBanners.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                    objWebsiteBanners.UpdatedBy = dr["UpdatedBy"].ToString();
                    objWebsiteBanners.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"].ToString());

                    lstWebsiteBanners.Add(objWebsiteBanners);
                }
            }
            return lstWebsiteBanners;
        }

        #endregion
    }
}
