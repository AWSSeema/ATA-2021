using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCAssociationTool.Entities
{
    public class MembershipTypes
    {
        public Int64 RId { get; set; }

        public Int64 MembershipTypeId { get; set; }

        public DateTime Validity { get; set; }

        public string MembershipType { get; set; }

        public Decimal Price { get; set; }

        public Int64 DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedTime { get; set; }
    }
}
