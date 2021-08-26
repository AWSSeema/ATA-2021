using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAssociationTool.Entities
{
 public   class CulturalRegistrations
    {
        public Int64 RId { get; set; }
        public Int64 Id  { get; set; }
        public string item_title    { get; set; }
        public string item_type    { get; set; }
        public string item_description { get; set; }
        public string minutes   { get; set; }
        public string group_name { get; set; }
        public string name  { get; set; }
        public string choreographer { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string address1  { get; set; }
        public string address2 { get; set; }
        public string city  { get; set; }
        public string state  { get; set; }
        public string zip  { get; set; }
        public string url     { get; set; }
        public DateTime date_created { get; set; }
        public bool status2 { get; set; }
        public DateTime date_modified  { get; set; }
        public string comments	   { get; set; }
    }
}
