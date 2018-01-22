namespace CMdm.Entities.Domain.Mdm
{
    using Dqi;
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MDM_REGEX")]
    public partial class MdmRegex
    {
        public MdmRegex()
        {
            MdmDqiCatalogs = new HashSet<MdmDqCatalog>();
            //REGEX_ID,REGEX_NAME,REGEX_DESC,REGEX_STRING
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal REGEX_ID { get; set; }
        [DisplayName("Regular Expression")]
        public short? REGEX_NAME { get; set; }
        [DisplayName("Regular Expression")]
        public string REGEX_DESC { get; set; }
        public string REGEX_STRING { get; set; }
        
        public virtual ICollection<MdmDqCatalog> MdmDqiCatalogs { get; set; }
    }
}
