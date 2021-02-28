using FluentValidation;
using EmergencyCall.Api.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmergencyCall.Api.DTO;

namespace EmergencyCall.Api.Validators
{
    public class CreateUserResourceValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserResourceValidator()
        {
            RuleFor(a => a.FirstName)
              .NotEmpty()
              .WithMessage("Ad boş geçilemez")
              .MaximumLength(50);

            RuleFor(a => a.LastName)
              .NotEmpty()
              .WithMessage("Soyad tarihi boş geçilemez")
              .MaximumLength(50);

            RuleFor(a => a.Email)
              .NotEmpty()
              .WithMessage("Email boş geçilemez")
              .EmailAddress();

            RuleFor(a => a.PhoneNumber)
              .NotEmpty()
              .WithMessage("Telefon numarası boş geçilemez")
              .MaximumLength(50);

            RuleFor(a => a.Password)
              .NotEmpty()
              .WithMessage("Şifre boş geçilemez")
              .MaximumLength(50);
            
            RuleFor(a => a.PasswordConfirm)
              .NotEmpty()
              .Equal(b => b.Password)
              .WithMessage("Şifre tekrarı boş geçilemez")
              .MaximumLength(50);

            RuleFor(a => a.Gender)
              .NotEmpty()
              .WithMessage("Doğum tarihi boş geçilemez")
              .MaximumLength(50);

            RuleFor(a => a.BirthDate)
              .Must(p=> !(p == DateTime.MinValue))
              .WithMessage("Doğum tarihi boş geçilemez");
        }
    }
}