using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Authorization.Command.Models;
using School.Core.Resources;

namespace School.Core.Features.Authorization.Command.Validations
{
    public class DeleteRoleValidation : AbstractValidator<DeleteRoleCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion
        #region Constructors
        public DeleteRoleValidation(IStringLocalizer<SharedResource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion


        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.ID)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                 .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Required]);

        }

        public void ApplyCustomValidationsRules()
        {

        }

        #endregion
    }
}
