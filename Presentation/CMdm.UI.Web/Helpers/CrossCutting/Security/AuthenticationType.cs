using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMdm.UI.Web.Helpers.CrossCutting.Security
{
    /// <summary>
    /// Represents an Authentication Mode
    /// </summary>
    public enum AuthenticationType
    {
        LDAP = 1,
        OpenAuth = 2,
        ApplicationAuth = 0,
        TwoFactor = 3
    }
   
}