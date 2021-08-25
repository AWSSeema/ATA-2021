using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAssociationTool.Entities
{
   public class Cultural
    {
		public Int64 RId { get; set; }

		public Int64 id { get; set; }

		public string firstname { get; set; }

		public string lastname { get; set; }

		public string middlename { get; set; }

		public string email { get; set; }

		public string address1 { get; set; }

		public string city { get; set; }

		public string state { get; set; }

		public string zip { get; set; }

		public string mobile { get; set; }

		public string comments { get; set; }

		public DateTime datesent { get; set; }

		public string category { get; set; }

		public string PaymentStatus { get; set; }

		public decimal amount { get; set; }

		public string PaymentMethod { get; set; }

		public Int64 payment_statusid { get; set; }
		public DateTime datemodified { get; set; }
		public string admincomments { get; set; }
		public Int64 payment_methodid { get; set; }

		public Int64 adminid { get; set; }

		public string cheque_details { get; set; }

		public string vcode { get; set; }
		public bool isconfirm { get; set; }

		public string cardno { get; set; }
		public string cultural_no { get; set; }

		public string dob { get; set; }
		public string age { get; set; }
		public string gender { get; set; }

		public Int64 location_id { get; set; }
		public string youtubeurl { get; set; }
		public string subcategory { get; set; }
		public string location { get; set; }
		public string description { get; set; }


	}
}
