using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUsersRepository
    {
        Task<Users?> Get(int id, CancellationToken cancellationToken);
        Task<Users?> GetByEmailAsync(string email);
        Task<IEnumerable<Users>> GetAllByTenantAsync(int tenantId, CancellationToken cancellationToken);
        Task Insert(Users user);
        Task Update(Users user,CancellationToken cancellationToken);
    }
}
