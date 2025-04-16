using Application.Interfaces;
using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpGet("{eventId}")]
        public async Task<Response<List<RegistrationVM>>> Get(int eventId, CancellationToken cancellationToken)
        {
            return await _registrationService.Get(eventId, cancellationToken);
        }

        [HttpPost]
        public async Task<Response<RegistrationVM>> Register(RequestRegistration request, CancellationToken cancellationToken)
        {
            return await _registrationService.Register(request, cancellationToken);
        }

        [HttpPut()]
        public async Task<Response<RegistrationVM>> UpdateStatus(RequestRegistration request,  CancellationToken cancellationToken)
        {
            return await _registrationService.UpdateStatus(request,  cancellationToken);
        }

        [HttpDelete()]
        public async Task<Response<bool>> Cancel(RequestRegistration request, CancellationToken cancellationToken)
        {
            return await _registrationService.CancelAsync(request, cancellationToken);
        }
    }
}
