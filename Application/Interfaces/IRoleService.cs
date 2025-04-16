using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;

namespace Application.Interfaces
{
    public interface IRoleService
    {
        Task<Response<RoleVM>> Insert(RequestRole request, CancellationToken cancellationToken);
        Task<Response<RoleVM>> GetById (int id, CancellationToken cancellationToken);
        Task<Response<List<RoleVM>>> GetAll (CancellationToken cancellationToken);
        Task<Response<RoleVM>> Update (RequestRole request, CancellationToken cancellationToken);
        Task<Response<bool>> Delete (int id, CancellationToken cancellationToken);
    }
}
