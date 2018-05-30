﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMdm.Entities.ViewModels
{
    public class MessageModel
    {
        public int JOBTYPE { get; set; }
        [DisplayName("Customer ID")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("From")]
        public string FROM_EMAIL { get; set; }
        [DisplayName("To")]
        public string RCPT_EMAIL { get; set; }
        [DisplayName("Subject")]
        public string MSG_SUBJECT { get; set; }
        [DisplayName("Branch")]
        public decimal BRANCH_ID { get; set; }
        [DisplayName("Branch Name")]
        public string BRANCHNAME { get; set; }
        public string MAILTYPE { get; set; }
        [DisplayName("Required Date")]
        public DateTime? RequiredDate { get; set; }
        [DisplayName("Delivered Date")]
        public DateTime? DeliveredDate { get; set; }
        [DisplayName("User")]
        public string UserID { get; set; }
        public string RECIPIENTNAME { get; set; }
    }
}