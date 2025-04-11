using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAll( CancellationToken cancellationToken);
        Task<Event> Get (int id, CancellationToken cancellationToken);
        Task Insert(Event eventEntity, CancellationToken cancellationToken);
        Task Update (Event eventEntity, CancellationToken cancellationToken);
        Task Delete (int id, CancellationToken cancellationToken);
    }
}
