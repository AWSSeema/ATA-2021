using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAssociationTool.Entities
{
  public  class Golf2021
    {
        public Int64 RId { get; set; }
        public Int64 Id { get; set; }
        public string playertype { get; set; }
        public string player1_name { get; set; }
        public string player1_phone { get; set; }
        public string player1_email { get; set; }
        public string player1_tshirt { get; set; }
        public string sponsorship { get; set; }
        public DateTime created_at { get; set; }
        public Int64 payment_status { get; set; }
        public string payment_method { get; set; }
        public string cardno { get; set; }
        public string payment_response { get; set; }
        public string ip_address { get; set; }
        public string player2_name { get; set; }
        public string player2_phone { get; set; }
        public string player2_email { get; set; }
        public string player2_tshirt { get; set; }
        public string player3_name { get; set; }
        public string player3_phone { get; set; }
        public string player3_email { get; set; }
        public string player3_tshirt { get; set; }
        public string player4_name { get; set; }
        public string player4_phone { get; set; }
        public string player4_email { get; set; }
        public string player4_tshirt { get; set; }
        public decimal amount { get; set; }
        public string ip { get; set; }
        public string vcode { get; set; }
        public string comments { get; set; }
        public string PaymentStauts { get; set; }


    }
}
