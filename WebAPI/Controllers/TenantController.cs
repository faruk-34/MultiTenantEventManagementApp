using Application.Interfaces;
using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpPost]
        public async Task<Response<TenantVM>> Insert(RequestTenant request,CancellationToken cancellationToken)
        {
            return await _tenantService.Insert(request,cancellationToken);
        }

        [HttpGet("id")]
        public async Task<Response<TenantVM>> Get(int id,CancellationToken cancellationToken)
        {
            return await _tenantService.Get(id,cancellationToken);
        }
    }
}
