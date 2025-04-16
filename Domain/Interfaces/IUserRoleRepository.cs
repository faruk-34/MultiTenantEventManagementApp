using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRoleRepository
    {
        Task  InsertUserRole(UserRole userRole,CancellationToken cancellationToken);
        Task<List<UserRole>> GetUserRolesByUserId(int userId, CancellationToken cancellationToken);
        Task<UserRole> GetUserRole(int Id, CancellationToken cancellationToken);
        Task  UpdateUserRole(UserRole userRole, CancellationToken cancellationToken);
        Task  DeleteUserRole(UserRole userRole, CancellationToken cancellationToken);
    }
}
 
