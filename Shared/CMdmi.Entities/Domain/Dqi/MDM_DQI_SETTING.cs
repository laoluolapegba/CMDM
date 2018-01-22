

namespace CMdm.Entities.Domain.Dqi
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MDM_DQI_SETTING")]
    public partial class MDM_DQI_SETTING
    {

        [Key]
        public decimal DQI_ID { get; set; }
        public string DQI_NAME { get; set; }
        public string DQI_DESC { get; set; }
        public string DQI_SCRIPT { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string LAST_UPDATED_BY { get; set; }
        public Nullable<System.DateTime> LAST_UPDATED_DATE { get; set; }


    }
}
