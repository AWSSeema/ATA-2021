using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace TCAssociationTool.DAL
{
  public  class Coupons
    {

        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

        public DataTable GetCouponsList(ref int qstatus)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("CouponsGetList", ref _sqlP);
                qstatus = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 UpdateCouponsStatus(Int64 id)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@id",id),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("CouponsUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 InsertCoupons(Entities.Coupons objCoupons)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@Id",objCoupons.Id),
                    new SqlParameter("@code",(objCoupons.code == null ?DBNull.Value:(object)objCoupons.code)),
                    new SqlParameter("@expirydate",(objCoupons.expirydate == DateTime.MinValue ?(object)DBNull.Value:objCoupons.expirydate)),
                    new SqlParameter("@status2",objCoupons.status2),
                    new SqlParameter("@membership_type",(objCoupons.membership_type == null ?DBNull.Value:(object)objCoupons.membership_type)),
                    new SqlParameter("@discount",(objCoupons.discount == 0 ?DBNull.Value:(object)objCoupons.discount)),
                    new SqlParameter("@datemodified",(objCoupons.datemodified == DateTime.MinValue ?(object)DBNull.Value:objCoupons.datemodified)),
                    new SqlParameter("@QStatus",0)
                    };
               
                _sqlP[7].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("CouponsInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[7].Value);

                 }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetCouponsListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
             {
                _sqlP = new[]
                {
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@Total",Total)
                };

                _sqlP[4].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("CouponsGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetCouponsById(Int64 id, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@id",id),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("CouponsGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteCoupons(Int64 id)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@id",id),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("CouponsDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

    }
}
