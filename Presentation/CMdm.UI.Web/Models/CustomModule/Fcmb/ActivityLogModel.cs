using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.CustomModule.Fcmb
{
    public class ActivityLogModel
    {
        public ActivityLogModel()
        {
            Branches = new List<SelectListItem>();
        }
        [DisplayName("Activity ID")]
        public int ACTIVITY_ID { get; set; }
        [DisplayName("User ID")]
        public int USER_ID { get; set; }
        [DisplayName("Username")]
        public string USER_NAME { get; set; }
        [DisplayName("Fullname")]
        public string FULLNAME { get; set; }
        [DisplayName("Activity Description")]
        public string ACTIVITY_DESC { get; set; }
        [DisplayName("Activity Date")]
        public DateTime? ACTIVITY_DATE { get; set; }
        [DisplayName("Branch Code")]
        public string BRANCH_CODE { get; set; }
        [DisplayName("Branch Name")]
        public string BRANCH_NAME { get; set; }

        [DisplayName("Start Date")]
        [UIHint("DateNullable")]
        public DateTime? CreatedOnFrom { get; set; }

        [DisplayName("End Date")]
        [UIHint("DateNullable")]
        public DateTime? CreatedOnTo { get; set; }

        public int Id
        {
            get; set;
        }

        public IList<SelectListItem> Branches { get; set; }
    }
}