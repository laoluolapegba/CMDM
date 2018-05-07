using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMdm.UI.Web.Models.Messaging
{
    public class MessageJobEnum
    {
        public enum JobType
        {
            SendMail = 1,
            PrepareMail = 2
        }
        public enum MailType
        {
            Authorize = 1,
            Change = 2,
            Reject = 3,
            PhoneValidation = 4,
            OutstandingDocuments = 5,
            AccountOfficer = 6,
            SectorSubsector = 7,
        }
    }
}