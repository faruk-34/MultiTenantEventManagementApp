using Application.Models.SubRequestModel;
using FluentValidation;

namespace Application.Validator
{
    public class LoginRequestValidator : AbstractValidator<RequestLogin>
    {
        public LoginRequestValidator()
        {
      
            RuleFor(x => x.Password).MinimumLength(3);
        }
    }
}
