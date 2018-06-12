using CMdm.Entities.ViewModels;
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
        void SaveUserActivity(int userProfile, string activity, DateTime activityDate);
        void SendMail(List<string> address = null, string subject = null, string body = null, string from = null, string sender = null);
        string GenerateBody(Entities.Domain.Customer.CM_BACK_JOBS job);
        string GetCSOEscalationMail(string branchId);
        string GetCSMEscalationMail(string branchId);
        string GetZSMEscalationMail(string branchId);
        string GetFinconEscalationMail(string branchId);
        string GetBMEscalationMail(string branchId);
        string GetZonalHeadEscalationMail(string branchId);
        string GetZonalControlEscalationMail(string branchId);
        string GetAMUEscalationMail(string branchId);
        string GetAMUCSMEscalationMail(string branchId);
        string GetAMUZSMEscalationMail(string branchId);
        string GetHRHelpDeskEscalationMail(string branchId);
    }
}
