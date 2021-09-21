using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCAssociationTool.Entities
{
    public class Members
    {
        #region

        public Int64 RId { get; set; }

        public Int64 MemberId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfileImage { get; set; }

        public string Occupation { get; set; }

        public string SpouseFirstName { get; set; }

        public string SpouseLastName { get; set; }

        public string SpouseOccupation { get; set; }

        public string OrderType { get; set; }

        public string SpouseEmail { get; set; }

        public string SpouseCell { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public Boolean IsApproved { get; set; }

        public Boolean IsLockedOut { get; set; }

        public Boolean IsActivated { get; set; }

        public DateTime DateActivated { get; set; }

        public Guid RegistrationGUID { get; set; }

        public Decimal MemberAmount { get; set; }

        public Decimal Amount { get; set; }

        public string TransactionId { get; set; }

        public Int64 PaymentStatusId { get; set; }

        public Int64 PaymentMethodId { get; set; }

        public Int64 MembershipOrderId { get; set; }

        public string CardNumber { get; set; }

        public string CSVMonth { get; set; }

        public string CSVYear { get; set; }

        public string Cvv { get; set; }

        public string CardType { get; set; }

        public string CardHolderFirstName { get; set; }

        public string CardHolderLastName { get; set; }

        public string PaymentStatus { get; set; }

        public string Fax { get; set; }

        public string WebsiteAddress { get; set; }

        public string Address2 { get; set; }

        public string PaymentMethod { get; set; }

        public string AdminComment { get; set; }

        public string UserComment { get; set; }

        public string BankName { get; set; }

        public string ChequeNo { get; set; }

        public DateTime ChequeDate { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public int FailedPasswordAttemptCount { get; set; }

        public DateTime LastPasswordChangedDate { get; set; }

        public DateTime LastActivityDate { get; set; }

        public Int64 MembershipTypeId { get; set; }

        public string MembershipType { get; set; }

        public Boolean IsVolunteer { get; set; }

        public Boolean IsTeluguorigin { get; set; }

        public string Comments { get; set; }

        public string PaymentBy { get; set; }

        public string ReferredBy { get; set; }

        public DateTime InsertedTime { get; set; }

        public DateTime UpdatedTime { get; set; }

        public string UpdatedBy { get; set; }
        public string MemberAge { get; set; }
        public string MemberSkils { get; set; }
        public string SpouseSkils { get; set; }

        public MembershipOrders objMembershipOrder = new MembershipOrders();

        public ChildrenInfo objChildrenInfo = new ChildrenInfo();

        public List<ChildrenInfo> lstChildrenInfo { get; set; }

        public List<MembershipOrders> lstMembershipOrder { get; set; }

        public List<MembershipTypes> lstMembershipType { get; set; }

        public List<PaymentStatus> lstPaymentStatus { get; set; }

        public List<PaymentMethods> lstPaymentMethod { get; set; }



        #endregion
    }

    public class MembershipOrders
    {
        public Int64 MembershipOrderId { get; set; }

        public Int64 MemberId { get; set; }

        public Int64 MembershipTypeId { get; set; }

        public string MembershipType { get; set; }

        public string OrderType { get; set; }

        public Decimal Amount { get; set; }

        public string TransactionId { get; set; }

        public Int64 PaymentStatusId { get; set; }

        public Int64 PaymentMethodId { get; set; }

        public string PaymentBy { get; set; }

        public string PaymentStatus { get; set; }

        public string PaymentMethod { get; set; }

        public string AdminComment { get; set; }

        public string UserComment { get; set; }

        public string BankName { get; set; }

        public string ChequeNo { get; set; }

        public string CardNumber { get; set; }

        public string CSVMonth { get; set; }

        public string CSVYear { get; set; }

        public string Cvv { get; set; }

        public DateTime ChequeDate { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime ExpiryDate { get; set; }
        public Int32 Expiry { get; set; }

        public DateTime UpdatedTime { get; set; }

        public string UpdatedBy { get; set; }
        public bool IsVolunteer { get; set; }

    }

    public class ChildrenInfo
    {
        public Int64 ChildrenInfoId { get; set; }

        public Int64 MemberId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Relationship { get; set; }

    }

}
