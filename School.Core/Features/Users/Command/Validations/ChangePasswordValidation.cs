using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Users.Command.Models;
using School.Core.Resources;

namespace School.Core.Features.Users.Command.Validations
{
    public class ChangePasswordValidation : AbstractValidator<ChangePasswordCommand>
    {
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public ChangePasswordValidation(IStringLocalizer<SharedResource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
            CustomValidation();
        }
        private void ApplyValidationRules()
        {
            RuleFor(s => s.ID)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Required]);

            RuleFor(s => s.CurrentPassword)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Required]);


            RuleFor(s => s.NewPassword)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Required]);


            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage(_stringLocalizer[SharedResourcesKey.PasswordNotEqualConfirmPass]);
        }

        private void CustomValidation()
        {


        }
    }
}
