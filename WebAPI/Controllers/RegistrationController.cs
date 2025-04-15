using Application.Interfaces;
using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpGet]
        public async Task<Response<RegistrationVM>> Get(int eventId, CancellationToken cancellationToken)
        {
            return await _registrationService.Get(eventId, cancellationToken);
        }

        [HttpPost]
        public async Task<Response<RegistrationVM>> Register(int eventId, RequestRegistration request, CancellationToken cancellationToken)
        {
            return await _registrationService.Register(eventId, request, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<Response<RegistrationVM>> UpdateStatus(int eventId, int id,
                                                                          RegistrationStatusEnum request,
                                                                        CancellationToken cancellationToken)
        {
            return await _registrationService.UpdateStatus(eventId, id, request, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<Response<bool>> Cancel(int eventId, int id, CancellationToken cancellationToken)
        {
            return await _registrationService.CancelAsync(eventId, id, cancellationToken);
        }
    }
}
