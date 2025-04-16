using Application.Interfaces;
using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using AutoMapper;
using Domain.Interfaces;

namespace Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _userRepository;
        private readonly IMapper _mapper;

         public UsersService(IUsersRepository userRepository, IMapper mapper )
        {
            _userRepository = userRepository;
            _mapper = mapper;
          
        }
        public async Task<Response<UsersVM>> Get(int id, CancellationToken cancellationToken)
        {
            var result = new Response<UsersVM>();
 
                var user = await _userRepository.Get(id, cancellationToken);

                if (user == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Kullanıcı bulunamadı!";
                    return result;
                }

                var userVm = _mapper.Map<UsersVM>(user);
                result.IsSuccess = true;
                result.Data = userVm;
 
                return result;
 
        }
        public async Task<Response<UsersVM>> Update(RequestUsers request, CancellationToken cancellationToken)
        {
            var result = new Response<UsersVM>();
 
                var user = await _userRepository.Get(request.Id, cancellationToken);

                if (user == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Kullanıcı bulunamadı!";
                    return result;
                }

                _mapper.Map(request, user);

                await _userRepository.Update(user, cancellationToken);

                var userVm = _mapper.Map<UsersVM>(user);
                result.IsSuccess = true;
                result.Data = userVm;
                result.MessageTitle = "Kullanıcı başarıyla güncellendi.";

                return result;             
        }
        public async Task<IEnumerable<UsersVM>> GetAllByTenantAsync(int tenantId, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllByTenantAsync(tenantId, cancellationToken);

            return users.Select(user => new UsersVM
            {
                Id = user.Id,
                Email = user.Email
            }).ToList();
        }
    }
}
