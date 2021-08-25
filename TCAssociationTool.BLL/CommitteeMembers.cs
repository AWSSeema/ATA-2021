using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCAssociationTool.DAL;
using System.Data;
namespace TCAssociationTool.BLL
{
  public  class CommitteeMembers
    {
        TCAssociationTool.DAL.CommitteeMembers _CommitteeMembers = new TCAssociationTool.DAL.CommitteeMembers();

        #region Methods

        public Int64 InsertCommitteeMembers(Entities.CommitteeMembers objCommitteeMembers)
        {
            Int64 _status = 0;
            if (objCommitteeMembers != null)
            {
                _status = _CommitteeMembers.InsertCommitteeMembers(objCommitteeMembers);

            }
            return _status;
        }

        public Int64 DeleteCommitteeMember(Int64 CommitteeMemberId)
        {
            Int64 _status = 0;
            _status = _CommitteeMembers.DeleteCommitteeMember(CommitteeMemberId);
            return _status;
        }

        public Int64 UpdateCommitteeMemberDisplayOrder(int DisplayOrder, Int64 CommitteeMemberId)
        {
            Int64 _status = 0;
            _status = _CommitteeMembers.UpdateCommitteeMemberDisplayOrder(DisplayOrder, CommitteeMemberId);
            return _status;
        }


        #endregion

        #region Entities filling

        public List<TCAssociationTool.Entities.CommitteeMembers> GetCommitteeMembersList(ref int status)
        {
            List<TCAssociationTool.Entities.CommitteeMembers> lstCommitteeMembers = new List<TCAssociationTool.Entities.CommitteeMembers>();
            DataTable dt = _CommitteeMembers.GetCommitteeMembersList(ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.CommitteeMembers objlstCommitteeMembers = new TCAssociationTool.Entities.CommitteeMembers();

                    objlstCommitteeMembers.RId = Convert.ToInt64(dr["RId"].ToString());
                    objlstCommitteeMembers.CommitteeMemberId = Convert.ToInt64(dr["CommitteeMemberId"].ToString());
                    objlstCommitteeMembers.CommitteeId = Convert.ToInt64(dr["CommitteeId"].ToString());
                    objlstCommitteeMembers.CommitteeCategoryId = Convert.ToInt64(dr["CommitteeCategoryId"].ToString());
                    objlstCommitteeMembers.Designation = (dr["Designation"] != DBNull.Value ? dr["Designation"].ToString() : null);

                    lstCommitteeMembers.Add(objlstCommitteeMembers);
                }

            }
            return lstCommitteeMembers;
        }        

        public TCAssociationTool.Entities.CommitteeMembers GetCommitteeMemberById(Int64 CommitteeMemberId, ref int status)
        {
            TCAssociationTool.Entities.CommitteeMembers objCommitteeMembers = new TCAssociationTool.Entities.CommitteeMembers();
            DataTable dt = new DataTable();
            if (CommitteeMemberId != 0)
            {
                dt = _CommitteeMembers.GetCommitteeMemberById(CommitteeMemberId, ref status);
                if (dt.Rows.Count == 1)
                {
                    objCommitteeMembers.CommitteeCategoryId = Convert.ToInt64(dt.Rows[0]["CommitteeCategoryId"].ToString());
                    objCommitteeMembers.CommitteeId = Convert.ToInt64(dt.Rows[0]["CommitteeId"].ToString());
                    objCommitteeMembers.CommitteeMemberId = Convert.ToInt64(dt.Rows[0]["CommitteeMemberId"].ToString());
                    objCommitteeMembers.DisplayOrder = (dt.Rows[0]["DisplayOrder"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["DisplayOrder"]) : 0);
                    objCommitteeMembers.Designation = (dt.Rows[0]["Designation"] != DBNull.Value ? dt.Rows[0]["Designation"].ToString() : null);

                }
            }
            return objCommitteeMembers;
        }

        public List<TCAssociationTool.Entities.CommitteeMembers> GetCommitteeMembersListByVariable(string Search, string Location, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.CommitteeMembers> lstCommitteeMembers = new List<TCAssociationTool.Entities.CommitteeMembers>();
            DataTable dt = _CommitteeMembers.GetCommitteeMembersListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _CommitteeMembers.GetCommitteeMembersListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.CommitteeMembers objCommitteeMembers = new TCAssociationTool.Entities.CommitteeMembers();

                    objCommitteeMembers.CommitteeMemberId = Convert.ToInt64(dr["CommitteeMemberId"].ToString());
                    objCommitteeMembers.CommitteeId =Convert.ToInt64 (dr["CommitteeId"].ToString());
                    objCommitteeMembers.CommitteeCategoryId = Convert.ToInt64(dr["CommitteeCategoryId"].ToString());
                    objCommitteeMembers.Designation = (dr["Designation"] != DBNull.Value ? dr["Designation"].ToString() : null);

                    lstCommitteeMembers.Add(objCommitteeMembers);
                }
            }
            return lstCommitteeMembers;
        }

        public List<TCAssociationTool.Entities.Committees> CommitteeMembersList(Int64 CommitteeCategoryId, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.Committees> lstCommittees = new List<TCAssociationTool.Entities.Committees>();
            DataTable dt = _CommitteeMembers.CommitteeMembersList(CommitteeCategoryId, Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _CommitteeMembers.CommitteeMembersList(CommitteeCategoryId, Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Committees objCommittees = new TCAssociationTool.Entities.Committees();

                    objCommittees.RId = Convert.ToInt64(dr["RId"].ToString());
                    objCommittees.CommitteeId = Convert.ToInt64(dr["CommitteeId"].ToString());
                    objCommittees.CommitteeMemberId = (dr["CommitteeMemberId"] != DBNull.Value ? Convert.ToInt32(dr["CommitteeMemberId"]) : 0);
                    objCommittees.CommitteeCategoryId = (dr["CommitteeCategoryId"] != DBNull.Value ? Convert.ToInt32(dr["CommitteeCategoryId"]) : 0);
                    objCommittees.Name = dr["Name"].ToString();
                    objCommittees.PhoneNo = dr["PhoneNo"].ToString();
                    objCommittees.Address = dr["Address"].ToString();
                    objCommittees.City = dr["City"].ToString();
                    objCommittees.State = dr["State"].ToString();
                    objCommittees.Email = dr["Email"].ToString();
                    objCommittees.ImageUrl = dr["ImageUrl"].ToString();
                    objCommittees.DisplayOrder = (dr["DisplayOrder"] != DBNull.Value ? Convert.ToInt32(dr["DisplayOrder"]) : 0);
                    objCommittees.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    objCommittees.Description = (dr["Description"] != DBNull.Value ? dr["Description"].ToString() : "");
                    objCommittees.UpdatedBy = dr["UpdatedBy"].ToString();
                    objCommittees.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"].ToString());

                    lstCommittees.Add(objCommittees);
                }
            }
            return lstCommittees;
        }

        #endregion
    }
}
