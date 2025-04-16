using Application.Interfaces;
using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpPost]
        public async Task<Response<UserRoleVM>> InsertUserRole(RequestUserRole request,CancellationToken cancellationToken )
        {
            return await _userRoleService.InsertUserRole(request,cancellationToken);
        }

        [HttpGet("{userId}")]
        public async Task<List<UserRoleVM>> GetUserRoles(int userId, CancellationToken cancellationToke)
        {
            return await _userRoleService.GetUserRolesByUserId(userId, cancellationToke);
        }

        [HttpPut()]
        public async Task<Response<UserRoleVM>> UpdateUserRole(RequestUserRole request, CancellationToken cancellationToke)
        {
            return await _userRoleService.UpdateUserRole(request,cancellationToke);
        }

        [HttpDelete()]
        public async Task<Response<bool>> DeleteUserRole(RequestUserRole request, CancellationToken cancellationToke)
        {
            return await _userRoleService.DeleteUserRole(request, cancellationToke);
        }
    }
}
