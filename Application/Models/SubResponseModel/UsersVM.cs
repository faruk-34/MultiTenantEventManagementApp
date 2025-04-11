
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.SubResponseModel
{
    public class UsersVM
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = null!;
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string TenantName { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
