
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Tenant : BaseEntity, ISoftDeletable
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Users> Users { get; set; }  
        public List<Event> Events { get; set; }  
    }
}
