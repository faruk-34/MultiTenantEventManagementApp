using Application.Interfaces;
using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<Response<UsersVM>> Get(int id, CancellationToken cancellationToken) => await _userService.Get(id, cancellationToken);

        [HttpPut]
        public async Task<Response<UsersVM>> Update(RequestUsers request, CancellationToken cancellationToken) => await _userService.Update(request, cancellationToken);


    }
}
