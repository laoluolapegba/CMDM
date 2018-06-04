using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace CMdm.UI.Web.Helpers.CrossCutting.Security
{
    /// <summary>
    /// An identity object represents the user on whose behalf the code is running.
    /// </summary>
    [Serializable]
    public class CustomIdentity : IIdentity
    {
        #region Properties

        public IIdentity Identity { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int UserRoleId { get; set; }

        public string UserRoleName { get; set; }
        public string DisplayName { get; set; }

        public string BranchId { get; set; }
        public string BranchName { get; set; }

        public int ProfileId { get; set; }

        public string RegionId { get; set; }
        public string RegionName { get; set; }

        public string ZoneId { get; set; }
        public string ZoneName { get; set; }
        #endregion


        #region Implementation of IIdentity

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <returns>
        /// The name of the user on whose behalf the code is running.
        /// </returns>
        public string Name
        {
            get { return Identity.Name; }
        }

        /// <summary>
        /// Gets the type of authentication used.
        /// </summary>
        /// <returns>
        /// The type of authentication used to identify the user.
        /// </returns>
        public string AuthenticationType
        {
            get { return Identity.AuthenticationType; }
        }

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        /// <returns>
        /// true if the user was authenticated; otherwise, false.
        /// </returns>
        public bool IsAuthenticated { get { return Identity.IsAuthenticated; } }

        #endregion

        #region Constructor

        public CustomIdentity(IIdentity identity)
        {
            Identity = identity;

            var customMembershipUser = (CustomMembershipUser)Membership.GetUser(HttpContext.Current.User.Identity.Name);
            if (customMembershipUser != null)
            {
                FirstName = customMembershipUser.FirstName;
                LastName = customMembershipUser.LastName;
                Email = customMembershipUser.Email;
                UserRoleId = customMembershipUser.UserRoleId;
                UserRoleName = customMembershipUser.UserRoleName;
                DisplayName = customMembershipUser.DisplayName;
                BranchId = customMembershipUser.BranchId;
                BranchName = customMembershipUser.BranchName;
                ProfileId = customMembershipUser.ProfileId;
                RegionId = customMembershipUser.RegionId;
                RegionName = customMembershipUser.RegionName;
                ZoneId = customMembershipUser.ZoneId;
                ZoneName = customMembershipUser.ZoneName;
            }
        }

        




        #endregion
    }
}