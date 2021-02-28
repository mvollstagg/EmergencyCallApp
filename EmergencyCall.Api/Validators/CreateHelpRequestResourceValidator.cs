using FluentValidation;
using EmergencyCall.Api.DTO.HelpRequestDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmergencyCall.Api.DTO;

namespace EmergencyCall.Api.Validators
{
    public class CreateHelpRequestResourceValidator : AbstractValidator<CreateHelpRequestDTO>
    {
        public CreateHelpRequestResourceValidator()
        {
            RuleFor(a => a.Location)
              .NotEmpty()
              .WithMessage("Lokasyon boş geçilemez")
              .MaximumLength(50);

            RuleFor(a => a.Details)
              .MaximumLength(250);

            RuleFor(a => a.UserId)
              .NotEmpty()
              .WithMessage("UserId boş geçilemez");

            RuleFor(a => a.RecordedAtDate)
              .Must(p=> !(p == DateTime.MinValue))
              .WithMessage("Kayıt tarihi boş geçilemez");
        }
    }
}