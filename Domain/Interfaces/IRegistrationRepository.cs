using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRegistrationRepository
    {
        Task<List<Registration>> GetByEventIdAsync(int eventId, CancellationToken cancellationToken);
        Task<Registration> Get(int eventId, int id, CancellationToken cancellationToken);
        Task Insert(Registration registration, CancellationToken cancellationToken);
        Task Update(Registration registration, CancellationToken cancellationToken);
        Task Delete(int eventId, int id, CancellationToken cancellationToken);
    }
}
