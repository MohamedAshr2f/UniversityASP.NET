using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Authentication.Command.Models;
using School.Core.Resources;

namespace School.Core.Features.Authentication.Command.Validations
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResource> _localizer;
        #endregion

        #region Constructors
        public ResetPasswordValidator(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Handle Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);
            RuleFor(x => x.Password)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);
            RuleFor(x => x.ConfirmPassword)
                 .Equal(x => x.Password).WithMessage(_localizer[SharedResourcesKey.PasswordNotEqualConfirmPass]);

        }

        public void ApplyCustomValidationsRules()
        {

        }

        #endregion
    }
}
