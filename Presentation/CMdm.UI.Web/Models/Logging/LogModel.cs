using CMdm.Framework.Mvc;
using System;
using System.ComponentModel;
using System.Web.Mvc;


namespace CMdm.UI.Web.Models.Logging
{
    public partial class LogModel
    {
        public int Id { get; set; }
        [DisplayName("LogLevel")]
        public string LogLevel { get; set; }

        [DisplayName("ShortMessage")]
        [AllowHtml]
        public string ShortMessage { get; set; }

        [DisplayName("FullMessage")]
        [AllowHtml]
        public string FullMessage { get; set; }

        [DisplayName("IPAddress")]
        [AllowHtml]
        public string IpAddress { get; set; }

        [DisplayName("Customer")]
        public string CustomerId { get; set; }
        [DisplayName("Customer")]
        public string CustomerEmail { get; set; }

        [DisplayName("PageURL")]
        [AllowHtml]
        public string PageUrl { get; set; }

        [DisplayName("ReferrerURL")]
        [AllowHtml]
        public string ReferrerUrl { get; set; }

        [DisplayName("CreatedOn")]
        public DateTime CreatedOn { get; set; }
    }
}