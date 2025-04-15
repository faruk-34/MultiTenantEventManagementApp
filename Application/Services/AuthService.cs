using Application.Interfaces;
using Application.Models.BaseResponse;
using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using Application.Validator;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IUsersRepository usersRepository, IMapper mapper,
             AppDbContext context,
             IJwtTokenGenerator jwtTokenGenerator)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _jwtTokenGenerator = jwtTokenGenerator;
            _context = context;
        }

        public async Task<Response<UsersVM>> Register(RequestUsers request, CancellationToken cancellationToken)
        {
            var result = new Response<UsersVM>();

            try
            {
                RegisterRequestValidator validator = new RegisterRequestValidator(); // Fluent Validation
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return result;
                }


                bool userNameExists = await _context.Users.AnyAsync(p => p.Username == request.Username);
                if (userNameExists)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Kullanıcı adı daha önce kayıt edilmiş";
                    return result;
                }

                bool emailExists = await _context.Users.AnyAsync(p => p.Email == request.Email);
                if (emailExists)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Email adresi daha önce kayıt edilmiş!";
                    return result;
                }

                // Şifre  hashleniyor
                request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

                Users user = _mapper.Map<Users>(request);

                if (string.IsNullOrEmpty(user.PasswordHash))
                    user.PasswordHash = request.Password;

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                result.IsSuccess = true;
                result.ErrorMessage = "Kullanıcı başarıyla kaydedildi!";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;

            }
            return result;
        }
        public async Task<Response<LoginVM>> Login(RequestLogin request, CancellationToken cancellationToken)
        {
            var result = new Response<LoginVM>();

            //try
            //{

                LoginRequestValidator validator = new LoginRequestValidator(); // Fluent Validation
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return result;
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
                if (user == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Kullanıcı bulunamadı!";
                    return result;
                }
                

                // Şifre doğrulama
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
                if (!isPasswordValid)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Şifre hatalı!";
                    return result;
                }

                // Token üretimi
                var token = _jwtTokenGenerator.GenerateToken(user);

                var loginVm = new LoginVM
                {
                    Token = token,
                };

                result.Data = loginVm;
                result.IsSuccess = true;
                result.MessageTitle = "Kullanıcı girişi başarılı!" ;

                return result;
            }
            //catch (Exception ex)
            //{
            //    result.IsSuccess = false;
            //    result.ErrorMessage = ex.Message;
            //    return result;
            //}
        }

    }
 
