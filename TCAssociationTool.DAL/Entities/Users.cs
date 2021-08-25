using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCAssociationTool.Entities
{
    public class Users
    {
        public Int64 RId { get; set; }

        public Int64 UserId { get; set; }

        public string UserName { get; set; }

        public string RoleIds { get; set; }

        public string Roles { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }        

        public string ConfirmPassword { get; set; }

        public string ProfileImage { get; set; }

        public string Designation { get; set; }

        public string MobilePhone { get; set; }

        public bool IsApproved { get; set; }

        public bool IsLockedOut { get; set; }

        public bool IsActivated { get; set; }

        public DateTime DateActivated { get; set; }

        public Guid RegistrationGUID { get; set; }

        public int FailedPasswordAttemptCount { get; set; }

        public DateTime LastPasswordChangedDate { get; set; }

        public DateTime LastLoginDate { get; set; }

        public string InsertedBy { get; set; }

        public DateTime InsertedTime { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedTime { get; set; }

        #region

        public Int64 RoleId { get; set; }

        public string RoleName { get; set; }

        #endregion
    }

    public class ChangePasswordModel
    {
        public Int64 UserId { get; set; }

        public Int64 MemberId { get; set; }

        public string Email { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string Captcha { get; set; }

        public string ReturnUrl { get; set; }
    }

    public class RegisterModel
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public bool IsApproved { get; set; }

        public bool IsLockedOut { get; set; }

        public int FailedPasswordAttemptCount { get; set; }

        public DateTime LastActivityDate { get; set; }

        public string InsertedBy { get; set; }

        public DateTime InsertedTime { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedTime { get; set; }
    }

    public class ForgotPasswordModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime DateofBirth { get; set; }

        public string Captcha { get; set; }
    }

    public class Roles
    {
        public string RoleName { get; set; }

        public Int64 RoleId { get; set; }

    }

    public class UserRoles
    {
        public string RoleName { get; set; }

        public Int64 RoleId { get; set; }

        public string RoleIds { get; set; }

        public Int64 UserId { get; set; }

    }
}
