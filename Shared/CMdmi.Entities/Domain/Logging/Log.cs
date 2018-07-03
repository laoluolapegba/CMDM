using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.Logging
{
    using Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// Represents a log record
    /// </summary>
    [Table("MDM_EXCEPTION_LOG")] //mdm_activitylog
    public partial class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the log level identifier
        /// </summary>
        public int LOGLEVELID { get; set; }

        /// <summary>
        /// Gets or sets the short message
        /// </summary>
        public string SHORTMESSAGE { get; set; }

        /// <summary>
        /// Gets or sets the full exception
        /// </summary>
        public string FULLMESSAGE { get; set; }

        /// <summary>
        /// Gets or sets the IP address
        /// </summary>
        public string IPADDRESS { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public string CUSTOMERID { get; set; }

        /// <summary>
        /// Gets or sets the page URL
        /// </summary>
        public string PAGEURL { get; set; }

        /// <summary>
        /// Gets or sets the referrer URL
        /// </summary>
        public string REFERRERURL { get; set; }
        public string CREATEDBY { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CREATEDDATE { get; set; }


        /// <summary>
        /// Gets or sets the log level
        /// </summary>
        //public LogLevel LogLevel
        //{
        //    get
        //    {
        //        return (LogLevel)this.LOGLEVELID;
        //    }
        //    set
        //    {
        //        this.LOGLEVELID = (int)value;
        //    }
        //}

    }
}
