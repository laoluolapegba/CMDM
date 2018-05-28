using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMdm.Entities.ViewModels
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
            CsoNotification = 4,
            CsmNotification = 5,
            ZsmNotification = 6,
            AmuNotification = 7,
            AmuCsmNotification = 8,
            AmuZsmNotification = 9,
            FinconNotification = 10,
            BmNotification = 11,
            HelpdeskNotification = 12,
            ZonalControlNotification = 13,
            ZonalHeadNotification = 14,
        }
    }
}