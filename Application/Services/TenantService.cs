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
    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IMapper _mapper;

        public TenantService(ITenantRepository tenantRepository, IMapper mapper)
        {
            _tenantRepository = tenantRepository;
            _mapper = mapper;
        }

        public async Task<Response<TenantVM>> Insert(RequestTenant request,CancellationToken cancellationToken)
        {
            var result = new Response<TenantVM>();

            try
            {
                var tenant = _mapper.Map<Tenant>(request);
                await _tenantRepository.Insert(tenant, cancellationToken);
                result.Data = _mapper.Map<TenantVM>(tenant);
                result.IsSuccess = true;
                result.MessageTitle = "Tenant başarıyla oluşturuldu.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public async Task<Response<TenantVM>> Get(int id, CancellationToken cancellationToken)
        {
            var result = new Response<TenantVM>();

            try
            {
                var tenant = await _tenantRepository.Get(id,cancellationToken);

                if (tenant == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Tenant bulunamadı.";
                    return result;
                }

                result.IsSuccess = true;
                result.Data = _mapper.Map<TenantVM>(tenant);
       
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
