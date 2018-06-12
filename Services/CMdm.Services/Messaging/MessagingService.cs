using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using CMdm.Data;
using CMdm.Entities.Domain.Customer;
using CMdm.Entities.ViewModels;
using System.Web.WebPages.Razor.Configuration;
using RazorEngine;
using RazorEngine.Configuration.Xml;
using RazorEngine.Templating;
using System.Configuration;
using System.Web.Configuration;
using System.Security.Cryptography;

namespace CMdm.Services.Messaging
{
    public class MessagingService : IMessagingService
    {
        private AppDbContext db;
        private TemplateService _templateService;
        static readonly string TemplateFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Views", "MessagingTemplates");
        static readonly string BackTemplateFolderPath = Path.Combine("C:\\Users\\salam\\Documents\\Visual Studio 2017\\Projects\\CMDM\\Presentation\\CMdm.UI.Web\\", "Views", "MessagingTemplates");
        static bool mailSent = false;
        private SymmetricAlgorithm mobjCryptoService;

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
            
            string htmlbody = GenerateBody(backJob);
            backJob.MAILBODY = htmlbody;
            db.CM_BACK_JOBS.Add(backJob);
            db.SaveChanges();
            SendMail(recepientNames, mailSubject, htmlbody, backJob.FROM_EMAIL, GetUserFullNamebyProdileId(userProfile));
        }

