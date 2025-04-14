using Application.Interfaces;
using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<Response<RoleVM>> Insert(RequestRole request,CancellationToken cancellationToken)
        {
            var response = await _roleService.Insert(request,cancellationToken);
            return response;
        }

        [HttpGet("{id}")]
        public async Task<Response<RoleVM>> GetById(int id, CancellationToken cancellationToken)
        {
            var response = await _roleService.GetById(id,cancellationToken);
            return response;
        }

        [HttpGet]
        public async Task<Response<List<RoleVM>>> GetAll( CancellationToken cancellationToken)
        {
            var response = await _roleService.GetAll(cancellationToken);
            return response;
        }

        [HttpPut]
        public async Task<Response<RoleVM>> Update(RequestRole request,CancellationToken cancellationToken)
        {
            var response = await _roleService.Update(request,cancellationToken);
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<Response<bool>> Delete(int id,CancellationToken cancellationToken)
        {
            var response = await _roleService.Delete(id,cancellationToken);
            return response;
        }
    }
}
