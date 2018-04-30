using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace CMdm.UI.Web.Models.Customer
{
    public class AccServicesModel
    {
        public AccServicesModel()
        {
            CardPreference = new List<SelectListItem>();
            ElectronicBankingPreference = new List<SelectListItem>();
            StatementPreference = new List<SelectListItem>();
            TransactionAlertPreference = new List<SelectListItem>();
            StatementFrequency = new List<SelectListItem>();
            ChequeBookRequisition = new List<SelectListItem>();
            ChequeLeavesRequired = new List<SelectListItem>();
            ChequeConfirmation = new List<SelectListItem>();
            ChequeConfirmationThreshold = new List<SelectListItem>();
            ChequeConfirmationThresholdRange = new List<SelectListItem>();
            OnlineTransferLimit = new List<SelectListItem>();
            OnlineTransferLimitRange = new List<SelectListItem>();
            Tokens = new List<SelectListItem>();
    }

        [DisplayName("Customer No")]
        public string CUSTOMER_NO { get; set; }
        [DisplayName("Account Number")]
        public string ACCOUNT_NUMBER { get; set; }
        [DisplayName("Card Preference")]
        public string CARD_PREFERENCE { get; set; }
        [DisplayName("Electronic Banking Preference")]
        public string ELECTRONIC_BANKING_PREFERENCE { get; set; }
        [DisplayName("Statement Preference")]
        public string STATEMENT_PREFERENCES { get; set; }
        [DisplayName("Transaction Alert Preference")]
        public string TRANSACTION_ALERT_PREFERENCE { get; set; }
        [DisplayName("Statement Frequency")]
        public string STATEMENT_FREQUENCY { get; set; }
        [DisplayName("Cheque Book Requisition")]
        public string CHEQUE_BOOK_REQUISITION { get; set; }
        [DisplayName("Cheque Leaves Required")]
        public string CHEQUE_LEAVES_REQUIRED { get; set; }
        [DisplayName("Cheque Confirmation")]
        public string CHEQUE_CONFIRMATION { get; set; }
        [DisplayName("Cheque Confirmation Threshold")]
        public string CHEQUE_CONFIRMATION_THRESHOLD { get; set; }
        [DisplayName("Cheque Confirmation Threshold Range")]
        public string CHEQUE_CONFIRM_THRESHOLD_RANGE { get; set; }
        [DisplayName("Online Transfer Limit")]
        public string ONLINE_TRANSFER_LIMIT { get; set; }
        [DisplayName("Online Transfer Limit Range")]
        public string ONLINE_TRANSFER_LIMIT_RANGE { get; set; }
        [DisplayName("Token")]
        public string TOKEN { get; set; }
        [DisplayName("Account Signatory")]
        public string ACCOUNT_SIGNATORY { get; set; }
        [DisplayName("Second Signatory")]
        public string SECOND_SIGNATORY { get; set; }

        public List<SelectListItem> CardPreference { get; set; }
        public List<SelectListItem> ElectronicBankingPreference { get; set; }
        public List<SelectListItem> StatementPreference { get; set; }
        public List<SelectListItem> TransactionAlertPreference { get; set; }
        public List<SelectListItem> StatementFrequency { get; set; }
        public List<SelectListItem> ChequeBookRequisition { get; set; }
        public List<SelectListItem> ChequeLeavesRequired { get; set; }
        public List<SelectListItem> ChequeConfirmation { get; set; }
        public List<SelectListItem> ChequeConfirmationThreshold { get; set; }
        public List<SelectListItem> ChequeConfirmationThresholdRange { get; set; }
        public List<SelectListItem> OnlineTransferLimit { get; set; }
        public List<SelectListItem> OnlineTransferLimitRange { get; set; }
        public List<SelectListItem> Tokens { get; set; }

        public string ReadOnlyForm { get; set; }
        public string LastUpdatedby { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LastAuthdby { get; set; }
        public DateTime? LastAuthDate { get; set; }
        public int ExceptionId { get; internal set; }
        public string AuthoriserRemarks { get; internal set; }
    }
}