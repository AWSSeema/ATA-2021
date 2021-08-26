using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TCAssociationTool.DAL
{
   public class Vaccinations
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

       

        public Int64 InsertVaccinations(Entities.Vaccinations objVaccinations)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@Id",objVaccinations.Id),
                    new SqlParameter("@membername",(objVaccinations.membername == null ?DBNull.Value:(object)objVaccinations.membername.Trim())),
                    new SqlParameter("@position",(objVaccinations.position == null ?DBNull.Value:(object)objVaccinations.position.Trim())),
                    new SqlParameter("@relation",(objVaccinations.relation == null ?DBNull.Value:(object)objVaccinations.relation.Trim())),
                    new SqlParameter("@firstname",(objVaccinations.firstname == null ?DBNull.Value:(object)objVaccinations.firstname.Trim())),
                    new SqlParameter("@lastname",(objVaccinations.lastname == null ?DBNull.Value:(object)objVaccinations.lastname.Trim())),
                    new SqlParameter("@phone",(objVaccinations.phone == null ?DBNull.Value:(object)objVaccinations.phone.Trim())),
                    new SqlParameter("@age",(objVaccinations.age == null ?DBNull.Value:(object)objVaccinations.age.Trim())),
                    new SqlParameter("@datesent",(objVaccinations.datesent == DateTime.MinValue ?DBNull.Value:(object)objVaccinations.datesent)),
                    new SqlParameter("@comments",(objVaccinations.comments == null ?DBNull.Value:(object)objVaccinations.comments.Trim())),
                    new SqlParameter("@QStatus",0)
                    };


                _sqlP[10].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("VaccinationsInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[10].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }


        public Int64 VaccinationsCommentUpdate(Entities.Vaccinations objVaccinations)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@Id",objVaccinations.Id),
                    new SqlParameter("@comments",(objVaccinations.comments == null ?DBNull.Value:(object)objVaccinations.comments.Trim())),
                    new SqlParameter("@QStatus",0)
                    };


                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("VaccinationsCommentUpdate", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }



        public DataTable GetVaccinationsListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
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
                dt = _dbAccess.GetDataTable("VaccinationsGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetVaccinationsById(Int64 Id, ref int status)
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
                dt = _dbAccess.GetDataTable("VaccinationsGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteVaccinations(Int64 Id)
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
                _dbAccess.SP_ExecuteScalar("VaccinationsDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 VaccinationsDeleteAll(string Id)
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
                _dbAccess.SP_ExecuteScalar("VaccinationsDeleteAll", ref _sqlP);
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
