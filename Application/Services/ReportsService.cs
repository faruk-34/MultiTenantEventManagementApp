using Application.Interfaces;
using Application.Models.BaseResponse;
using Application.Models.SubResponseModel;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IRegistrationRepository _registrationRepository;

        public ReportService(IEventRepository eventRepository, IRegistrationRepository registrationRepository)
        {
            _eventRepository = eventRepository;
            _registrationRepository = registrationRepository;
        }

        public async Task<Response<EventStatisticsModel>> GetEventStatistics(int eventId, CancellationToken cancellationToken)
        {
            var result = new Response<EventStatisticsModel>();

            var recEvent = await _eventRepository.Get(eventId, cancellationToken);
            if (recEvent == null)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Etkinlik bulunamadı.";
                return result;
            }

            var registrations = await _registrationRepository.GetByEventIdAsync(eventId, cancellationToken);

            var statistics = new EventStatisticsModel
            {
                EventId = recEvent.Id,
                EventTitle = recEvent.Title,
                TotalRegistrations = registrations.Count,
                Confirmed = registrations.Count(r => r.Status == RegistrationStatusEnum.Approved),
                Cancelled = registrations.Count(r => r.Status == RegistrationStatusEnum.Canceled),
                Waitlisted = registrations.Count(r => r.Status == RegistrationStatusEnum.Waitlisted)
            };

            result.IsSuccess = true;
            result.Data = statistics;
            return result;
        }

        public async Task<Response<List<UpcomingEvenModel>>> GetUpcomingEvents(CancellationToken cancellationToken)
        {
            var result = new Response<List<UpcomingEvenModel>>();

            var upcomingEvents = await _eventRepository.GetAll(cancellationToken);

            var dtoList = upcomingEvents
                .Where(e => e.DateTime >= DateTime.UtcNow) // Sadece gelecekteki etkinlikler
                .Select(e => new UpcomingEvenModel
                {
                    EventId = e.Id,
                    Title = e.Title,
                    DateTime = e.DateTime,
                    Location = e.Location,
                    Capacity = e.Capacity,
                    CurrentRegistrations = e.Registrations?.Count ?? 0
                })
                .ToList();

            result.IsSuccess = true;
            result.Data = dtoList;
            return result;
        }
    }
}
