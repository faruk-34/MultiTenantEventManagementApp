using Application.Interfaces;
using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<Response<RegistrationVM>> Get(int eventId, CancellationToken cancellationToken)
        {
            var result = new Response<RegistrationVM>();

            try
            {
                var eventExist = await _eventRepository.Get(eventId, cancellationToken);
                if (eventExist == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Etkinlik bulunamadı.";
                    return result;
                }

                var registrations = await _registrationRepository.GetByEventIdAsync(eventId, cancellationToken);
                result.IsSuccess = true;
                result.Data = _mapper.Map<RegistrationVM>(registrations);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public async Task<Response<RegistrationVM>> Register(int eventId, RequestRegistration request, CancellationToken cancellationToken)
        {
            var result = new Response<RegistrationVM>();

            try
            {
                var eventExist = await _eventRepository.Get(eventId, cancellationToken);
                if (eventExist == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Etkinlik bulunamadı.";
                    return result;
                }

                // Check if the event is full
                if (eventExist.Capacity <= eventExist.Registrations.Count)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Etkinlik kapasitesi dolmuş.";
                    return result;
                }

                var registration = _mapper.Map<Registration>(request);
                registration.EventId = eventId;
                await _registrationRepository.Insert(registration, cancellationToken);

                result.IsSuccess = true;
                result.Data = _mapper.Map<RegistrationVM>(registration);
                result.MessageTitle = "Kayıt başarılı.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public async Task<Response<RegistrationVM>> UpdateStatusAsync(int eventId, int registrationId,
                                                                                // RequestUpdateStatus request, 
                                                                                CancellationToken cancellationToken)
        {
            var result = new Response<RegistrationVM>();

            try
            {
                var registration = await _registrationRepository.Get(eventId,registrationId, cancellationToken);
                if (registration == null || registration.EventId != eventId)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Kayıt bulunamadı.";
                    return result;
                }

               // registration.Status = request.Status;
                await _registrationRepository.Update(registration, cancellationToken);

                result.IsSuccess = true;
                result.Data = _mapper.Map<RegistrationVM>(registration);
                result.MessageTitle = "Kayıt durumu güncellendi.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public async Task<Response<bool>> CancelAsync(int eventId, int registrationId, CancellationToken cancellation)
        {
            var result = new Response<bool>();

            try
            {
                var registration = await _registrationRepository.Get(eventId,registrationId, cancellation);
                if (registration == null || registration.EventId != eventId)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Kayıt bulunamadı.";
                    return result;
                }

                await _registrationRepository.Delete(eventId, registrationId, cancellation);
                result.IsSuccess = true;
                result.Data = true;
                result.MessageTitle = "Kayıt iptal edildi.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }

}
