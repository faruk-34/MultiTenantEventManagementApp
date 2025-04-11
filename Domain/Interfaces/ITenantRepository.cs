using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITenantRepository
    {
        Task<Tenant> Get(int id, CancellationToken cancellationToken);
        Task Insert(Tenant tenant, CancellationToken cancellationToken);
        Task<IEnumerable<Tenant>> GetAll();
        Task Update(Tenant tenant);
        Task Delete(Tenant tenant);
    }

}
