using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TCAssociationTool.BLL
{
  public  class Coupons
    {

        TCAssociationTool.DAL.Coupons _Coupons = new TCAssociationTool.DAL.Coupons();

        #region Methods

        public Int64 InsertCoupons(Entities.Coupons objCoupons)
        {
            Int64 _status = 0;
            if (objCoupons != null)
            {
                _status = _Coupons.InsertCoupons(objCoupons);

            }
            return _status;
        }

        public Int64 DeleteCoupons(Int64 Id)
        {
            Int64 _status = 0;
            _status = _Coupons.DeleteCoupons(Id);
            return _status;
        }

        public Int64 UpdateCouponsStatus(Int64 Id)
        {
            Int64 _status = 0;
            _status = _Coupons.UpdateCouponsStatus(Id);
            return _status;
        }

       
        #endregion

        #region Entities filling

        public List<TCAssociationTool.Entities.Coupons> GetCouponsList(ref int status)
        {
            List<TCAssociationTool.Entities.Coupons> lstCoupons = new List<Entities.Coupons>();
            DataTable dt = _Coupons.GetCouponsList(ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Coupons objlstCoupons = new TCAssociationTool.Entities.Coupons();

                    objlstCoupons.Id = Convert.ToInt64(dr["Id"].ToString());
                    objlstCoupons.code = (dr["code"] != DBNull.Value ? dr["code"].ToString() : "");
                    objlstCoupons.expirydate = (dr["expirydate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["expirydate"].ToString()) : DateTime.MinValue);
                    objlstCoupons.status2 = Convert.ToBoolean(dr["status2"]);
                    objlstCoupons.membership_type = (dr["membership_type"] != DBNull.Value ? dr["membership_type"].ToString() : "");
                    objlstCoupons.discount = (dt.Rows[0]["discount"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[0]["discount"]) : 0);
                    objlstCoupons.datemodified = (dr["datemodified"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["datemodified"].ToString()) : DateTime.MinValue);



                    
                    lstCoupons.Add(objlstCoupons);
                }

            }
            return lstCoupons;
        }

        public TCAssociationTool.Entities.Coupons GetCouponsById(Int64 Id, ref int status)
        {
            TCAssociationTool.Entities.Coupons objCoupons = new TCAssociationTool.Entities.Coupons();
            DataTable dt = new DataTable();
            if (Id != 0)
            {
                dt = _Coupons.GetCouponsById(Id, ref status);
                if (dt.Rows.Count == 1)
                {
                    objCoupons.Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                    objCoupons.code = (dt.Rows[0]["code"] != DBNull.Value ? dt.Rows[0]["code"].ToString() : "");
                    objCoupons.expirydate = (dt.Rows[0]["expirydate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["expirydate"]) : DateTime.MinValue);
                    objCoupons.Eexpirydate = objCoupons.expirydate.ToString("dd/MM/yyyy");

                    objCoupons.status2 = Convert.ToBoolean(dt.Rows[0]["status2"]);
                    objCoupons.membership_type = (dt.Rows[0]["membership_type"] != DBNull.Value ? dt.Rows[0]["membership_type"].ToString() : "");
                    objCoupons.discount = (dt.Rows[0]["discount"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[0]["discount"]) : 0);
                    objCoupons.datemodified = (dt.Rows[0]["datemodified"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["datemodified"]) : DateTime.MinValue);

                }
            }
            return objCoupons;
        }

        public List<TCAssociationTool.Entities.Coupons> GetCouponsListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.Coupons> lstCoupons = new List<TCAssociationTool.Entities.Coupons>();
            DataTable dt = _Coupons.GetCouponsListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _Coupons.GetCouponsListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Coupons objCoupons = new TCAssociationTool.Entities.Coupons();

                    objCoupons.Rid = Convert.ToInt64(dr["Rid"].ToString());
                    objCoupons.Id = Convert.ToInt64(dr["Id"].ToString());
                    objCoupons.code = (dr["code"] != DBNull.Value ? dr["code"].ToString() : "");
                    objCoupons.expirydate = (dr["expirydate"] != DBNull.Value ? Convert.ToDateTime(dr["expirydate"]) : DateTime.MinValue);
                    objCoupons.status2 = Convert.ToBoolean(dr["status2"]);
                    objCoupons.membership_type = (dr["membership_type"] != DBNull.Value ? dr["membership_type"].ToString() : "");
                    objCoupons.discount = (dr["discount"] != DBNull.Value ? Convert.ToDecimal(dr["discount"]) : 0);
                    objCoupons.datemodified = (dr["datemodified"] != DBNull.Value ? Convert.ToDateTime(dr["datemodified"]) : DateTime.MinValue);

                 
                    lstCoupons.Add(objCoupons);
                }
            }
            return lstCoupons;
        }

        #endregion

    }
}
