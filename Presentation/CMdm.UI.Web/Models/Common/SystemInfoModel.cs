using CMdm.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CMdm.UI.Web.Models.Common
{
    public partial class SystemInfoModel : BaseModel
    {
        public SystemInfoModel()
        {
            this.ServerVariables = new List<ServerVariableModel>();
            this.LoadedAssemblies = new List<LoadedAssembly>();
        }

        [DisplayName("ASPNETInfo")]
        public string AspNetInfo { get; set; }

        [DisplayName("IsFullTrust")]
        public string IsFullTrust { get; set; }

        [DisplayName("CMDM Version")]
        public string Version { get; set; }

        [DisplayName("OperatingSystem")]
        public string OperatingSystem { get; set; }

        [DisplayName("ServerLocalTime")]
        public DateTime ServerLocalTime { get; set; }

        [DisplayName("ServerTimeZone")]
        public string ServerTimeZone { get; set; }

        [DisplayName("UTCTime")]
        public DateTime UtcTime { get; set; }

        [DisplayName("CurrentUserTime")]
        public DateTime CurrentUserTime { get; set; }

        [DisplayName("HTTPHOST")]
        public string HttpHost { get; set; }

        [DisplayName("ServerVariables")]
        public IList<ServerVariableModel> ServerVariables { get; set; }

        [DisplayName("LoadedAssemblies")]
        public IList<LoadedAssembly> LoadedAssemblies { get; set; }

        public partial class ServerVariableModel : BaseModel
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        public partial class LoadedAssembly : BaseModel
        {
            public string FullName { get; set; }
            public string Location { get; set; }
            public bool IsDebug { get; set; }
            public DateTime? BuildDate { get; set; }
        }
    }
}