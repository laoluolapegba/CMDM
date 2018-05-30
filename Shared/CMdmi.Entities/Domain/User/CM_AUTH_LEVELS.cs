using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.User
{
    [Table("CM_AUTH_LEVELS")]
    public class CM_AUTH_LEVEL
    {
                   

            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int LEVEL_ID { get; set; }
            [DisplayName("Level Name")]
            public string LEVEL_NAME { get; set; }


        }
    }
