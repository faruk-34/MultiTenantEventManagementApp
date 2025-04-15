
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Event : BaseEntity,ISoftDeletable
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public EventStatusEnum Status { get; set; }  // Pending, Active, Completed
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } 
        public List<Registration> Registrations { get; set; } 
    }
}
 
