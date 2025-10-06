using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Authorization.Command.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Authorization.Command.Validations
{
    public class AddRoleValidation : AbstractValidator<AddRoleCommand>
    {
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;

        public AddRoleValidation(IStringLocalizer<SharedResource> stringLocalizer, IAuthorizationService authorizationService)
        {

            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            ApplyValidationRules();
            CustomValidation();
        }
        public void ApplyValidationRules()
        {

            RuleFor(r => r.RoleName)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Required]);
        }

        public async Task CustomValidation()
        {
            RuleFor(r => r.RoleName)
                .MustAsync(async (Key, CancellationToken) => !await _authorizationService.RoleIsExist(Key))
                .WithMessage(_stringLocalizer[SharedResourcesKey.IsExist]);
        }

    }
}
