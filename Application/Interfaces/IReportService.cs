using Application.Models.BaseResponse;
using Application.Models.SubResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IReportService
    {
        Task<Response<EventStatisticsModel>> GetEventStatistics(int eventId,CancellationToken cancellationToken);
        Task<Response<List<UpcomingEvenModel>>>  GetUpcomingEvents(CancellationToken cancellationToken);
    }
}
