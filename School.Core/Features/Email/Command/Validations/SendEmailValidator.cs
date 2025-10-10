using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Email.Command.Models;
using School.Core.Resources;

namespace School.Core.Features.Email.Command.Validations
{
    public class SendEmailValidator : AbstractValidator<SendEmailCommand>
    {

        private readonly IStringLocalizer<SharedResource> _stringLocalizer;


        public SendEmailValidator(IStringLocalizer<SharedResource> stringLocalizer)
        {

            _stringLocalizer = stringLocalizer;

            ApplyValidationRules();

        }

        private void ApplyValidationRules()
        {
            RuleFor(e => e.Email)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Required]);


            RuleFor(x => x.Message)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Required]);
        }
    }
}
