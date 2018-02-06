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
        
        public cDMA_INDIVIDUAL_IDENTIFICATION BioData { get; set; }
        public CDMA_INDIVIDUAL_ADDRESS_DETAIL AddressDetails { get; set; }
        public CDMA_INDIVIDUAL_IDENTIFICATION identification { get; set; }
        public CDMA_INDIVIDUAL_OTHER_DETAILS otherdetails { get; set; }
        public CDMA_INDIVIDUAL_CONTACT_DETAIL contact { get; set; }

    }
}
