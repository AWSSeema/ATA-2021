using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TCAssociationTool.BLL
{
    public class Volunteers
    {
        TCAssociationTool.DAL.Volunteers _Volunteers = new TCAssociationTool.DAL.Volunteers();

        #region Admin

        #region Methods

        public Int64 InsertVolunteers(Entities.Volunteers objVolunteers)
        {
            Int64 _status = 0;
            if (objVolunteers != null)
            {
                _status = _Volunteers.InsertVolunteers(objVolunteers);

            }
            return _status;
        }

        public Int64 DeleteVolunteer(Int64 VolunteerId)
        {
            Int64 _status = 0;
            _status = _Volunteers.DeleteVolunteer(VolunteerId);
            return _status;
        }

        public Int64 UpdateVolunteerstatus(Int64 VolunteerId)
        {
            Int64 _status = 0;
            _status = _Volunteers.UpdateVolunteerstatus(VolunteerId);
            return _status;
        }

        public Int64 UpdateVolunteerHours(string HoursSpent, Int64 VolunteerId)
        {
            Int64 _status = 0;
            _status = _Volunteers.UpdateVolunteerHours(HoursSpent, VolunteerId);
            return _status;
        }
        
        #endregion

        #region Entities filling

        public TCAssociationTool.Entities.Volunteers GetVolunteerById(Int64 VolunteerId, ref int status)
        {
            TCAssociationTool.Entities.Volunteers objVolunteers = new TCAssociationTool.Entities.Volunteers();
            DataTable dt = new DataTable();
            if (VolunteerId != 0)
            {
                dt = _Volunteers.GetVolunteerById(VolunteerId, ref status);
                if (dt.Rows.Count == 1)
                {
                    objVolunteers.VolunteerId = Convert.ToInt64(dt.Rows[0]["VolunteerId"].ToString());
                    objVolunteers.VolunteerCategoryId = (dt.Rows[0]["VolunteerCategoryId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["VolunteerCategoryId"]) : 0);
                    objVolunteers.EventId = (dt.Rows[0]["EventId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["EventId"]) : 0);
                    objVolunteers.FirstName = dt.Rows[0]["FirstName"].ToString();
                    objVolunteers.LastName = dt.Rows[0]["LastName"].ToString();
                    objVolunteers.Email = dt.Rows[0]["Email"].ToString();
                    objVolunteers.PhoneNo = (dt.Rows[0]["PhoneNo"] != DBNull.Value ? dt.Rows[0]["PhoneNo"].ToString() : null);
                    objVolunteers.Address = (dt.Rows[0]["Address"] != DBNull.Value ? dt.Rows[0]["Address"].ToString() : null);
                    objVolunteers.Comments = (dt.Rows[0]["Comments"] != DBNull.Value ? dt.Rows[0]["Comments"].ToString() : null);
                    objVolunteers.IsActive = (dt.Rows[0]["IsActive"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsActive"]) : false);
                    objVolunteers.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                    objVolunteers.UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"].ToString());

                }
            }
            return objVolunteers;
        }

        public TCAssociationTool.Entities.Volunteers GetVolunteerInfoById(Int64 VolunteerId, ref int status)
        {
            TCAssociationTool.Entities.Volunteers objVolunteers = new TCAssociationTool.Entities.Volunteers();
            DataTable dt = new DataTable();
            if (VolunteerId != 0)
            {
                dt = _Volunteers.GetVolunteerInfoById(VolunteerId, ref status);
                if (dt.Rows.Count == 1)
                {
                    objVolunteers.VolunteerId = Convert.ToInt64(dt.Rows[0]["VolunteerId"].ToString());
                    objVolunteers.VolunteerCategoryId = (dt.Rows[0]["VolunteerCategoryId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["VolunteerCategoryId"]) : 0);
                    objVolunteers.EventId = (dt.Rows[0]["EventId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["EventId"]) : 0);
                    objVolunteers.FirstName = dt.Rows[0]["FirstName"].ToString();
                    objVolunteers.LastName = dt.Rows[0]["LastName"].ToString();
                    objVolunteers.Email = dt.Rows[0]["Email"].ToString();
                    objVolunteers.PhoneNo = (dt.Rows[0]["PhoneNo"] != DBNull.Value ? dt.Rows[0]["PhoneNo"].ToString() : null);
                    objVolunteers.HoursSpent = (dt.Rows[0]["HoursSpent"] != DBNull.Value ? dt.Rows[0]["HoursSpent"].ToString() : null);
                    objVolunteers.Address = (dt.Rows[0]["Address"] != DBNull.Value ? dt.Rows[0]["Address"].ToString() : null);
                    objVolunteers.Comments = (dt.Rows[0]["Comments"] != DBNull.Value ? dt.Rows[0]["Comments"].ToString() : null);
                    objVolunteers.IsActive = (dt.Rows[0]["IsActive"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsActive"]) : false);
                    objVolunteers.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                    objVolunteers.UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"].ToString());
                    objVolunteers.CategoryName = (dt.Rows[0]["CategoryName"] != DBNull.Value ? dt.Rows[0]["CategoryName"].ToString() : null);
                    objVolunteers.EventName = (dt.Rows[0]["EventName"] != DBNull.Value ? dt.Rows[0]["EventName"].ToString() : null);

                }
            }
            return objVolunteers;
        }

        public List<TCAssociationTool.Entities.Volunteers> GetVolunteersListByVariable(Int64 VolunteerCategoryId, Int64 EventId, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.Volunteers> lstVolunteers = new List<TCAssociationTool.Entities.Volunteers>();
            DataTable dt = _Volunteers.GetVolunteersListByVariable(VolunteerCategoryId, EventId, Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _Volunteers.GetVolunteersListByVariable(VolunteerCategoryId, EventId, Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Volunteers objVolunteers = new TCAssociationTool.Entities.Volunteers();

                    objVolunteers.RId = Convert.ToInt64(dr["RId"].ToString());
                    objVolunteers.VolunteerId = Convert.ToInt64(dr["VolunteerId"].ToString());
                    objVolunteers.FirstName = (dr["FirstName"] != DBNull.Value ? dr["FirstName"].ToString() : null);
                    objVolunteers.LastName = (dr["LastName"] != DBNull.Value ? dr["LastName"].ToString() : null);
                    objVolunteers.PhoneNo = (dr["PhoneNo"] != DBNull.Value ? dr["PhoneNo"].ToString() : null);
                    objVolunteers.Address = (dr["Address"] != DBNull.Value ? dr["Address"].ToString() : null);
                    objVolunteers.Email = (dr["Email"] != DBNull.Value ? dr["Email"].ToString() : null);
                    objVolunteers.IsActive = (dr["IsActive"] != DBNull.Value ? Convert.ToBoolean(dr["IsActive"]) : false);
                    objVolunteers.UpdatedBy = dr["UpdatedBy"].ToString();
                    objVolunteers.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"].ToString());

                    lstVolunteers.Add(objVolunteers);
                }
            }
            return lstVolunteers;
        }

        public Entities.Volunteers VolunteerValidationByEmail(string Email, ref int status)
        {
            TCAssociationTool.Entities.Volunteers objVolunteers = new TCAssociationTool.Entities.Volunteers();
            DataTable dt = _Volunteers.VolunteerValidationByEmail(Email, ref status);

            if (Email != null && Email.Trim() != "")
            {
                if (dt.Rows.Count == 1)
                {
                    objVolunteers.VolunteerId = Convert.ToInt64(dt.Rows[0]["VolunteerId"]);
                    objVolunteers.FirstName = dt.Rows[0]["FirstName"].ToString();
                    objVolunteers.LastName = dt.Rows[0]["LastName"].ToString();
                    objVolunteers.Email = dt.Rows[0]["Email"].ToString();          
                }
            }
            return objVolunteers;
        }

        #endregion

        #endregion

        #region Front-end

        public List<TCAssociationTool.Entities.Volunteers> FEGetVolunteersList(Int64 VolunteerCategoryId, Int64 EventId, string Email, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.Volunteers> lstVolunteers = new List<TCAssociationTool.Entities.Volunteers>();
            DataTable dt = _Volunteers.FEGetVolunteersList(VolunteerCategoryId, EventId, Email, Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _Volunteers.FEGetVolunteersList(VolunteerCategoryId, EventId, Email, Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Volunteers objVolunteers = new TCAssociationTool.Entities.Volunteers();

                    objVolunteers.RId = Convert.ToInt64(dr["RId"].ToString());
                    objVolunteers.VolunteerId = Convert.ToInt64(dr["VolunteerId"]);
                    objVolunteers.FirstName = dr["FirstName"].ToString();
                    objVolunteers.LastName = dr["LastName"].ToString();
                    objVolunteers.Email = dr["Email"].ToString();
                    objVolunteers.PhoneNo = dr["PhoneNo"].ToString();
                    objVolunteers.Address = dr["Address"].ToString();
                    objVolunteers.HoursSpent = dr["HoursSpent"].ToString();
                    objVolunteers.Comments = dr["Comments"].ToString();
                    objVolunteers.IsActive = (dr["IsActive"] != DBNull.Value ? Convert.ToBoolean(dr["IsActive"]) : false);
                    objVolunteers.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                    objVolunteers.UpdatedBy = dr["UpdatedBy"].ToString();
                    objVolunteers.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                    objVolunteers.InsertedBy = dr["InsertedBy"].ToString();
                    lstVolunteers.Add(objVolunteers);
                }

            }
            return lstVolunteers;
        }

        public Int64 FEVolunteerInsert(Entities.Volunteers objVolunteer)
        {
            Int64 _status = 0;
            if (objVolunteer != null)
            {
                _status = _Volunteers.FEVolunteerInsert(objVolunteer);

            }
            return _status;
        }

        public List<TCAssociationTool.Entities.Volunteers> FEVolunteersList(Int64 MemberId, ref int Total)
        {
            List<TCAssociationTool.Entities.Volunteers> lstVolunteers = new List<TCAssociationTool.Entities.Volunteers>();
            DataTable dt = _Volunteers.FEVolunteersList(MemberId, ref Total);
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Volunteers objVolunteers = new TCAssociationTool.Entities.Volunteers();

                    objVolunteers.VolunteerId = Convert.ToInt64(dr["VolunteerId"]);
                    objVolunteers.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objVolunteers.FirstName = dr["FirstName"].ToString();
                    objVolunteers.LastName = dr["LastName"].ToString();
                    objVolunteers.Email = dr["Email"].ToString();
                    objVolunteers.PhoneNo = dr["PhoneNo"].ToString();
                    objVolunteers.Address = dr["Address"].ToString();
                    objVolunteers.HoursSpent = dr["HoursSpent"].ToString();
                    objVolunteers.Comments = dr["Comments"].ToString();
                    objVolunteers.IsActive = (dr["IsActive"] != DBNull.Value ? Convert.ToBoolean(dr["IsActive"]) : false);
                    objVolunteers.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                    objVolunteers.UpdatedBy = dr["UpdatedBy"].ToString();
                    objVolunteers.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                    objVolunteers.InsertedBy = dr["InsertedBy"].ToString();
                    objVolunteers.EventName = (dt.Rows[0]["EventName"] != DBNull.Value ? dt.Rows[0]["EventName"].ToString() : null);
                    lstVolunteers.Add(objVolunteers);
                }

            }
            return lstVolunteers;
        }

        #endregion
    }
}
