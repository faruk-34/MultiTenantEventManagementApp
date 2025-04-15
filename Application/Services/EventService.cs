using Application.Interfaces;
using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Redis;

namespace Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IRedisService _redis;

        public EventService(IEventRepository eventRepository, IMapper mapper, IRedisService redisService)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _redis = redisService;
        }

        #region Etkinlik Detay Catchcleme (Redis)
        public async Task<EventVM?> GetEventByIdAsync(int id, CancellationToken cancellationToken)
        {
            // 1. Redis önbelleğinde varsa onu al
            var cacheKey = $"event:{id}";
            var cached = await _redis.GetAsync<EventVM>(cacheKey);
            if (cached != null)
                return cached;

            // 2. Veritabanından etkinlik verisini al
            var evt = await _eventRepository.Get(id, cancellationToken);
            if (evt == null)
                return null;

            // 3. Entity'den ViewModel'e (EventVM) dönüştür
            var dto = new EventVM
            {
                Id = evt.Id,
                Title = evt.Title,
                Description = evt.Description,
                //  StartDateTime = evt.StartDateTime,
                Location = evt.Location,
                Capacity = evt.Capacity,
                //  Status = evt.Status,
                TenantId = evt.TenantId,
                Tenant = evt.Tenant
            };

            // 4. Redis'e cache'le (10 dakika süreyle)
            await _redis.SetAsync(cacheKey, dto, TimeSpan.FromMinutes(10));

            // 5. Sonuç olarak dto’yu dön
            return dto;
        }
        #endregion

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
                await _eventRepository.Update(eventExist, cancellationToken);

                ///cache boşalt
                var cacheKey = $"event:{request.Id}";
                var cached = await _redis.GetAsync<EventVM>(cacheKey);
                if (cached != null)
                    await _redis.RemoveAsync(cacheKey);


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

        public async Task<Response<bool>> Delete(int id, CancellationToken cancellationToken)
        {
            var result = new Response<bool>();

            try
            {
                var eventExist = await _eventRepository.Get(id, cancellationToken);
                if (eventExist == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Etkinlik bulunamadı.";
                    return result;
                }

                eventExist.IsDeleted = true;
                await _eventRepository.Update(eventExist, cancellationToken);
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
