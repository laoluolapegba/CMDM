using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using CMdm.Data;
using CMdm.Entities.Domain.Customer;
using CMdm.UI.Web.Models.Messaging;
using System.Web.WebPages.Razor.Configuration;
using RazorEngine;
using RazorEngine.Configuration.Xml;
using RazorEngine.Templating;
using System.Configuration;
using System.Web.Configuration;

namespace CMdm.Services.Messaging
{
    public class MessagingService : IMessagingService
    {
        private AppDbContext db;
        private TemplateService _templateService;
        static readonly string TemplateFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Views", "MessagingTemplates");
        static bool mailSent = false;

        public MessagingService()
        {
            _templateService = new TemplateService();
            db = new AppDbContext();
        }

        public void LogEmailJob(int userProfile, string customerNo, MessageJobEnum.MailType mailType, int? createdBy = null)
        {
            string mailTemplate = null;
            string mailSubject = null;
            int branchId = Convert.ToInt32(GetBranchIdbyProdileId(userProfile));

            List<string> recepientNames = new List<string>();
            CM_BACK_JOBS backJob = new CM_BACK_JOBS();

            switch (mailType)
            {
                case MessageJobEnum.MailType.Authorize:
                    mailTemplate = "Authorize.cshtml";
                    mailSubject = "A new Customer information has been authorized";
                    recepientNames[0] = GetUserNamebyUserId(createdBy);
                    break;
                case MessageJobEnum.MailType.Change:
                    mailTemplate = "Change.cshtml";
                    mailSubject = "A new Customer information has been changed";
                    recepientNames = GetChekersbyMakerRole(userProfile);
                    break;
                case MessageJobEnum.MailType.Reject:
                    mailTemplate = "Reject.cshtml";
                    mailSubject = "A new Customer information has been Rejected";
                    recepientNames[0] = GetUserNamebyUserId(createdBy);
                    break;
                default:
                    mailTemplate = "Change.cshtml";
                    recepientNames[0] = "";
                    break;
            }

            if(recepientNames.Count == 1)
            {
                backJob = new CM_BACK_JOBS
                {
                    CUSTOMER_NO = customerNo,
                    JOB_TYPE = 2,
                    FROM_EMAIL = GetUserNamebyUserId(userProfile),
                    DATE_LOGGED = DateTime.Now,
                    CREATED_BY = userProfile.ToString(),
                    RCPT_EMAIL = recepientNames.SingleOrDefault(),
                    FLG_STATUS = 1,
                    DATE_SENT = DateTime.Now,
                    COUNT_RETRIES = 0,
                    MSG_SUBJECT = mailSubject,
                    BRANCH_ID = branchId,
                    RECIPIENTNAME = recepientNames.SingleOrDefault(),
                    REQUIREDDATE = DateTime.Now,
                    MAILBODY = null,
                    MAILBODY_2 = null,
                    MAILTYPE = (int)mailType,
                    MAIL_TEMPLATE = mailTemplate,
                    USERFULLNAME = GetUserFullNamebyProdileId(userProfile),
                    BRANCHNAME = GetBranchName(branchId.ToString())
                };
            }
            else if(recepientNames.Count > 1)
            {
                for(int i = 0; i < recepientNames.Count; i++)
                {
                    backJob = new CM_BACK_JOBS
                    {
                        JOB_TYPE = 2,
                        FROM_EMAIL = GetUserNamebyUserId(userProfile),
                        DATE_LOGGED = DateTime.Now,
                        CREATED_BY = userProfile.ToString(),
                        RCPT_EMAIL = recepientNames.SingleOrDefault(),
                        FLG_STATUS = 1,
                        DATE_SENT = DateTime.Now,
                        COUNT_RETRIES = 0,
                        MSG_SUBJECT = mailSubject,
                        BRANCH_ID = branchId,
                        RECIPIENTNAME = recepientNames[i],
                        REQUIREDDATE = DateTime.Now,
                        MAILBODY = null,
                        MAILBODY_2 = null,
                        MAILTYPE = (int)mailType,
                        MAIL_TEMPLATE = mailTemplate,
                        USERFULLNAME = GetUserFullNamebyProdileId(userProfile),
                        BRANCHNAME = GetBranchName(branchId.ToString())
                    };
                }
            }

            db.CM_BACK_JOBS.Add(backJob);
            db.SaveChanges();
            //SendMail(backJob.RECIPIENTNAME, mailSubject, backJob.MAILBODY + backJob.MAILBODY_2, backJob.FROM_EMAIL, GetUserFullNamebyProdileId(userProfile));
        }
        public void SendMail(string address, string subject, string body, string from, string sender)
        {
            try
            {
                MailMessage mail = new MailMessage();

                //Setting From , To and CC
                mail.Subject = subject;
                mail.From = new MailAddress(from, sender);
                mail.To.Add(new MailAddress(address));
                mail.Body = body;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                var networkCred = new System.Net.NetworkCredential("sirlamlanre", "my_password", "domaiin");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = networkCred;
                smtp.Port = 465;

                System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                (s, cert, chain, sslPolicyErrors) => true;
                smtp.Send(mail);
            }
            catch (SmtpException ste)
            {
                Console.WriteLine(ste.ToString());
            }

        }
        public string GenerateBody(CM_BACK_JOBS job)
        {
            string htmlBody = string.Empty;
            switch (job.MAILTYPE)
            {
                case (int)MessageJobEnum.MailType.Authorize:
                    var auth_customer = db.CM_BACK_JOBS.FirstOrDefault(o => o.CUSTOMER_NO == job.CUSTOMER_NO);
                    // start template 
                    var auth_model = new MessageModel()
                    {
                        CUSTOMER_NO = auth_customer.CUSTOMER_NO,
                        RequiredDate = Convert.ToDateTime(auth_customer.REQUIREDDATE),
                        UserID = job.USERFULLNAME,
                        BRANCHNAME = job.BRANCHNAME,
                        RCPT_EMAIL = job.RCPT_EMAIL,
                        RECIPIENTNAME = job.RECIPIENTNAME
                    };

                    var authEmailTemplate = File.ReadAllText(Path.Combine(TemplateFolderPath, job.MAIL_TEMPLATE));

                    AddDefaultNamespacesFromWebConfig(_templateService);
                    htmlBody = _templateService.Parse(authEmailTemplate, auth_model, null, "AuthNotifyApprover");
                    //end template
                    break;
                case (int)MessageJobEnum.MailType.Change:
                    var change_customer = db.CM_BACK_JOBS.FirstOrDefault(o => o.CUSTOMER_NO == job.CUSTOMER_NO);
                    // start template 
                    var change_model = new MessageModel()
                    {
                        CUSTOMER_NO = change_customer.CUSTOMER_NO,
                        RequiredDate = Convert.ToDateTime(change_customer.REQUIREDDATE),
                        UserID = job.USERFULLNAME,
                        BRANCHNAME = job.BRANCHNAME,
                        RCPT_EMAIL = job.RCPT_EMAIL,
                        RECIPIENTNAME = job.RECIPIENTNAME
                    };

                    var changeEmailTemplate = File.ReadAllText(Path.Combine(TemplateFolderPath, job.MAIL_TEMPLATE));

                    AddDefaultNamespacesFromWebConfig(_templateService);
                    htmlBody = _templateService.Parse(changeEmailTemplate, change_model, null, "ChangeNotifyApprover");
                    //end template
                    break;
                case (int)MessageJobEnum.MailType.Reject:
                    var reject_customer = db.CM_BACK_JOBS.FirstOrDefault(o => o.CUSTOMER_NO == job.CUSTOMER_NO);
                    // start template 
                    var reject_model = new MessageModel()
                    {
                        CUSTOMER_NO = reject_customer.CUSTOMER_NO,
                        RequiredDate = Convert.ToDateTime(reject_customer.REQUIREDDATE),
                        UserID = job.USERFULLNAME,
                        BRANCHNAME = job.BRANCHNAME,
                        RCPT_EMAIL = job.RCPT_EMAIL,
                        RECIPIENTNAME = job.RECIPIENTNAME
                    };

                    var rejectEmailTemplate = File.ReadAllText(Path.Combine(TemplateFolderPath, job.MAIL_TEMPLATE));

                    AddDefaultNamespacesFromWebConfig(_templateService);
                    htmlBody = _templateService.Parse(rejectEmailTemplate, reject_model, null, "ChangeNotifyApprover");
                    //end template
                    break;
                default:
                    break;
            }
            return

        htmlBody;
        }
        private string GetBranchName(string BranchId)
        {
            return
            (
             from p in db.CM_BRANCH
             where p.BRANCH_ID == BranchId
             select p.BRANCH_NAME
            ).SingleOrDefault();

        }
        private string GetUserFullNamebyProdileId(decimal userid)
        {
            return
            (
             from p in db.CM_USER_PROFILE
             where p.PROFILE_ID == userid
             select p.FIRSTNAME + " " + p.LASTNAME
            ).SingleOrDefault();
        }
        public string GetUserNamebyUserId(int? userid)
        {
            return
            (
             from p in db.CM_USER_PROFILE
             where p.PROFILE_ID == userid
             select p.USER_ID
            ).SingleOrDefault();
        }
        public List<string> GetChekersbyMakerRole(int userid)
        {
            var makerRole = (from p in db.CM_USER_PROFILE
                            where p.PROFILE_ID == userid
                            select p.ROLE_ID).SingleOrDefault();

            var checkerRole = (from q in db.CM_USER_ROLES
                               where q.ROLE_ID == makerRole
                               select q.CHECKER_ID).SingleOrDefault();

            List<string> checkers = (from r in db.CM_USER_PROFILE
                                     where r.ROLE_ID == checkerRole
                                     select r.USER_ID).ToList();

            return checkers;
        }
        public string GetUserFullNamebyUserId(string userid)
        {
            return
            (
             from p in db.CM_USER_PROFILE
             where p.USER_ID == userid
             select p.FIRSTNAME + " " + p.LASTNAME
            ).SingleOrDefault();
        }
        private string GetBranchIdbyProdileId(int userid)
        {
            return
            (
             from p in db.CM_USER_PROFILE
             where p.PROFILE_ID == userid
             select p.BRANCH_ID
            ).SingleOrDefault();
        }
        private static void AddDefaultNamespacesFromWebConfig(TemplateService templateService)
        {
            var webConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Web.config");
            if (!File.Exists(webConfigPath))
                return;

            var fileMap = new ExeConfigurationFileMap() { ExeConfigFilename = webConfigPath };
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var razorConfig = configuration.GetSection("system.web.webPages.razor/pages") as RazorPagesSection;

            if (razorConfig == null)
                return;

            foreach (NamespaceInfo namespaceInfo in razorConfig.Namespaces)
            {
                templateService.AddNamespace(namespaceInfo.Namespace);
            }
        }
    }
}

