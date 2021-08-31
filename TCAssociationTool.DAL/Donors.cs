using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace TCAssociationTool.DAL
{
    public class Donors
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

        public Int64 InsertDonors(Entities.Donors objDonors, ref Int64 DonorId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@DonorId",objDonors.DonorId),
                    new SqlParameter("@UserId",(objDonors.UserId==0?DBNull.Value:(object)objDonors.UserId)),
                    new SqlParameter("@FirstName",objDonors.FirstName),
                    new SqlParameter("@LastName",objDonors.LastName),
                    new SqlParameter("@Email",objDonors.Email),
                    new SqlParameter("@PhoneNo",(objDonors.PhoneNo == null ?DBNull.Value:(object)objDonors.PhoneNo)),
                    new SqlParameter("@Address",(objDonors.Address == null ?DBNull.Value:(object)objDonors.Address)),
                    new SqlParameter("@DonationProgram",(objDonors.DonationProgram == null ?DBNull.Value:(object)objDonors.DonationProgram)),
                    new SqlParameter("@DonationCause",(objDonors.DonationCause == null ?DBNull.Value:(object)objDonors.DonationCause)),
                    new SqlParameter("@IsActive",objDonors.IsActive),
                    new SqlParameter("@Amount",(objDonors.Amount == 0 ?DBNull.Value:(object)objDonors.Amount)),
                    new SqlParameter("@TransactionId",(objDonors.TransactionId == null ?DBNull.Value:(object)objDonors.TransactionId)),
                    new SqlParameter("@PaymentStatusId",(objDonors.PaymentStatusId == 0 ?DBNull.Value:(object)objDonors.PaymentStatusId)),
                    new SqlParameter("@PaymentMethodId",(objDonors.PaymentMethodId == 0 ?DBNull.Value:(object)objDonors.PaymentMethodId)),
                    new SqlParameter("@OrderDate",(objDonors.OrderDate == DateTime.MinValue ?DBNull.Value:(object)objDonors.OrderDate)),
                    new SqlParameter("@PaymentBy",(objDonors.PaymentBy == null ?DBNull.Value:(object)objDonors.PaymentBy)),                    
                    new SqlParameter("@InsertedBy",objDonors.InsertedBy),
                    new SqlParameter("@InsertedDate",DateTime.UtcNow),
                    new SqlParameter("@UpdatedBy",objDonors.UpdatedBy),
                    new SqlParameter("@UpdatedDate",DateTime.UtcNow),
                    new SqlParameter("@QStatus",0)
                    };

                _sqlP[0].SqlDbType = SqlDbType.NVarChar;
                _sqlP[0].Size = 512;
                _sqlP[0].Direction = System.Data.ParameterDirection.InputOutput;

                _sqlP[20].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("DonorsInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[20].Value);

                DonorId = Convert.ToInt64(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateDonorsStatus(Int64 DonorId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@DonorId",DonorId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("DonorsUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateDonors(Entities.Donors objDonors)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@DonorId",objDonors.DonorId),                    
                    new SqlParameter("@Amount",(objDonors.Amount == 0 ?DBNull.Value:(object)objDonors.Amount)),
                    new SqlParameter("@TransactionId",(objDonors.TransactionId == null ?DBNull.Value:(object)objDonors.TransactionId)),
                    new SqlParameter("@PaymentStatusId",(objDonors.PaymentStatusId == 0 ?DBNull.Value:(object)objDonors.PaymentStatusId)),
                    new SqlParameter("@PaymentMethodId",(objDonors.PaymentMethodId == 0 ?DBNull.Value:(object)objDonors.PaymentMethodId)),
                    new SqlParameter("@PaymentStatus",(objDonors.PaymentStatus == null ?DBNull.Value:(object)objDonors.PaymentStatus)),
                    new SqlParameter("@PaymentMethod",(objDonors.PaymentMethod == null ?DBNull.Value:(object)objDonors.PaymentMethod)),
                    new SqlParameter("@OrderDate",(objDonors.OrderDate == DateTime.MinValue ?DBNull.Value:(object)objDonors.OrderDate)),                   
                    new SqlParameter("@UpdatedBy",objDonors.UpdatedBy),
                    new SqlParameter("@UpdatedDate",DateTime.UtcNow),
                    new SqlParameter("@QStatus",0)
                    };


                _sqlP[10].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("DonorsUpdate", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[10].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }
        
        public DataTable GetDonorsListByVariable(Int64 PaymentMethodId, Int64 PaymentStatusId, string StartDate, string EndDate, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@PaymentStatusId",PaymentStatusId),
                    new SqlParameter("@PaymentMethodId",PaymentMethodId),
                    new SqlParameter("@StartDate",StartDate),
                    new SqlParameter("@EndDate",EndDate),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),                    
                    new SqlParameter("@Total",Total)
                };

                _sqlP[8].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("DonorsGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[8].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetDonorById(Int64 DonorId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@DonorId",DonorId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("DonorsGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteDonor(Int64 DonorId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@DonorId",DonorId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("DonorsDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable DonorRegistrationsExportToExcel(Int64 PaymentStatusId, Int64 PaymentMethodId, string Search, string Sort)
        {
            DataTable dt = null;
            Int32 Total = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@PaymentStatusId",PaymentStatusId),
                    new SqlParameter("@PaymentMethodId",PaymentMethodId),                    
                    //new SqlParameter("@StartDate",StartDate),
                    //new SqlParameter("@EndDate",EndDate),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@Total",0)
                };
                _sqlP[4].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("DonorRegistrationsExportToExcel", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        #region Front-end

        public DataTable FEDonorsGetList(string Search, string Type, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Type",Type),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),                    
                    new SqlParameter("@Total",Total)
                };

                _sqlP[5].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("FEDonorsGetList", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[5].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        #endregion
    }
}
