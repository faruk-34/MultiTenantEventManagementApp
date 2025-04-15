using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface   IRegistrationService
    {
        Task<Response<RegistrationVM>> Get(int eventId,CancellationToken cancellation);
        Task<Response<RegistrationVM>> Register(int eventId, RequestRegistration request,CancellationToken cancellationToken);
        Task<Response<RegistrationVM>> UpdateStatus(int eventId, int registrationId,
                                                                                  RegistrationStatusEnum request,
                                                                                CancellationToken cancellationToken);
        Task<Response<bool>> CancelAsync(int eventId, int registrationId, CancellationToken cancellationToken);
    }
}
