using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Departments.Command.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Departments.Command.Validations
{
    public class AddDepartmentValidation : AbstractValidator<AddDepartmentCommand>
    {

        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IDepartmentService _departmentService;

        public AddDepartmentValidation(IStringLocalizer<SharedResource> stringLocalizer, IDepartmentService departmentService)
        {
            _stringLocalizer = stringLocalizer;
            _departmentService = departmentService;
            ApplyValidationRules();
            CustomValidation();
        }

        private void ApplyValidationRules()
        {
            RuleFor(d => d.DepartmentNameAr)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Required])
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKey.MaxLengthis100]);

            RuleFor(d => d.DepartmentNameEn)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Required])
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKey.MaxLengthis100]);


        }

        private void CustomValidation()
        {
            RuleFor(x => x.DepartmentNameAr)
                .MustAsync(async (name, cancellationToken) =>
                    !await _departmentService.IsNameARExist(name))
                .WithMessage(_stringLocalizer[SharedResourcesKey.IsExist]);


            RuleFor(x => x.DepartmentNameEn)
                .MustAsync(async (name, cancellationToken) =>
                    !await _departmentService.IsNameENExist(name))
                .WithMessage(_stringLocalizer[SharedResourcesKey.IsExist]);
        }
    }
}
