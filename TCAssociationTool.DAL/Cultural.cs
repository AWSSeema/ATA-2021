using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TCAssociationTool.DAL
{
   public class Cultural
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

        public Int64 InsertCulturals(Entities.Cultural objCultural,ref Int64 id)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@id",objCultural.id),
                    new SqlParameter("@firstname",(objCultural.firstname==null?DBNull.Value:(object)objCultural.firstname)),
                    new SqlParameter("@lastname",(objCultural.lastname==null?DBNull.Value:(object)objCultural.lastname)),
                    new SqlParameter("@middlename",(objCultural.middlename==null?DBNull.Value:(object)objCultural.middlename)),
                    new SqlParameter("@email",(objCultural.email==null?DBNull.Value:(object)objCultural.email)),
                    new SqlParameter("@address1",(objCultural.address1==null?DBNull.Value:(object)objCultural.address1)),
                    new SqlParameter("@city",(objCultural.city == null ?DBNull.Value:(object)objCultural.city)),
                    new SqlParameter("@state",(objCultural.state == null ?DBNull.Value:(object)objCultural.state)),
                    new SqlParameter("@zip",(objCultural.zip == null ?DBNull.Value:(object)objCultural.zip)),
                    new SqlParameter("@mobile",(objCultural.mobile == null ?DBNull.Value:(object)objCultural.mobile)),
                    new SqlParameter("@comments",(objCultural.comments == null ?DBNull.Value:(object)objCultural.comments)),
                    new SqlParameter("@datesent",DateTime.UtcNow),
                    new SqlParameter("@category",(objCultural.category == null ?DBNull.Value:(object)objCultural.category)),
                    new SqlParameter("@amount",(objCultural.amount == 0 ?DBNull.Value:(object)objCultural.amount)),
                    new SqlParameter("@payment_statusid",(objCultural.payment_statusid == 0 ?DBNull.Value:(object)objCultural.payment_statusid)),
                    new SqlParameter("@payment_methodid",(objCultural.payment_methodid == 0 ?DBNull.Value:(object)objCultural.payment_methodid)),
                    new SqlParameter("@datemodified",(objCultural.datemodified == DateTime.MinValue ?DBNull.Value:(object)objCultural.datemodified)),
                    new SqlParameter("@admincomments",(objCultural.admincomments == null ?DBNull.Value:(object)objCultural.admincomments)),
                    new SqlParameter("@adminid",(objCultural.adminid == 0 ?DBNull.Value:(object)objCultural.adminid)),
                    new SqlParameter("@cheque_details",(objCultural.cheque_details == null ?DBNull.Value:(object)objCultural.cheque_details)),
                    new SqlParameter("@vcode",(objCultural.vcode == null ?DBNull.Value:(object)objCultural.vcode)),
                    new SqlParameter("@isconfirm",objCultural.isconfirm),
                    new SqlParameter("@cardno",(objCultural.cardno == null ?DBNull.Value:(object)objCultural.cardno)),
                    new SqlParameter("@cultural_no",(objCultural.cultural_no == null ?DBNull.Value:(object)objCultural.cultural_no)),
                    new SqlParameter("@dob",(objCultural.dob == null ?DBNull.Value:(object)objCultural.dob)),
                    new SqlParameter("@age",(objCultural.age == null ?DBNull.Value:(object)objCultural.age)),
                    new SqlParameter("@gender",(objCultural.gender == null ?DBNull.Value:(object)objCultural.gender)),
                    new SqlParameter("@location_id",(objCultural.location_id == 0 ?DBNull.Value:(object)objCultural.location_id)),
                    new SqlParameter("@youtubeurl",(objCultural.youtubeurl == null ?DBNull.Value:(object)objCultural.youtubeurl)),
                    new SqlParameter("@subcategory",(objCultural.subcategory == null ?DBNull.Value:(object)objCultural.subcategory)),
                    new SqlParameter("@location",(objCultural.location == null ?DBNull.Value:(object)objCultural.location)),
                    new SqlParameter("@description",(objCultural.description == null ?DBNull.Value:(object)objCultural.description)),
                    new SqlParameter("@QStatus",0)
                    };

               

                _sqlP[32].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("culturalInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[32].Value);

                id = Convert.ToInt64(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

      
       public DataTable GetCulturalsListByVariable(Int64 PaymentMethod, Int64 PaymentStatus, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@PaymentStatus",PaymentStatus),
                    new SqlParameter("@PaymentMethod",PaymentMethod),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@Total",Total)
                };

                _sqlP[6].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("culturalGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[6].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetCulturalById(Int64 id, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@id",id),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("culturalGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteCultural(Int64 id)
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
                _dbAccess.SP_ExecuteScalar("culturalDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable ExportCulturalList(string Search,Int64 PaymentStatusId, string StartDate, string EndDate, string ExpiryDate, string Sort)
        {
            DataTable dt = null;
            int Total = 0;
            try
            {
                _sqlP = new[]
                {
                   
                    new SqlParameter("@PaymentStatusId",PaymentStatusId),
                    new SqlParameter("@StartDate",StartDate),
                    new SqlParameter("@EndDate",EndDate),
                    new SqlParameter("@ExpiryDate",ExpiryDate),
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@Total",Total)
                };

                _sqlP[6].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("ExportCulturalGetList", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[6].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteAllCulturals(string id)
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
                _dbAccess.SP_ExecuteScalar("culturalDeleteAll", ref _sqlP);
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
