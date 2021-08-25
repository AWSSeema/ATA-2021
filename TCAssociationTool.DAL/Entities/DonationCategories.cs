using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCAssociationTool.Entities
{
    public class DonationCategories
    {
        public Int64 RId { get; set; }

        public Int64 DonationCategoryId { get; set; }

        public string CategoryName { get; set; }

        public Int32 OrderNo { get; set; }

        public Boolean IsActive { get; set; }

        public Int32 DonorCount { get; set; }
    }
}
