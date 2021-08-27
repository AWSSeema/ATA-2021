using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TCAssociationTool.BLL
{
   public class Ticket
    {
        TCAssociationTool.DAL.Ticket _Ticket = new TCAssociationTool.DAL.Ticket();
        #region Methods

        public Int64 InsertTicket(Entities.Ticket objTickets)
        {
            Int64 _status = 0;
            if (objTickets != null)
            {
                _status = _Ticket.InsertTicket(objTickets);

            }
            return _status;
        }

        //public Int64 TicketUpdateComments(Entities.Ticket objTickets)
        //{
        //    Int64 _status = 0;
        //    if (objTickets != null)
        //    {
        //        _status = _Ticket.TicketUpdateComments(objTickets);

        //    }
        //    return _status;
        //}

        public Int64 DeleteTicket(Int64 Id)
        {
            Int64 _status = 0;
            _status = _Ticket.DeleteTicket(Id);
            return _status;
        }

        public Int64 TicketDeleteAll(string Id)
        {
            Int64 _status = 0;
            _status = _Ticket.TicketDeleteAll(Id);
            return _status;
        }



        #endregion

        #region Entities filling



        public TCAssociationTool.Entities.Ticket GetTicketById(Int64 Id, ref int status)
        {
            TCAssociationTool.Entities.Ticket objTicket = new TCAssociationTool.Entities.Ticket();
            DataTable dt = new DataTable();
            if (Id != 0)
            {
                dt = _Ticket.GetTicketById(Id, ref status);
                if (dt.Rows.Count == 1)
                {
                    objTicket.Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                    objTicket.firstname = (dt.Rows[0]["firstname"] != DBNull.Value ? dt.Rows[0]["firstname"].ToString() : "");
                    objTicket.lastname = (dt.Rows[0]["lastname"] != DBNull.Value ? dt.Rows[0]["lastname"].ToString() : "");
                    objTicket.age = (dt.Rows[0]["age"] != DBNull.Value ? dt.Rows[0]["age"].ToString() : "");
                    objTicket.category = (dt.Rows[0]["category"] != DBNull.Value ? dt.Rows[0]["category"].ToString() : "");
                    objTicket.amount = (dt.Rows[0]["amount"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[0]["amount"]) : 0);
                    objTicket.ticketid = (dt.Rows[0]["ticketid"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["ticketid"]) : 0);


                }
            }
            return objTicket;
        }




        public List<TCAssociationTool.Entities.Ticket> GetTicketListByVariable(Int64 eventid, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<TCAssociationTool.Entities.Ticket> lstTicket = new List<TCAssociationTool.Entities.Ticket>();
            DataTable dt = _Ticket.GetTicketListByVariable(eventid, Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _Ticket.GetTicketListByVariable(eventid, Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TCAssociationTool.Entities.Ticket objTicket = new TCAssociationTool.Entities.Ticket();

                    objTicket.RId = Convert.ToInt64(dr["RId"].ToString());
                    objTicket.Id = Convert.ToInt64(dr["Id"].ToString());
                    objTicket.firstname = (dr["firstname"] != DBNull.Value ? dr["firstname"].ToString() : "");
                    objTicket.lastname = (dr["lastname"] != DBNull.Value ? dr["lastname"].ToString() : "");
                    objTicket.age = (dr["age"] != DBNull.Value ? dr["age"].ToString() : "");
                    objTicket.category = (dr["category"] != DBNull.Value ? dr["category"].ToString() : "");
                    objTicket.amount = (dr["amount"] != DBNull.Value ? Convert.ToDecimal(dr["amount"]) : 0);
                    objTicket.ticketid = (dr["ticketid"] != DBNull.Value ? Convert.ToInt64(dr["ticketid"]) : 0);

                    lstTicket.Add(objTicket);
                }
            }
            return lstTicket;
        }

        #endregion
    }
}
