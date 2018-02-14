﻿using System;
using System.Web.Security;
using CMdm.Data.Rbac;


namespace CMdm.UI.Web.Helpers.CrossCutting.Security
{
    public class CustomMembershipUser : MembershipUser
    {
        #region Properties

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int UserRoleId { get; set; }

        public string UserRoleName { get; set; }

        public string DisplayName { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int ProfileId { get; set; }

        #endregion

        public CustomMembershipUser(CM_USER_PROFILE user)
            : base("CustomMembershipProvider", user.USER_ID, user.PROFILE_ID, user.EMAIL_ADDRESS, string.Empty, string.Empty, true, false,
            user.CREATED_DATE, (DateTime)user.LASTLOGINDATE, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            FirstName = user.FIRSTNAME;
            LastName = user.LASTNAME;
            UserRoleId = (int)user.ROLE_ID;
            UserRoleName = user.CM_USER_ROLES.ROLE_NAME;
            DisplayName = user.DISPLAY_NAME;
            ProfileId = (int)user.PROFILE_ID;
            BranchId = (int)user.BRANCH_ID;
            BranchName = user.CM_BRANCH.BRANCH_NAME;

        }

    }
}