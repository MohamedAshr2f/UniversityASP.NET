using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Students.Command.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Students.Command.Validations
{
    public class AddStudentValidation : AbstractValidator<AddStudentCommand>
    {
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IDepartmentService _departmentService;

        public AddStudentValidation(IStudentService studentService, IStringLocalizer<SharedResource> stringLocalizer, IDepartmentService departmentService)
        {
            _studentService = studentService;
            _stringLocalizer = stringLocalizer;
            _departmentService = departmentService;
            ApplyValidationRules();
            CustomValidation();
        }

        private void ApplyValidationRules()
        {
            RuleFor(s => s.NameAr)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Required])
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKey.MaxLengthis100]);

            RuleFor(s => s.NameEn)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Required])
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKey.MaxLengthis100]);

            RuleFor(s => s.Address)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Required])
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKey.MaxLengthis100]);

            RuleFor(s => s.Phone)
               .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Required])
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKey.MaxLengthis100]);

            RuleFor(x => x.DepartmentId)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Required]);
        }

        private void CustomValidation()
        {
            RuleFor(x => x.NameAr)
                .MustAsync(async (name, cancellationToken) =>
                    !await _studentService.IsNameARExist(name))
                .WithMessage(_stringLocalizer[SharedResourcesKey.IsExist]);


            RuleFor(x => x.NameEn)
                .MustAsync(async (name, cancellationToken) =>
                    !await _studentService.IsNameENExist(name))
                .WithMessage(_stringLocalizer[SharedResourcesKey.IsExist]);

            RuleFor(x => x.DepartmentId)
           .MustAsync(async (Key, CancellationToken) => await _departmentService.IsDepartmentIdExist(Key))
           .WithMessage(_stringLocalizer[SharedResourcesKey.IsNotExist]);

        }
    }
}
