using Application.Interfaces;
using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public RegistrationService(
            IRegistrationRepository registrationRepository,
            IEventRepository eventRepository,
            IMapper mapper)
        {
            _registrationRepository = registrationRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<RegistrationVM>>> Get(int eventId, CancellationToken cancellationToken)
        {
            var eventExist = await _eventRepository.Get(eventId, cancellationToken);

            if (eventExist == null)
            {
                return new Response<List<RegistrationVM>>
                {
                    IsSuccess = false,
                    ErrorMessage = "Etkinlik bulunamadı."
                };
            }

            var registrations = await _registrationRepository.GetByEventIdAsync(eventId, cancellationToken);

            var registrationVMs = _mapper.Map<List<RegistrationVM>>(registrations);

            return new Response<List<RegistrationVM>>
            {
                IsSuccess = true,
                Data = registrationVMs
            };
        }


        public async Task<Response<RegistrationVM>> Register(RequestRegistration request, CancellationToken cancellationToken)
        {
            var result = new Response<RegistrationVM>();

            var eventExist = await _eventRepository.Get(request.EventId, cancellationToken);
            if (eventExist == null)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Etkinlik bulunamadı.";
                return result;
            }

            if (eventExist.Capacity <= eventExist.Registrations.Count)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Etkinlik kapasitesi dolmuş.";
                return result;
            }

            var registration = _mapper.Map<Registration>(request);
            await _registrationRepository.Insert(registration, cancellationToken);

            result.IsSuccess = true;
            result.Data = _mapper.Map<RegistrationVM>(registration);
            result.MessageTitle = "Kayıt başarılı.";

            return result;
        }

        public async Task<Response<RegistrationVM>> UpdateStatus(RequestRegistration request, CancellationToken cancellationToken)
        {
            var result = new Response<RegistrationVM>();

            var registration = await _registrationRepository.Get(request.EventId, request.Id, cancellationToken);
            if (registration == null || registration.EventId != request.EventId)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Kayıt bulunamadı.";
                return result;
            }

            registration.Status = request.Status;
            await _registrationRepository.Update(registration, cancellationToken);

            result.IsSuccess = true;
            result.Data = _mapper.Map<RegistrationVM>(registration);
            result.MessageTitle = "Kayıt durumu güncellendi.";


            return result;
        }
        public async Task<Response<bool>> CancelAsync(RequestRegistration request, CancellationToken cancellation)
        {
            var result = new Response<bool>();

            var registration = await _registrationRepository.Get(request.EventId, request.Id, cancellation);
            if (registration == null || registration.EventId != request.EventId)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Kayıt bulunamadı.";
                return result;
            }

            registration.Status = RegistrationStatusEnum.Canceled;
            await _registrationRepository.Update(registration, cancellation);
            result.IsSuccess = true;
            result.Data = true;
            result.MessageTitle = "Kayıt iptal edildi.";

            return result;
        }
    }
}
