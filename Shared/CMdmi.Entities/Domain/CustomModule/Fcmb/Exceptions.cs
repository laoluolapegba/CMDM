using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.CustomModule.Fcmb
{
    public class Exceptions
    {
        public int ACCOUNT_OFFICER { get; set; }
        public int WRONG_SECTOR { get; set; }
        public int WRONG_SCHEME_CODES { get; set; }
        public int MULTIPLE_AO_CODES { get; set; }
        public int EMAIL_PHONE_VAL { get; set; }
        public int SEGMENT_MAPPING { get; set; }
        public int MULTIPLE_ID { get; set; }
        public int OUTSTANDING_DOCS { get; set; }
        public int PHONE_NUMBER_VAL { get; set; }
    }
}
