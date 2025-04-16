using Application.Models.BaseResponse;
using Application.Models.SubResponseModel;

namespace Application.Interfaces
{
    public interface IReportService
    {
        Task<Response<EventStatisticsModel>> GetEventStatistics(int eventId,CancellationToken cancellationToken);
        Task<Response<List<UpcomingEvenModel>>>  GetUpcomingEvents(CancellationToken cancellationToken);
    }
}
