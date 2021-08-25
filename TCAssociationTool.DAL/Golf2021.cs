using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TCAssociationTool.DAL
{
    public class Golf2021
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;



        public Int64 InsertGolf2021(Entities.Golf2021 objGolf2021)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@Id",objGolf2021.Id),
                    new SqlParameter("@playertype",(objGolf2021.playertype == null ?DBNull.Value:(object)objGolf2021.playertype.Trim())),
                    new SqlParameter("@player1_name",(objGolf2021.player1_name == null ?DBNull.Value:(object)objGolf2021.player1_name.Trim())),
                    new SqlParameter("@player1_phone",(objGolf2021.player1_phone == null ?DBNull.Value:(object)objGolf2021.player1_phone.Trim())),
                    new SqlParameter("@player1_email",(objGolf2021.player1_email == null ?DBNull.Value:(object)objGolf2021.player1_email.Trim())),
                    new SqlParameter("@player1_tshirt",(objGolf2021.player1_tshirt == null ?DBNull.Value:(object)objGolf2021.player1_tshirt.Trim())),
                    new SqlParameter("@sponsorship",(objGolf2021.sponsorship == null ?DBNull.Value:(object)objGolf2021.sponsorship.Trim())),
                    new SqlParameter("@created_at",(objGolf2021.created_at == DateTime.MinValue ?DBNull.Value:(object)objGolf2021.created_at)),
                    new SqlParameter("@payment_status",(objGolf2021.payment_status==0?(object)DBNull.Value:objGolf2021.payment_status)),
                    new SqlParameter("@payment_method",(objGolf2021.payment_method == null ?DBNull.Value:(object)objGolf2021.payment_method.Trim())),
                    new SqlParameter("@cardno",(objGolf2021.cardno == null ?DBNull.Value:(object)objGolf2021.cardno.Trim())),
                    new SqlParameter("@payment_response",(objGolf2021.payment_response == null ?DBNull.Value:(object)objGolf2021.payment_response.Trim())),
                    new SqlParameter("@ip_address",(objGolf2021.ip_address == null ?DBNull.Value:(object)objGolf2021.ip_address.Trim())),
                    new SqlParameter("@player2_name",(objGolf2021.player2_name == null ?DBNull.Value:(object)objGolf2021.player2_name.Trim())),
                    new SqlParameter("@player2_phone",(objGolf2021.player2_phone == null ?DBNull.Value:(object)objGolf2021.player2_phone.Trim())),
                    new SqlParameter("@player2_email",(objGolf2021.player2_email == null ?DBNull.Value:(object)objGolf2021.player2_email.Trim())),
                    new SqlParameter("@player2_tshirt",(objGolf2021.player2_tshirt == null ?DBNull.Value:(object)objGolf2021.player2_tshirt.Trim())),
                    new SqlParameter("@player3_name",(objGolf2021.player3_name == null ?DBNull.Value:(object)objGolf2021.player3_name.Trim())),
                    new SqlParameter("@player3_phone",(objGolf2021.player3_phone == null ?DBNull.Value:(object)objGolf2021.player3_phone.Trim())),
                    new SqlParameter("@player3_email",(objGolf2021.player3_email == null ?DBNull.Value:(object)objGolf2021.player3_email.Trim())),
                    new SqlParameter("@player3_tshirt",(objGolf2021.player3_tshirt == null ?DBNull.Value:(object)objGolf2021.player3_tshirt.Trim())),
                    new SqlParameter("@player4_name",(objGolf2021.player4_name == null ?DBNull.Value:(object)objGolf2021.player4_name.Trim())),
                    new SqlParameter("@player4_phone",(objGolf2021.player4_phone == null ?DBNull.Value:(object)objGolf2021.player4_phone.Trim())),
                    new SqlParameter("@player4_email",(objGolf2021.player4_email == null ?DBNull.Value:(object)objGolf2021.player4_email.Trim())),
                    new SqlParameter("@player4_tshirt",(objGolf2021.player4_tshirt == null ?DBNull.Value:(object)objGolf2021.player4_tshirt.Trim())),
                    new SqlParameter("@amount",(objGolf2021.amount == 0 ?DBNull.Value:(object)objGolf2021.amount)),
                    new SqlParameter("@ip",(objGolf2021.ip == null ?DBNull.Value:(object)objGolf2021.ip.Trim())),
                    new SqlParameter("@vcode",(objGolf2021.vcode == null ?DBNull.Value:(object)objGolf2021.vcode.Trim())),
                    new SqlParameter("@comments",(objGolf2021.comments == null ?DBNull.Value:(object)objGolf2021.comments.Trim())),

                    new SqlParameter("@QStatus",0)
                    };


                _sqlP[29].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("Golf2021Insert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[29].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }


        public Int64 Golf2021CommentUpdate(Entities.Golf2021 objGolf2021)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@Id",objGolf2021.Id),
                    new SqlParameter("@comments",(objGolf2021.comments == null ?DBNull.Value:(object)objGolf2021.comments.Trim())),
                    new SqlParameter("@QStatus",0)
                    };


                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("Golf2021CommentUpdate", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }


        public DataTable GetGolf2021ListByVariable(string StartDate,string EndDate,string Search, string Sort, int PageNo, int Items, ref int Total)
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
                dt = _dbAccess.GetDataTable("Golf2021GetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[6].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public DataTable GetGolf2021ById(Int64 Id, ref int status)
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
                dt = _dbAccess.GetDataTable("Golf2021GetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteGolf2021(Int64 Id)
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
                _dbAccess.SP_ExecuteScalar("Golf2021Delete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable ExportGolf2021List(string Search,string StartDate, string EndDate,  string Sort)
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
                dt = _dbAccess.GetDataTable("ExportGolf2021GetList", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

    }
}
