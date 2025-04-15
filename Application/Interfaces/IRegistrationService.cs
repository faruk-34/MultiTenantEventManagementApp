using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface   IRegistrationService
    {
        Task<Response<List<RegistrationVM>>> Get(int eventId,CancellationToken cancellation);
        Task<Response<RegistrationVM>> Register( RequestRegistration request,CancellationToken cancellationToken);
        Task<Response<RegistrationVM>> UpdateStatus(RequestRegistration request,   CancellationToken cancellationToken);
        Task<Response<bool>> CancelAsync(RequestRegistration request, CancellationToken cancellationToken);
    }
}
