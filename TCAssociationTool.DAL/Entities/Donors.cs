﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCAssociationTool.Entities
{
    public class Donors
    {
        public Int64 RId { get; set; }

        public Int64  DonorId {get ; set ;}

        public Int64 UserId { get; set; }

        public string  FirstName  {get ; set ;}

	    public string  LastName  {get ; set ;}

	    public string  Email  {get ; set ;}

	    public string  PhoneNo  {get ; set ;}

	    public string  Address  {get ; set ;}

	    public string  DonationProgram  {get ; set ;}

	    public string  DonationCause  {get ; set ;}

	    public bool  IsActive  {get ; set ;}

	    public Decimal  Amount  {get ; set ;}

	    public string  TransactionId  {get ; set ;}

	    public Int64  PaymentStatusId  {get ; set ;}

        public string PaymentStatus { get; set; }

	    public Int64  PaymentMethodId  {get ; set ;}

        public string PaymentMethod { get; set; }

	    public DateTime  OrderDate  {get ; set ;}

	    public string  PaymentBy  {get ; set ;}

	    public string  InsertedBy  {get ; set ;}

	    public DateTime  InsertedDate  {get ; set ;}

	    public string  UpdatedBy  {get ; set ;}

	    public DateTime  UpdatedDate  {get ; set ;}
      
        public string MI { get; set; }

        public string Profession { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string MobilePhone { get; set; }

        public string Fax { get; set; }

        public string WebsiteAddress { get; set; }

        public string Field1 { get; set; }

        public string Filed2 { get; set; }

        public string CardNumber { get; set; }

        public string CSVMonth { get; set; }

        public string CSVYear { get; set; }

        public string Cvv { get; set; }

        public string CardType { get; set; }

        public string CardHolderFirstName { get; set; }

        public string CardHolderLastName { get; set; }

        

    }
}