        public void SaveUserActivity(int userProfile, string activity, DateTime activityDate)
        {
            string username = GetUserNamebyUserId(userProfile);
            string fullname = GetUserFullNamebyProdileId(userProfile);
            string desc = username + " " + activity + " on " + activityDate;
            string branchCode = GetBranchIdbyProdileId(userProfile);
            string branchName = GetBranchName(branchCode);

            CMDM_ACTIVITY_LOG activity_log = new CMDM_ACTIVITY_LOG();

            try
            {
                activity_log = new CMDM_ACTIVITY_LOG
                {
                    USER_ID = userProfile,
                    USER_NAME = username,
                    FULLNAME = fullname,
                    BRANCH_CODE = branchCode,
                    BRANCH_NAME = branchName,
                    ACTIVITY_DESC = desc,
                    ACTIVITY_DATE = activityDate
                };

                db.CMDM_ACTIVITY_LOG.Add(activity_log);
                db.SaveChanges();
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
        }

        public void SendMail(List<string> addresses, string subject, string body, string from, string sender)
        {
            string smtpHost = db.Settings.Where(a => a.SETTING_NAME == "SMTP_HOST").Select(a => a.SETTING_VALUE).FirstOrDefault();
            string smtpMail = db.Settings.Where(a => a.SETTING_NAME == "SMTP_MAIL").Select(a => a.SETTING_VALUE).FirstOrDefault();
            string smtpKey = db.Settings.Where(a => a.SETTING_NAME == "SMTP_KEY").Select(a => a.SETTING_VALUE).FirstOrDefault();
            string initPassword = db.Settings.Where(a => a.SETTING_NAME == "SMTP_PASS").Select(a => a.SETTING_VALUE).FirstOrDefault();
            string smtpPort = db.Settings.Where(a => a.SETTING_NAME == "SMTP_PORT").Select(a => a.SETTING_VALUE).FirstOrDefault();
            string smtpDomain = db.Settings.Where(a => a.SETTING_NAME == "SMTP_DOMAIN").Select(a => a.SETTING_VALUE).FirstOrDefault();
            string smtpDomainGroup = db.Settings.Where(a => a.SETTING_NAME == "SMTP_DOMAIN").Select(a => a.SETTING_VALUE).FirstOrDefault();//Needed if mails are attached to a group

            EncryptString enc = new EncryptString(System.Security.Cryptography.SymmetricAlgorithm.Create());  //System.Security.Cryptography.RC2
            string test = enc.Encrypt("s.lanre(02)", smtpKey);
            string smtpPassword = enc.Decrypt(initPassword, smtpKey);

            try
            {
                MailMessage mail = new MailMessage();

                //Setting From , To and CC
                mail.Subject = subject;
                mail.From = new MailAddress(from+"@" + smtpDomain, sender);

                foreach(var address in addresses)
                {
                    mail.To.Add(new MailAddress(address + "@" + smtpDomain));
                }                    

                mail.Body = body;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient();
                smtp.Host = smtpHost;
                smtp.EnableSsl = false; //Can be sent to true if authentication is needed
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                var networkCred = new System.Net.NetworkCredential(smtpMail, smtpPassword /*smtp domain if needed*/);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = networkCred;
                smtp.Port = Convert.ToInt32(smtpPort);

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
                    htmlBody = _templateService.Parse(rejectEmailTemplate, reject_model, null, "RejectNotifyApprover");
                    //end template
                    break;
                case (int)MessageJobEnum.MailType.CsoNotification:
                    var cso_customer = db.CM_BACK_JOBS.FirstOrDefault(o => o.JOBID == job.JOBID);
                    // start template 
                    var cso_model = new MessageModel()
                    {
                        CUSTOMER_NO = cso_customer.CUSTOMER_NO,
                        RequiredDate = Convert.ToDateTime(cso_customer.REQUIREDDATE),
                        UserID = job.USERFULLNAME,
                        BRANCH_ID = job.BRANCH_ID,
                        BRANCHNAME = job.BRANCHNAME,
                        RCPT_EMAIL = job.RCPT_EMAIL,
                        RECIPIENTNAME = job.RECIPIENTNAME,
                        OUTSTANDING_DOCS = job.OUTSTANDING_DOCS,
                        MULTIPLE_AO_CODES = job.MULTIPLE_AO_CODES,
                        PHONE_NUMBER_VAL = job.PHONE_NUMBER_VAL,
                        ACCOUNT_OFFICER = job.ACCOUNT_OFFICER,
                        WRONG_SECTOR = job.WRONG_SECTOR,
                        SEGMENT_MAPPING = job.SEGMENT_MAPPING,
                        WRONG_SCHEME_CODES = job.WRONG_SCHEME_CODES
                    };

                    var csoEmailTemplate = File.ReadAllText(Path.Combine(BackTemplateFolderPath, job.MAIL_TEMPLATE));

                    AddDefaultNamespacesFromWebConfig(_templateService);
                    htmlBody = _templateService.Parse(csoEmailTemplate, cso_model, null, "NotifyCSO");
                    //end template
                    break;
                case (int)MessageJobEnum.MailType.CsmNotification:
                    var csm_customer = db.CM_BACK_JOBS.FirstOrDefault(o => o.JOBID == job.JOBID);
                    // start template 
                    var csm_model = new MessageModel()
                    {
                        CUSTOMER_NO = csm_customer.CUSTOMER_NO,
                        RequiredDate = Convert.ToDateTime(csm_customer.REQUIREDDATE),
                        UserID = job.USERFULLNAME,
                        BRANCH_ID = job.BRANCH_ID,
                        BRANCHNAME = job.BRANCHNAME,
                        RCPT_EMAIL = job.RCPT_EMAIL,
                        RECIPIENTNAME = job.RECIPIENTNAME,
                        OUTSTANDING_DOCS = job.OUTSTANDING_DOCS,
                        MULTIPLE_AO_CODES = job.MULTIPLE_AO_CODES,
                        PHONE_NUMBER_VAL = job.PHONE_NUMBER_VAL,
                        ACCOUNT_OFFICER = job.ACCOUNT_OFFICER,
                        WRONG_SECTOR = job.WRONG_SECTOR,
                        SEGMENT_MAPPING = job.SEGMENT_MAPPING,
                        WRONG_SCHEME_CODES = job.WRONG_SCHEME_CODES
                    };

                    var csmEmailTemplate = File.ReadAllText(Path.Combine(BackTemplateFolderPath, job.MAIL_TEMPLATE));

                    AddDefaultNamespacesFromWebConfig(_templateService);
                    htmlBody = _templateService.Parse(csmEmailTemplate, csm_model, null, "NotifyCsm");
                    //end template
                    break;
                case (int)MessageJobEnum.MailType.ZsmNotification:
                    var zsm_customer = db.CM_BACK_JOBS.FirstOrDefault(o => o.JOBID == job.JOBID);
                    // start template 
                    var zsm_model = new MessageModel()
                    {
                        CUSTOMER_NO = zsm_customer.CUSTOMER_NO,
                        RequiredDate = Convert.ToDateTime(zsm_customer.REQUIREDDATE),
                        UserID = job.USERFULLNAME,
                        BRANCH_ID = job.BRANCH_ID,
                        BRANCHNAME = job.BRANCHNAME,
                        RCPT_EMAIL = job.RCPT_EMAIL,
                        RECIPIENTNAME = job.RECIPIENTNAME,
                        OUTSTANDING_DOCS = job.OUTSTANDING_DOCS,
                        MULTIPLE_AO_CODES = job.MULTIPLE_AO_CODES,
                        PHONE_NUMBER_VAL = job.PHONE_NUMBER_VAL,
                        ACCOUNT_OFFICER = job.ACCOUNT_OFFICER,
                        WRONG_SECTOR = job.WRONG_SECTOR,
                        SEGMENT_MAPPING = job.SEGMENT_MAPPING,
                        WRONG_SCHEME_CODES = job.WRONG_SCHEME_CODES
                    };

                    var zsmEmailTemplate = File.ReadAllText(Path.Combine(BackTemplateFolderPath, job.MAIL_TEMPLATE));

                    AddDefaultNamespacesFromWebConfig(_templateService);
                    htmlBody = _templateService.Parse(zsmEmailTemplate, zsm_model, null, "NotifyZsm");
                    //end template
                    break;
                case (int)MessageJobEnum.MailType.AmuNotification:
                    var amu_customer = db.CM_BACK_JOBS.FirstOrDefault(o => o.JOBID == job.JOBID);
                    // start template 
                    var amu_model = new MessageModel()
                    {
                        CUSTOMER_NO = amu_customer.CUSTOMER_NO,
                        RequiredDate = Convert.ToDateTime(amu_customer.REQUIREDDATE),
                        UserID = job.USERFULLNAME,
                        BRANCH_ID = job.BRANCH_ID,
                        BRANCHNAME = job.BRANCHNAME,
                        RCPT_EMAIL = job.RCPT_EMAIL,
                        RECIPIENTNAME = job.RECIPIENTNAME,
                        MULTIPLE_ID = job.MULTIPLE_ID
                    };

                    var amuEmailTemplate = File.ReadAllText(Path.Combine(BackTemplateFolderPath, job.MAIL_TEMPLATE));

                    AddDefaultNamespacesFromWebConfig(_templateService);
                    htmlBody = _templateService.Parse(amuEmailTemplate, amu_model, null, "NotifyAmu");
                    //end template
                    break;
                case (int)MessageJobEnum.MailType.AmuCsmNotification:
                    var amuCsm_customer = db.CM_BACK_JOBS.FirstOrDefault(o => o.JOBID == job.JOBID);
                    // start template 
                    var amuCsm_model = new MessageModel()
                    {
                        CUSTOMER_NO = amuCsm_customer.CUSTOMER_NO,
                        RequiredDate = Convert.ToDateTime(amuCsm_customer.REQUIREDDATE),
                        UserID = job.USERFULLNAME,
                        BRANCH_ID = job.BRANCH_ID,
                        BRANCHNAME = job.BRANCHNAME,
                        RCPT_EMAIL = job.RCPT_EMAIL,
                        RECIPIENTNAME = job.RECIPIENTNAME,
                        MULTIPLE_ID = job.MULTIPLE_ID
                    };

                    var amuCsmEmailTemplate = File.ReadAllText(Path.Combine(BackTemplateFolderPath, job.MAIL_TEMPLATE));

                    AddDefaultNamespacesFromWebConfig(_templateService);
                    htmlBody = _templateService.Parse(amuCsmEmailTemplate, amuCsm_model, null, "NotifyAmuCsm");
                    //end template
                    break;
                case (int)MessageJobEnum.MailType.AmuZsmNotification:
                    var amuZsm_customer = db.CM_BACK_JOBS.FirstOrDefault(o => o.JOBID == job.JOBID);
                    // start template 
                    var amuZsm_model = new MessageModel()
                    {
                        CUSTOMER_NO = amuZsm_customer.CUSTOMER_NO,
                        RequiredDate = Convert.ToDateTime(amuZsm_customer.REQUIREDDATE),
                        UserID = job.USERFULLNAME,
                        BRANCH_ID = job.BRANCH_ID,
                        BRANCHNAME = job.BRANCHNAME,
                        RCPT_EMAIL = job.RCPT_EMAIL,
                        RECIPIENTNAME = job.RECIPIENTNAME,
                        MULTIPLE_ID = job.MULTIPLE_ID
                    };

                    var amuZsmEmailTemplate = File.ReadAllText(Path.Combine(BackTemplateFolderPath, job.MAIL_TEMPLATE));

                    AddDefaultNamespacesFromWebConfig(_templateService);
                    htmlBody = _templateService.Parse(amuZsmEmailTemplate, amuZsm_model, null, "NotifyAmuZsm");
                    //end template
                    break;
                case (int)MessageJobEnum.MailType.FinconNotification:
                    var fincon_customer = db.CM_BACK_JOBS.FirstOrDefault(o => o.JOBID == job.JOBID);
                    // start template 
                    var fincon_model = new MessageModel()
                    {
                        CUSTOMER_NO = fincon_customer.CUSTOMER_NO,
                        RequiredDate = Convert.ToDateTime(fincon_customer.REQUIREDDATE),
                        UserID = job.USERFULLNAME,
                        BRANCH_ID = job.BRANCH_ID,
                        BRANCHNAME = job.BRANCHNAME,
                        RCPT_EMAIL = job.RCPT_EMAIL,
                        RECIPIENTNAME = job.RECIPIENTNAME,
                        MULTIPLE_AO_CODES = job.MULTIPLE_AO_CODES
                    };

                    var finconEmailTemplate = File.ReadAllText(Path.Combine(BackTemplateFolderPath, job.MAIL_TEMPLATE));

                    AddDefaultNamespacesFromWebConfig(_templateService);
                    htmlBody = _templateService.Parse(finconEmailTemplate, fincon_model, null, "NotifyFincon");
                    //end template
                    break;
                case (int)MessageJobEnum.MailType.BmNotification:
                    var bm_customer = db.CM_BACK_JOBS.FirstOrDefault(o => o.JOBID == job.JOBID);
                    // start template 
                    var bm_model = new MessageModel()
                    {
                        CUSTOMER_NO = bm_customer.CUSTOMER_NO,
                        RequiredDate = Convert.ToDateTime(bm_customer.REQUIREDDATE),
                        UserID = job.USERFULLNAME,
                        BRANCH_ID = job.BRANCH_ID,
                        BRANCHNAME = job.BRANCHNAME,
                        RCPT_EMAIL = job.RCPT_EMAIL,
                        RECIPIENTNAME = job.RECIPIENTNAME,
                        MULTIPLE_AO_CODES = job.MULTIPLE_AO_CODES
                    };

                    var bmEmailTemplate = File.ReadAllText(Path.Combine(BackTemplateFolderPath, job.MAIL_TEMPLATE));

                    AddDefaultNamespacesFromWebConfig(_templateService);
                    htmlBody = _templateService.Parse(bmEmailTemplate, bm_model, null, "NotifyBm");
                    //end template
                    break;
                case (int)MessageJobEnum.MailType.ZonalControlNotification:
                    var zonal_control_customer = db.CM_BACK_JOBS.FirstOrDefault(o => o.JOBID == job.JOBID);
                    // start template 
                    var zonal_control_model = new MessageModel()
                    {
                        CUSTOMER_NO = zonal_control_customer.CUSTOMER_NO,
                        RequiredDate = Convert.ToDateTime(zonal_control_customer.REQUIREDDATE),
                        UserID = job.USERFULLNAME,
                        BRANCH_ID = job.BRANCH_ID,
                        BRANCHNAME = job.BRANCHNAME,
                        RCPT_EMAIL = job.RCPT_EMAIL,
                        RECIPIENTNAME = job.RECIPIENTNAME,
                        OUTSTANDING_DOCS = job.OUTSTANDING_DOCS,
                        PHONE_NUMBER_VAL = job.PHONE_NUMBER_VAL,
                        ACCOUNT_OFFICER = job.ACCOUNT_OFFICER,
                        WRONG_SECTOR = job.WRONG_SECTOR,
                        SEGMENT_MAPPING = job.SEGMENT_MAPPING,
                        WRONG_SCHEME_CODES = job.WRONG_SCHEME_CODES
                    };

                    var zonalControlEmailTemplate = File.ReadAllText(Path.Combine(BackTemplateFolderPath, job.MAIL_TEMPLATE));

                    AddDefaultNamespacesFromWebConfig(_templateService);
                    htmlBody = _templateService.Parse(zonalControlEmailTemplate, zonal_control_model, null, "NotifyZonalControl");
                    //end template
                    break;
                case (int)MessageJobEnum.MailType.ZonalHeadNotification:
                    var zonal_head_customer = db.CM_BACK_JOBS.FirstOrDefault(o => o.JOBID == job.JOBID);
                    // start template 
                    var zonal_head_model = new MessageModel()
                    {
                        CUSTOMER_NO = zonal_head_customer.CUSTOMER_NO,
                        RequiredDate = Convert.ToDateTime(zonal_head_customer.REQUIREDDATE),
                        UserID = job.USERFULLNAME,
                        BRANCH_ID = job.BRANCH_ID,
                        BRANCHNAME = job.BRANCHNAME,
                        RCPT_EMAIL = job.RCPT_EMAIL,
                        RECIPIENTNAME = job.RECIPIENTNAME,
                        MULTIPLE_AO_CODES = job.MULTIPLE_AO_CODES
                    };

                    var zonalHeadEmailTemplate = File.ReadAllText(Path.Combine(BackTemplateFolderPath, job.MAIL_TEMPLATE));

                    AddDefaultNamespacesFromWebConfig(_templateService);
                    htmlBody = _templateService.Parse(zonalHeadEmailTemplate, zonal_head_model, null, "NotifyZonalHead");
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
        public string GetCSOEscalationMail(string branchId)
        {
            return (
                from p in db.CMDM_BRANCH_MAIL
                where p.BRANCH_ID == branchId
                select p.CSO_MAIL
                ).SingleOrDefault();
        }
        public string GetCSMEscalationMail(string branchId)
        {
            return (
                from p in db.CMDM_BRANCH_MAIL
                where p.BRANCH_ID == branchId
                select p.CSM_MAIL
                ).SingleOrDefault();
        }
        public string GetZSMEscalationMail(string branchId)
        {
            return (
                from p in db.CMDM_BRANCH_MAIL
                where p.BRANCH_ID == branchId
                select p.ZSM_MAIL
                ).SingleOrDefault();
        }
        public string GetFinconEscalationMail(string branchId)
        {
            return (
                from p in db.CMDM_BRANCH_MAIL
                where p.BRANCH_ID == branchId
                select p.FINCON_MAIL
                ).SingleOrDefault();
        }
        public string GetBMEscalationMail(string branchId)
        {
            return (
                from p in db.CMDM_BRANCH_MAIL
                where p.BRANCH_ID == branchId
                select p.BM_MAIL
                ).SingleOrDefault();
        }
        public string GetZonalHeadEscalationMail(string branchId)
        {
            return (
                from p in db.CMDM_BRANCH_MAIL
                where p.BRANCH_ID == branchId
                select p.ZONALHEAD_MAIL
                ).SingleOrDefault();
        }
        public string GetZonalControlEscalationMail(string branchId)
        {
            return (
                from p in db.CMDM_BRANCH_MAIL
                where p.BRANCH_ID == branchId
                select p.ZONALCONTROLHEAD_MAIL
                ).SingleOrDefault();
        }
        public string GetAMUEscalationMail(string branchId)
        {
            return (
                from p in db.CMDM_BRANCH_MAIL
                where p.BRANCH_ID == branchId
                select p.AMU_MAIL
                ).SingleOrDefault();
        }
        public string GetAMUCSMEscalationMail(string branchId)
        {
            return (
                from p in db.CMDM_BRANCH_MAIL
                where p.BRANCH_ID == branchId
                select p.AMUCSM_MAIL
                ).SingleOrDefault();
        }
        public string GetAMUZSMEscalationMail(string branchId)
        {
            return (
                from p in db.CMDM_BRANCH_MAIL
                where p.BRANCH_ID == branchId
                select p.AMUZSM_MAIL
                ).SingleOrDefault();
        }
        public string GetHRHelpDeskEscalationMail(string branchId)
        {
            return (
                from p in db.CMDM_BRANCH_MAIL
                where p.BRANCH_ID == branchId
                select p.HRHELPDESK_MAIL
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

    public class EncryptString
    {

        /// <summary>
        /// SymmCrypto is a wrapper of System.Security.Cryptography.SymmetricAlgorithm classes
        /// and simplifies the interface. It supports customized SymmetricAlgorithm as well.
        /// </summary>
        /// <remarks>

        /// Supported .Net intrinsic SymmetricAlgorithm classes.
        /// </remarks>

        public enum SymmProvEnum : int
        {
            DES, RC2, Rijndael
        }

        private SymmetricAlgorithm mobjCryptoService;

        /// <remarks>

        /// Constructor for using an intrinsic .Net SymmetricAlgorithm class.

        /// </remarks>

        public EncryptString(SymmProvEnum NetSelected)
        {
            switch (NetSelected)
            {
                case SymmProvEnum.DES:
                    mobjCryptoService = new DESCryptoServiceProvider();
                    break;
                case SymmProvEnum.RC2:
                    mobjCryptoService = new RC2CryptoServiceProvider();
                    break;
                case SymmProvEnum.Rijndael:
                    mobjCryptoService = new RijndaelManaged();
                    break;
            }
        }

        /// <remarks>

        /// Constructor for using a customized SymmetricAlgorithm class.

        /// </remarks>

        public EncryptString(SymmetricAlgorithm ServiceProvider)
        {
            mobjCryptoService = ServiceProvider;
        }

        /// <remarks>

        /// Depending on the legal key size limitations of a specific CryptoService provider
        /// and length of the private key provided, padding the secret key with space character
        /// to meet the legal size of the algorithm.
        /// </remarks>

        private byte[] GetLegalKey(string Key)
        {
            string sTemp;
            if (mobjCryptoService.LegalKeySizes.Length > 0)
            {
                int lessSize = 0, moreSize = mobjCryptoService.LegalKeySizes[0].MinSize;
                // key sizes are in bits

                while (Key.Length * 8 > moreSize)
                {
                    lessSize = moreSize;
                    moreSize += mobjCryptoService.LegalKeySizes[0].SkipSize;
                }
                sTemp = Key.PadRight(moreSize / 8, ' ');
            }
            else
                sTemp = Key;

            // convert the secret key to byte array

            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        public string Encrypt(string Source, string Key)
        {
            byte[] bytIn = System.Text.ASCIIEncoding.ASCII.GetBytes(Source);
            // create a MemoryStream so that the process can be done without I/O files

            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            byte[] bytKey = GetLegalKey(Key);

            // set the private key

            mobjCryptoService.Key = bytKey;
            mobjCryptoService.IV = bytKey;

            // create an Encryptor from the Provider Service instance

            ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();

            // create Crypto Stream that transforms a stream using the encryption

            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);

            // write out encrypted content into MemoryStream

            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();

            // get the output and trim the '\0' bytes

            byte[] bytOut = ms.GetBuffer();
            int i = 0;
            for (i = 0; i < bytOut.Length; i++)
                if (bytOut[i] == 0)
                    break;

            // convert into Base64 so that the result can be used in xml

            return System.Convert.ToBase64String(bytOut, 0, i);
        }

        public string Decrypt(string Source, string Key)
        {
            // convert from Base64 to binary
            byte[] bytIn = System.Convert.FromBase64String(Source);
            // create a MemoryStream with the input
            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytIn, 0, bytIn.Length);
            byte[] bytKey = GetLegalKey(Key);
            // set the private key
            mobjCryptoService.Key = bytKey;
            mobjCryptoService.IV = bytKey;
            // create a Decryptor from the Provider Service instance
            ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
            // create Crypto Stream that transforms a stream using the decryption
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
            // read out the result from the Crypto Stream
            System.IO.StreamReader sr = new System.IO.StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}

