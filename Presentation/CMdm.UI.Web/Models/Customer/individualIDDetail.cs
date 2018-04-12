using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.Customer
{
    public class individualIDDetail
    {
        public individualIDDetail()
        {
            IdType = new List<SelectListItem>();

        }
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Identification Type")]
        public string IDENTIFICATION_TYPE { get; set; }
        [DisplayName("Identification Number")]
        public string ID_NO { get; set; }
        [DisplayName("Expiry Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ID_EXPIRY_DATE { get; set; }
        [DisplayName("Issued Date")]

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ID_ISSUE_DATE { get; set; }
        [DisplayName("Place of Issuance")]
        public string PLACE_OF_ISSUANCE { get; set; }

        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }

        public Nullable<System.DateTime> LAST_MODIFIED_DATE { get; set; }
        public string LAST_MODIFIED_BY { get; set; }

        public string AUTHORISED { get; set; }
        public string AUTHORISED_BY { get; set; }
        public Nullable<System.DateTime> AUTHORISED_DATE { get; set; }
        public string IP_ADDRESS { get; set; }

        public List<SelectListItem> IdType { get; set; }

    }
}