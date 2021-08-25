using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TCAssociationTool.BLL
{
 public   class Scholarships
    {
        TCAssociationTool.DAL.Scholarships _Scholarships = new TCAssociationTool.DAL.Scholarships();
        #region Methods

        public Int64 InsertScholarships(Entities.Scholarships objScholarships,ref string documents)
        {
            Int64 _status = 0;
            if (objScholarships != null)
            {
                _status = _Scholarships.InsertScholarships(objScholarships,ref documents);

            }
            return _status;
        }

    
        public Int64 DeleteScholarships(Int64 Id)
        {
            Int64 _status = 0;
            _status = _Scholarships.DeleteScholarships(Id);
            return _status;
        }

        public Int64 UpdateScholarshipsStatus(Int64 id)
        {
            Int64 _status = 0;
            _status = _Scholarships.UpdateScholarshipsStatus(id);
            return _status;
        }



        #endregion

        #region Entities filling



        public TCAssociationTool.Entities.Scholarships GetScholarshipsById(Int64 Id, ref int status)
        {
            TCAssociationTool.Entities.Scholarships objScholarships = new TCAssociationTool.Entities.Scholarships();
            DataTable dt = new DataTable();
            if (Id != 0)
            {
                dt = _Scholarships.GetScholarshipsById(Id, ref status);
                if (dt.Rows.Count == 1)
                {
                    objScholarships.Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                    objScholarships.prefix = (dt.Rows[0]["prefix"] != DBNull.Value ? dt.Rows[0]["prefix"].ToString() : "");
                    objScholarships.firstname = (dt.Rows[0]["firstname"] != DBNull.Value ? dt.Rows[0]["firstname"].ToString() : "");
                    objScholarships.lastname = (dt.Rows[0]["lastname"] != DBNull.Value ? dt.Rows[0]["lastname"].ToString() : "");
                    objScholarships.middlename = (dt.Rows[0]["middlename"] != DBNull.Value ? dt.Rows[0]["middlename"].ToString() : "");
                    objScholarships.email = (dt.Rows[0]["email"] != DBNull.Value ? dt.Rows[0]["email"].ToString() : "");
                    objScholarships.phone = (dt.Rows[0]["phone"] != DBNull.Value ? dt.Rows[0]["phone"].ToString() : "");
                    objScholarships.address = (dt.Rows[0]["address"] != DBNull.Value ? dt.Rows[0]["address"].ToString() : "");
                    objScholarships.address2 = (dt.Rows[0]["address2"] != DBNull.Value ? dt.Rows[0]["address2"].ToString() : "");
                    objScholarships.city = (dt.Rows[0]["city"] != DBNull.Value ? dt.Rows[0]["city"].ToString() : "");
                    objScholarships.state = (dt.Rows[0]["state"] != DBNull.Value ? dt.Rows[0]["state"].ToString() : "");
                    objScholarships.country = (dt.Rows[0]["country"] != DBNull.Value ? dt.Rows[0]["country"].ToString() : "");
                    objScholarships.zipcode = (dt.Rows[0]["zipcode"] != DBNull.Value ? dt.Rows[0]["zipcode"].ToString() : "");
                    objScholarships.permanent_address = (dt.Rows[0]["permanent_address"] != DBNull.Value ? dt.Rows[0]["permanent_address"].ToString() : "");
                    objScholarships.permanent_address2 = (dt.Rows[0]["permanent_address2"] != DBNull.Value ? dt.Rows[0]["permanent_address2"].ToString() : "");
                    objScholarships.permanent_city = (dt.Rows[0]["permanent_city"] != DBNull.Value ? dt.Rows[0]["permanent_city"].ToString() : "");
                    objScholarships.permanent_state = (dt.Rows[0]["permanent_state"] != DBNull.Value ? dt.Rows[0]["permanent_state"].ToString() : "");
                    objScholarships.permanent_country = (dt.Rows[0]["permanent_country"] != DBNull.Value ? dt.Rows[0]["permanent_country"].ToString() : "");
                    objScholarships.permanent_zipcode = (dt.Rows[0]["permanent_zipcode"] != DBNull.Value ? dt.Rows[0]["permanent_zipcode"].ToString() : "");
                    objScholarships.dob = (dt.Rows[0]["dob"] != DBNull.Value ? dt.Rows[0]["dob"].ToString() : "");
                    objScholarships.place_of_birth = (dt.Rows[0]["place_of_birth"] != DBNull.Value ? dt.Rows[0]["place_of_birth"].ToString() : "");
                    objScholarships.citizenship = (dt.Rows[0]["citizenship"] != DBNull.Value ? dt.Rows[0]["citizenship"].ToString() : "");
                    objScholarships.institution = (dt.Rows[0]["institution"] != DBNull.Value ? dt.Rows[0]["institution"].ToString() : "");
                    objScholarships.study = (dt.Rows[0]["study"] != DBNull.Value ? dt.Rows[0]["study"].ToString() : "");
                    objScholarships.parent_prefix = (dt.Rows[0]["parent_prefix"] != DBNull.Value ? dt.Rows[0]["parent_prefix"].ToString() : "");
                    objScholarships.parent_firstname = (dt.Rows[0]["parent_firstname"] != DBNull.Value ? dt.Rows[0]["parent_firstname"].ToString() : "");
                    objScholarships.parent_middlename = (dt.Rows[0]["parent_middlename"] != DBNull.Value ? dt.Rows[0]["parent_middlename"].ToString() : "");
                    objScholarships.parent_lastname = (dt.Rows[0]["parent_lastname"] != DBNull.Value ? dt.Rows[0]["parent_lastname"].ToString() : "");
                    objScholarships.parent_email = (dt.Rows[0]["parent_email"] != DBNull.Value ? dt.Rows[0]["parent_email"].ToString() : "");
                    objScholarships.parent_phone = (dt.Rows[0]["parent_phone"] != DBNull.Value ? dt.Rows[0]["parent_phone"].ToString() : "");
                    objScholarships.parent_address = (dt.Rows[0]["parent_address"] != DBNull.Value ? dt.Rows[0]["parent_address"].ToString() : "");
                    objScholarships.parent_address2 = (dt.Rows[0]["parent_address2"] != DBNull.Value ? dt.Rows[0]["parent_address2"].ToString() : "");
                    objScholarships.parent_city = (dt.Rows[0]["parent_city"] != DBNull.Value ? dt.Rows[0]["parent_city"].ToString() : "");
                    objScholarships.parent_state = (dt.Rows[0]["parent_state"] != DBNull.Value ? dt.Rows[0]["parent_state"].ToString() : "");
                    objScholarships.parent_country = (dt.Rows[0]["parent_country"] != DBNull.Value ? dt.Rows[0]["parent_country"].ToString() : "");
                    objScholarships.parent_zipcode = (dt.Rows[0]["parent_zipcode"] != DBNull.Value ? dt.Rows[0]["parent_zipcode"].ToString() : "");
                    objScholarships.relationship = (dt.Rows[0]["relationship"] != DBNull.Value ? dt.Rows[0]["relationship"].ToString() : "");
                    objScholarships.father_occupation = (dt.Rows[0]["father_occupation"] != DBNull.Value ? dt.Rows[0]["father_occupation"].ToString() : "");
                    objScholarships.mother_occupation = (dt.Rows[0]["mother_occupation"] != DBNull.Value ? dt.Rows[0]["mother_occupation"].ToString() : "");
                    objScholarships.income = (dt.Rows[0]["income"] != DBNull.Value ? dt.Rows[0]["income"].ToString() : "");
                    objScholarships.school_name = (dt.Rows[0]["school_name"] != DBNull.Value ? dt.Rows[0]["school_name"].ToString() : "");
                    objScholarships.school_location = (dt.Rows[0]["school_location"] != DBNull.Value ? dt.Rows[0]["school_location"].ToString() : "");
                    objScholarships.attendance = (dt.Rows[0]["attendance"] != DBNull.Value ? dt.Rows[0]["attendance"].ToString() : "");
                    objScholarships.award1_name = (dt.Rows[0]["award1_name"] != DBNull.Value ? dt.Rows[0]["award1_name"].ToString() : "");
                    objScholarships.award1_year = (dt.Rows[0]["award1_year"] != DBNull.Value ? dt.Rows[0]["award1_year"].ToString() : "");
                    objScholarships.award2_name = (dt.Rows[0]["award2_name"] != DBNull.Value ? dt.Rows[0]["award2_name"].ToString() : "");
                    objScholarships.award2_year = (dt.Rows[0]["award2_year"] != DBNull.Value ? dt.Rows[0]["award2_year"].ToString() : "");
                    objScholarships.tuition_cost = (dt.Rows[0]["tuition_cost"] != DBNull.Value ? dt.Rows[0]["tuition_cost"].ToString() : "");
                    objScholarships.housing_cost = (dt.Rows[0]["housing_cost"] != DBNull.Value ? dt.Rows[0]["housing_cost"].ToString() : "");
                    objScholarships.extra_activities = (dt.Rows[0]["extra_activities"] != DBNull.Value ? dt.Rows[0]["extra_activities"].ToString() : "");
                    objScholarships.documents = (dt.Rows[0]["documents"] != DBNull.Value ? dt.Rows[0]["documents"].ToString() : "");
                    objScholarships.as_number = (dt.Rows[0]["as_number"] != DBNull.Value ? dt.Rows[0]["as_number"].ToString() : "");
                    objScholarships.datesent = (dt.Rows[0]["datesent"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["datesent"]) : DateTime.MinValue);
                    objScholarships.status2 = (dt.Rows[0]["status2"] != DBNull.Value ? dt.Rows[0]["status2"].ToString() : "");
                    objScholarships.brief_note = (dt.Rows[0]["brief_note"] != DBNull.Value ? dt.Rows[0]["brief_note"].ToString() : "");
                    objScholarships.is_ata_member = (dt.Rows[0]["is_ata_member"] != DBNull.Value ? dt.Rows[0]["is_ata_member"].ToString() : "");
                  
        

                }
            }
            return objScholarships;
        }



        public List<TCAssociationTool.Entities.Scholarships> GetScholarshipsListByVariable(string StartDate, string EndDate, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.Scholarships> lstScholarships = new List<TCAssociationTool.Entities.Scholarships>();
            DataTable dt = _Scholarships.GetScholarshipsListByVariable(StartDate, EndDate, Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _Scholarships.GetScholarshipsListByVariable(StartDate, EndDate, Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Scholarships objScholarships = new TCAssociationTool.Entities.Scholarships();

                    objScholarships.RId = Convert.ToInt64(dr["RId"].ToString());
                    objScholarships.Id = Convert.ToInt64(dr["Id"].ToString());
                    objScholarships.prefix = (dr["prefix"] != DBNull.Value ? dr["prefix"].ToString() : "");
                    objScholarships.firstname = (dr["firstname"] != DBNull.Value ? dr["firstname"].ToString() : "");
                    objScholarships.middlename = (dr["middlename"] != DBNull.Value ? dr["middlename"].ToString() : "");
                    objScholarships.lastname = (dr["lastname"] != DBNull.Value ? dr["lastname"].ToString() : "");
                    objScholarships.email = (dr["email"] != DBNull.Value ? dr["email"].ToString() : "");
                    objScholarships.phone = (dr["phone"] != DBNull.Value ? dr["phone"].ToString() : "");
                    objScholarships.address = (dr["address"] != DBNull.Value ? dr["address"].ToString() : "");
                    objScholarships.address2 = (dr["address2"] != DBNull.Value ? dr["address2"].ToString() : "");
                    objScholarships.city = (dr["city"] != DBNull.Value ? dr["city"].ToString() : "");
                    objScholarships.state = (dr["state"] != DBNull.Value ? dr["state"].ToString() : "");
                    objScholarships.country = (dr["country"] != DBNull.Value ? dr["country"].ToString() : "");
                    objScholarships.zipcode = (dr["zipcode"] != DBNull.Value ? dr["zipcode"].ToString() : "");
                    objScholarships.permanent_address = (dr["permanent_address"] != DBNull.Value ? dr["permanent_address"].ToString() : "");
                    objScholarships.permanent_address2 = (dr["permanent_address2"] != DBNull.Value ? dr["permanent_address2"].ToString() : "");
                    objScholarships.permanent_city = (dr["permanent_city"] != DBNull.Value ? dr["permanent_city"].ToString() : "");
                    objScholarships.permanent_country = (dr["permanent_country"] != DBNull.Value ? dr["permanent_country"].ToString() : "");
                    objScholarships.permanent_state = (dr["permanent_state"] != DBNull.Value ? dr["permanent_state"].ToString() : "");
                    objScholarships.permanent_zipcode = (dr["permanent_zipcode"] != DBNull.Value ? dr["permanent_zipcode"].ToString() : "");
                    objScholarships.dob = (dr["dob"] != DBNull.Value ? dr["dob"].ToString() : "");
                    objScholarships.place_of_birth = (dr["place_of_birth"] != DBNull.Value ? dr["place_of_birth"].ToString() : "");
                    objScholarships.citizenship = (dr["citizenship"] != DBNull.Value ? dr["citizenship"].ToString() : "");
                    objScholarships.institution = (dr["institution"] != DBNull.Value ? dr["institution"].ToString() : "");
                    objScholarships.study = (dr["study"] != DBNull.Value ? dr["study"].ToString() : "");
                    objScholarships.parent_prefix = (dr["parent_prefix"] != DBNull.Value ? dr["parent_prefix"].ToString() : "");
                    objScholarships.parent_firstname = (dr["parent_firstname"] != DBNull.Value ? dr["parent_firstname"].ToString() : "");
                    objScholarships.parent_middlename = (dr["parent_middlename"] != DBNull.Value ? dr["parent_middlename"].ToString() : "");
                    objScholarships.parent_lastname = (dr["parent_lastname"] != DBNull.Value ? dr["parent_lastname"].ToString() : "");
                    objScholarships.parent_email = (dr["parent_email"] != DBNull.Value ? dr["parent_email"].ToString() : "");
                    objScholarships.parent_phone = (dr["parent_phone"] != DBNull.Value ? dr["parent_phone"].ToString() : "");
                    objScholarships.parent_address = (dr["parent_address"] != DBNull.Value ? dr["parent_address"].ToString() : "");
                    objScholarships.parent_address2 = (dr["parent_address2"] != DBNull.Value ? dr["parent_address2"].ToString() : "");
                    objScholarships.parent_city = (dr["parent_city"] != DBNull.Value ? dr["parent_city"].ToString() : "");
                    objScholarships.parent_state = (dr["parent_state"] != DBNull.Value ? dr["parent_state"].ToString() : "");
                    objScholarships.parent_country = (dr["parent_country"] != DBNull.Value ? dr["parent_country"].ToString() : "");
                    objScholarships.parent_zipcode = (dr["parent_zipcode"] != DBNull.Value ? dr["parent_zipcode"].ToString() : "");
                    objScholarships.relationship = (dr["relationship"] != DBNull.Value ? dr["relationship"].ToString() : "");
                    objScholarships.father_occupation = (dr["father_occupation"] != DBNull.Value ? dr["father_occupation"].ToString() : "");
                    objScholarships.mother_occupation = (dr["mother_occupation"] != DBNull.Value ? dr["mother_occupation"].ToString() : "");
                    objScholarships.income = (dr["income"] != DBNull.Value ? dr["income"].ToString() : "");
                    objScholarships.school_name = (dr["school_name"] != DBNull.Value ? dr["school_name"].ToString() : "");
                    objScholarships.school_location = (dr["school_location"] != DBNull.Value ? dr["school_location"].ToString() : "");
                    objScholarships.attendance = (dr["attendance"] != DBNull.Value ? dr["attendance"].ToString() : "");
                    objScholarships.award1_name = (dr["award1_name"] != DBNull.Value ? dr["award1_name"].ToString() : "");
                    objScholarships.award1_year = (dr["award1_year"] != DBNull.Value ? dr["award1_year"].ToString() : "");
                    objScholarships.award2_name = (dr["award2_name"] != DBNull.Value ? dr["award2_name"].ToString() : "");
                    objScholarships.award2_year = (dr["award2_year"] != DBNull.Value ? dr["award2_year"].ToString() : "");
                    objScholarships.tuition_cost = (dr["tuition_cost"] != DBNull.Value ? dr["tuition_cost"].ToString() : "");
                    objScholarships.housing_cost = (dr["housing_cost"] != DBNull.Value ? dr["housing_cost"].ToString() : "");
                    objScholarships.extra_activities = (dr["extra_activities"] != DBNull.Value ? dr["extra_activities"].ToString() : "");
                    objScholarships.documents = (dr["documents"] != DBNull.Value ? dr["documents"].ToString() : "");
                    objScholarships.as_number = (dr["as_number"] != DBNull.Value ? dr["as_number"].ToString() : "");
                    objScholarships.datesent = (dr["datesent"] != DBNull.Value ? Convert.ToDateTime(dr["datesent"]) : DateTime.MinValue);
                    objScholarships.status2 = (dr["status2"] != DBNull.Value ? dr["status2"].ToString() : "");
                    objScholarships.brief_note = (dr["brief_note"] != DBNull.Value ? dr["brief_note"].ToString() : "");
                    objScholarships.is_ata_member = (dr["is_ata_member"] != DBNull.Value ? dr["is_ata_member"].ToString() : "");



                    lstScholarships.Add(objScholarships);
                }
            }
            return lstScholarships;
        }

        #endregion
    }
}
