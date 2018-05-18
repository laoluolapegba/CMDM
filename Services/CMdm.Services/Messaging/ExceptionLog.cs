using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Services.Messaging
{
    public class ExceptionLog
    {
        public void WriteMessage (string msg, int errtype)
        {
            EventLog eventLog1 = new EventLog();
            EventLogEntryType type;
            switch (errtype)
            {
                case 1: type = EventLogEntryType.Information;
                    break;
                case 2: type = EventLogEntryType.Warning;
                    break;
                case 3: type = EventLogEntryType.Error;
                    break;

                default:
                    type = EventLogEntryType.Error;
                    break;
            }
            try
            {
                eventLog1.Source = "CmdmBackService";
                eventLog1.WriteEntry("CmdmBackService :"+ msg, type);
            }
            catch(Exception ex)
            {
                eventLog1.WriteEntry("CmdmBackService :" + msg, type);
            }
        }
    }
}
