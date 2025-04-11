
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Users : BaseEntity
    {
        public string Firstname { get; set; } = null!;
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int TenantId { get; set; }

    }
}
