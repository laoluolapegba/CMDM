using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.Report
{
   
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CM_REPORTDEFN")]
    public partial class CmReportDefn
    {
        [Key]
        public int REPORT_ID { get; set; }
        public string REPORT_NAME { get; set; }
        public string REPORT_DESCRIPTION { get; set; }
        public string REPORT_URL { get; set; }
        public string DATASETNAME { get; set; }
    }
}
