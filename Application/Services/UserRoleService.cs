using Application.Interfaces;
using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;

        public UserRoleService(IUserRoleRepository userRoleRepository, IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }

        public async Task<Response<UserRoleVM>> InsertUserRole(RequestUserRole request, CancellationToken cancellationToken)
        {
            var result = new Response<UserRoleVM>();

            var userRole = _mapper.Map<UserRole>(request);
            await _userRoleRepository.InsertUserRole(userRole, cancellationToken);

            result.IsSuccess = true;
            result.Data = _mapper.Map<UserRoleVM>(userRole);

            return result;
        }

        public async Task<List<UserRoleVM>> GetUserRolesByUserId(int userId, CancellationToken cancellationToken)
        {
            var userRoles = await _userRoleRepository.GetUserRolesByUserId(userId, cancellationToken);
            return userRoles.Select(role => new UserRoleVM
            {
                UserId = role.UserId,
                RoleId = role.RoleId
            }).ToList();
        }

        public async Task<Response<UserRoleVM>> UpdateUserRole(RequestUserRole request, CancellationToken cancellationToken)
        {
            var result = new Response<UserRoleVM>();

                var userRoleExist = await _userRoleRepository.GetUserRole(request.Id, cancellationToken);
                if (userRoleExist == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Kayıt bulunamadı.";
                    return result;
                }
                _mapper.Map(request, userRoleExist);
                await _userRoleRepository.UpdateUserRole(userRoleExist, cancellationToken);

                result.IsSuccess = true;
                result.Data = _mapper.Map<UserRoleVM>(userRoleExist);
                result.MessageTitle = "UserRole başarıyla güncellendi.";


            return result;
        }

        public async Task<Response<bool>> DeleteUserRole(RequestUserRole request, CancellationToken cancellationToken)
        {
            var result = new Response<bool>();

        
                var userRoleExist = await _userRoleRepository.GetUserRole(request.Id, cancellationToken);
                if (userRoleExist == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Kayıt bulunamadı.";
                    return result;
                }
 
                userRoleExist.IsDeleted = true;
                await _userRoleRepository.UpdateUserRole(userRoleExist, cancellationToken);
                result.IsSuccess = true;
                result.Data = true;
                result.MessageTitle = "Kayıt başarıyla silindi.";

            return result;
        }
    }
}
