using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.Audit
{
    [Table("CDMA_CHANGE_LOG")]
    public partial class CDMA_CHANGE_LOG
    {
        public int ID { get; set; }
        public string ENTITYNAME { get; set; }
        public string PROPERTYNAME { get; set; }
        public string PRIMARYKEYVALUE { get; set; }
        public string OLDVALUE { get; set; }
        public string NEWVALUE { get; set; }
        public DateTime DATECHANGED { get; set; }
        public string EVENTTYPE { get; set; }
        public string USERID { get; set; }
        public string CHANGEID { get; set; }
    }

}
