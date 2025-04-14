using Application.Interfaces;
using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;
using System.Threading;

namespace Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<Response<RoleVM>> Insert(RequestRole request, CancellationToken cancellationToken)
        {
            var result = new Response<RoleVM>();

            try
            {
                var role = _mapper.Map<Role>(request);
                await _roleRepository.Insert(role, cancellationToken);
                result.IsSuccess = true;
                result.Data = _mapper.Map<RoleVM>(role);

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;

            }
            return result;

        }

        public async Task<Response<RoleVM>> GetById(int id, CancellationToken cancellationToken)
        {
            var result = new Response<RoleVM>();

            try
            {
                var role = await _roleRepository.GetById(id, cancellationToken);
                if (role == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Role bulunamadı.";
                    return result;
                }

                result.IsSuccess = true;
                result.Data = _mapper.Map<RoleVM>(role); ;

                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
                return result;
            }
        }

        public async Task<Response<List<RoleVM>>> GetAll(CancellationToken cancellationToken)
        {
            var result = new Response<List<RoleVM>>();



            try
            {
                var events = await _roleRepository.GetAll(cancellationToken);
                result.IsSuccess = true;
                result.Data = _mapper.Map<List<RoleVM>>(events);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;

        }

        public async Task<Response<RoleVM>> Update(RequestRole request, CancellationToken cancellationToken)
        {
            var result = new Response<RoleVM>();

            try
            {
                if (request == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Role cannot be null.";
                    return result;
                }

                var existingRole = await _roleRepository.GetById(request.Id, cancellationToken);
                if (existingRole == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Role not found.";
                    return result;
                }

                existingRole.Name = request.Name;
                await _roleRepository.Update(existingRole, cancellationToken);
                result.IsSuccess = true;
                result.Data = _mapper.Map<RoleVM>(existingRole);
                result.ErrorMessage = "Role updated successfully.";
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"An error occurred while updating the role: {ex.Message}";
                return result;
            }
        }

        public async Task<Response<bool>> Delete(int id, CancellationToken cancellationToken)
        {
            var result = new Response<bool>();

            try
            {
                var role = await _roleRepository.GetById(id, cancellationToken);
                if (role == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Role not found.";
                    return result;
                }

                role.IsDeleted = true;  
                await _roleRepository.Update(role, cancellationToken);
                result.IsSuccess = true;
                result.Data = true;
                result.MessageTitle = "Rol başarıyla silindi.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"An error occurred while deleting the role: {ex.Message}";

            }
            return result;
        }
    }
}
