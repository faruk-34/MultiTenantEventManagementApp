using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.SubResponseModel
{
    public class TenantVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
         public string Address { get; set; }

        //public ICollection<User> Users { get; set; } = new List<User>();
        //public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
