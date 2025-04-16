using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IEventService
    {
        Task<Response<List<EventVM>>> GetAll ( CancellationToken cancellationToken);
        Task<Response<List<EventVM>>> GetAllFilter(EventFilterVM filter, CancellationToken cancellationToken);
         Task<Response<EventVM>> Get (int id,CancellationToken cancellationToken);
        Task<Response<EventVM>> Insert(RequestEvent request,CancellationToken cancellationToken);
        Task<Response<EventVM>> Update ( RequestEvent request, CancellationToken cancellationToken);
        Task<Response<bool>> Delete (int id, CancellationToken cancellationToken);
    }
}
