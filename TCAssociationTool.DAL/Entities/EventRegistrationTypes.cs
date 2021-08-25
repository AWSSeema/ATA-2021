using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAssociationTool.Entities
{
    public class EventRegistrationTypes
    {
        public Int64 EventRegistrationTypeId { get; set; }
        public Int64 EventId { get; set; }
        public decimal MemberAmount { get; set; }
        public decimal NonMemberAmount { get; set; }
        public string RegistrationTitle { get; set; }
        public Int64 RegCount { get; set; }
        public int OrderNo { get; set; }
        public bool IsActive { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
