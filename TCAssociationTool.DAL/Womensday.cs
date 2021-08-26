using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TCAssociationTool.DAL
{
  public  class Womensday
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;



        public Int64 InsertWomensday(Entities.Womensday objWomensday)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@Id",objWomensday.Id),
                    new SqlParameter("@firstname",(objWomensday.firstname == null ?DBNull.Value:(object)objWomensday.firstname.Trim())),
                    new SqlParameter("@lastname",(objWomensday.lastname == null ?DBNull.Value:(object)objWomensday.lastname.Trim())),
                    new SqlParameter("@email",(objWomensday.email == null ?DBNull.Value:(object)objWomensday.email.Trim())),
                    new SqlParameter("@phone",(objWomensday.phone == null ?DBNull.Value:(object)objWomensday.phone.Trim())),
                    new SqlParameter("@datesent",(objWomensday.datesent == DateTime.MinValue ?DBNull.Value:(object)objWomensday.datesent)),
                    new SqlParameter("@amount",(objWomensday.amount == 0 ?DBNull.Value:(object)objWomensday.amount)),
                    new SqlParameter("@address",(objWomensday.address == null ?DBNull.Value:(object)objWomensday.address.Trim())),
                    new SqlParameter("@location",(objWomensday.location == null ?DBNull.Value:(object)objWomensday.location.Trim())),
                    new SqlParameter("@comments",(objWomensday.comments == null ?DBNull.Value:(object)objWomensday.comments.Trim())),
                    new SqlParameter("@city",(objWomensday.city == null ?DBNull.Value:(object)objWomensday.city.Trim())),
                    new SqlParameter("@state",(objWomensday.state == null ?DBNull.Value:(object)objWomensday.state.Trim())),
                    new SqlParameter("@event_id",(objWomensday.event_id == 0 ?DBNull.Value:(object)objWomensday.event_id)),
                    new SqlParameter("@QStatus",0)
                    };


                _sqlP[13].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("WomensdayInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[13].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }




        public Int64 WomensdayUpdateComments(Entities.Womensday objWomensday)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@Id",objWomensday.Id),
                    new SqlParameter("@comments",(objWomensday.comments == null ?DBNull.Value:(object)objWomensday.comments.Trim())),
                    new SqlParameter("@QStatus",0)
                    };


                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("WomensdayUpdateComments", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }


        public DataTable GetWomensdayListByVariable(string location,string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@location",location),
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@Total",Total)
                };

                _sqlP[5].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("WomensdayGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[5].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

     
        public DataSet GetWomensdayById(Int64 Id, ref int Status)
        {
            DataSet dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@Id",Id),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataSet("WomensdayGetById", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }



        public Int64 DeleteWomensday(Int64 Id)
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
                _dbAccess.SP_ExecuteScalar("WomensdayDelete", ref _sqlP);
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
