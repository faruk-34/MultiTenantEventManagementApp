
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Registration : BaseEntity,  ISoftDeletable
    {
        public int EventId { get; set; }
        public Event Event { get; set; }  
        public int UserId { get; set; }
        public Users User { get; set; }  
      //  public RegistrationStatus Status { get; set; } // Enum: Approved, Canceled, Waitlisted
    }
}
