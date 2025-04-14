using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Role : BaseEntity, ISoftDeletable
    {
        public string Name{ get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
