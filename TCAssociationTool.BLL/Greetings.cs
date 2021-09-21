using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TCAssociationTool.BLL
{
    public class Greetings
    {
        TCAssociationTool.DAL.Greetings _Greetings = new TCAssociationTool.DAL.Greetings();

        #region Methods

        public Int64 InsertGreetings(Entities.Greetings objGreetings, ref string imgsrc)
        {
            Int64 _status = 0;
            if (objGreetings != null)
            {
                _status = _Greetings.InsertGreetings(objGreetings, ref imgsrc);

            }
            return _status;
        }

        public Int64 DeleteGreetings(Int64 id)
        {
            Int64 _status = 0;
            _status = _Greetings.DeleteGreetings(id);
            return _status;
        }

        public Int64 UpdateGreetingsStatus(Int64 id)
        {
            Int64 _status = 0;
            _status = _Greetings.UpdateGreetingsStatus(id);
            return _status;
        }

   

        #endregion

        #region Entities filling

        public List<TCAssociationTool.Entities.Greetings> GetGreetingsList(ref int status)
        {
            List<TCAssociationTool.Entities.Greetings> lstGreetings = new List<Entities.Greetings>();
            DataTable dt = _Greetings.GetGreetingsList(ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Greetings objlstGreetings = new TCAssociationTool.Entities.Greetings();

                    objlstGreetings.id = Convert.ToInt64(dr["id"].ToString());
                    objlstGreetings.title = dr["title"].ToString();
                    objlstGreetings.imgsrc = (dr["imgsrc"] != DBNull.Value ? dr["imgsrc"].ToString() : "");
                    objlstGreetings.imgwidth = (dr["imgwidth"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["imgwidth"].ToString()) : 0);
                    objlstGreetings.status2 = Convert.ToBoolean(dr["status2"]);
                    objlstGreetings.date_modified = Convert.ToDateTime(dr["date_modified"].ToString());
                    objlstGreetings.top_padding = (dr["top_padding"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["top_padding"].ToString()) : 0);
                    objlstGreetings.link = (dt.Rows[0]["link"] != DBNull.Value ? dt.Rows[0]["link"].ToString() : "");
                    objlstGreetings.target = (dt.Rows[0]["target"] != DBNull.Value ? dt.Rows[0]["target"].ToString() : "");
                    objlstGreetings.lastdate = (dr["lastdate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["lastdate"].ToString()) : DateTime.MinValue);
                  
                    lstGreetings.Add(objlstGreetings);
                }

            }
            return lstGreetings;
        }

        public TCAssociationTool.Entities.Greetings GetGreetingsById(Int64 id, ref int status)
        {
            TCAssociationTool.Entities.Greetings objGreetings = new TCAssociationTool.Entities.Greetings();
            DataTable dt = new DataTable();
            if (id != 0)
            {
                dt = _Greetings.GetGreetingsById(id, ref status);
                if (dt.Rows.Count == 1)
                {
                    objGreetings.id = Convert.ToInt64(dt.Rows[0]["id"].ToString());
                    objGreetings.title = dt.Rows[0]["title"].ToString();
                    objGreetings.imgsrc = (dt.Rows[0]["imgsrc"] != DBNull.Value ? dt.Rows[0]["imgsrc"].ToString() : "");
                    objGreetings.imgwidth = (dt.Rows[0]["imgwidth"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["imgwidth"].ToString()) : 0);
                    objGreetings.status2 = Convert.ToBoolean(dt.Rows[0]["status2"]);
                    objGreetings.date_modified = Convert.ToDateTime(dt.Rows[0]["date_modified"].ToString());
                    objGreetings.top_padding = (dt.Rows[0]["top_padding"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["top_padding"].ToString()) : 0);
                    objGreetings.link = (dt.Rows[0]["link"] != DBNull.Value ? dt.Rows[0]["link"].ToString() : "");
                    objGreetings.target = (dt.Rows[0]["target"] != DBNull.Value ? dt.Rows[0]["target"].ToString() : "");
                    objGreetings.lastdate = (dt.Rows[0]["lastdate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["lastdate"].ToString()) : DateTime.MinValue);
                    objGreetings.Elastdate = objGreetings.lastdate.ToString("dd/MM/yyyy");


                }
            }
            return objGreetings;
        }

        public List<TCAssociationTool.Entities.Greetings> GetGreetingsListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.Greetings> lstGreetings = new List<TCAssociationTool.Entities.Greetings>();

            DataTable dt = _Greetings.GetGreetingsListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _Greetings.GetGreetingsListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Greetings objGreetings = new TCAssociationTool.Entities.Greetings();

                    objGreetings.RId = Convert.ToInt32(dr["RId"].ToString());
                    objGreetings.id = Convert.ToInt64(dr["id"].ToString());
                    objGreetings.title = dr["title"].ToString();
                    objGreetings.imgsrc = (dr["imgsrc"] != DBNull.Value ? dr["imgsrc"].ToString() : "");
                    objGreetings.imgwidth = (dr["imgwidth"] != DBNull.Value ? Convert.ToInt64(dr["imgwidth"].ToString()) : 0);
                    objGreetings.status2 = Convert.ToBoolean(dr["status2"]);
                    objGreetings.date_modified = Convert.ToDateTime(dr["date_modified"].ToString());
                    objGreetings.top_padding = (dr["top_padding"] != DBNull.Value ? Convert.ToInt64(dr["top_padding"].ToString()) : 0);
                    objGreetings.link = (dr["link"] != DBNull.Value ? dr["link"].ToString() : "");
                    objGreetings.target = (dr["target"] != DBNull.Value ? dr["target"].ToString() : "");
                    objGreetings.lastdate = (dr["lastdate"] != DBNull.Value ? Convert.ToDateTime(dr["lastdate"]) : DateTime.MinValue);


                    lstGreetings.Add(objGreetings);
                }
            }
            return lstGreetings;
        }

        #endregion
    }
}
