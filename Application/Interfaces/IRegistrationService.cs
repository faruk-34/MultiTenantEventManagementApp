using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;

namespace Application.Interfaces
{
    public interface   IRegistrationService
    {
        Task<Response<RegistrationVM>> Get(int eventId,CancellationToken cancellation);
        Task<Response<RegistrationVM>> Register(int eventId, RequestRegistration request,CancellationToken cancellationToken);
        Task<Response<RegistrationVM>> UpdateStatusAsync(int eventId, int registrationId
            // , RequestUpdateStatus request 
            //   RequestUpdateStatus request 
            //   public enum RegistrationStatus
            //   Pending = 1,   // Beklemede
            //   Confirmed = 2, // Onaylı
            //  Canceled = 3   // İptal Edildi
            , CancellationToken cancellationToken);
        Task<Response<bool>> CancelAsync(int eventId, int registrationId, CancellationToken cancellationToken);
    }
}
