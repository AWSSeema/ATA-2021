using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCAssociationTool.DAL;
using System.Data;
namespace TCAssociationTool.BLL
{
   public class Committees
    {
        TCAssociationTool.DAL.Committees _Committees = new TCAssociationTool.DAL.Committees();
        TCAssociationTool.DAL.CommitteeCategories _CommitteeCategories = new TCAssociationTool.DAL.CommitteeCategories();
        string CommiteeImage = System.Configuration.ConfigurationManager.AppSettings["adminsiteurl"] + "committees/thumbphoto/";

        #region Admin

        #region Methods

        public Int64 InsertCommittees(Entities.Committees objCommittees, ref string ImageUrl)
        {
            Int64 _status = 0;
            if (objCommittees != null )
            {
                _status = _Committees.InsertCommittees(objCommittees, ref ImageUrl);

            }
            return _status;
        }

        public Int64 DeleteCommittee(Int64 CommitteeId)
        {
            Int64 _status = 0;
            _status = _Committees.DeleteCommittee(CommitteeId);
            return _status;
        }

        public Int64 UpdateCommitteeStatus(Int64 CommitteeId)
        {
            Int64 _status = 0;
            _status = _Committees.UpdateCommitteeStatus(CommitteeId);
            return _status;
        }

        public Int64 UpdateCommitteesDisplayOrder(int DisplayOrder, Int64 CommitteeId)
        {
            Int64 _status = 0;
            _status = _Committees.UpdateCommitteesDisplayOrder(DisplayOrder, CommitteeId);
            return _status;
        }

        #endregion

        #region Entities filling

        public List<TCAssociationTool.Entities.Committees> GetCommitteesList(ref int status)
        {
            List<TCAssociationTool.Entities.Committees> lstCommittees = new List<Entities.Committees>();
            DataTable dt = _Committees.GetCommitteesList(ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Committees objlstCommittees = new TCAssociationTool.Entities.Committees();

                    objlstCommittees.CommitteeId =Convert.ToInt32( dr["CommitteeId"].ToString());
                    objlstCommittees.Name = (dr["Name"] != DBNull.Value ? dr["Name"].ToString() : null);
                    objlstCommittees.PhoneNo = (dr["PhoneNo"] != DBNull.Value ? dr["PhoneNo"].ToString() : null);
                    objlstCommittees.Address = (dr["Address"] != DBNull.Value ? dr["Address"].ToString() : null);
                    objlstCommittees.City = (dr["City"] != DBNull.Value ? dr["City"].ToString() : null);
                    objlstCommittees.State = (dr["State"] != DBNull.Value ? dr["State"].ToString() : null);
                    objlstCommittees.Email = (dr["Email"] != DBNull.Value ? dr["Email"].ToString() : null);
                    objlstCommittees.ImageUrl = dr["ImageUrl"].ToString();
                    objlstCommittees.DisplayOrder = (dr["DisplayOrder"] != DBNull.Value ? Convert.ToInt32(dr["DisplayOrder"]) : 0);
                    objlstCommittees.IsActive =Convert.ToBoolean(dr["IsActive"]);
                    objlstCommittees.Description = (dr["Description"] != DBNull.Value ? dr["Description"].ToString() : null);
                    objlstCommittees.UpdatedBy = dr["UpdatedBy"].ToString();
                    objlstCommittees.UpdatedTime =Convert.ToDateTime(dr["UpdatedTime"].ToString());

                    lstCommittees.Add(objlstCommittees);
                }

            }
            return lstCommittees;
        }

