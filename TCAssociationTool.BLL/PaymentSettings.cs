using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TCAssociationTool.BLL
{
    public class PaymentSettings
    {
        TCAssociationTool.DAL.PaymentSettings _PaymentSettings = new DAL.PaymentSettings();

        #region Methods

        public Int64 UpdatePaymentSettingsDetails(Entities.PaymentSettings objAppInfo)
        {
            Int64 _status = 0;
            if (objAppInfo != null)
            {
                _status = _PaymentSettings.UpdatePaymentDetails(objAppInfo);
            }
            return _status;
        }

        public Int64 DeletePaymentSettingById(Int64 Id)
        {
            Int64 _status = 0;
            _status = _PaymentSettings.DeletePaymentSettingById(Id);
            return _status;
        }

        public Int64 DeleteAllPaymentSettingsById(string Id)
        {
            Int64 _status = 0;
            _status = _PaymentSettings.DeleteAllPaymentSettingsById(Id);
            return _status;
        }

        public Int64 UpdatePaymentSettingApprovalById(Int64 PaymentSettingId)
        {
            Int64 _status = 0;
            _status = _PaymentSettings.UpdatePaymentSettingApprovalById(PaymentSettingId);
            return _status;
        }

        #endregion

        #region Entity Loading

        public Entities.PaymentSettings GetPaymentSettingsDetails(Int64 PaymentSettingId,string PaymentMethod, ref int Status)
        {
            DataTable dt = _PaymentSettings.GetPaymentSettingsDetails(PaymentSettingId,PaymentMethod, ref Status);
            TCAssociationTool.Entities.PaymentSettings objPaymentSettings = new TCAssociationTool.Entities.PaymentSettings();

            if (Status == 1 && dt.Rows.Count == 1)
            {
                objPaymentSettings.PaymentSettingId = Convert.ToInt64(dt.Rows[0]["PaymentSettingId"].ToString());
                objPaymentSettings.PaymentMethodId = Convert.ToInt64(dt.Rows[0]["PaymentMethodId"].ToString());
                objPaymentSettings.CurrencyCodeId = Convert.ToInt64(dt.Rows[0]["CurrencyCodeId"].ToString());
                objPaymentSettings.PaymentUrl = dt.Rows[0]["PaymentUrl"].ToString();
                objPaymentSettings.PaymentEmail = dt.Rows[0]["PaymentEmail"].ToString();
                objPaymentSettings.PaymentPassword = dt.Rows[0]["PaymentPassword"].ToString();
                objPaymentSettings.CurrencyCode = dt.Rows[0]["CurrencyCode"].ToString();
                objPaymentSettings.SuccessUrl = dt.Rows[0]["SuccessUrl"].ToString();
                objPaymentSettings.PaymentMethod = dt.Rows[0]["PaymentMethod"].ToString();
                objPaymentSettings.AccountType = dt.Rows[0]["AccountType"].ToString();
                objPaymentSettings.CancelUrl = dt.Rows[0]["CancelUrl"].ToString();
                objPaymentSettings.NotifyUrl = dt.Rows[0]["NotifyUrl"].ToString();
                objPaymentSettings.TokenNo = dt.Rows[0]["TokenNo"].ToString();
               
            }
            return objPaymentSettings;
        }

        public TCAssociationTool.Entities.PaymentSettings GetPaymentSettingById(Int64 Id)
        {
            TCAssociationTool.Entities.PaymentSettings objPaymentSettings = new TCAssociationTool.Entities.PaymentSettings();
            DataTable dt = new DataTable();
            if (Id != 0)
            {
                dt = _PaymentSettings.GetPaymentSettingById(Id);
                if (dt.Rows.Count == 1)
                {
                    objPaymentSettings.PaymentSettingId = Convert.ToInt64(dt.Rows[0]["PaymentSettingId"].ToString());
                    objPaymentSettings.PaymentMethodId = Convert.ToInt64(dt.Rows[0]["PaymentMethodId"].ToString());
                    objPaymentSettings.CurrencyCodeId = Convert.ToInt64(dt.Rows[0]["CurrencyCodeId"].ToString());
                    objPaymentSettings.PaymentUrl = dt.Rows[0]["PaymentUrl"].ToString();
                    objPaymentSettings.PaymentEmail = dt.Rows[0]["PaymentEmail"].ToString();
                    objPaymentSettings.PaymentPassword = dt.Rows[0]["PaymentPassword"].ToString();
                    objPaymentSettings.CurrencyCode = dt.Rows[0]["CurrencyCode"].ToString();
                    objPaymentSettings.SuccessUrl = dt.Rows[0]["SuccessUrl"].ToString();
                    objPaymentSettings.CancelUrl = dt.Rows[0]["CancelUrl"].ToString();
                    objPaymentSettings.PaymentMethod = dt.Rows[0]["PaymentMethod"].ToString();
                    objPaymentSettings.AccountType = dt.Rows[0]["AccountType"].ToString();
                    objPaymentSettings.NotifyUrl = dt.Rows[0]["NotifyUrl"].ToString();
                    objPaymentSettings.TokenNo = dt.Rows[0]["TokenNo"].ToString();
                }
            }
            return objPaymentSettings;
        }

        public List<TCAssociationTool.Entities.PaymentSettings> GetPaymentSettingsList(string search,int pageno, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.PaymentSettings> lstPaymentSettings = new List<TCAssociationTool.Entities.PaymentSettings>();
            DataTable dt = _PaymentSettings.GetPaymentSettingsListByVariable(search, pageno, Items, ref Total);
            if (dt.Rows.Count == 0 && pageno != 0)
            {
                dt = _PaymentSettings.GetPaymentSettingsListByVariable(search,pageno - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.PaymentSettings objPaymentSettings = new TCAssociationTool.Entities.PaymentSettings();

                    objPaymentSettings.PaymentSettingId = Convert.ToInt64(dr["PaymentSettingId"].ToString());
                    objPaymentSettings.RId = Convert.ToInt64(dr["RId"].ToString());
                    objPaymentSettings.PaymentMethodId = Convert.ToInt64(dr["PaymentMethodId"].ToString());
                    objPaymentSettings.CurrencyCodeId = Convert.ToInt64(dr["CurrencyCodeId"].ToString());
                    objPaymentSettings.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                    objPaymentSettings.PaymentUrl = dr["PaymentUrl"].ToString();
                    objPaymentSettings.PaymentEmail = dr["PaymentEmail"].ToString();
                    objPaymentSettings.PaymentPassword = dr["PaymentPassword"].ToString();
                    objPaymentSettings.CurrencyCode = dr["CurrencyCode"].ToString();
                    objPaymentSettings.PaymentMethod = dr["PaymentMethod"].ToString();
                    objPaymentSettings.AccountType = dr["AccountType"].ToString();
                    objPaymentSettings.SuccessUrl = dr["SuccessUrl"].ToString();
                    objPaymentSettings.CancelUrl = dr["CancelUrl"].ToString();
                    objPaymentSettings.NotifyUrl = dr["NotifyUrl"].ToString();
                    objPaymentSettings.TokenNo = dr["TokenNo"].ToString();

                    lstPaymentSettings.Add(objPaymentSettings);
                }
            }
            return lstPaymentSettings;
        }

        public List<TCAssociationTool.Entities.CurrencyCodes> GetCurrencyCodesList(ref int Total)
        {
            List<TCAssociationTool.Entities.CurrencyCodes> lstCurrencyCodes = new List<TCAssociationTool.Entities.CurrencyCodes>();
            DataTable dt = _PaymentSettings.CurrencyCodesList(ref Total);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.CurrencyCodes objCurrencyCodes = new TCAssociationTool.Entities.CurrencyCodes();
                    objCurrencyCodes.CurrencyCodeId =Convert.ToInt64(dr["CurrencyCodeId"].ToString());
                    objCurrencyCodes.CurrencyCode = dr["CurrencyCode"].ToString();

                    lstCurrencyCodes.Add(objCurrencyCodes);

                }

            }
            return lstCurrencyCodes;
        }

        public List<TCAssociationTool.Entities.PaymentMethods> GetPaymentMethodsList(ref int Total)
        {
            List<TCAssociationTool.Entities.PaymentMethods> lstPaymentMethods = new List<TCAssociationTool.Entities.PaymentMethods>();
            DataTable dt = _PaymentSettings.PaymentMethodsList(ref Total);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.PaymentMethods objPaymentMethods = new TCAssociationTool.Entities.PaymentMethods();
                    objPaymentMethods.PaymentMethodId = Convert.ToInt64(dr["PaymentMethodId"].ToString());
                    objPaymentMethods.PaymentMethod = dr["PaymentMethod"].ToString();

                    lstPaymentMethods.Add(objPaymentMethods);

                }

            }
            return lstPaymentMethods;
        }

        public Entities.PaymentSettings GetPaymentSettings( ref int Status)
        {
            DataTable dt = _PaymentSettings.GetPaymentSettings( ref Status);
            TCAssociationTool.Entities.PaymentSettings objPaymentSettings = new TCAssociationTool.Entities.PaymentSettings();

            if (Status == 1 && dt.Rows.Count == 1)
            {
                objPaymentSettings.PaymentSettingId = Convert.ToInt64(dt.Rows[0]["PaymentSettingId"].ToString());
                objPaymentSettings.PaymentMethodId = Convert.ToInt64(dt.Rows[0]["PaymentMethodId"].ToString());
                objPaymentSettings.CurrencyCodeId = Convert.ToInt64(dt.Rows[0]["CurrencyCodeId"].ToString());
                objPaymentSettings.PaymentUrl = dt.Rows[0]["PaymentUrl"].ToString();
                objPaymentSettings.PaymentEmail = dt.Rows[0]["PaymentEmail"].ToString();
                objPaymentSettings.PaymentPassword = dt.Rows[0]["PaymentPassword"].ToString();
                objPaymentSettings.CurrencyCode = dt.Rows[0]["CurrencyCode"].ToString();
                objPaymentSettings.SuccessUrl = dt.Rows[0]["SuccessUrl"].ToString();
                objPaymentSettings.PaymentMethod = dt.Rows[0]["PaymentMethod"].ToString();
                objPaymentSettings.AccountType = dt.Rows[0]["AccountType"].ToString();
                objPaymentSettings.CancelUrl = dt.Rows[0]["CancelUrl"].ToString();
                objPaymentSettings.NotifyUrl = dt.Rows[0]["NotifyUrl"].ToString();
                objPaymentSettings.TokenNo = dt.Rows[0]["TokenNo"].ToString();

            }
            return objPaymentSettings;
        }

        #endregion

    }
}
