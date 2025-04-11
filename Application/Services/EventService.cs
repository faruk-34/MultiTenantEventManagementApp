using Application.Interfaces;
using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<EventVM>>> GetAll(CancellationToken cancellationToken)
        {
            var result = new Response<List<EventVM>>();

            try
            {
                var events = await _eventRepository.GetAll(cancellationToken);
                result.IsSuccess = true;
                result.Data = _mapper.Map<List<EventVM>>(events);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public async Task<Response<EventVM>> Get(int id, CancellationToken cancellationToken)
        {
            var result = new Response<EventVM>();

            try
            {
                var eventExist = await _eventRepository.Get(id, cancellationToken);
                if (eventExist == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Etkinlik bulunamadı.";
                    return result;
                }

                result.IsSuccess = true;
                result.Data = _mapper.Map<EventVM>(eventExist);
                result.MessageTitle = "Etkinlik başarıyla getirildi.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public async Task<Response<EventVM>> Insert(RequestEvent request, CancellationToken cancellationToken)
        {
            var result = new Response<EventVM>();

            try
            {
                var eventEntity = _mapper.Map<Event>(request);
                await _eventRepository.Insert(eventEntity, cancellationToken);

                result.IsSuccess = true;
                result.Data = _mapper.Map<EventVM>(eventEntity);
                result.MessageTitle = "Etkinlik başarıyla oluşturuldu.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public async Task<Response<EventVM>> Update(RequestEvent request, CancellationToken cancellationToken)
        {
            var result = new Response<EventVM>();

            try
            {
                var eventExist = await _eventRepository.Get(request.Id, cancellationToken);
                if (eventExist == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Etkinlik bulunamadı.";
                    return result;
                }

                _mapper.Map(request, eventExist);
                await _eventRepository.Update(eventExist,cancellationToken);

                result.IsSuccess = true;
                result.Data = _mapper.Map<EventVM>(eventExist);
                result.MessageTitle = "Etkinlik başarıyla güncellendi.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public async Task<Response<bool>> Delete(int id,CancellationToken cancellationToken)
        {
            var result = new Response<bool>();

            try
            {
                var eventExist = await _eventRepository.Get(id,cancellationToken);
                if (eventExist == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Etkinlik bulunamadı.";
                    return result;
                }

                await _eventRepository.Delete(eventExist.Id,cancellationToken);

                result.IsSuccess = true;
                result.Data = true;
                result.MessageTitle = "Etkinlik başarıyla silindi.";
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