        public TCAssociationTool.Entities.Committees GetCommitteeById(Int64 CommitteeId, ref int status)
        {
            TCAssociationTool.Entities.Committees objCommittees = new TCAssociationTool.Entities.Committees();
            DataTable dt = new DataTable();
            if (CommitteeId != 0)
            {
                dt = _Committees.GetCommitteeById(CommitteeId, ref status);
                if (dt.Rows.Count == 1)
                {
                    objCommittees.CommitteeId = Convert.ToInt64(dt.Rows[0]["CommitteeId"].ToString());
                    objCommittees.Name = (dt.Rows[0]["Name"] != DBNull.Value ? dt.Rows[0]["Name"].ToString() : null);
                    objCommittees.PhoneNo = (dt.Rows[0]["PhoneNo"] != DBNull.Value ? dt.Rows[0]["PhoneNo"].ToString() : null);
                    objCommittees.Address = (dt.Rows[0]["Address"] != DBNull.Value ? dt.Rows[0]["Address"].ToString() : null);
                    objCommittees.City = (dt.Rows[0]["City"] != DBNull.Value ? dt.Rows[0]["City"].ToString() : null);
                    objCommittees.State = (dt.Rows[0]["State"] != DBNull.Value ? dt.Rows[0]["State"].ToString() : null);
                    objCommittees.Email = (dt.Rows[0]["Email"] != DBNull.Value ? dt.Rows[0]["Email"].ToString() : null);
                    objCommittees.ImageUrl = (dt.Rows[0]["ImageUrl"] != DBNull.Value ? dt.Rows[0]["ImageUrl"].ToString() : null);
                    objCommittees.DisplayOrder = (dt.Rows[0]["DisplayOrder"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["DisplayOrder"]) : 0);
                    objCommittees.IsActive =Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                    objCommittees.Description = (dt.Rows[0]["Description"] != DBNull.Value ? dt.Rows[0]["Description"].ToString() : "");
                    objCommittees.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                    objCommittees.UpdatedTime = Convert.ToDateTime(dt.Rows[0]["UpdatedTime"].ToString());
                  
                }
            }
            return objCommittees;
        }

