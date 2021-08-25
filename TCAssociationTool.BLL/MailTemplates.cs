using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TCAssociationTool.BLL
{
   public class MailTemplates
    {
       TCAssociationTool.DAL.MailTemplates _MailTemplates = new TCAssociationTool.DAL.MailTemplates();

        #region Methods

       public Int64 InsertMailTemplates(Entities.MailTemplates objMailTemplates)
        {
            Int64 _status = 0;
            if (objMailTemplates != null )
            {
                _status = _MailTemplates.InsertMailTemplates(objMailTemplates);

            }
            return _status;
        }

       public Int64 DeleteMailTemplate(Int64 MailTemplateId)
        {
            Int64 _status = 0;
            _status = _MailTemplates.DeleteMailTemplate(MailTemplateId);
            return _status;
        }

        #endregion

        #region Entities filling

       public List<TCAssociationTool.Entities.MailTemplates> GetMailTemplatesList(string MailType, ref int status)
       {
           List<TCAssociationTool.Entities.MailTemplates> lstMailTemplates = new List<Entities.MailTemplates>();
           DataTable dt = _MailTemplates.GetMailTemplatesList(MailType, ref status);

           if (dt.Rows.Count != 0)
           {
               foreach (DataRow dr in dt.Rows)
               {
                   TCAssociationTool.Entities.MailTemplates objlstMailTemplates = new TCAssociationTool.Entities.MailTemplates();

                   objlstMailTemplates.MailTemplateId = Convert.ToInt64(dr["MailTemplateId"].ToString());
                   objlstMailTemplates.Heading = dr["Heading"].ToString();
                   objlstMailTemplates.Subject = dr["Subject"].ToString();
                   objlstMailTemplates.Description = dr["Description"].ToString();
                   objlstMailTemplates.MailType = (dr["MailType"] != DBNull.Value ? dr["MailType"].ToString() : "");
                   objlstMailTemplates.UpdatedBy = dr["UpdatedBy"].ToString();
                   objlstMailTemplates.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"].ToString());

                   lstMailTemplates.Add(objlstMailTemplates);
               }

           }
           return lstMailTemplates;
       }

       public TCAssociationTool.Entities.MailTemplates GetMailTemplateById(string TemplateName, Int64 MailTemplateId, ref int status)
        {
            TCAssociationTool.Entities.MailTemplates objMailTemplates = new TCAssociationTool.Entities.MailTemplates();
            DataTable dt = new DataTable();
                dt = _MailTemplates.GetMailTemplateById(TemplateName,MailTemplateId, ref status);
                if (dt.Rows.Count == 1)
                {
                    objMailTemplates.MailTemplateId = Convert.ToInt64(dt.Rows[0]["MailTemplateId"].ToString());
                    objMailTemplates.Heading = dt.Rows[0]["Heading"].ToString();
                    objMailTemplates.Subject = dt.Rows[0]["Subject"].ToString();
                    objMailTemplates.Description = dt.Rows[0]["Description"].ToString();
                    objMailTemplates.MailType = (dt.Rows[0]["MailType"] != DBNull.Value ? dt.Rows[0]["MailType"].ToString() : "");
                    objMailTemplates.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                    objMailTemplates.UpdatedTime = Convert.ToDateTime(dt.Rows[0]["UpdatedTime"].ToString());

                }
            return objMailTemplates;
        }

       public List<TCAssociationTool.Entities.MailTemplates> GetMailTemplatesListByVariable(string Search,string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.MailTemplates> lstMailTemplates = new List<TCAssociationTool.Entities.MailTemplates>();
            DataTable dt = _MailTemplates.GetMailTemplatesListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _MailTemplates.GetMailTemplatesListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.MailTemplates objMailTemplates = new TCAssociationTool.Entities.MailTemplates();

                    objMailTemplates.RId = Convert.ToInt64(dr["RId"].ToString());
                    objMailTemplates.MailTemplateId = Convert.ToInt64(dr["MailTemplateId"].ToString());
                    objMailTemplates.Heading = dr["Heading"].ToString();
                    objMailTemplates.Subject = dr["Subject"].ToString();
                    objMailTemplates.Description = dr["Description"].ToString();
                    objMailTemplates.MailType = (dr["MailType"] != DBNull.Value ? dr["MailType"].ToString() : "");
                    objMailTemplates.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                    objMailTemplates.UpdatedTime = Convert.ToDateTime(dt.Rows[0]["UpdatedTime"].ToString());

                    lstMailTemplates.Add(objMailTemplates);
                }
            }
            return lstMailTemplates;
        }

        #endregion
    }
}
