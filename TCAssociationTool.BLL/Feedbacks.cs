using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TCAssociationTool.BLL
{
   public class Feedbacks
    {
        TCAssociationTool.DAL.Feedbacks _Feedbacks = new TCAssociationTool.DAL.Feedbacks();
        #region Methods

        public Int64 InsertFeedbacks(Entities.Feedbacks objFeedbackss)
        {
            Int64 _status = 0;
            if (objFeedbackss != null)
            {
                _status = _Feedbacks.InsertFeedbacks(objFeedbackss);

            }
            return _status;
        }

        public Int64 FeedbacksCommentUpdate(Entities.Feedbacks objFeedbackss)
        {
            Int64 _status = 0;
            if (objFeedbackss != null)
            {
                _status = _Feedbacks.FeedbacksCommentUpdate(objFeedbackss);

            }
            return _status;
        }

        public Int64 DeleteFeedbacks(Int64 Id)
        {
            Int64 _status = 0;
            _status = _Feedbacks.DeleteFeedbacks(Id);
            return _status;
        }

        public Int64 FeedbacksDeleteAll(string Id)
        {
            Int64 _status = 0;
            _status = _Feedbacks.FeedbacksDeleteAll(Id);
            return _status;
        }



        #endregion

        #region Entities filling



        public TCAssociationTool.Entities.Feedbacks GetFeedbacksById(Int64 Id, ref int status)
        {
            TCAssociationTool.Entities.Feedbacks objFeedbacks = new TCAssociationTool.Entities.Feedbacks();
            DataTable dt = new DataTable();
            if (Id != 0)
            {
                dt = _Feedbacks.GetFeedbacksById(Id, ref status);
                if (dt.Rows.Count == 1)
                {
                    objFeedbacks.Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                    objFeedbacks.firstname = (dt.Rows[0]["firstname"] != DBNull.Value ? dt.Rows[0]["firstname"].ToString() : "");
                    objFeedbacks.lastname = (dt.Rows[0]["lastname"] != DBNull.Value ? dt.Rows[0]["lastname"].ToString() : "");
                    objFeedbacks.email = (dt.Rows[0]["email"] != DBNull.Value ? dt.Rows[0]["email"].ToString() : "");
                    objFeedbacks.datesent = (dt.Rows[0]["datesent"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["datesent"]) : DateTime.MinValue);
                    objFeedbacks.message = (dt.Rows[0]["message"] != DBNull.Value ? dt.Rows[0]["message"].ToString() : "");
                    objFeedbacks.admincomments = (dt.Rows[0]["admincomments"] != DBNull.Value ? dt.Rows[0]["admincomments"].ToString() : "");
                 

                }
            }
            return objFeedbacks;
        }



        public List<TCAssociationTool.Entities.Feedbacks> GetFeedbacksListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.Feedbacks> lstFeedbacks = new List<TCAssociationTool.Entities.Feedbacks>();
            DataTable dt = _Feedbacks.GetFeedbacksListByVariable( Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _Feedbacks.GetFeedbacksListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Feedbacks objFeedbacks = new TCAssociationTool.Entities.Feedbacks();

                    objFeedbacks.RId = Convert.ToInt64(dr["RId"].ToString());
                    objFeedbacks.Id = Convert.ToInt64(dr["Id"].ToString());
                    objFeedbacks.firstname = (dr["firstname"] != DBNull.Value ? dr["firstname"].ToString() : "");
                    objFeedbacks.lastname = (dr["lastname"] != DBNull.Value ? dr["lastname"].ToString() : "");
                    objFeedbacks.email = (dr["email"] != DBNull.Value ? dr["email"].ToString() : "");
                    objFeedbacks.admincomments = (dr["admincomments"] != DBNull.Value ? dr["admincomments"].ToString() : "");
                    objFeedbacks.message = (dr["message"] != DBNull.Value ? dr["message"].ToString() : "");
                    objFeedbacks.datesent = (dr["datesent"] != DBNull.Value ? Convert.ToDateTime(dr["datesent"]) : DateTime.MinValue);


                    lstFeedbacks.Add(objFeedbacks);
                }
            }
            return lstFeedbacks;
        }

        #endregion
    }
}
