using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCAssociationTool.Entities
{
    public class Volunteers
    {
        public Int64 RId { get; set; }

        public Int64 VolunteerId { get; set; }

        public Int64 VolunteerCategoryId { get; set; }

        public Int64 EventId { get; set; }

	    public string FirstName  { get; set; }

	    public string LastName  { get; set; }

	    public string Email  { get; set; }

	    public string PhoneNo  { get; set; }

	    public string Address  { get; set; }

        public string Comments { get; set; }

        public string HoursSpent { get; set; }

        public string Field1 { get; set; }

        public string Field2 { get; set; }

	    public bool IsActive  { get; set; }

	    public string InsertedBy  { get; set; }

	    public DateTime InsertedDate { get; set; }

	    public string UpdatedBy  { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string CategoryName { get; set; }

        public string EventName { get; set; }

        public Int64 MemberId { get; set; }
    }
}
