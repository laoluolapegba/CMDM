using CMdm.Entities.Domain.Dqi;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CMdm.Entities.Domain.Mdm
{
   
    [Table("MDM_CATALOG")]
    public partial class MdmCatalog
    {
        public MdmCatalog()
        {
            MdmDqRules = new HashSet<MdmDqRule>();
        }
        [Key]
        public int CATALOG_ID { get; set; }  //,,,
        public string CATALOG_NAME { get; set; }
        public int CATEGORY_ID { get; set; }
        public string CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(20)]
        public string LAST_MODIFIED_BY { get; set; }

        public DateTime? LAST_MODIFIED_DATE { get; set; }
        public virtual ICollection<MdmDqRule> MdmDqRules { get; set; }
    }
}