using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMdm.UI.Web.Models.Customer
{
    public class CorpCustModel
    {
        public CompDetailsModel CompDetailsModel { get; set; }
        public CompInfoModel CompInfoModel { get; set; }
        public BenOwnersModel BenOwnersModel { get; set; }
        public CorpADDModel CorpADDModel { get; set; }
        public GuarantorModel GuarantorModel { get; set; }
    }
}