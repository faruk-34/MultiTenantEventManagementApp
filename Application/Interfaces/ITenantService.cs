using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;

namespace Application.Interfaces
{
    public interface ITenantService
    {
        Task<Response<TenantVM>> Insert(RequestTenant request, CancellationToken cancellationToken);
        Task<Response<TenantVM>> Get(int id, CancellationToken cancellationToken);
    }
}
