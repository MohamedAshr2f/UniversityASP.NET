using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Users.Command.Models;
using School.Core.Resources;

namespace School.Core.Features.Users.Command.Validations
{
    public class EditUserValidations : AbstractValidator<EditUserCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResource> _localizer;
        #endregion

        #region Constructors
        public EditUserValidations(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Handle Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.FullName)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKey.Required])
                 .MaximumLength(100).WithMessage(_localizer[SharedResourcesKey.MaxLengthis100]);
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKey.Required])
                .MaximumLength(100).WithMessage(_localizer[SharedResourcesKey.MaxLengthis100]);
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);
        }

        public void ApplyCustomValidationsRules()
        {

        }

        #endregion
    }
}


