using Application.Interfaces;
using Application.Models.BaseResponse;
using Application.Models.SubResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("{eventId}")]
        public async Task<Response<EventStatisticsModel>> GetEventStatistics(int eventId, CancellationToken cancellationToken)
        {
            var response = await _reportService.GetEventStatistics(eventId, cancellationToken);
            return response;
        }

        [HttpGet()] 
        public async Task<Response<List<UpcomingEvenModel>>> GetUpcomingEvents(CancellationToken cancellationToken)
        {
            var response = await _reportService.GetUpcomingEvents(cancellationToken);
            return response;
        }
    }
}

 