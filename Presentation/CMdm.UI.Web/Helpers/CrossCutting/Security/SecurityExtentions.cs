using System.Security.Principal;

namespace CMdm.UI.Web.Helpers.CrossCutting.Security
{
    public static class SecurityExtentions
    {
        public static CustomPrincipal ToCustomPrincipal(this IPrincipal principal)
        {
            return (CustomPrincipal)principal;
        }
    }
}