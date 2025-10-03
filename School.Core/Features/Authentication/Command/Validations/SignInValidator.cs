using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Authentication.Command.Models;
using School.Core.Resources;

namespace School.Core.Features.Authentication.Command.Validations
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public SignInValidator(IStringLocalizer<SharedResource> stringLocalizer)
        {

            _stringLocalizer = stringLocalizer;

            ApplyValidationRules();
            CustomValidation();
        }

        private void ApplyValidationRules()
        {

            RuleFor(s => s.UserName)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Required]);

            RuleFor(s => s.Password)
               .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Required]);

        }

        private void CustomValidation()
        {


        }
    }
}
