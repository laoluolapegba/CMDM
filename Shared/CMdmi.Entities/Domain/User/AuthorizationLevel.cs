using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Entities.Domain.User
{
    public enum AuthorizationLevel
    {
        Branch = 1,
        Zonal = 2,
        Regional = 3,
        Enterprise = 4
    }
    public class AuthorizationLevelModel
    {
        public int Value { get; set; }
        public string Name { get; set; }
    }

}
