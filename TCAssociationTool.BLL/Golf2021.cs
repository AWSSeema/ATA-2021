using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TCAssociationTool.BLL
{
 public   class Golf2021
    {
        TCAssociationTool.DAL.Golf2021 _Golf2021 = new TCAssociationTool.DAL.Golf2021();
        #region Methods

        public Int64 InsertGolf2021(Entities.Golf2021 objGolf2021s)
        {
            Int64 _status = 0;
            if (objGolf2021s != null)
            {
                _status = _Golf2021.InsertGolf2021(objGolf2021s);

            }
            return _status;
        }

        public Int64 Golf2021CommentUpdate(Entities.Golf2021 objGolf2021s)
        {
            Int64 _status = 0;
            if (objGolf2021s != null)
            {
                _status = _Golf2021.Golf2021CommentUpdate(objGolf2021s);

            }
            return _status;
        }

        public Int64 DeleteGolf2021(Int64 Id)
        {
            Int64 _status = 0;
            _status = _Golf2021.DeleteGolf2021(Id);
            return _status;
        }



        #endregion

        #region Entities filling



        public TCAssociationTool.Entities.Golf2021 GetGolf2021ById(Int64 Id, ref int status)
        {
            TCAssociationTool.Entities.Golf2021 objGolf2021 = new TCAssociationTool.Entities.Golf2021();
            DataTable dt = new DataTable();
            if (Id != 0)
            {
                dt = _Golf2021.GetGolf2021ById(Id, ref status);
                if (dt.Rows.Count == 1)
                {
                    objGolf2021.Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                    objGolf2021.playertype = (dt.Rows[0]["playertype"] != DBNull.Value ? dt.Rows[0]["playertype"].ToString() : "");
                    objGolf2021.player1_name = (dt.Rows[0]["player1_name"] != DBNull.Value ? dt.Rows[0]["player1_name"].ToString() : "");
                    objGolf2021.player1_phone = (dt.Rows[0]["player1_phone"] != DBNull.Value ? dt.Rows[0]["player1_phone"].ToString() : "");
                    objGolf2021.player1_email = (dt.Rows[0]["player1_email"] != DBNull.Value ? dt.Rows[0]["player1_email"].ToString() : "");
                    objGolf2021.player1_tshirt = (dt.Rows[0]["player1_tshirt"] != DBNull.Value ? dt.Rows[0]["player1_tshirt"].ToString() : "");
                    objGolf2021.sponsorship = (dt.Rows[0]["sponsorship"] != DBNull.Value ? dt.Rows[0]["sponsorship"].ToString() : "");
                    objGolf2021.created_at = (dt.Rows[0]["created_at"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["created_at"]) : DateTime.MinValue);
                    objGolf2021.payment_status = (dt.Rows[0]["payment_status"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["payment_status"]) : 0);
                    objGolf2021.payment_method = (dt.Rows[0]["payment_method"] != DBNull.Value ? dt.Rows[0]["payment_method"].ToString() : "");
                    objGolf2021.cardno = (dt.Rows[0]["cardno"] != DBNull.Value ? dt.Rows[0]["cardno"].ToString() : "");
                    objGolf2021.payment_response = (dt.Rows[0]["payment_response"] != DBNull.Value ? dt.Rows[0]["payment_response"].ToString() : "");
                    objGolf2021.ip_address = (dt.Rows[0]["ip_address"] != DBNull.Value ? dt.Rows[0]["ip_address"].ToString() : "");
                    objGolf2021.player2_name = (dt.Rows[0]["player2_name"] != DBNull.Value ? dt.Rows[0]["player2_name"].ToString() : "");
                    objGolf2021.player2_phone = (dt.Rows[0]["player2_phone"] != DBNull.Value ? dt.Rows[0]["player2_phone"].ToString() : "");
                    objGolf2021.player2_email = (dt.Rows[0]["player2_email"] != DBNull.Value ? dt.Rows[0]["player2_email"].ToString() : "");
                    objGolf2021.player2_tshirt = (dt.Rows[0]["player2_tshirt"] != DBNull.Value ? dt.Rows[0]["player2_tshirt"].ToString() : "");
                    objGolf2021.player3_name = (dt.Rows[0]["player3_name"] != DBNull.Value ? dt.Rows[0]["player3_name"].ToString() : "");
                    objGolf2021.player3_phone = (dt.Rows[0]["player3_phone"] != DBNull.Value ? dt.Rows[0]["player3_phone"].ToString() : "");
                    objGolf2021.player3_email = (dt.Rows[0]["player3_email"] != DBNull.Value ? dt.Rows[0]["player3_email"].ToString() : "");
                    objGolf2021.player3_tshirt = (dt.Rows[0]["player3_tshirt"] != DBNull.Value ? dt.Rows[0]["player3_tshirt"].ToString() : "");
                    objGolf2021.player4_name = (dt.Rows[0]["player4_name"] != DBNull.Value ? dt.Rows[0]["player4_name"].ToString() : "");
                    objGolf2021.player4_phone = (dt.Rows[0]["player4_phone"] != DBNull.Value ? dt.Rows[0]["player4_phone"].ToString() : "");
                    objGolf2021.player4_email = (dt.Rows[0]["player4_email"] != DBNull.Value ? dt.Rows[0]["player4_email"].ToString() : "");
                    objGolf2021.player4_tshirt = (dt.Rows[0]["player4_tshirt"] != DBNull.Value ? dt.Rows[0]["player4_tshirt"].ToString() : "");
                    objGolf2021.amount = (dt.Rows[0]["amount"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[0]["amount"]) : 0);
                    objGolf2021.ip = (dt.Rows[0]["ip"] != DBNull.Value ? dt.Rows[0]["ip"].ToString() : "");
                    objGolf2021.vcode = (dt.Rows[0]["vcode"] != DBNull.Value ? dt.Rows[0]["vcode"].ToString() : "");
                    objGolf2021.comments = (dt.Rows[0]["comments"] != DBNull.Value ? dt.Rows[0]["comments"].ToString() : "");
                    objGolf2021.PaymentStauts = (dt.Rows[0]["PaymentStauts"] != DBNull.Value ? dt.Rows[0]["PaymentStauts"].ToString() : "");


                }
            }
            return objGolf2021;
        }



        public List<TCAssociationTool.Entities.Golf2021> GetGolf2021ListByVariable(string StartDate, string EndDate,string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.Golf2021> lstGolf2021 = new List<TCAssociationTool.Entities.Golf2021>();
            DataTable dt = _Golf2021.GetGolf2021ListByVariable(StartDate, EndDate,Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _Golf2021.GetGolf2021ListByVariable(StartDate, EndDate, Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Golf2021 objGolf2021 = new TCAssociationTool.Entities.Golf2021();

                    objGolf2021.RId = Convert.ToInt64(dr["RId"].ToString());
                    objGolf2021.Id = Convert.ToInt64(dr["Id"].ToString());
                    objGolf2021.playertype = (dr["playertype"] != DBNull.Value ? dr["playertype"].ToString() : "");
                    objGolf2021.player1_name = (dr["player1_name"] != DBNull.Value ? dr["player1_name"].ToString() : "");
                    objGolf2021.player1_phone = (dr["player1_phone"] != DBNull.Value ? dr["player1_phone"].ToString() : "");
                    objGolf2021.player1_email = (dr["player1_email"] != DBNull.Value ? dr["player1_email"].ToString() : "");
                    objGolf2021.player1_tshirt = (dr["player1_tshirt"] != DBNull.Value ? dr["player1_tshirt"].ToString() : "");
                    objGolf2021.sponsorship = (dr["sponsorship"] != DBNull.Value ? dr["sponsorship"].ToString() : "");
                    objGolf2021.created_at = (dr["created_at"] != DBNull.Value ? Convert.ToDateTime(dr["created_at"]) : DateTime.MinValue);
                    objGolf2021.payment_status = (dr["payment_status"] != DBNull.Value ? Convert.ToInt64(dr["payment_status"]) : 0);
                    objGolf2021.payment_method = (dr["payment_method"] != DBNull.Value ? dr["payment_method"].ToString() : "");
                    objGolf2021.cardno = (dr["cardno"] != DBNull.Value ? dr["cardno"].ToString() : "");
                    objGolf2021.payment_response = (dr["payment_response"] != DBNull.Value ? dr["payment_response"].ToString() : "");
                    objGolf2021.ip_address = (dr["ip_address"] != DBNull.Value ? dr["ip_address"].ToString() : "");
                    objGolf2021.player2_name = (dr["player2_name"] != DBNull.Value ? dr["player2_name"].ToString() : "");
                    objGolf2021.player2_phone = (dr["player2_phone"] != DBNull.Value ? dr["player2_phone"].ToString() : "");
                    objGolf2021.player2_email = (dr["player2_email"] != DBNull.Value ? dr["player2_email"].ToString() : "");
                    objGolf2021.player2_tshirt = (dr["player2_tshirt"] != DBNull.Value ? dr["player2_tshirt"].ToString() : "");
                    objGolf2021.player3_name = (dr["player3_name"] != DBNull.Value ? dr["player3_name"].ToString() : "");
                    objGolf2021.player3_phone = (dr["player3_phone"] != DBNull.Value ? dr["player3_phone"].ToString() : "");
                    objGolf2021.player3_email = (dr["player3_email"] != DBNull.Value ? dr["player3_email"].ToString() : "");
                    objGolf2021.player3_tshirt = (dr["player3_tshirt"] != DBNull.Value ? dr["player3_tshirt"].ToString() : "");
                    objGolf2021.player4_name = (dr["player4_name"] != DBNull.Value ? dr["player4_name"].ToString() : "");
                    objGolf2021.player4_phone = (dr["player4_phone"] != DBNull.Value ? dr["player4_phone"].ToString() : "");
                    objGolf2021.player4_email = (dr["player4_email"] != DBNull.Value ? dr["player4_email"].ToString() : "");
                    objGolf2021.player4_tshirt = (dr["player4_tshirt"] != DBNull.Value ? dr["player4_tshirt"].ToString() : "");
                    objGolf2021.amount = (dr["amount"] != DBNull.Value ? Convert.ToDecimal(dr["amount"]) : 0);
                    objGolf2021.ip = (dr["ip"] != DBNull.Value ? dr["ip"].ToString() : "");
                    objGolf2021.vcode = (dr["vcode"] != DBNull.Value ? dr["vcode"].ToString() : "");
                    objGolf2021.comments = (dr["comments"] != DBNull.Value ? dr["comments"].ToString() : "");
                    objGolf2021.PaymentStauts = (dr["PaymentStauts"] != DBNull.Value ? dr["PaymentStauts"].ToString() : "");

                    
                    lstGolf2021.Add(objGolf2021);
                }
            }
            return lstGolf2021;
        }

        #endregion
    }
}
