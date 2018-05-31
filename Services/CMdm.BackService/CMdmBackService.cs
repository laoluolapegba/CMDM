using CMdm.Data;
using CMdm.Services.Messaging;
using CMdm.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CMdm.Entities.Domain.Customer;
using static CMdm.Entities.ViewModels.MessageJobEnum;
using CMdm.Entities.Domain.CustomModule.Fcmb;

namespace CMdm.BackService
{
    public partial class CmdmBackService : ServiceBase
    {
        private int IsBusy = 0;
        private AppDbContext db = new AppDbContext();
        private IMessagingService _messagingService;

        public CmdmBackService()
        {
            InitializeComponent();
            try
            {
                if (!System.Diagnostics.EventLog.SourceExists("CmdmBackService"))
                {
                    System.Diagnostics.EventLog.CreateEventSource("CmdmBackService", "CmdmBackServiceLog");
                }
                eventLog1.Source = "CmdmBackService";
                eventLog1.Log = "CmdmBackServiceLog";
            }
            catch (Exception)
            { }

        }

        protected override void OnStart(string[] args)
        {
            try
            {
                int interval = Convert.ToInt32(120000);  //* 60 * 1000*  2mins/
                tmrJobs.Interval = interval;
                tmrJobs.Enabled = true;

                //eventLog1.Log = "";
                //eventLog1.Source = "";
                eventLog1.WriteEntry("CMDM back service started at " + DateTime.Now.ToString());
            }
            catch (Exception exp)
            {
                StringBuilder sBld = new StringBuilder();
                sBld.Append("Error Occured while starting CMDM back service at " + DateTime.Today + "\n");
                sBld.Append(exp.Message);
                sBld.Append(exp.Source);
                sBld.Append(exp.StackTrace);

                eventLog1.WriteEntry(sBld.ToString(), EventLogEntryType.Error);
            }
        }

        protected override void OnStop()
        {
            eventLog1.Source = "CmdmBackService";
            eventLog1.WriteEntry("CMDM back service stopped at " + DateTime.Now.ToString());
        }

        private void tmrJobs_Tick(object sender, EventArgs e)
        {

        }

        private void LogNotificationJob(string subject, string recipient, string template, int mailtype, string branch, string branchname, List<Exceptions> allExceptions)
        {
            using (var _db = new AppDbContext())
            { 
                CM_BACK_JOBS backJob = new CM_BACK_JOBS
                {
                    CUSTOMER_NO = null,
                    JOB_TYPE = 2,
                    FROM_EMAIL = "SVC_CMDM",
                    DATE_LOGGED = DateTime.Now,
                    CREATED_BY = "SYSTEM",
                    RCPT_EMAIL = recipient,
                    FLG_STATUS = 0,
                    DATE_SENT = null,
                    COUNT_RETRIES = 0,
                    MSG_SUBJECT = subject,
                    BRANCH_ID = Convert.ToInt32(branch),
                    RECIPIENTNAME = recipient,
                    REQUIREDDATE = DateTime.Now,
                    MAILBODY = null,
                    MAILBODY_2 = null,
                    MAILTYPE = mailtype,
                    MAIL_TEMPLATE = template,
                    USERFULLNAME = "SVC_CMDM",
                    BRANCHNAME = branchname,
                    ACCOUNT_OFFICER = allExceptions[0].ACCOUNT_OFFICER,
                    WRONG_SECTOR = allExceptions[0].WRONG_SECTOR,
                    WRONG_SCHEME_CODES = allExceptions[0].WRONG_SCHEME_CODES,
                    MULTIPLE_AO_CODES = allExceptions[0].MULTIPLE_AO_CODES,
                    EMAIL_PHONE_VAL = allExceptions[0].EMAIL_PHONE_VAL,
                    SEGMENT_MAPPING = allExceptions[0].SEGMENT_MAPPING,
                    MULTIPLE_ID = allExceptions[0].MULTIPLE_ID,
                    OUTSTANDING_DOCS = allExceptions[0].OUTSTANDING_DOCS,
                    PHONE_NUMBER_VAL = allExceptions[0].PHONE_NUMBER_VAL
                };
                _db.CM_BACK_JOBS.Add(backJob);
                _db.SaveChanges();
            }
        }

        private void CheckPending_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Interlocked.CompareExchange(ref IsBusy, 1, 0) != 0)
                return;
            ExceptionLog expLogger = new ExceptionLog();