        public List<TCAssociationTool.Entities.CommitteeMembers> GetCommitteeMembersListById(Int64 CommitteeCategoryId, ref int status)
        {
            List<TCAssociationTool.Entities.CommitteeMembers> lstCommitteeMembers = new List<TCAssociationTool.Entities.CommitteeMembers>();
            DataTable dt = _Committees.GetCommitteeMembersListById(CommitteeCategoryId, ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.CommitteeMembers objlstCommitteeMembers = new TCAssociationTool.Entities.CommitteeMembers();

                    objlstCommitteeMembers.CommitteeId = Convert.ToInt64(dr["CommitteeId"].ToString());
                    objlstCommitteeMembers.Name = dr["Name"].ToString();

                    lstCommitteeMembers.Add(objlstCommitteeMembers);
                }

            }
            return lstCommitteeMembers;
        }

        public List<TCAssociationTool.Entities.Committees> GetCommitteesListByVariable(Int64 CommitteeCategoryId,string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.Committees> lstCommittees = new List<TCAssociationTool.Entities.Committees>();
            DataTable dt = _Committees.GetCommitteesListByVariable(CommitteeCategoryId,Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _Committees.GetCommitteesListByVariable(CommitteeCategoryId,Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Committees objCommittees = new TCAssociationTool.Entities.Committees();

                    objCommittees.RId = Convert.ToInt64(dr["RId"].ToString());
                    objCommittees.CommitteeId = Convert.ToInt64(dr["CommitteeId"].ToString());
                    objCommittees.Name = (dr["Name"] != DBNull.Value ? dr["Name"].ToString() : null);
                    objCommittees.PhoneNo = (dr["PhoneNo"] != DBNull.Value ? dr["PhoneNo"].ToString() : null);
                    objCommittees.Address = (dr["Address"] != DBNull.Value ? dr["Address"].ToString() : null);
                    objCommittees.City = (dr["City"] != DBNull.Value ? dr["City"].ToString() : null);
                    objCommittees.State = (dr["Name"] != DBNull.Value ? dr["Name"].ToString() : null);
                    objCommittees.Email = (dr["Email"] != DBNull.Value ? dr["Email"].ToString() : null);
                    objCommittees.ImageUrl = (dr["ImageUrl"] != DBNull.Value ? dr["ImageUrl"].ToString() : null);
                    objCommittees.DisplayOrder = (dr["DisplayOrder"] != DBNull.Value ? Convert.ToInt32(dr["DisplayOrder"]) : 0);
                    objCommittees.IsActive =Convert.ToBoolean(dr["IsActive"]);
                    objCommittees.Description = (dr["Description"] != DBNull.Value ? dr["Description"].ToString() : null);
                    objCommittees.UpdatedBy = dr["UpdatedBy"].ToString();
                    objCommittees.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"].ToString());
                    objCommittees.CommitteeMemberCount = Convert.ToInt64(dr["CommitteeMemberCount"].ToString());

                    lstCommittees.Add(objCommittees);
                }
            }
            return lstCommittees;
        }

        #endregion

        #endregion

        #region Front End

        public List<TCAssociationTool.Entities.Committees> FECommitteesGetList(ref List<TCAssociationTool.Entities.CommitteeCategories> lstCommitteeCategories, string Type, ref int status)
        {
            List<TCAssociationTool.Entities.Committees> lstCommittees = new List<TCAssociationTool.Entities.Committees>();
            if (Type != "")
            {
                DataSet ds = _Committees.FECommitteesGetList(Type, ref status);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TCAssociationTool.Entities.Committees objCommittees = new TCAssociationTool.Entities.Committees();

                        objCommittees.CommitteeId = Convert.ToInt64(dr["CommitteeId"].ToString());
                        objCommittees.Name = (dr["Name"] != DBNull.Value ? dr["Name"].ToString() : null);
                        objCommittees.PhoneNo = (dr["PhoneNo"] != DBNull.Value ? dr["PhoneNo"].ToString() : null);
                        objCommittees.Address = (dr["Address"] != DBNull.Value ? dr["Address"].ToString() : null);
                        objCommittees.City = (dr["City"] != DBNull.Value ? dr["City"].ToString() : null);
                        objCommittees.State = (dr["State"] != DBNull.Value ? dr["State"].ToString() : null);
                        objCommittees.Email = (dr["Email"] != DBNull.Value ? dr["Email"].ToString() : null);
                        objCommittees.ImageUrl = (dr["ImageUrl"] != DBNull.Value ? dr["ImageUrl"].ToString() : null);
                        objCommittees.DisplayOrder = (dr["DisplayOrder"] != DBNull.Value ? Convert.ToInt32(dr["DisplayOrder"]) : 0);
                        objCommittees.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objCommittees.Description = (dr["Description"] != DBNull.Value ? dr["Description"].ToString() : "");
                        objCommittees.UpdatedBy = dr["UpdatedBy"].ToString();
                        objCommittees.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"].ToString());
                        objCommittees.CommitteeCategoryId = Convert.ToInt64(dr["CommitteeCategoryId"].ToString());
                        objCommittees.CategoryName = dr["CategoryName"].ToString();
                        objCommittees.Designation = (dr["Designation"] != DBNull.Value ? dr["Designation"].ToString() : "");
                        objCommittees.Type = dr["Type"].ToString();

                        lstCommittees.Add(objCommittees);
                    }
                }
                DataTable dt1 = ds.Tables[1];
                if (dt1.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        TCAssociationTool.Entities.CommitteeCategories objlstCommitteeCategories = new TCAssociationTool.Entities.CommitteeCategories();

                        objlstCommitteeCategories.CommitteeCategoryId = Convert.ToInt64(dr["CommitteeCategoryId"].ToString());
                        objlstCommitteeCategories.CategoryName = dr["CategoryName"].ToString();
                        objlstCommitteeCategories.Type = dr["Type"].ToString();
                        objlstCommitteeCategories.DisplayOrder = Convert.ToInt32(dr["DisplayOrder"]);
                        objlstCommitteeCategories.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objlstCommitteeCategories.UpdatedBy = dr["UpdatedBy"].ToString();
                        objlstCommitteeCategories.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"].ToString());
                        objlstCommitteeCategories.CommitteeMemberCount = Convert.ToInt64(dr["CommitteeCount"]);

                        lstCommitteeCategories.Add(objlstCommitteeCategories);
                    }
                }
            }
            return lstCommittees;
        }

        public List<TCAssociationTool.Entities.Committees> FEPastCommitteesGetList(ref List<TCAssociationTool.Entities.CommitteeCategories> lstCommitteeCategories, string Type, string Year, ref int status)
        {
            List<TCAssociationTool.Entities.Committees> lstCommittees = new List<TCAssociationTool.Entities.Committees>();
            
                DataSet ds = _Committees.FEPastCommitteesGetList(Type, Year, ref status);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TCAssociationTool.Entities.Committees objCommittees = new TCAssociationTool.Entities.Committees();

                        objCommittees.CommitteeId = Convert.ToInt64(dr["CommitteeId"].ToString());
                        objCommittees.Name = (dr["Name"] != DBNull.Value ? dr["Name"].ToString() : null);
                        objCommittees.PhoneNo = (dr["PhoneNo"] != DBNull.Value ? dr["PhoneNo"].ToString() : null);
                        objCommittees.Address = (dr["Address"] != DBNull.Value ? dr["Address"].ToString() : null);
                        objCommittees.City = (dr["City"] != DBNull.Value ? dr["City"].ToString() : null);
                        objCommittees.State = (dr["State"] != DBNull.Value ? dr["State"].ToString() : null);
                        objCommittees.Email = (dr["Email"] != DBNull.Value ? dr["Email"].ToString() : null);
                        objCommittees.ImageUrl = (dr["ImageUrl"] != DBNull.Value ? dr["ImageUrl"].ToString() : null);
                        objCommittees.DisplayOrder = (dr["DisplayOrder"] != DBNull.Value ? Convert.ToInt32(dr["DisplayOrder"]) : 0);
                        objCommittees.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objCommittees.Description = (dr["Description"] != DBNull.Value ? dr["Description"].ToString() : "");
                        objCommittees.UpdatedBy = dr["UpdatedBy"].ToString();
                        objCommittees.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"].ToString());
                        objCommittees.CommitteeCategoryId = Convert.ToInt64(dr["CommitteeCategoryId"].ToString());
                        objCommittees.CategoryName = dr["CategoryName"].ToString();
                        objCommittees.Designation = (dr["Designation"] != DBNull.Value ? dr["Designation"].ToString() : "");
                        objCommittees.Type = dr["Type"].ToString();

                        lstCommittees.Add(objCommittees);
                    }
                }
                DataTable dt1 = ds.Tables[1];
                if (dt1.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        TCAssociationTool.Entities.CommitteeCategories objlstCommitteeCategories = new TCAssociationTool.Entities.CommitteeCategories();

                        objlstCommitteeCategories.CommitteeCategoryId = Convert.ToInt64(dr["CommitteeCategoryId"].ToString());
                        objlstCommitteeCategories.CategoryName = dr["CategoryName"].ToString();
                        objlstCommitteeCategories.Type = dr["Type"].ToString();
                        objlstCommitteeCategories.DisplayOrder = Convert.ToInt32(dr["DisplayOrder"]);
                        objlstCommitteeCategories.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objlstCommitteeCategories.Year = dr["Year"].ToString();
                        objlstCommitteeCategories.UpdatedBy = dr["UpdatedBy"].ToString();
                        objlstCommitteeCategories.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"].ToString());
                        objlstCommitteeCategories.CommitteeMemberCount = Convert.ToInt64(dr["CommitteeCount"]);

                        lstCommitteeCategories.Add(objlstCommitteeCategories);
                    }
                }
            return lstCommittees;
        }

        public List<TCAssociationTool.Entities.CommitteeCategories> GetCommitteeYears(ref int status)
        {
            List<TCAssociationTool.Entities.CommitteeCategories> lstCommitteeCategories = new List<Entities.CommitteeCategories>();
            DataTable dt = _Committees.GetCommitteeYears(ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.CommitteeCategories objlstCommitteeCategories = new TCAssociationTool.Entities.CommitteeCategories();

                    objlstCommitteeCategories.Year = dr["Year"].ToString();

                    lstCommitteeCategories.Add(objlstCommitteeCategories);
                }

            }
            return lstCommitteeCategories;
        }

        #endregion
        #region API
        public List<TCAssociationTool.Entities.Committees> APIFECommitteesGetList(ref List<TCAssociationTool.Entities.CommitteeCategories> lstCommitteeCategories, string Type, ref int status)
        {
            List<TCAssociationTool.Entities.Committees> lstCommittees = new List<TCAssociationTool.Entities.Committees>();
            if (Type != "")
            {
                DataSet ds = _Committees.FECommitteesGetList(Type, ref status);
                DataTable dt = ds.Tables[0];
                string ImageAPI= "API";
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TCAssociationTool.Entities.Committees objCommittees = new TCAssociationTool.Entities.Committees();

                        objCommittees.CommitteeId = Convert.ToInt64(dr["CommitteeId"].ToString());
                        objCommittees.Name = (dr["Name"] != DBNull.Value ? dr["Name"].ToString() : null);
                        objCommittees.PhoneNo = (dr["PhoneNo"] != DBNull.Value ? dr["PhoneNo"].ToString() : null);
                        objCommittees.Address = (dr["Address"] != DBNull.Value ? dr["Address"].ToString() : null);
                        objCommittees.City = (dr["City"] != DBNull.Value ? dr["City"].ToString() : null);
                        objCommittees.State = (dr["State"] != DBNull.Value ? dr["State"].ToString() : null);
                        objCommittees.Email = (dr["Email"] != DBNull.Value ? dr["Email"].ToString() : null);
                        if(ImageAPI=="API")
                        {
                            objCommittees.ImageUrl = (dr["ImageUrl"] != DBNull.Value ? CommiteeImage + dr["ImageUrl"].ToString() : "");
                        }
                        else
                        {
                            objCommittees.ImageUrl = (dr["ImageUrl"] != DBNull.Value ? dr["ImageUrl"].ToString() : null);
                        }
                        objCommittees.DisplayOrder = (dr["DisplayOrder"] != DBNull.Value ? Convert.ToInt32(dr["DisplayOrder"]) : 0);
                        objCommittees.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objCommittees.Description = (dr["Description"] != DBNull.Value ? dr["Description"].ToString() : "");
                        objCommittees.UpdatedBy = dr["UpdatedBy"].ToString();
                        objCommittees.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"].ToString());
                        objCommittees.CommitteeCategoryId = Convert.ToInt64(dr["CommitteeCategoryId"].ToString());
                        objCommittees.CategoryName = dr["CategoryName"].ToString();
                        objCommittees.Designation = (dr["Designation"] != DBNull.Value ? dr["Designation"].ToString() : "");
                        objCommittees.Type = dr["Type"].ToString();

                        lstCommittees.Add(objCommittees);
                    }
                }
                DataTable dt1 = ds.Tables[1];
                if (dt1.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        TCAssociationTool.Entities.CommitteeCategories objlstCommitteeCategories = new TCAssociationTool.Entities.CommitteeCategories();

                        objlstCommitteeCategories.CommitteeCategoryId = Convert.ToInt64(dr["CommitteeCategoryId"].ToString());
                        objlstCommitteeCategories.CategoryName = dr["CategoryName"].ToString();
                        objlstCommitteeCategories.Type = dr["Type"].ToString();
                        objlstCommitteeCategories.DisplayOrder = Convert.ToInt32(dr["DisplayOrder"]);
                        objlstCommitteeCategories.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objlstCommitteeCategories.UpdatedBy = dr["UpdatedBy"].ToString();
                        objlstCommitteeCategories.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"].ToString());
                        objlstCommitteeCategories.CommitteeMemberCount = Convert.ToInt64(dr["CommitteeCount"]);

                        lstCommitteeCategories.Add(objlstCommitteeCategories);
                    }
                }
            }
            return lstCommittees;
        }

        #endregion
    }

}
