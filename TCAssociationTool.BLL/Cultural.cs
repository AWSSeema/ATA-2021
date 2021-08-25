using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TCAssociationTool.BLL
{
   public class Cultural
    {
        TCAssociationTool.DAL.Cultural _Cultural = new TCAssociationTool.DAL.Cultural();

        #region Methods

        public Int64 InsertCultural(Entities.Cultural objCultural,ref Int64 id)
        {
            Int64 _status = 0;
            if (objCultural != null)
            {
                _status = _Cultural.InsertCulturals(objCultural, ref id);

            }
            return _status;
        }

        public Int64 DeleteAllCulturals(string id)
        {
            Int64 _status = 0;
            _status = _Cultural.DeleteAllCulturals(id);
            return _status;
        }

        public Int64 DeleteCultural(Int64 id)
        {
            Int64 _status = 0;
            _status = _Cultural.DeleteCultural(id);
            return _status;
        }

         #endregion

        #region Entities filling

        public TCAssociationTool.Entities.Cultural GetCulturalById(Int64 id, ref int status)
        {
            TCAssociationTool.Entities.Cultural objCultural = new TCAssociationTool.Entities.Cultural();
            DataTable dt = new DataTable();
            if (id != 0)
            {
                dt = _Cultural.GetCulturalById(id, ref status);
                if (dt.Rows.Count == 1)
                {
                    objCultural.id = Convert.ToInt64(dt.Rows[0]["id"].ToString());
                    objCultural.firstname = dt.Rows[0]["firstname"] != DBNull.Value ? dt.Rows[0]["firstname"].ToString() : null;
                    objCultural.lastname = dt.Rows[0]["lastname"] != DBNull.Value ? dt.Rows[0]["lastname"].ToString() : null;
                    objCultural.email = dt.Rows[0]["email"] != DBNull.Value ? dt.Rows[0]["email"].ToString() : null;
                    objCultural.address1 = dt.Rows[0]["address1"] != DBNull.Value ? dt.Rows[0]["address1"].ToString() : null;
                    objCultural.city = dt.Rows[0]["city"] != DBNull.Value ? dt.Rows[0]["city"].ToString() : null;
                    objCultural.state = dt.Rows[0]["state"] != DBNull.Value ? dt.Rows[0]["state"].ToString() : null;
                    objCultural.zip = dt.Rows[0]["zip"] != DBNull.Value ? dt.Rows[0]["zip"].ToString() : null;
                    objCultural.mobile = dt.Rows[0]["mobile"] != DBNull.Value ? dt.Rows[0]["mobile"].ToString() : null;
                    objCultural.comments = dt.Rows[0]["comments"] != DBNull.Value ? dt.Rows[0]["comments"].ToString() : null;
                    objCultural.datesent = Convert.ToDateTime(dt.Rows[0]["datesent"].ToString());
                    objCultural.category = dt.Rows[0]["category"] != DBNull.Value ? dt.Rows[0]["category"].ToString() : null;
                    objCultural.amount = (dt.Rows[0]["amount"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[0]["amount"]) : 0);
                    objCultural.payment_statusid = (dt.Rows[0]["payment_statusid"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["payment_statusid"]) : 0);
                    objCultural.datemodified = (dt.Rows[0]["datemodified"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["datemodified"]) : DateTime.MinValue);
                    objCultural.admincomments = dt.Rows[0]["admincomments"] != DBNull.Value ? dt.Rows[0]["admincomments"].ToString() : null;
                    objCultural.payment_methodid = (dt.Rows[0]["payment_methodid"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["payment_methodid"]) : 0);
                    objCultural.adminid = (dt.Rows[0]["adminid"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["adminid"]) : 0);
                    objCultural.cheque_details = dt.Rows[0]["cheque_details"] != DBNull.Value ? dt.Rows[0]["cheque_details"].ToString() : null;
                    objCultural.vcode = dt.Rows[0]["vcode"] != DBNull.Value ? dt.Rows[0]["vcode"].ToString() : null;
                    objCultural.isconfirm = (dt.Rows[0]["isconfirm"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["isconfirm"]) : false);
                    objCultural.cardno = dt.Rows[0]["cardno"] != DBNull.Value ? dt.Rows[0]["cardno"].ToString() : null;
                    objCultural.cultural_no = dt.Rows[0]["cultural_no"] != DBNull.Value ? dt.Rows[0]["cultural_no"].ToString() : null;
                    objCultural.dob = dt.Rows[0]["dob"] != DBNull.Value ? dt.Rows[0]["dob"].ToString() : null;
                    objCultural.age = dt.Rows[0]["age"] != DBNull.Value ? dt.Rows[0]["age"].ToString() : null;
                    objCultural.gender = dt.Rows[0]["gender"] != DBNull.Value ? dt.Rows[0]["gender"].ToString() : null;
                    objCultural.location_id = (dt.Rows[0]["location_id"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["location_id"]) : 0);
                    objCultural.PaymentMethod = dt.Rows[0]["PaymentMethod"] != DBNull.Value ? dt.Rows[0]["PaymentMethod"].ToString() : null;
                    objCultural.PaymentStatus = dt.Rows[0]["PaymentStatus"] != DBNull.Value ? dt.Rows[0]["PaymentStatus"].ToString() : null;
                    objCultural.youtubeurl = dt.Rows[0]["youtubeurl"] != DBNull.Value ? dt.Rows[0]["youtubeurl"].ToString() : null;
                    objCultural.subcategory = dt.Rows[0]["subcategory"] != DBNull.Value ? dt.Rows[0]["subcategory"].ToString() : null;
                    objCultural.location = dt.Rows[0]["location"] != DBNull.Value ? dt.Rows[0]["location"].ToString() : null;
                    objCultural.description = dt.Rows[0]["description"] != DBNull.Value ? dt.Rows[0]["description"].ToString() : null;


                }
            }
            return objCultural;
        }

        public List<TCAssociationTool.Entities.Cultural> GetCulturalListByVariable(Int64 PaymentMethod, Int64 PaymentStatus, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.Cultural> lstCultural = new List<TCAssociationTool.Entities.Cultural>();
            DataTable dt = _Cultural.GetCulturalsListByVariable(PaymentMethod, PaymentStatus, Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _Cultural.GetCulturalsListByVariable(PaymentMethod, PaymentStatus, Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Cultural objCultural = new TCAssociationTool.Entities.Cultural();

                    objCultural.RId = Convert.ToInt64(dr["RId"].ToString());
                    objCultural.id = Convert.ToInt64(dr["id"].ToString());
                    objCultural.firstname = dr["firstname"] != DBNull.Value ? dr["firstname"].ToString() : null;
                    objCultural.lastname = dr["lastname"] != DBNull.Value ? dr["lastname"].ToString() : null;
                    objCultural.email = dr["email"] != DBNull.Value ? dr["email"].ToString() : null;
                    objCultural.address1 = dr["address1"] != DBNull.Value ? dr["address1"].ToString() : null;
                    objCultural.city = dr["city"] != DBNull.Value ? dr["city"].ToString() : null;
                    objCultural.state = dr["state"] != DBNull.Value ? dr["state"].ToString() : null;
                    objCultural.zip = dr["zip"] != DBNull.Value ? dr["zip"].ToString() : null;
                    objCultural.mobile = dr["mobile"] != DBNull.Value ? dr["mobile"].ToString() : null;
                    objCultural.comments = dr["comments"] != DBNull.Value ? dr["comments"].ToString() : null;
                    objCultural.datesent = Convert.ToDateTime(dr["datesent"].ToString());
                    objCultural.category = dr["category"] != DBNull.Value ? dr["category"].ToString() : null;
                    objCultural.amount = (dr["amount"] != DBNull.Value ? Convert.ToDecimal(dr["amount"]) : 0);
                    objCultural.payment_statusid = (dr["payment_statusid"] != DBNull.Value ? Convert.ToInt64(dr["payment_statusid"]) : 0);
                    objCultural.datemodified = (dr["datemodified"] != DBNull.Value ? Convert.ToDateTime(dr["datemodified"]) : DateTime.MinValue);
                    objCultural.admincomments = dr["admincomments"] != DBNull.Value ? dr["admincomments"].ToString() : null;
                    objCultural.payment_methodid = (dr["payment_methodid"] != DBNull.Value ? Convert.ToInt64(dr["payment_methodid"]) : 0);
                    objCultural.adminid = (dr["adminid"] != DBNull.Value ? Convert.ToInt64(dr["adminid"]) : 0);
                    objCultural.cheque_details = dr["cheque_details"] != DBNull.Value ? dr["cheque_details"].ToString() : null;
                    objCultural.vcode = dr["vcode"] != DBNull.Value ? dr["vcode"].ToString() : null;
                    objCultural.isconfirm = (dr["isconfirm"] != DBNull.Value ? Convert.ToBoolean(dr["isconfirm"]) : false);
                    objCultural.cardno = dr["cardno"] != DBNull.Value ? dr["cardno"].ToString() : null;
                    objCultural.cultural_no = dr["cultural_no"] != DBNull.Value ? dr["cultural_no"].ToString() : null;
                    objCultural.dob = dr["dob"] != DBNull.Value ? dr["dob"].ToString() : null;
                    objCultural.age = dr["age"] != DBNull.Value ? dr["age"].ToString() : null;
                    objCultural.gender = dr["gender"] != DBNull.Value ? dr["gender"].ToString() : null;
                    objCultural.location_id = (dr["location_id"] != DBNull.Value ? Convert.ToInt64(dr["location_id"]) : 0);
                    objCultural.PaymentMethod = dr["PaymentMethod"] != DBNull.Value ? dr["PaymentMethod"].ToString() : null;
                    objCultural.PaymentStatus = dr["PaymentStatus"] != DBNull.Value ? dr["PaymentStatus"].ToString() : null;
                    objCultural.youtubeurl = dr["youtubeurl"] != DBNull.Value ? dr["youtubeurl"].ToString() : null;
                    objCultural.subcategory = dr["subcategory"] != DBNull.Value ? dr["subcategory"].ToString() : null;
                    objCultural.location = dr["location"] != DBNull.Value ? dr["location"].ToString() : null;
                    objCultural.description = dr["description"] != DBNull.Value ? dr["description"].ToString() : null;

                    lstCultural.Add(objCultural);
                }
            }
            return lstCultural;
        }

        #endregion

    }
}
