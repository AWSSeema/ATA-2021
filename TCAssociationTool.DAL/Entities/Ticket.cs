using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAssociationTool.Entities
{
	public class Ticket
    {
		public Int64 RId { get; set; }
		public Int64 Id { get; set; }
		public string firstname { get; set; }
		public string lastname { get; set; }
		public string age { get; set; }
		public string category { get; set; }
		public decimal amount { get; set; }
		public Int64 ticketid { get; set; }
	}
}

