using CMdm.Data;
using CMdm.Services.Messaging;
using CMdm.UI.Web.Models.Messaging;
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


namespace CMdm.BackService
{
    public partial class CmdmBackService : ServiceBase
    {
        private int IsBusy = 0;
        private AppDbContext db;
        private IMessagingService _messagingService;

        public CmdmBackService()
        {
            InitializeComponent();
            try
            {
                if (!System.Diagnostics.EventLog.SourceExists("CmdmBackService"))
                {
                    System.Diagnostics.EventLog.CreateEventSource("CmdmBackService", "CmdmBackService");
                }
                eventLog1.Source = "CmdmBackService";
                eventLog1.Log = "CmdmBackService";
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

                eventLog1.Log = "";
                eventLog1.Source = "";
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


        private void CheckPending_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Interlocked.CompareExchange(ref IsBusy, 1, 0) != 0)
                return;
            ExceptionLog expLogger = new ExceptionLog();
            try
            {
                _messagingService = new MessagingService();

                int pendingCount = db.CM_BACK_JOBS.Where(j => j.JOB_TYPE == 2).Where(j => j.FLG_STATUS != 1).Count();
                int max_retry = 5;
                expLogger.WriteMessage("Checking pending mails @" + DateTime.Now.ToString() + ":" + pendingCount + " pending", 1);
                if (pendingCount > 0)
                {
                    using (var _db = new AppDbContext())
                    {
                        var pendingjobs = _db.CM_BACK_JOBS.Where(j => j.JOB_TYPE == 2).Where(j => j.FLG_STATUS != 1).ToList();
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
                                        htmlbody = _messagingService.GenerateBody(item);
                                        if (htmlbody != string.Empty)
                                        {
                                            entity.JOB_TYPE = (int)MessageJobEnum.JobType.SendMail;
                                            entity.MAILBODY = htmlbody;
                                            entity.DATE_SENT = DateTime.Now;
                                        }
                                        break;
                                    case (int)MessageJobEnum.JobType.SendMail:
                                        recepient.Add(item.RECIPIENTNAME);
                                        _messagingService.SendMail(recepient, item.MSG_SUBJECT, item.MAILBODY, item.FROM_EMAIL, item.FROM_EMAIL);
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
