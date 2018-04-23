using CMdm.UI.Web.Models.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Services.Messaging
{
    public partial interface IMessagingService
    {
        void LogEmailJob(int userProfile, string customerNo, MessageJobEnum.MailType mailType, int? createdBy = null);
        void SendMail(List<string> address = null, string subject = null, string body = null, string from = null, string sender = null);
    }
}
