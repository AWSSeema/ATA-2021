using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCAssociationTool.Entities
{
 public class CommitteeMembers
    {

        public Int64 RId { get; set; }

        public Int64 CommitteeMemberId { get; set; }

        public Int64 CommitteeId { get; set; }

        public Int64 CommitteeCategoryId { get; set; }

        public int DisplayOrder { get; set; }

        public string Designation { get; set; }

        public string Name { get; set; }
       
    }
}
