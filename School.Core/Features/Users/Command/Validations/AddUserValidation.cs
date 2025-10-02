using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Users.Command.Models;
using School.Core.Resources;

namespace School.Core.Features.Users.Command.Validations
{
    public class AddUserValidation : AbstractValidator<AddUserCommand>
    {
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public AddUserValidation(IStringLocalizer<SharedResource> stringLocalizer)
        {

            _stringLocalizer = stringLocalizer;

            ApplyValidationRules();
            CustomValidation();
        }

        private void ApplyValidationRules()
        {
            RuleFor(s => s.FullName)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Required])
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKey.MaxLengthis100]);

            RuleFor(s => s.UserName)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Required])
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKey.MaxLengthis100]);

            RuleFor(s => s.Email)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Required]);


            RuleFor(s => s.Password)
               .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Required]);


            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage(_stringLocalizer[SharedResourcesKey.PasswordNotEqualConfirmPass]);
        }

        private void CustomValidation()
        {


        }
    }
}
