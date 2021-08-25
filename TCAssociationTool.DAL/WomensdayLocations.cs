using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TCAssociationTool.DAL
{
public    class WomensdayLocations
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;



        public Int64 InsertWomensdayLocations(Entities.WomensdayLocations objWomensdayLocations)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@Id",objWomensdayLocations.Id),
                    new SqlParameter("@location",(objWomensdayLocations.location == null ?DBNull.Value:(object)objWomensdayLocations.location.Trim())),
                    new SqlParameter("@adminemail",(objWomensdayLocations.adminemail == null ?DBNull.Value:(object)objWomensdayLocations.adminemail.Trim())),
                    new SqlParameter("@amount",(objWomensdayLocations.amount == 0 ?DBNull.Value:(object)objWomensdayLocations.amount)),
                    new SqlParameter("@datemodified",(objWomensdayLocations.datemodified == DateTime.MinValue ?DBNull.Value:(object)objWomensdayLocations.datemodified)),
                    new SqlParameter("@status2",objWomensdayLocations.status2),
                    new SqlParameter("@event_details",(objWomensdayLocations.event_details == 0 ?DBNull.Value:(object)objWomensdayLocations.event_details)),
                    new SqlParameter("@QStatus",0)
                    };


                _sqlP[7].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("Womensday_locationsInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[7].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }




        public Int64 WomensdayLocationsCommentUpdate(Int64 Id)
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
                _dbAccess.SP_ExecuteScalar("Womensday_locationsUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }



        public DataTable GetWomensdayLocationsListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
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
                dt = _dbAccess.GetDataTable("Womensday_locationsGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetWomensdayLocationsById(Int64 Id, ref int status)
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
                dt = _dbAccess.GetDataTable("Womensday_locationsGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteWomensdayLocations(Int64 Id)
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
                _dbAccess.SP_ExecuteScalar("Womensday_locationsDelete", ref _sqlP);
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
