using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace TCAssociationTool.DAL
{
   public class LafitnessVouchers
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;



        public Int64 InsertLafitnessVouchers(Entities.LafitnessVouchers objLafitnessVouchers)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@Id",objLafitnessVouchers.Id),
                    new SqlParameter("@voucher",(objLafitnessVouchers.voucher == null ?DBNull.Value:(object)objLafitnessVouchers.voucher.Trim())),
                    new SqlParameter("@member_id",(objLafitnessVouchers.member_id == null ?DBNull.Value:(object)objLafitnessVouchers.member_id.Trim())),
                    new SqlParameter("@assigned_on",(objLafitnessVouchers.assigned_on == DateTime.MinValue ?DBNull.Value:(object)objLafitnessVouchers.assigned_on)),
                    new SqlParameter("@status2",objLafitnessVouchers.status2),
                    new SqlParameter("@created_at",(objLafitnessVouchers.created_at == DateTime.MinValue ?DBNull.Value:(object)objLafitnessVouchers.created_at)),
                    new SqlParameter("@updated_at",(objLafitnessVouchers.updated_at == DateTime.MinValue ?DBNull.Value:(object)objLafitnessVouchers.updated_at)),
                    new SqlParameter("@expire_date",(objLafitnessVouchers.expire_date == DateTime.MinValue ?DBNull.Value:(object)objLafitnessVouchers.expire_date)),
                    new SqlParameter("@QStatus",0)
                    };

        
                _sqlP[8].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("lafitness_vouchersInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[8].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }




        public DataTable GetLafitnessVouchersListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
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
                dt = _dbAccess.GetDataTable("lafitness_vouchersGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


       

        public DataTable GetLafitnessVouchersById(Int64 Id, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@Id",Id),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("lafitness_vouchersGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public Int64 DeleteLafitnessVouchers(Int64 Id)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@Id",Id),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("lafitness_vouchersDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }



        public Int64 lafitness_vouchersUpdateStatus(Int64 Id)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@Id",Id),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("lafitness_vouchersUpdateStatus", ref _sqlP);
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

