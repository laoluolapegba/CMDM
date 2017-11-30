using System;
using System.Security.Principal;

namespace CMdm.UI.Web.Helpers.CrossCutting.Security
{
    [Serializable]
    public class CustomPrincipal : IPrincipal
    {
        #region Implementation of IPrincipal

        /// <summary>
        /// Determines whether the current principal belongs to the specified role.
        /// </summary>
        /// <returns>
        /// true if the current principal is a member of the specified role; otherwise, false.
        /// </returns>
        /// <param name="role">The name of the role for which to check membership. </param>
        public bool IsInRole(string role)
        {
            return Identity is CustomIdentity &&
                   string.Compare(role, ((CustomIdentity)Identity).UserRoleName, StringComparison.CurrentCultureIgnoreCase) == 0;
        }

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Security.Principal.IIdentity"/> object associated with the current principal.
        /// </returns>
        public IIdentity Identity { get; private set; }

        #endregion

        public CustomIdentity CustomIdentity { get { return (CustomIdentity)Identity; } set { Identity = value; } }

        public CustomPrincipal(CustomIdentity identity)
        {
            Identity = identity;
        }
    }
}