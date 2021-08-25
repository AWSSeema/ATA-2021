using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAssociationTool.Entities
{
    public class EventRegistrationCounts
    {
        public Int64 RId { get; set; }
        public Int64 EventRegCountId { get; set; }
        public Int64 EventId { get; set; }
        public Int64 EventUserInfoId { get; set; }
        public Int64 EventRegistrationTypeId { get; set; }
        public Int64 Count { get; set; }
        public decimal Amount { get; set; }
        public Int64 VisitCount { get; set; }
        public string Field1 { get; set; }
        public bool Field2 { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string TransactionId { get; set; }
        public string RegistrationTitle { get; set; }


        public string SulekhaId { get; set; }
    }

    public class EventsTickets
    {
        public Int64 Barcode { get; set; }
        public Int64 EventId { get; set; }
        public Int64 EventUserInfoId { get; set; }
        public Int64 EventRegistrationTypeId { get; set; }
        public string BarCodeImage { get; set; }
        public Int64 Count { get; set; }
        public decimal Amount { get; set; }
        public string OrderBy { get; set; }
        public string Field1 { get; set; }
        public bool Field2 { get; set; }
        public string Field3 { get; set; }
        public string RegistrationTitle { get; set; }

    }

}
