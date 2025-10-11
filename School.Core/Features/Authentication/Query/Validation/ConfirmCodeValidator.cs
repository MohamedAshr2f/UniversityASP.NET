using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Authentication.Query.Models;
using School.Core.Resources;

namespace School.Core.Features.Authentication.Query.Validation
{
    public class ConfirmCodeValidator : AbstractValidator<ConfirmResetPassword>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResource> _localizer;
        #endregion

        #region Constructors
        public ConfirmCodeValidator(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);
        }

        public void ApplyCustomValidationsRules()
        {
        }

        #endregion
    }
}
