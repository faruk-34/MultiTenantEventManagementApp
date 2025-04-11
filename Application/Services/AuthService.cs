using Application.Interfaces;
using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using Application.Validator;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;



namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<Users> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IUsersRepository usersRepository, IMapper mapper, UserManager<Users> userManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Response<UsersVM>> Register(RequestUsers request, CancellationToken cancellationToken)
        {
            var result = new Response<UsersVM>();

            try
            {
                RegisterRequestValidator validator = new RegisterRequestValidator();
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return result;
                }


                bool userNameExists = await _userManager.Users.AnyAsync(p => p.UserName == request.UserName);
                if (userNameExists)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage =  "Kullanıcı adı daha önce kayıt edilmiş" ;
                    return result;
                }

                bool emailExists = await _userManager.Users.AnyAsync(p => p.Email == request.Email);
                if (emailExists)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Email adresi daha önce kayıt edilmiş!" ;
                    return result;
                }

                Users user = _mapper.Map<Users>(request);

                var identityResult = await _userManager.CreateAsync(user, request.Password);
                if (identityResult.Succeeded)
                {

                    result.IsSuccess = true;
                    result.MessageTitle = "Kullanıcı kaydı başarılı!";
                    return result;
                }

            }
            catch (Exception)
            {
                result.IsSuccess = false;
                result.ErrorMessage =  "İşlem sırasında bir hata oluştu!" ;
                return result;
            }
            return result;
        }
        public async Task<Response<UsersVM>> Login(RequestUsers request,CancellationToken cancellationToken)
        {
            var result = new Response<UsersVM>();

            try
            {
                var user = await _usersRepository.GetByEmailAsync(request.Email);

                if (user == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Kullanıcı bulunamadı!";
                    return result;
                }

                var passwordCheck = await _userManager.CheckPasswordAsync(user, request.Password);

                if (!passwordCheck)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Kullanıcı girişi başarısız!";
                    return result;
                }

               
                var userVm = _mapper.Map<UsersVM>(user);
                userVm.Token = _jwtTokenGenerator.GenerateToken();  

                result.IsSuccess = true;
                result.MessageTitle = "Kullanıcı girişi başarılı!";
                result.Data = userVm;

                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
                return result;
            }
        }

    }
}
