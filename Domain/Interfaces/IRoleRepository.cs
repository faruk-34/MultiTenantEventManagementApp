using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{

    public interface IRoleRepository
    {
        Task  Insert(Role request,CancellationToken cancellationToken);
        Task<Role> GetById(int id,CancellationToken cancellationToken);
        Task<List<Role>> GetAll(CancellationToken cancellationToken);
        Task  Update(Role request, CancellationToken cancellationToken);
    }
}
