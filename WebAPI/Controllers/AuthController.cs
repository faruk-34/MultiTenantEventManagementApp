using Application.Interfaces;
using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<Response<UsersVM>> Register(RequestUsers request,CancellationToken cancellationToken) =>  await _authService.Register(request, cancellationToken) ;

        [HttpPost]
        public async Task<Response<LoginVM>> Login(RequestLogin request, CancellationToken cancellationToken) =>  await _authService.Login(request, cancellationToken) ;
    }
}
