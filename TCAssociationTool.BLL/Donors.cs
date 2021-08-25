using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TCAssociationTool.BLL
{
    public class Donors
    {
        TCAssociationTool.DAL.Donors _Donors = new TCAssociationTool.DAL.Donors();

        #region Methods

        public Int64 InsertDonors(Entities.Donors objDonors, ref Int64 DonorId)
        {
            Int64 _status = 0;
            if (objDonors != null)
            {
                _status = _Donors.InsertDonors(objDonors, ref DonorId);

            }
            return _status;
        }

        public Int64 UpdateDonors(Entities.Donors objDonors)
        {
            Int64 _status = 0;
            if (objDonors != null)
            {
                _status = _Donors.UpdateDonors(objDonors);

            }
            return _status;
        }

        public Int64 DeleteDonor(Int64 DonorId)
        {
            Int64 _status = 0;
            _status = _Donors.DeleteDonor(DonorId);
            return _status;
        }

        public Int64 UpdateDonorsStatus(Int64 DonorId)
        {
            Int64 _status = 0;
            _status = _Donors.UpdateDonorsStatus(DonorId);
            return _status;
        }

        #endregion

        #region Entities filling
        
        public TCAssociationTool.Entities.Donors GetDonorsById(Int64 DonorId, ref int status)
        {
            TCAssociationTool.Entities.Donors objDonors = new TCAssociationTool.Entities.Donors();
            DataTable dt = new DataTable();
            if (DonorId != 0)
            {
                dt = _Donors.GetDonorById(DonorId, ref status);
                if (dt.Rows.Count == 1)
                {
                    objDonors.DonorId = Convert.ToInt64(dt.Rows[0]["DonorId"].ToString());
                    objDonors.FirstName = dt.Rows[0]["FirstName"].ToString();
                    objDonors.Email = dt.Rows[0]["Email"].ToString();
                    objDonors.LastName = dt.Rows[0]["LastName"].ToString();
                    objDonors.Address = dt.Rows[0]["Address"] != DBNull.Value ? dt.Rows[0]["Address"].ToString() : null;
                    objDonors.PhoneNo = dt.Rows[0]["PhoneNo"] != DBNull.Value ? dt.Rows[0]["PhoneNo"].ToString() : null;
                    objDonors.DonationProgram = dt.Rows[0]["DonationProgram"] != DBNull.Value ?dt.Rows[0]["DonationProgram"].ToString():null;
                    objDonors.DonationCause = dt.Rows[0]["DonationCause"] != DBNull.Value ? dt.Rows[0]["DonationCause"].ToString() : null;
                    objDonors.IsActive = (dt.Rows[0]["IsActive"] != DBNull.Value ?Convert.ToBoolean(dt.Rows[0]["IsActive"]) : false);
                    objDonors.Amount = (dt.Rows[0]["Amount"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[0]["Amount"]) : 0);
                    objDonors.TransactionId = dt.Rows[0]["TransactionId"] != DBNull.Value ? dt.Rows[0]["TransactionId"].ToString() : null;
                    objDonors.PaymentMethodId = (dt.Rows[0]["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["PaymentMethodId"]) : 0);
                    objDonors.PaymentMethod = dt.Rows[0]["PaymentMethod"] != DBNull.Value ? dt.Rows[0]["PaymentMethod"].ToString() : null;
                    objDonors.PaymentStatusId = (dt.Rows[0]["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["PaymentStatusId"]) : 0);
                    objDonors.PaymentStatus = dt.Rows[0]["PaymentStatus"] != DBNull.Value ? dt.Rows[0]["PaymentStatus"].ToString() : null;
                    objDonors.OrderDate = (dt.Rows[0]["OrderDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["OrderDate"]) : DateTime.MinValue);
                    objDonors.PaymentBy = (dt.Rows[0]["PaymentBy"] != DBNull.Value? dt.Rows[0]["PaymentBy"].ToString():null);
                    objDonors.InsertedDate = Convert.ToDateTime(dt.Rows[0]["InsertedDate"].ToString());

                }
            }
            return objDonors;
        }

        public List<TCAssociationTool.Entities.Donors> GetDonorsListByVariable(Int64 PaymentMethod,Int64 PaymentStatus, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.Donors> lstDonors = new List<TCAssociationTool.Entities.Donors>();
            DataTable dt = _Donors.GetDonorsListByVariable(PaymentMethod, PaymentStatus, Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _Donors.GetDonorsListByVariable(PaymentMethod, PaymentStatus, Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Donors objDonors = new TCAssociationTool.Entities.Donors();

                    objDonors.RId = Convert.ToInt64(dr["RId"].ToString());
                    objDonors.DonorId = Convert.ToInt64(dr["DonorId"].ToString());
                    objDonors.FirstName = dr["FirstName"].ToString();
                    objDonors.LastName = dr["LastName"].ToString();
                    objDonors.Email = dr["Email"].ToString();
                    objDonors.PhoneNo = dr["PhoneNo"].ToString();
                    objDonors.DonationProgram = (dr["DonationProgram"] != DBNull.Value ? (dr["DonationProgram"].ToString()) : null);
                    objDonors.OrderDate = (dr["OrderDate"] != DBNull.Value ?Convert.ToDateTime(dr["OrderDate"].ToString()) : DateTime.MinValue);
                    objDonors.TransactionId = (dr["TransactionId"] != DBNull.Value ? (dr["TransactionId"].ToString()) : null);
                    objDonors.IsActive = (dr["IsActive"] != DBNull.Value ? Convert.ToBoolean(dr["IsActive"]) : false);
                    objDonors.Amount = (dr["Amount"] != DBNull.Value ? Convert.ToDecimal(dr["Amount"]) : 0);
                    objDonors.PaymentMethod = (dr["PaymentMethod"] != DBNull.Value ? (dr["PaymentMethod"].ToString()) : null);
                    objDonors.PaymentStatus = dr["PaymentStatus"].ToString();
                    objDonors.InsertedDate = Convert.ToDateTime(dr["InsertedDate"].ToString());

                    lstDonors.Add(objDonors);
                }
            }
            return lstDonors;
        }
        
        #endregion

        #region

        public List<TCAssociationTool.Entities.Donors> FEDonorsGetList(string Search, string Type, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.Donors> lstDonors = new List<Entities.Donors>();

            DataTable dt = _Donors.FEDonorsGetList(Search, Type, Sort, PageNo, Items, ref Total);

            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _Donors.FEDonorsGetList(Search, Type, Sort, PageNo - 1, Items, ref Total);
            }

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Donors objDonors = new TCAssociationTool.Entities.Donors();

                    objDonors.RId = Convert.ToInt64(dr["RId"].ToString());

                    objDonors.DonorId = Convert.ToInt64(dr["DonorId"].ToString());
                    objDonors.FirstName = dr["FirstName"].ToString();
                    objDonors.LastName = dr["LastName"].ToString();
                    objDonors.Email = dr["Email"].ToString();
                    objDonors.PhoneNo = dr["PhoneNo"].ToString();
                     
                    objDonors.DonationProgram = dr["DonationProgram"].ToString();
                    
                   
                    objDonors.Amount = (dr["Amount"] != DBNull.Value ? Convert.ToDecimal(dr["Amount"]) : 0);
                    
                    objDonors.InsertedDate = Convert.ToDateTime(dr["InsertedDate"].ToString());

                    lstDonors.Add(objDonors);
                }

            }
            return lstDonors;
        }

        #endregion


    }
}
