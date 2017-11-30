using System.Linq;

namespace CMdm.UI.Web.Helpers.CrossCutting.Security
{
    public class UserRoleAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public UserRoleAuthorizeAttribute() { }

        public UserRoleAuthorizeAttribute(params UserRole[] roles)
        {
            Roles = string.Join(",", roles.Select(r => r.ToString()));
        }
    }

    public enum UserRole
    {
        Administrator = 1,
        User = 2
    }
}