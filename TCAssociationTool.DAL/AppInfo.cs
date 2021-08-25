using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCAssociationTool.DAL;
using System.Data.SqlClient;
using System.Data;

namespace TCAssociationTool.DAL
{
    public class AppInfo
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

        #region Admin

        public DataTable GetAppInfoDetails(ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("AppInfoGetList", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 UpdateAppInfoDetails(Entities.AppInfo objAppInfo)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@AppInfoId",objAppInfo.AppInfoId),
                    new SqlParameter("@SiteName",objAppInfo.SiteName),
                    new SqlParameter("@CompanyAddress",objAppInfo.CompanyAddress),
                    new SqlParameter("@CompanyWebSite",objAppInfo.CompanyWebSite),
                    new SqlParameter("@CompanyEmail",objAppInfo.CompanyEmail),
                    new SqlParameter("@CompanyPhone",(objAppInfo.CompanyPhone==null?(object)DBNull.Value:objAppInfo.CompanyPhone.Trim())),
                    new SqlParameter("@PresidentEmail",(objAppInfo.PresidentEmail==null?(object)DBNull.Value:objAppInfo.PresidentEmail.Trim())),
                    new SqlParameter("@PresidentPhone",(objAppInfo.PresidentPhone==null?(object)DBNull.Value:objAppInfo.PresidentPhone.Trim())),
                    new SqlParameter("@SecretaryEmail",(objAppInfo.SecretaryEmail==null?(object)DBNull.Value:objAppInfo.SecretaryEmail.Trim())),
                    new SqlParameter("@SecretaryPhone",(objAppInfo.SecretaryPhone==null?(object)DBNull.Value:objAppInfo.SecretaryPhone.Trim())),
                    new SqlParameter("@CustomerCareNumber",(objAppInfo.CustomerCareNumber==null?(object)DBNull.Value:objAppInfo.CustomerCareNumber.Trim())),
                    new SqlParameter("@TollFreeNumber",(objAppInfo.TollFreeNumber==null?(object)DBNull.Value:objAppInfo.TollFreeNumber.Trim())),
                    new SqlParameter("@FacebookUrl",(objAppInfo.FacebookUrl==null?(object)DBNull.Value:objAppInfo.FacebookUrl.Trim())),
                    new SqlParameter("@TwitterUrl",(objAppInfo.TwitterUrl==null?(object)DBNull.Value:objAppInfo.TwitterUrl.Trim())),
                    new SqlParameter("@YoutubeUrl",(objAppInfo.YoutubeUrl==null?(object)DBNull.Value:objAppInfo.YoutubeUrl.Trim())),
                    new SqlParameter("@SupportEmail",(objAppInfo.SupportEmail==null?(object)DBNull.Value:objAppInfo.SupportEmail.Trim())),
                    new SqlParameter("@EnqueryEmail",(objAppInfo.EnqueryEmail==null?(object)DBNull.Value:objAppInfo.EnqueryEmail.Trim())),
                    new SqlParameter("@PageTitle",(objAppInfo.PageTitle==null?(object)DBNull.Value:objAppInfo.PageTitle.Trim())),
                    new SqlParameter("@MetaDescription",(objAppInfo.MetaDescription==null?(object)DBNull.Value:objAppInfo.MetaDescription.Trim())),
                    new SqlParameter("@MetaKeywords",(objAppInfo.MetaKeywords==null?(object)DBNull.Value:objAppInfo.MetaKeywords.Trim())),
                    new SqlParameter("@Topline",(objAppInfo.Topline==null?(object)DBNull.Value:objAppInfo.Topline.Trim())),
                    new SqlParameter("@PageItems",(objAppInfo.PageItems==0?(object)DBNull.Value:objAppInfo.PageItems)),
                    new SqlParameter("@UpdatedBy",objAppInfo.UpdatedBy),
                    new SqlParameter("@UpdatedTime",DateTime.UtcNow),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[24].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("AppInfoInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[24].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 GetAppInfoEmail(ref string CompanyEmail)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@CompanyEmail",CompanyEmail),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[0].SqlDbType = SqlDbType.NVarChar;
                _sqlP[0].Size = 100;
                _sqlP[0].Direction = System.Data.ParameterDirection.InputOutput;
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("AppInfoGetByCompanyEmail", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
                CompanyEmail = _sqlP[0].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        #endregion

        #region Front End

        public DataSet FEGetListInitialFlyer(ref int Status)
        {
            DataSet ds = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataSet("FEGetListInitialFlyer", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet FEGetListAppInfo(ref int Status)
        {
            DataSet ds = null;
            try
            {
                _sqlP = new[] 
                    {
                        new SqlParameter("@QStatus",0)
                    };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataSet("FEGetListAppInfo", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet FEGetListInitialLoad(ref int Status)
        {
            DataSet ds = null;
            try
            {
                _sqlP = new[] 
                {
                    
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataSet("FEHomePageInitialLoad", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet FEGetListMainLayout(string Email, ref int Status)
        {
            DataSet ds = null;
            try
            {
                _sqlP = new[] 
                    {
                        new SqlParameter("@Email",Email),
                        new SqlParameter("@QStatus",0)
                    };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataSet("FEGetListMainLayout", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet APIFEGetListInitialLoad(ref int Status)
        {
            DataSet ds = null;
            try
            {
                _sqlP = new[]
                {

                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataSet("APIFEHomePageInitialLoad", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        #endregion
    }
}
