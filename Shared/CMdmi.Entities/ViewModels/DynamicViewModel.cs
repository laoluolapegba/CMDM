using CMdm.Entities.Domain.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.ViewModels
{
    public class DynamicViewModel
    {
        
        public CDMA_INDIVIDUAL_BIO_DATA BioData { get; set; }
        public CDMA_INDIVIDUAL_ADDRESS_DETAIL AddressDetails { get; set; }
        public CDMA_INDIVIDUAL_IDENTIFICATION identification { get; set; }
        public CDMA_INDIVIDUAL_OTHER_DETAILS otherdetails { get; set; }
        public CDMA_INDIVIDUAL_CONTACT_DETAIL contact { get; set; }

        //Log Object

        public CDMA_INDIVIDUAL_BIO_DATA_LOG BioData_LOG { get; set; }
        public CDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG AddressDetails_LOG { get; set; }
        public CDMA_INDIVIDUAL_IDENTIFICATION_LOG identification_LOG { get; set; }
        public CDMA_INDIVIDUAL_OTHER_DETAILS_LOG otherdetails_LOG { get; set; }
        public CDMA_INDIVIDUAL_CONTACT_DETAIL_LOG contact_LOG { get; set; }

        //account info
        public CDMA_ACCOUNT_INFO AccInfo { get; set; }
        public CDMA_ACCT_SERVICES_REQUIRED ServiceInfo { get; set; }
        public CDMA_CUSTOMER_INCOME CusIncomeInfo { get; set; }
    }

    public class LogViewModel
    {

        public CDMA_INDIVIDUAL_BIO_DATA BioData { get; set; }
        public CDMA_INDIVIDUAL_ADDRESS_DETAIL AddressDetails { get; set; }
        public CDMA_INDIVIDUAL_IDENTIFICATION identification { get; set; }
        public CDMA_INDIVIDUAL_OTHER_DETAILS otherdetails { get; set; }
        public CDMA_INDIVIDUAL_CONTACT_DETAIL contact { get; set; }

        //Log Object

        public CDMA_INDIVIDUAL_BIO_DATA_LOG BioData_LOG { get; set; }
        public CDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG AddressDetails_LOG { get; set; }
        public CDMA_INDIVIDUAL_IDENTIFICATION_LOG identification_LOG { get; set; }
        public CDMA_INDIVIDUAL_OTHER_DETAILS_LOG otherdetails_LOG { get; set; }
        public CDMA_INDIVIDUAL_CONTACT_DETAIL_LOG contact_LOG { get; set; }

    }
}
