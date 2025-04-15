using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.SubResponseModel
{
    public class RegistrationVM
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;
        public int UserId { get; set; }
        public Users User { get; set; } = null!;
        public RegistrationStatusEnum Status { get; set; } 
    }
}
