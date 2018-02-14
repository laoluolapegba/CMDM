using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.Dqi
{
    using Mdm;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MDM_DQI_PARAMETERS")]
    public partial class MdmDqCatalog
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int COLUMNID { get; set; }

        public string TABLE_CATEGORIES { get; set; }

        public string TABLE_NAMES { get; set; }

        public string TABLE_DESC { get; set; }
        [DisplayName("Attribute Name")]
        public string COLUMN_NAMES { get; set; }
        [DisplayName("Attribute Description")]
        public string COLUMN_DESC { get; set; }
        public int COLUMN_WEIGHT { get; set; }
        public string IMPORTANCE_LEVEL { get; set; }
        public string DEFAULT_VALUE { get; set; }

        public int REGEX { get; set; }
        [StringLength(20)]
        public string CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(20)]
        public string LAST_MODIFIED_BY { get; set; }

        public DateTime? LAST_MODIFIED_DATE { get; set; }

        [StringLength(1)]
        public string RECORD_STATUS { get; set; }

        public bool COLUMN_REQUIRED { get; set; }
        public int MANDATORY_LEVEL { get; set; }
        public int? COLUMN_ORDER { get; set; }
        public virtual MdmWeights MdmWeights { get; set; }
        public virtual MdmRegex MdmRegex { get; set; }

    }
}
