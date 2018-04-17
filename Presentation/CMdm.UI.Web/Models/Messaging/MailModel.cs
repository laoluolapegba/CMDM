using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CMdm.UI.Web.Models.Messaging
{
    public class MailModel
    {
        [DisplayName("To:")]
        public List<string> ToEmail { get; set; }
        [DisplayName("Subject:")]
        public string Subject { get; set; }
        [DisplayName("Body:")]
        public string BodyEmail { get; set; }
    }
}