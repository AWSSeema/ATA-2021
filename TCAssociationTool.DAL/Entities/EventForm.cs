using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAssociationTool.Entities
{
   public class EventForm
    {
		public Int64 Id { get; set; }
		public Int64 RId { get; set; }
		public string firstname { get; set; }
		public string lastname { get; set; }
		public string email { get; set; }
		public string phone { get; set; }
		public Int64 members { get; set; }
	    public string address { get; set; }
		public string city { get; set; }
		public string state { get; set; }
		public string zip { get; set; }
		public string ismember { get; set; }
		public DateTime datesent { get; set; }
		public bool status2 { get; set; }
		public string comments { get; set; }
		public string eventname { get; set; }
		public Int64 eventid { get; set; }
		
	}
}
