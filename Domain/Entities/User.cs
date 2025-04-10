
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = "Attendee";

        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;

        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
