using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TCAssociationTool.BLL
{
  public  class LafitnessVouchers
    {

        TCAssociationTool.DAL.LafitnessVouchers _LafitnessVouchers = new TCAssociationTool.DAL.LafitnessVouchers();
        #region Methods

        public Int64 InsertLafitnessVouchers(Entities.LafitnessVouchers objLafitnessVoucherss)
        {
            Int64 _status = 0;
            if (objLafitnessVoucherss != null)
            {
                _status = _LafitnessVouchers.InsertLafitnessVouchers(objLafitnessVoucherss);

            }
            return _status;
        }

        public Int64 lafitness_vouchersUpdateStatus(Int64 Id)
        {
            Int64 _status = 0;
            _status = _LafitnessVouchers.lafitness_vouchersUpdateStatus(Id);
            return _status;
        }

        public Int64 DeleteLafitnessVouchers(Int64 Id)
        {
            Int64 _status = 0;
            _status = _LafitnessVouchers.DeleteLafitnessVouchers(Id);
            return _status;
        }





        #endregion

        #region Entities filling




        public TCAssociationTool.Entities.LafitnessVouchers GetLafitnessVouchersById(Int64 Id, ref int status)
        {
            TCAssociationTool.Entities.LafitnessVouchers objLafitnessVouchers = new TCAssociationTool.Entities.LafitnessVouchers();
            DataTable dt = new DataTable();
            if (Id != 0)
            {
                dt = _LafitnessVouchers.GetLafitnessVouchersById(Id, ref status);
                if (dt.Rows.Count == 1)
                {
                    objLafitnessVouchers.Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                    objLafitnessVouchers.voucher = (dt.Rows[0]["voucher"] != DBNull.Value ? dt.Rows[0]["voucher"].ToString() : "");
                    objLafitnessVouchers.member_id = (dt.Rows[0]["member_id"] != DBNull.Value ? dt.Rows[0]["member_id"].ToString() : "");
                    objLafitnessVouchers.assigned_on = (dt.Rows[0]["assigned_on"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["assigned_on"]) : DateTime.MinValue);
                    objLafitnessVouchers.status2 = (dt.Rows[0]["status2"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["status2"]) : false);
                    objLafitnessVouchers.created_at = (dt.Rows[0]["created_at"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["created_at"]) : DateTime.MinValue);
                    objLafitnessVouchers.updated_at = (dt.Rows[0]["updated_at"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["updated_at"]) : DateTime.MinValue);
                    objLafitnessVouchers.expire_date = (dt.Rows[0]["expire_date"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["expire_date"]) : DateTime.MinValue);
                    objLafitnessVouchers.Eexpire_date = objLafitnessVouchers.expire_date.ToString("dd/MM/yyyy");

                }
            }
            return objLafitnessVouchers;
        }


        public List<TCAssociationTool.Entities.LafitnessVouchers> GetLafitnessVouchersListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.LafitnessVouchers> lstLafitnessVouchers = new List<TCAssociationTool.Entities.LafitnessVouchers>();
            DataTable dt = _LafitnessVouchers.GetLafitnessVouchersListByVariable( Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _LafitnessVouchers.GetLafitnessVouchersListByVariable( Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.LafitnessVouchers objLafitnessVouchers = new TCAssociationTool.Entities.LafitnessVouchers();

                    objLafitnessVouchers.RId = Convert.ToInt64(dr["RId"].ToString());
                    objLafitnessVouchers.Id = Convert.ToInt64(dr["Id"].ToString());
                    objLafitnessVouchers.voucher = (dr["voucher"] != DBNull.Value ? dr["voucher"].ToString() : "");
                    objLafitnessVouchers.member_id = (dr["member_id"] != DBNull.Value ? dr["member_id"].ToString() : "");
                    objLafitnessVouchers.assigned_on = (dr["assigned_on"] != DBNull.Value ? Convert.ToDateTime(dr["assigned_on"]) : DateTime.MinValue);
                    objLafitnessVouchers.status2 = (dr["status2"] != DBNull.Value ? Convert.ToBoolean(dr["status2"]) : false);
                    objLafitnessVouchers.created_at = (dr["created_at"] != DBNull.Value ? Convert.ToDateTime(dr["created_at"]) : DateTime.MinValue);
                    objLafitnessVouchers.updated_at = (dr["updated_at"] != DBNull.Value ? Convert.ToDateTime(dr["updated_at"]) : DateTime.MinValue);
                    objLafitnessVouchers.expire_date = (dr["expire_date"] != DBNull.Value ? Convert.ToDateTime(dr["expire_date"]) : DateTime.MinValue);

   

                    lstLafitnessVouchers.Add(objLafitnessVouchers);
                }
            }
            return lstLafitnessVouchers;
        }

        #endregion
    }
}
