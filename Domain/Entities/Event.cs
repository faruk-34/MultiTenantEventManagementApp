
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Event : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDateTime { get; set; }
        public string Location { get; set; } = null!;
        public int Capacity { get; set; }
        public string Status { get; set; } = "Scheduled";
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
