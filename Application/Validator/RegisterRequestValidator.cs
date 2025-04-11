using Application.Models.SubRequestModel;
using FluentValidation;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validator
{
    public class RegisterRequestValidator : AbstractValidator<RequestUsers>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Firstname).NotEmpty().WithMessage("Ad alanı boş olamaz");
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6);
        }
    }
}
