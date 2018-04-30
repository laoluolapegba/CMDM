using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMdm.Entities.Domain.Customer
{
    [Table("CDMA_INDIVIDUAL_NEXT_OF_KIN")]
    public partial class CDMA_INDIVIDUAL_NEXT_OF_KIN
    {
        [Key, Column(Order = 0)]
        public string CUSTOMER_NO { get; set; }
        public int? TITLE { get; set; }
        public string SURNAME { get; set; }
        public string FIRST_NAME { get; set; }
        public string OTHER_NAME { get; set; }
        public DateTime? DATE_OF_BIRTH { get; set; }
        public string SEX { get; set; }
        public int? RELATIONSHIP { get; set; }
        public string OFFICE_NO { get; set; }
        public string MOBILE_NO { get; set; }
        public string EMAIL_ADDRESS { get; set; }
        public string HOUSE_NUMBER { get; set; }
        public decimal? IDENTIFICATION_TYPE { get; set; }
        public DateTime? ID_EXPIRY_DATE { get; set; }
        public DateTime? ID_ISSUE_DATE { get; set; }
        public string RESIDENT_PERMIT_NUMBER { get; set; }
        public string PLACE_OF_ISSUANCE { get; set; }
        public string STREET_NAME { get; set; }
        public string NEAREST_BUS_STOP_LANDMARK { get; set; }
        public string CITY_TOWN { get; set; }
        public decimal? LGA { get; set; }
        public string ZIP_POSTAL_CODE { get; set; }
        public decimal? STATE { get; set; }
        public decimal? COUNTRY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? LAST_MODIFIED_DATE { get; set; }
        public string LAST_MODIFIED_BY { get; set; }
        [Key, Column(Order = 1)]
        public string AUTHORISED { get; set; }
        public string AUTHORISED_BY { get; set; }
        public DateTime? AUTHORISED_DATE { get; set; }
        public string IP_ADDRESS { get; set; }
        public string BRANCH_CODE { get; set; }

        //public string MNT_STATUS { get; set; }

        public virtual CDMA_CUST_REL_TYPE RelationshipTypes { get; set; }
        public virtual CDMA_CUST_TITLE TitleTypes { get; set; }
        public virtual CDMA_COUNTRIES Countries { get; set; }
        public virtual SRC_CDMA_STATE States { get; set; }
        public virtual SRC_CDMA_LGA LocalGovts { get; set; }
        public virtual CDMA_IDENTIFICATION_TYPE IdTypes { get; set; }
    }
}
