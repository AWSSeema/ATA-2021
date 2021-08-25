using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TCAssociationTool.BLL
{
   public class Vaccinations
    {
        TCAssociationTool.DAL.Vaccinations _Vaccinations = new TCAssociationTool.DAL.Vaccinations();
        #region Methods

        public Int64 InsertVaccinations(Entities.Vaccinations objVaccinationss)
        {
            Int64 _status = 0;
            if (objVaccinationss != null)
            {
                _status = _Vaccinations.InsertVaccinations(objVaccinationss);

            }
            return _status;
        }

        public Int64 VaccinationsCommentUpdate(Entities.Vaccinations objVaccinationss)
        {
            Int64 _status = 0;
            if (objVaccinationss != null)
            {
                _status = _Vaccinations.VaccinationsCommentUpdate(objVaccinationss);

            }
            return _status;
        }

        public Int64 DeleteVaccinations(Int64 Id)
        {
            Int64 _status = 0;
            _status = _Vaccinations.DeleteVaccinations(Id);
            return _status;
        }

    

        #endregion

        #region Entities filling

     

        public TCAssociationTool.Entities.Vaccinations GetVaccinationsById(Int64 Id, ref int status)
        {
            TCAssociationTool.Entities.Vaccinations objVaccinations = new TCAssociationTool.Entities.Vaccinations();
            DataTable dt = new DataTable();
            if (Id != 0)
            {
                dt = _Vaccinations.GetVaccinationsById(Id, ref status);
                if (dt.Rows.Count == 1)
                {
                    objVaccinations.Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                    objVaccinations.membername = (dt.Rows[0]["membername"] != DBNull.Value ? dt.Rows[0]["membername"].ToString() : "");
                    objVaccinations.position = (dt.Rows[0]["position"] != DBNull.Value ? dt.Rows[0]["position"].ToString() : "");
                    objVaccinations.relation = (dt.Rows[0]["relation"] != DBNull.Value ? dt.Rows[0]["relation"].ToString() : "");
                    objVaccinations.firstname = (dt.Rows[0]["firstname"] != DBNull.Value ? dt.Rows[0]["firstname"].ToString() : "");
                    objVaccinations.lastname = (dt.Rows[0]["lastname"] != DBNull.Value ? dt.Rows[0]["lastname"].ToString() : "");
                    objVaccinations.phone = (dt.Rows[0]["phone"] != DBNull.Value ? dt.Rows[0]["phone"].ToString() : "");
                    objVaccinations.age = (dt.Rows[0]["age"] != DBNull.Value ? dt.Rows[0]["age"].ToString() : "");
                    objVaccinations.comments = (dt.Rows[0]["comments"] != DBNull.Value ? dt.Rows[0]["comments"].ToString() : "");
                    objVaccinations.datesent = (dt.Rows[0]["datesent"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["datesent"]) : DateTime.MinValue);


                }
            }
            return objVaccinations;
        }

  

        public List<TCAssociationTool.Entities.Vaccinations> GetVaccinationsListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.Vaccinations> lstVaccinations = new List<TCAssociationTool.Entities.Vaccinations>();
            DataTable dt = _Vaccinations.GetVaccinationsListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _Vaccinations.GetVaccinationsListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Vaccinations objVaccinations = new TCAssociationTool.Entities.Vaccinations();

                    objVaccinations.RId = Convert.ToInt64(dr["RId"].ToString());
                    objVaccinations.Id = Convert.ToInt64(dr["Id"].ToString());
                    objVaccinations.membername = (dr["membername"] != DBNull.Value ? dr["membername"].ToString() : "");
                    objVaccinations.position = (dr["position"] != DBNull.Value ? dr["position"].ToString() : "");
                    objVaccinations.relation = (dr["relation"] != DBNull.Value ? dr["relation"].ToString() : "");
                    objVaccinations.firstname = (dr["firstname"] != DBNull.Value ? dr["firstname"].ToString() : "");
                    objVaccinations.phone = (dr["phone"] != DBNull.Value ? dr["phone"].ToString() : "");
                    objVaccinations.age = (dr["age"] != DBNull.Value ? dr["age"].ToString() : "");
                    objVaccinations.comments = (dr["comments"] != DBNull.Value ? dr["comments"].ToString() : "");
                    objVaccinations.datesent = (dr["datesent"] != DBNull.Value ? Convert.ToDateTime(dr["datesent"]) : DateTime.MinValue);


                    lstVaccinations.Add(objVaccinations);
                }
            }
            return lstVaccinations;
        }

        #endregion

    }
}
