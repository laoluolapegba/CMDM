using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMdm.UI.Web.Models.Customer 
{
    public class BiodataInfoViewModel
    {
        public IndividualBioDataModel BioData { get; set; }
        //public IndividualAddressDetails AddressDetails { get; set; }
        public IndividualIDDetail identification { get; set; }
        //public OtherDetails otherdetails { get; set; }
        public IndividualContactDetails contact { get; set; }
    }
}