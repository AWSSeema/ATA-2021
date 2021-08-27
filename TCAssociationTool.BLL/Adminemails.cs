using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TCAssociationTool.BLL
{
   public class Adminemails
    {
        TCAssociationTool.DAL.Adminemails _Adminemails = new TCAssociationTool.DAL.Adminemails();
        #region Methods

        public Int64 InsertAdminemails(Entities.Adminemails objAdminemailss)
        {
            Int64 _status = 0;
            if (objAdminemailss != null)
            {
                _status = _Adminemails.InsertAdminemails(objAdminemailss);

            }
            return _status;
        }

        public Int64 AdminemailsUpdatedefaultdonation(Int64 Id)
        {
            Int64 _status = 0;
            _status = _Adminemails.AdminemailsUpdatedefaultdonation(Id);
            return _status;
        }

        public Int64 AdminemailsUpdatedefaultmembership(Int64 Id)
        {
            Int64 _status = 0;
            _status = _Adminemails.AdminemailsUpdatedefaultmembership(Id);
            return _status;
        }

        public Int64 DeleteAdminemails(Int64 Id)
        {
            Int64 _status = 0;
            _status = _Adminemails.DeleteAdminemails(Id);
            return _status;
        }





        #endregion

        #region Entities filling




        public TCAssociationTool.Entities.Adminemails GetAdminemailsById(Int64 Id, ref int status)
        {
            TCAssociationTool.Entities.Adminemails objAdminemails = new TCAssociationTool.Entities.Adminemails();
            DataTable dt = new DataTable();
            if (Id != 0)
            {
                dt = _Adminemails.GetAdminemailsById(Id, ref status);
                if (dt.Rows.Count == 1)
                {
                    objAdminemails.Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                    objAdminemails.name = (dt.Rows[0]["name"] != DBNull.Value ? dt.Rows[0]["name"].ToString() : "");
                    objAdminemails.designation = (dt.Rows[0]["designation"] != DBNull.Value ? dt.Rows[0]["designation"].ToString() : "");
                    objAdminemails.email = (dt.Rows[0]["email"] != DBNull.Value ? dt.Rows[0]["email"].ToString() : "");
                    objAdminemails.isdonation = (dt.Rows[0]["isdonation"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["isdonation"]) : false);
                    objAdminemails.ismembership = (dt.Rows[0]["ismembership"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["ismembership"]) : false);
                    objAdminemails.datemodified = (dt.Rows[0]["datemodified"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["datemodified"]) : DateTime.MinValue);
                    objAdminemails.defaultmembership = (dt.Rows[0]["defaultmembership"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["defaultmembership"]) : false);
                    objAdminemails.defaultdonation = (dt.Rows[0]["defaultdonation"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["defaultdonation"]) : false);




                }
            }
            return objAdminemails;
        }


        public List<TCAssociationTool.Entities.Adminemails> GetAdminemailsListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.Adminemails> lstAdminemails = new List<TCAssociationTool.Entities.Adminemails>();
            DataTable dt = _Adminemails.GetAdminemailsListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _Adminemails.GetAdminemailsListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Adminemails objAdminemails = new TCAssociationTool.Entities.Adminemails();

                    objAdminemails.RId = Convert.ToInt64(dr["RId"].ToString());
                    objAdminemails.Id = Convert.ToInt64(dr["Id"].ToString());
                    objAdminemails.name = (dr["name"] != DBNull.Value ? dr["name"].ToString() : "");
                    objAdminemails.designation = (dr["designation"] != DBNull.Value ? dr["designation"].ToString() : "");
                    objAdminemails.email = (dr["email"] != DBNull.Value ? dr["email"].ToString() : "");
                    objAdminemails.isdonation = (dr["isdonation"] != DBNull.Value ? Convert.ToBoolean(dr["isdonation"]) : false);
                    objAdminemails.ismembership = (dr["ismembership"] != DBNull.Value ? Convert.ToBoolean(dr["ismembership"]) : false);
                    objAdminemails.datemodified = (dr["datemodified"] != DBNull.Value ? Convert.ToDateTime(dr["datemodified"]) : DateTime.MinValue);
                    objAdminemails.defaultmembership = (dr["defaultmembership"] != DBNull.Value ? Convert.ToBoolean(dr["defaultmembership"]) : false);
                    objAdminemails.defaultdonation = (dr["defaultdonation"] != DBNull.Value ? Convert.ToBoolean(dr["defaultdonation"]) : false);


                    lstAdminemails.Add(objAdminemails);
                }
            }
            return lstAdminemails;
        }

        #endregion
    }
}
