using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMdm.UI.Web.Models.MdmWeight
{
    public class MdmWeightModel
    {
        [DisplayName("Weight Id")]
        public int WEIGHT_ID { get; set; }
        [DisplayName("Weight value")]

        public int? WEIGHT_VALUE { get; set; }

        [StringLength(20)]
        [DisplayName("Created by")]
        public string CREATED_BY { get; set; }
        [DisplayName("Created date")]

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(20)]
        [DisplayName("Last modified by")]
        public string LAST_MODIFIED_BY { get; set; }
        [DisplayName("Last modified date")]
        public DateTime? LAST_MODIFIED_DATE { get; set; }
        [DisplayName("Active")]
        public int RECORD_STATUS { get; set; }
        [DisplayName("Weight")]
        public string WEIGHT_DESC { get; set; }
        //public bool RECORD_STATUS_BOOL { get; set; }
        public bool RECORD_STATUS_BOOL
        { // Wrapper property that works around the Boolean issue
            //https://blog.vijay.name/2013/02/working-with-net-boolean-and-oracle-along-with-asp-net-mvc/
            get
            {
                return RECORD_STATUS !=0;
            }
            set
            {
                RECORD_STATUS = value ? 1 : 0;
            }
        }
    }
}