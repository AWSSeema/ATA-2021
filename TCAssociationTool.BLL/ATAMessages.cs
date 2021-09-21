using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TCAssociationTool.BLL
{
   public class ATAMessages
    {
        TCAssociationTool.DAL.ATAMessages _ATAMessages = new TCAssociationTool.DAL.ATAMessages();

        #region Methods

        public Int64 InsertATAMessages(Entities.ATAMessages objATAMessages, ref string attachment)
        {
            Int64 _status = 0;
            if (objATAMessages != null)
            {
                _status = _ATAMessages.InsertATAMessages(objATAMessages, ref attachment);

            }
            return _status;
        }

        public Int64 DeleteATAMessages(Int64 id)
        {
            Int64 _status = 0;
            _status = _ATAMessages.DeleteATAMessages(id);
            return _status;
        }

        public Int64 UpdateATAMessagesStatus(Int64 id)
        {
            Int64 _status = 0;
            _status = _ATAMessages.UpdateATAMessagesStatus(id);
            return _status;
        }

        public Int64 UpdateATAMessagesOrderNo(int orderno, Int64 id)
        {
            Int64 _status = 0;
            _status = _ATAMessages.UpdateATAMessagesOrderNo(orderno, id);
            return _status;
        }
        #endregion

        #region Entities filling

        public List<TCAssociationTool.Entities.ATAMessages> GetATAMessagesList(ref int status)
        {
            List<TCAssociationTool.Entities.ATAMessages> lstATAMessages = new List<Entities.ATAMessages>();
            DataTable dt = _ATAMessages.GetATAMessagesList(ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.ATAMessages objATAMessages = new TCAssociationTool.Entities.ATAMessages();

                    objATAMessages.id = Convert.ToInt64(dr["id"].ToString());
                    objATAMessages.heading = dr["heading"].ToString();
                    objATAMessages.status2 = Convert.ToBoolean(dr["IsActive"]);
                    objATAMessages.datemodified = Convert.ToDateTime(dr["UpdatedDate"].ToString());
                    objATAMessages.attachment = (dr["attachment"] != DBNull.Value ? dr["attachment"].ToString() : "");
                    objATAMessages.shortdesc = (dr["shortdesc"] != DBNull.Value ? dr["shortdesc"].ToString() : "");
                    objATAMessages.pageurl = (dr["pageurl"] != DBNull.Value ? dr["pageurl"].ToString() : "");
                    objATAMessages.orderno = (dr["orderno"] != DBNull.Value ? Convert.ToInt32(dr["orderno"].ToString()) : 0);
                    objATAMessages.target = (dr["target"] != DBNull.Value ? dr["target"].ToString() : "");
                    objATAMessages.pdate = (dr["pdate"] != DBNull.Value ? Convert.ToDateTime(dr["pdate"].ToString()) : DateTime.MinValue);

                    lstATAMessages.Add(objATAMessages);
                }

            }
            return lstATAMessages;
        }

        public TCAssociationTool.Entities.ATAMessages GetATAMessagesById(Int64 id, ref int status)
        {
            TCAssociationTool.Entities.ATAMessages objATAMessages = new TCAssociationTool.Entities.ATAMessages();
            DataTable dt = new DataTable();
            if (id != 0)
            {
                dt = _ATAMessages.GetATAMessagesById(id, ref status);
                if (dt.Rows.Count == 1)
                {
                    objATAMessages.id = Convert.ToInt64(dt.Rows[0]["id"].ToString());
                    objATAMessages.heading = dt.Rows[0]["heading"].ToString();
                    objATAMessages.status2 = Convert.ToBoolean(dt.Rows[0]["status2"]);
                    objATAMessages.datemodified = Convert.ToDateTime(dt.Rows[0]["datemodified"].ToString());
                    objATAMessages.attachment = (dt.Rows[0]["attachment"] != DBNull.Value ? dt.Rows[0]["attachment"].ToString() : "");
                    objATAMessages.shortdesc = (dt.Rows[0]["shortdesc"] != DBNull.Value ? dt.Rows[0]["shortdesc"].ToString() : "");
                    objATAMessages.pageurl = (dt.Rows[0]["pageurl"] != DBNull.Value ? dt.Rows[0]["pageurl"].ToString() : "");
                    objATAMessages.orderno = (dt.Rows[0]["orderno"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["orderno"].ToString()) : 0);
                    objATAMessages.target = (dt.Rows[0]["target"] != DBNull.Value ? dt.Rows[0]["target"].ToString() : "");
                    objATAMessages.pdate = (dt.Rows[0]["pdate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["pdate"].ToString()) : DateTime.MinValue);
                }
            }
            return objATAMessages;
        }

        public List<TCAssociationTool.Entities.ATAMessages> GetATAMessagesListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.ATAMessages> lstATAMessages = new List<TCAssociationTool.Entities.ATAMessages>();

            DataTable dt = _ATAMessages.GetATAMessagesListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _ATAMessages.GetATAMessagesListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.ATAMessages objATAMessages = new TCAssociationTool.Entities.ATAMessages();

                    objATAMessages.RId = Convert.ToInt32(dr["RId"].ToString());
                    objATAMessages.id = Convert.ToInt64(dr["id"].ToString());
                    objATAMessages.heading = dr["heading"].ToString();
                    objATAMessages.status2 = Convert.ToBoolean(dr["status2"]);
                    objATAMessages.datemodified = Convert.ToDateTime(dr["datemodified"].ToString());
                    objATAMessages.attachment = (dr["attachment"] != DBNull.Value ? dr["attachment"].ToString() : "");
                    objATAMessages.shortdesc = (dr["shortdesc"] != DBNull.Value ? dr["shortdesc"].ToString() : "");
                    objATAMessages.pageurl = (dr["pageurl"] != DBNull.Value ? dr["pageurl"].ToString() : "");
                    objATAMessages.orderno = (dr["orderno"] != DBNull.Value ? Convert.ToInt32(dr["orderno"].ToString()) : 0);
                    objATAMessages.target = (dr["target"] != DBNull.Value ? dr["target"].ToString() : "");
                    objATAMessages.pdate = (dr["pdate"] != DBNull.Value ? Convert.ToDateTime(dr["pdate"].ToString()) : DateTime.MinValue);

                    lstATAMessages.Add(objATAMessages);
                }
            }
            return lstATAMessages;
        }

        #endregion
    }
}
