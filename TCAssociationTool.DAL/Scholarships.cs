using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace TCAssociationTool.DAL
{
  public  class Scholarships
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;



        public Int64 InsertScholarships(Entities.Scholarships objScholarships,ref string documents)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@Id",objScholarships.Id),
                    new SqlParameter("@prefix",(objScholarships.prefix == null ?DBNull.Value:(object)objScholarships.prefix.Trim())),
                    new SqlParameter("@firstname",(objScholarships.firstname == null ?DBNull.Value:(object)objScholarships.firstname.Trim())),
                    new SqlParameter("@middlename",(objScholarships.middlename == null ?DBNull.Value:(object)objScholarships.middlename.Trim())),
                    new SqlParameter("@lastname",(objScholarships.lastname == null ?DBNull.Value:(object)objScholarships.lastname.Trim())),
                    new SqlParameter("@email",(objScholarships.email == null ?DBNull.Value:(object)objScholarships.email.Trim())),
                    new SqlParameter("@phone",(objScholarships.phone == null ?DBNull.Value:(object)objScholarships.phone.Trim())),
                    new SqlParameter("@address",(objScholarships.address == null ?DBNull.Value:(object)objScholarships.address.Trim())),
                    new SqlParameter("@address2",(objScholarships.address2 == null ?DBNull.Value:(object)objScholarships.address2.Trim())),
                    new SqlParameter("@city",(objScholarships.city == null ?DBNull.Value:(object)objScholarships.city.Trim())),
                    new SqlParameter("@state",(objScholarships.state == null ?DBNull.Value:(object)objScholarships.state.Trim())),
                    new SqlParameter("@country",(objScholarships.country == null ?DBNull.Value:(object)objScholarships.country.Trim())),
                    new SqlParameter("@zipcode",(objScholarships.zipcode == null ?DBNull.Value:(object)objScholarships.zipcode.Trim())),
                    new SqlParameter("@permanent_address",(objScholarships.permanent_address == null ?DBNull.Value:(object)objScholarships.permanent_address.Trim())),
                    new SqlParameter("@permanent_address2",(objScholarships.permanent_address2 == null ?DBNull.Value:(object)objScholarships.permanent_address2.Trim())),
                    new SqlParameter("@permanent_city",(objScholarships.permanent_city == null ?DBNull.Value:(object)objScholarships.permanent_city.Trim())),
                    new SqlParameter("@permanent_state",(objScholarships.permanent_state == null ?DBNull.Value:(object)objScholarships.permanent_state.Trim())),
                    new SqlParameter("@permanent_country",(objScholarships.permanent_country == null ?DBNull.Value:(object)objScholarships.permanent_country.Trim())),
                    new SqlParameter("@permanent_zipcode",(objScholarships.permanent_zipcode == null ?DBNull.Value:(object)objScholarships.permanent_zipcode.Trim())),
                    new SqlParameter("@dob",(objScholarships.dob == null ?DBNull.Value:(object)objScholarships.dob.Trim())),
                    new SqlParameter("@place_of_birth",(objScholarships.place_of_birth == null ?DBNull.Value:(object)objScholarships.place_of_birth.Trim())),
                    new SqlParameter("@citizenship",(objScholarships.citizenship == null ?DBNull.Value:(object)objScholarships.citizenship.Trim())),
                    new SqlParameter("@institution",(objScholarships.institution == null ?DBNull.Value:(object)objScholarships.institution.Trim())),
                    new SqlParameter("@study",(objScholarships.study == null ?DBNull.Value:(object)objScholarships.study.Trim())),
                    new SqlParameter("@parent_prefix",(objScholarships.parent_prefix == null ?DBNull.Value:(object)objScholarships.parent_prefix.Trim())),
                    new SqlParameter("@parent_firstname",(objScholarships.parent_firstname == null ?DBNull.Value:(object)objScholarships.parent_firstname.Trim())),
                    new SqlParameter("@parent_middlename",(objScholarships.parent_middlename == null ?DBNull.Value:(object)objScholarships.parent_middlename.Trim())),
                    new SqlParameter("@parent_lastname",(objScholarships.parent_lastname == null ?DBNull.Value:(object)objScholarships.parent_lastname.Trim())),
                    new SqlParameter("@parent_email",(objScholarships.parent_email == null ?DBNull.Value:(object)objScholarships.parent_email.Trim())),
                    new SqlParameter("@parent_phone",(objScholarships.parent_phone == null ?DBNull.Value:(object)objScholarships.parent_phone.Trim())),
                    new SqlParameter("@parent_address",(objScholarships.parent_address == null ?DBNull.Value:(object)objScholarships.parent_address.Trim())),
                    new SqlParameter("@parent_address2",(objScholarships.parent_address2 == null ?DBNull.Value:(object)objScholarships.parent_address2.Trim())),
                    new SqlParameter("@parent_city",(objScholarships.parent_city == null ?DBNull.Value:(object)objScholarships.parent_city.Trim())),
                    new SqlParameter("@parent_state",(objScholarships.parent_state == null ?DBNull.Value:(object)objScholarships.parent_state.Trim())),
                    new SqlParameter("@parent_country",(objScholarships.parent_country == null ?DBNull.Value:(object)objScholarships.parent_country.Trim())),
                    new SqlParameter("@parent_zipcode",(objScholarships.parent_zipcode == null ?DBNull.Value:(object)objScholarships.parent_zipcode.Trim())),
                    new SqlParameter("@relationship",(objScholarships.relationship == null ?DBNull.Value:(object)objScholarships.relationship.Trim())),
                    new SqlParameter("@father_occupation",(objScholarships.father_occupation == null ?DBNull.Value:(object)objScholarships.father_occupation.Trim())),
                    new SqlParameter("@mother_occupation",(objScholarships.mother_occupation == null ?DBNull.Value:(object)objScholarships.mother_occupation.Trim())),
                    new SqlParameter("@income",(objScholarships.income == null ?DBNull.Value:(object)objScholarships.income.Trim())),
                    new SqlParameter("@school_name",(objScholarships.school_name == null ?DBNull.Value:(object)objScholarships.school_name.Trim())),
                    new SqlParameter("@school_location",(objScholarships.school_location == null ?DBNull.Value:(object)objScholarships.school_location.Trim())),
                    new SqlParameter("@attendance",(objScholarships.attendance == null ?DBNull.Value:(object)objScholarships.attendance.Trim())),
                    new SqlParameter("@award1_name",(objScholarships.award1_name == null ?DBNull.Value:(object)objScholarships.award1_name.Trim())),
                    new SqlParameter("@award1_year",(objScholarships.award1_year == null ?DBNull.Value:(object)objScholarships.award1_year.Trim())),
                    new SqlParameter("@award2_name",(objScholarships.award2_name == null ?DBNull.Value:(object)objScholarships.award2_name.Trim())),
                    new SqlParameter("@award2_year",(objScholarships.award2_year == null ?DBNull.Value:(object)objScholarships.award2_year.Trim())),
                    new SqlParameter("@tuition_cost",(objScholarships.tuition_cost == null ?DBNull.Value:(object)objScholarships.tuition_cost.Trim())),
                    new SqlParameter("@housing_cost",(objScholarships.housing_cost == null ?DBNull.Value:(object)objScholarships.housing_cost.Trim())),
                    new SqlParameter("@extra_activities",(objScholarships.extra_activities == null ?DBNull.Value:(object)objScholarships.extra_activities.Trim())),
                    new SqlParameter("@documents",documents),
                    new SqlParameter("@as_number",(objScholarships.as_number == null ?DBNull.Value:(object)objScholarships.as_number.Trim())),
                    new SqlParameter("@datesent",(objScholarships.datesent == DateTime.MinValue ?DBNull.Value:(object)objScholarships.datesent)),
                    new SqlParameter("@status2",(objScholarships.status2 == null ?DBNull.Value:(object)objScholarships.status2.Trim())),
                    new SqlParameter("@brief_note",(objScholarships.brief_note == null ?DBNull.Value:(object)objScholarships.brief_note.Trim())),
                    new SqlParameter("@is_ata_member",(objScholarships.is_ata_member == null ?DBNull.Value:(object)objScholarships.is_ata_member.Trim())),
                    new SqlParameter("@QStatus",0)
                    };

                _sqlP[50].SqlDbType = SqlDbType.NVarChar;
                _sqlP[50].Size = 512;
                _sqlP[50].Direction = System.Data.ParameterDirection.InputOutput;


                _sqlP[56].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("ScholarshipsInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[56].Value);

                documents = _sqlP[50].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }


     


        public DataTable GetScholarshipsListByVariable(string StartDate, string EndDate, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@StartDate",StartDate),
                    new SqlParameter("@EndDate",EndDate),
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@Total",Total)
                };

                _sqlP[6].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("ScholarshipsGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[6].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public DataTable GetScholarshipsById(Int64 Id, ref int status)
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
                dt = _dbAccess.GetDataTable("ScholarshipsGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteScholarships(Int64 Id)
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
                _dbAccess.SP_ExecuteScalar("ScholarshipsDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable ExportScholarshipsList(string Search, string StartDate, string EndDate, string Sort)
        {
            DataTable dt = null;
            int Total = 0;
            try
            {
                _sqlP = new[]
                {

                    new SqlParameter("@StartDate",StartDate),
                    new SqlParameter("@EndDate",EndDate),
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@Total",Total)
                };

                _sqlP[4].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("ExportScholarshipsGetList", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public Int64 UpdateScholarshipsStatus(Int64 id)
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
                _dbAccess.SP_ExecuteScalar("ScholarshipsUpdateStatus", ref _sqlP);
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
