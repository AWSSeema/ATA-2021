using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TCAssociationTool.DAL
{
   public class CulturalLocations
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;



        public Int64 InsertCulturalLocations(Entities.CulturalLocations objCulturalLocations)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@Id",objCulturalLocations.Id),
                    new SqlParameter("@location",(objCulturalLocations.location == null ?DBNull.Value:(object)objCulturalLocations.location.Trim())),
                    new SqlParameter("@adminemail",(objCulturalLocations.adminemail == null ?DBNull.Value:(object)objCulturalLocations.adminemail.Trim())),
                    new SqlParameter("@datemodified",(objCulturalLocations.datemodified == DateTime.MinValue ?(object)DBNull.Value:objCulturalLocations.datemodified)),
                    new SqlParameter("@status2",(objCulturalLocations.status2 )),
                    new SqlParameter("@amount",(objCulturalLocations.amount == 0 ?DBNull.Value:(object)objCulturalLocations.amount)),
                    new SqlParameter("@lastdate",(objCulturalLocations.lastdate == DateTime.MinValue ?(object)DBNull.Value:objCulturalLocations.lastdate)),
                    new SqlParameter("@eventdate",(objCulturalLocations.eventdate == DateTime.MinValue ?(object)DBNull.Value:objCulturalLocations.eventdate)),
                    new SqlParameter("@address",(objCulturalLocations.address == null ?DBNull.Value:(object)objCulturalLocations.address.Trim())),
                    new SqlParameter("@contact_name",(objCulturalLocations.contact_name == null ?DBNull.Value:(object)objCulturalLocations.contact_name.Trim())),
                    new SqlParameter("@contact_phone",(objCulturalLocations.contact_phone == null ?DBNull.Value:(object)objCulturalLocations.contact_phone.Trim())),
                    new SqlParameter("@QStatus",0)
                    };


                _sqlP[11].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("cultural_locationsInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[11].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }


        public DataTable GetCulturalLocationsListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
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
                dt = _dbAccess.GetDataTable("cultural_locationsGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public DataTable GetCulturalLocationslist(ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("cultural_locationslist", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public DataTable GetCulturalLocationsById(Int64 Id, ref int status)
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
                dt = _dbAccess.GetDataTable("cultural_locationsGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteCulturalLocations(Int64 Id)
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
                _dbAccess.SP_ExecuteScalar("cultural_locationsDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }


        public Int64 CulturalLocationsStatus(Int64 Id)
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
                _dbAccess.SP_ExecuteScalar("cultural_locationsUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }



        


        public DataTable ExportCulturalLocationsList(string Search, string Sort)
        {
            DataTable dt = null;
            int Total = 0;
            try
            {
                _sqlP = new[]
                {


                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@Total",Total)
                };

                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("Exportcultural_locationsGetList", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}