            //Get all the branches and their pending then log them
            try
            {
                var allBranches = db.CM_BRANCH.Select(x => x).ToList();

                foreach(var branch in allBranches)
                {
                    int ACCOUNT_OFFICER = db.AccountOfficers.Where(x => x.SOL_ID == branch.BRANCH_ID).Count();
                    int WRONG_SECTOR = db.CustSegment.Where(x => x.PRIMARY_SOL_ID == branch.BRANCH_ID).Count();
                    int WRONG_SCHEME_CODES = db.WrongSchemeCodes.Where(x => x.SOL_ID == branch.BRANCH_ID).Count();
                    int MULTIPLE_AO_CODES = db.MultipleRefCode.Where(x => x.SOL_ID == branch.BRANCH_ID).Count();
                    int EMAIL_PHONE_VAL = db.EmailPhone.Where(x => x.BRANCH_CODE == branch.BRANCH_ID).Count();
                    int SEGMENT_MAPPING = db.WrongSegment.Where(x => x.PRIMARY_SOL_ID == branch.BRANCH_ID).Count();
                    int MULTIPLE_ID = db.CdmaGoldenRecords.Where(x => x.BRANCH_CODE == branch.BRANCH_ID).Count();
                    int OUTSTANDING_DOCS = db.OutStandingDocs.Where(x => x.SOL_ID == branch.BRANCH_ID).Count();
                    int PHONE_NUMBER_VAL = db.CMDM_PHONEVALIDATION_RESULTS.Where(x => x.BRANCH_CODE == branch.BRANCH_ID).Count();

                    int cso = ACCOUNT_OFFICER + WRONG_SECTOR + WRONG_SCHEME_CODES + SEGMENT_MAPPING + OUTSTANDING_DOCS + PHONE_NUMBER_VAL;
                    int fincon = MULTIPLE_AO_CODES;
                    int amu = MULTIPLE_ID;

                    string subject = "";
                    string recipient = "";
                    string template = "";
                    int mailtype = 0;
                    _messagingService = new MessagingService();

                    //Attempt to log cso notification (Branch level mail)
                    if (cso != 0)
                    {
                        subject = "CMDM Notification Service";
                        recipient = _messagingService.GetCSOEscalationMail(branch.BRANCH_ID);
                        template = "Cso.cshtml";
                        mailtype = (int)MailType.CsoNotification;

                        List<Exceptions> allExceptions = new List<Exceptions>();
                        Exceptions myExceptions = new Exceptions{
                            ACCOUNT_OFFICER = ACCOUNT_OFFICER,
                            WRONG_SECTOR = WRONG_SECTOR,
                            WRONG_SCHEME_CODES = WRONG_SCHEME_CODES,
                            MULTIPLE_AO_CODES = MULTIPLE_AO_CODES,
                            EMAIL_PHONE_VAL = EMAIL_PHONE_VAL,
                            SEGMENT_MAPPING = SEGMENT_MAPPING,        
                            MULTIPLE_ID = MULTIPLE_ID,
                            OUTSTANDING_DOCS = OUTSTANDING_DOCS,
                            PHONE_NUMBER_VAL = PHONE_NUMBER_VAL
                        };
                        allExceptions.Add(myExceptions);
                        
                        LogNotificationJob(subject, recipient, template, mailtype, branch.BRANCH_ID, branch.BRANCH_NAME,allExceptions);
                    }
                    //Attempt to log fincon notification (Branch level mail)
                    if(fincon != 0){
                        subject = "CMDM Notification Service";
                        recipient = _messagingService.GetFinconEscalationMail(branch.BRANCH_ID);
                        template = "Fincon.cshtml";
                        mailtype = (int)MailType.FinconNotification;

                        List<Exceptions> allExceptions = new List<Exceptions>();
                        Exceptions myExceptions = new Exceptions
                        {
                            ACCOUNT_OFFICER = ACCOUNT_OFFICER,
                            WRONG_SECTOR = WRONG_SECTOR,
                            WRONG_SCHEME_CODES = WRONG_SCHEME_CODES,
                            MULTIPLE_AO_CODES = MULTIPLE_AO_CODES,
                            EMAIL_PHONE_VAL = EMAIL_PHONE_VAL,
                            SEGMENT_MAPPING = SEGMENT_MAPPING,
                            MULTIPLE_ID = MULTIPLE_ID,
                            OUTSTANDING_DOCS = OUTSTANDING_DOCS,
                            PHONE_NUMBER_VAL = PHONE_NUMBER_VAL
                        };
                        allExceptions.Add(myExceptions);

                        LogNotificationJob(subject, recipient, template, mailtype, branch.BRANCH_ID, branch.BRANCH_NAME, allExceptions);
                    }
                    //Attempt to log amu notification (Branch Level mail)
                    if (amu != 0)
                    {
                        subject = "CMDM Notification Service";
                        recipient = _messagingService.GetAMUEscalationMail(branch.BRANCH_ID);
                        template = "Amu.cshtml";
                        mailtype = (int)MailType.AmuNotification;

                        List<Exceptions> allExceptions = new List<Exceptions>();
                        Exceptions myExceptions = new Exceptions
                        {
                            ACCOUNT_OFFICER = ACCOUNT_OFFICER,
                            WRONG_SECTOR = WRONG_SECTOR,
                            WRONG_SCHEME_CODES = WRONG_SCHEME_CODES,
                            MULTIPLE_AO_CODES = MULTIPLE_AO_CODES,
                            EMAIL_PHONE_VAL = EMAIL_PHONE_VAL,
                            SEGMENT_MAPPING = SEGMENT_MAPPING,
                            MULTIPLE_ID = MULTIPLE_ID,
                            OUTSTANDING_DOCS = OUTSTANDING_DOCS,
                            PHONE_NUMBER_VAL = PHONE_NUMBER_VAL
                        };
                        allExceptions.Add(myExceptions);

                        LogNotificationJob(subject, recipient, template, mailtype, branch.BRANCH_ID, branch.BRANCH_NAME, allExceptions);
                    }

                    //Attempt to log a csm escalation mail (first level cso escalation)
                    if (cso > 500 || cso < 500)
                    {
                        subject = "CMDM First Level Escalation";
                        recipient = _messagingService.GetCSMEscalationMail(branch.BRANCH_ID);
                        template = "Csm.cshtml";
                        mailtype = (int)MailType.CsmNotification;

                        List<Exceptions> allExceptions = new List<Exceptions>();
                        Exceptions myExceptions = new Exceptions
                        {
                            ACCOUNT_OFFICER = ACCOUNT_OFFICER,
                            WRONG_SECTOR = WRONG_SECTOR,
                            WRONG_SCHEME_CODES = WRONG_SCHEME_CODES,
                            MULTIPLE_AO_CODES = MULTIPLE_AO_CODES,
                            EMAIL_PHONE_VAL = EMAIL_PHONE_VAL,
                            SEGMENT_MAPPING = SEGMENT_MAPPING,
                            MULTIPLE_ID = MULTIPLE_ID,
                            OUTSTANDING_DOCS = OUTSTANDING_DOCS,
                            PHONE_NUMBER_VAL = PHONE_NUMBER_VAL
                        };
                        allExceptions.Add(myExceptions);

                        LogNotificationJob(subject, recipient, template, mailtype, branch.BRANCH_ID, branch.BRANCH_NAME, allExceptions);
                    }

                    //Attempt to log bm escalation mail (First level fincon escalation to BM)
                    if (fincon > 50 || fincon < 50)
                    {
                        subject = "CMDM First Level Escalation";
                        recipient = _messagingService.GetBMEscalationMail(branch.BRANCH_ID);
                        template = "Bm.cshtml";
                        mailtype = (int)MailType.BmNotification;

                        List<Exceptions> allExceptions = new List<Exceptions>();
                        Exceptions myExceptions = new Exceptions
                        {
                            ACCOUNT_OFFICER = ACCOUNT_OFFICER,
                            WRONG_SECTOR = WRONG_SECTOR,
                            WRONG_SCHEME_CODES = WRONG_SCHEME_CODES,
                            MULTIPLE_AO_CODES = MULTIPLE_AO_CODES,
                            EMAIL_PHONE_VAL = EMAIL_PHONE_VAL,
                            SEGMENT_MAPPING = SEGMENT_MAPPING,
                            MULTIPLE_ID = MULTIPLE_ID,
                            OUTSTANDING_DOCS = OUTSTANDING_DOCS,
                            PHONE_NUMBER_VAL = PHONE_NUMBER_VAL
                        };
                        allExceptions.Add(myExceptions);

                        LogNotificationJob(subject, recipient, template, mailtype, branch.BRANCH_ID, branch.BRANCH_NAME, allExceptions);
                    }

                    //Attempt to log amu escalation mail (First level amu escalation)
                    if (amu > 50 || amu < 50)
                    {
                        subject = "CMDM First Level Escalation";
                        recipient = _messagingService.GetAMUCSMEscalationMail(branch.BRANCH_ID);
                        template = "AmuCsm.cshtml";
                        mailtype = (int)MailType.AmuCsmNotification;

                        List<Exceptions> allExceptions = new List<Exceptions>();
                        Exceptions myExceptions = new Exceptions
                        {
                            ACCOUNT_OFFICER = ACCOUNT_OFFICER,
                            WRONG_SECTOR = WRONG_SECTOR,
                            WRONG_SCHEME_CODES = WRONG_SCHEME_CODES,
                            MULTIPLE_AO_CODES = MULTIPLE_AO_CODES,
                            EMAIL_PHONE_VAL = EMAIL_PHONE_VAL,
                            SEGMENT_MAPPING = SEGMENT_MAPPING,
                            MULTIPLE_ID = MULTIPLE_ID,
                            OUTSTANDING_DOCS = OUTSTANDING_DOCS,
                            PHONE_NUMBER_VAL = PHONE_NUMBER_VAL
                        };
                        allExceptions.Add(myExceptions);

                        LogNotificationJob(subject, recipient, template, mailtype, branch.BRANCH_ID, branch.BRANCH_NAME, allExceptions);
                    }

                    //Attempt to log a zsm final escalation (Second level cso escalation to zsm and zonal control)
                    if (cso > 1000)
                    {
                        subject = "CMDM Final Escalation";
                        recipient = _messagingService.GetZSMEscalationMail(branch.BRANCH_ID);
                        template = "Zsm.cshtml";
                        mailtype = (int)MailType.ZsmNotification;

                        List<Exceptions> allExceptions = new List<Exceptions>();
                        Exceptions myExceptions = new Exceptions
                        {
                            ACCOUNT_OFFICER = ACCOUNT_OFFICER,
                            WRONG_SECTOR = WRONG_SECTOR,
                            WRONG_SCHEME_CODES = WRONG_SCHEME_CODES,
                            MULTIPLE_AO_CODES = MULTIPLE_AO_CODES,
                            EMAIL_PHONE_VAL = EMAIL_PHONE_VAL,
                            SEGMENT_MAPPING = SEGMENT_MAPPING,
                            MULTIPLE_ID = MULTIPLE_ID,
                            OUTSTANDING_DOCS = OUTSTANDING_DOCS,
                            PHONE_NUMBER_VAL = PHONE_NUMBER_VAL
                        };
                        allExceptions.Add(myExceptions);

                        LogNotificationJob(subject, recipient, template, mailtype, branch.BRANCH_ID, branch.BRANCH_NAME, allExceptions);

                        recipient = _messagingService.GetZonalControlEscalationMail(branch.BRANCH_ID);
                        template = "ZonalControl.cshtml";
                        mailtype = (int)MailType.ZonalControlNotification;

                        LogNotificationJob(subject, recipient, template, mailtype, branch.BRANCH_ID, branch.BRANCH_NAME, allExceptions);
                    }

                    //Attempt to log hrhelpdesk final escalation (Second level fincon escalation to zonal head)
                    if (fincon > 1000)
                    {
                        subject = "CMDM Final Escalation";
                        recipient = _messagingService.GetZonalHeadEscalationMail(branch.BRANCH_ID);
                        template = "ZonalHead.cshtml";
                        mailtype = (int)MailType.ZonalHeadNotification;

                        List<Exceptions> allExceptions = new List<Exceptions>();
                        Exceptions myExceptions = new Exceptions
                        {
                            ACCOUNT_OFFICER = ACCOUNT_OFFICER,
                            WRONG_SECTOR = WRONG_SECTOR,
                            WRONG_SCHEME_CODES = WRONG_SCHEME_CODES,
                            MULTIPLE_AO_CODES = MULTIPLE_AO_CODES,
                            EMAIL_PHONE_VAL = EMAIL_PHONE_VAL,
                            SEGMENT_MAPPING = SEGMENT_MAPPING,
                            MULTIPLE_ID = MULTIPLE_ID,
                            OUTSTANDING_DOCS = OUTSTANDING_DOCS,
                            PHONE_NUMBER_VAL = PHONE_NUMBER_VAL
                        };
                        allExceptions.Add(myExceptions);

                        LogNotificationJob(subject, recipient, template, mailtype, branch.BRANCH_ID, branch.BRANCH_NAME, allExceptions);
                    }

                    //Attempt to log amu final escalation (second level amu escalation)
                    if (amu > 1000)
                    {
                        subject = "CMDM Final Escalation";
                        recipient = _messagingService.GetAMUZSMEscalationMail(branch.BRANCH_ID);
                        template = "AmuZsm.cshtml";
                        mailtype = (int)MailType.AmuZsmNotification;

                        List<Exceptions> allExceptions = new List<Exceptions>();
                        Exceptions myExceptions = new Exceptions
                        {
                            ACCOUNT_OFFICER = ACCOUNT_OFFICER,
                            WRONG_SECTOR = WRONG_SECTOR,
                            WRONG_SCHEME_CODES = WRONG_SCHEME_CODES,
                            MULTIPLE_AO_CODES = MULTIPLE_AO_CODES,
                            EMAIL_PHONE_VAL = EMAIL_PHONE_VAL,
                            SEGMENT_MAPPING = SEGMENT_MAPPING,
                            MULTIPLE_ID = MULTIPLE_ID,
                            OUTSTANDING_DOCS = OUTSTANDING_DOCS,
                            PHONE_NUMBER_VAL = PHONE_NUMBER_VAL
                        };
                        allExceptions.Add(myExceptions);

                        LogNotificationJob(subject, recipient, template, mailtype, branch.BRANCH_ID, branch.BRANCH_NAME, allExceptions);
                    }
                }
            }
            catch (Exception exp)
            {
                expLogger.WriteMessage(exp.Message + exp.StackTrace.ToString(), 3);
            }
            

