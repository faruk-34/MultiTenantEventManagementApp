using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;

namespace Application.Interfaces
{
    public interface IUserRoleService
    {
        Task<Response<UserRoleVM>> InsertUserRole(RequestUserRole request, CancellationToken cancellationToken);
        Task<List<UserRoleVM>> GetUserRolesByUserId(int userId, CancellationToken cancellationToken);
        Task<Response<UserRoleVM>> UpdateUserRole(RequestUserRole request, CancellationToken cancellationToken);
        Task<Response<bool>> DeleteUserRole(RequestUserRole request, CancellationToken cancellationToken);
    }
}
