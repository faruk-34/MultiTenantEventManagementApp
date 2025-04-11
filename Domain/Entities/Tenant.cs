
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Tenant : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Identifier { get; set; } = null!;

        public ICollection<Users> Users { get; set; } = new List<Users>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