            try
            {
                _messagingService = new MessagingService();

                int pendingCount = db.CM_BACK_JOBS.Where(j => j.FLG_STATUS == 0).Count();
                int max_retry = 5;
                expLogger.WriteMessage("Checking pending mails @" + DateTime.Now.ToString() + ":" + pendingCount + " pending", 1);
                if (pendingCount > 0)
                {
                    using (var _db = new AppDbContext())
                    {
                        var pendingjobs = _db.CM_BACK_JOBS.Where(j => j.FLG_STATUS == 0).ToList();
                        foreach (var item in pendingjobs)
                        {
                            if (item.COUNT_RETRIES < max_retry)
                            {
                                string htmlbody = string.Empty;
                                bool mailsent = false;
                                List<string> recepient = new List<string>();
                                var entity = _db.CM_BACK_JOBS.FirstOrDefault(c => c.JOBID == item.JOBID);
                                switch (item.JOB_TYPE)
                                {
                                    case (int)MessageJobEnum.JobType.PrepareMail:
                                        //htmlbody = "Dear" +  item.RECIPIENTNAME + ",< br />" +
                                        //    "Please be informed that the customer information needs to be attended. Customer NO: " + item.CUSTOMER_NO;
                                        htmlbody = _messagingService.GenerateBody(entity);
                                        if (htmlbody != string.Empty)
                                        {
                                            entity.JOB_TYPE = (int)MessageJobEnum.JobType.SendMail;
                                            entity.MAILBODY = htmlbody;
                                            if(htmlbody.Length > 4000)
                                            {
                                                entity.MAILBODY = htmlbody.Substring(1, 3999);
                                                entity.MAILBODY_2 = htmlbody.Substring(4000);
                                            }
                                            else
                                            {
                                                entity.MAILBODY = htmlbody;
                                            }
                                            entity.DATE_SENT = DateTime.Now;
                                        }
                                        break;
                                    case (int)MessageJobEnum.JobType.SendMail:
                                        recepient.Add(item.RECIPIENTNAME);
                                        _messagingService.SendMail(recepient, item.MSG_SUBJECT, item.MAILBODY+item.MAILBODY_2, item.FROM_EMAIL, item.FROM_EMAIL);
                                        entity.FLG_STATUS = 1;
                                        entity.DATE_SENT = DateTime.Now;
                                        break;
                                    default:
                                        break;
                                }
                                _db.CM_BACK_JOBS.Attach(entity);
                                _db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                                _db.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                expLogger.WriteMessage(exp.Message + exp.StackTrace.ToString(), 3);
            }
            finally
            {
                Interlocked.Exchange(ref IsBusy, 0);
            };
        }
    }
}
