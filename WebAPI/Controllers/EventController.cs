using Application.Interfaces;
using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }


        [HttpGet()]
        public async Task<Response<List<EventVM>>> GetAllFilteredEvents([FromQuery] EventFilterVM filter, CancellationToken cancellationToken)
        {
            var response = await _eventService.GetAllFilter(filter, cancellationToken);

            return response;
        }

        [HttpGet("{id}")]
        public async Task<Response<EventVM>> Get(int id,CancellationToken cancellationToken)
        {
            var response = await _eventService.Get(id,cancellationToken);
            return response;
        }

        [HttpPost]
        public async Task<Response<EventVM>> Insert(RequestEvent request,CancellationToken cancellationToken)
        {
            var response = await _eventService.Insert(request,cancellationToken);
            return response;
        }

        [HttpPut()]
        public async Task<Response<EventVM>> Update( RequestEvent request,CancellationToken cancellationToken)
        {
            var response = await _eventService.Update( request, cancellationToken);
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<Response<bool>> Delete(int id,CancellationToken cancellationToken)
        {
            var response = await _eventService.Delete(id, cancellationToken);
            return response;
        }
    }

}
