using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Students.Command.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Students.Command.Validations
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public EditStudentValidator(IStudentService studentService, IStringLocalizer<SharedResource> stringLocalizer)
        {
            ApplyValidationRules();
            CustomValidation();
            _studentService = studentService;
            _stringLocalizer = stringLocalizer;
        }
        public void ApplyValidationRules()
        {
            RuleFor(s => s.NameAr).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage("Name Must be Not Null")
                .MaximumLength(50).WithMessage(_stringLocalizer[SharedResourcesKey.MaxLengthis100]);

            RuleFor(s => s.NameEn).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
              .NotNull().WithMessage("Name Must be Not Null")
              .MaximumLength(50).WithMessage(_stringLocalizer[SharedResourcesKey.MaxLengthis100]);

            RuleFor(s => s.Address).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
               .NotNull().WithMessage("{PropertyValue} Name Must be Not Null")
               .MaximumLength(50).WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty]);
        }
        public void CustomValidation()
        {
            RuleFor(x => x.NameAr)
                .MustAsync(async (model, Key, CancellationToken) => !await _studentService.IsNameARExistExcludeSelf(Key, model.Id))
                .WithMessage(_stringLocalizer[SharedResourcesKey.IsExist]);

            RuleFor(x => x.NameEn)
               .MustAsync(async (model, Key, CancellationToken) => !await _studentService.IsNameENExistExcludeSelf(Key, model.Id))
               .WithMessage(_stringLocalizer[SharedResourcesKey.IsExist]);

        }
    }
}
