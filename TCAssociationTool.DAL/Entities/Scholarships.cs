using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAssociationTool.Entities
{
  public  class Scholarships

    {
	public Int64 Id { get; set; }
    public Int64 RId { get; set; }
    public string prefix    { get; set; }
	public string firstname    { get; set; }
	public string middlename    { get; set; }
	public string lastname    { get; set; }
	public string email    { get; set; }
	public string phone    { get; set; }
	public string address    { get; set; }
	public string address2    { get; set; }
	public string city    { get; set; }
	public string state    { get; set; }
	public string country    { get; set; }
	public string zipcode    { get; set; }
	public string permanent_address    { get; set; }
	public string permanent_address2    { get; set; }
	public string permanent_city    { get; set; }
	public string permanent_state{ get; set; }
	public string permanent_country    { get; set; }
	public string permanent_zipcode    { get; set; }
    public string dob    { get; set; }
	public string place_of_birth    { get; set; }
	public string citizenship    { get; set; }
	public string institution    { get; set; }
	public string study    { get; set; }
	public string parent_prefix    { get; set; }
	public string parent_firstname    { get; set; }
	public string parent_middlename    { get; set; }
	public string parent_lastname    { get; set; }
	public string parent_email    { get; set; }
	public string parent_phone    { get; set; }
	public string parent_address    { get; set; }
	public string parent_address2    { get; set; }
	public string parent_city    { get; set; }
	public string parent_state    { get; set; }
	public string parent_country    { get; set; }
	public string parent_zipcode    { get; set; }
	public string relationship    { get; set; }
	public string father_occupation    { get; set; }
	public string mother_occupation    { get; set; }
	public string income    { get; set; }
	public string school_name    { get; set; }
	public string school_location    { get; set; }
	public string attendance    { get; set; }
	public string award1_name    { get; set; }
	public string award1_year    { get; set; }
	public string award2_name    { get; set; }
	public string award2_year    { get; set; }
	public string tuition_cost    { get; set; }
	public string housing_cost    { get; set; }
	public string extra_activities    { get; set; }
	public string documents    { get; set; }
	public string as_number    { get; set; }
	public DateTime datesent { get; set; }
	public string  status2    { get; set; }
	public string  brief_note    { get; set; }
	public string  is_ata_member    { get; set; }

	}
}
