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

        public AddStudentValidation(IStudentService studentService, IStringLocalizer<SharedResource> stringLocalizer)
        {
            ApplyValidationRules();
            CustomValidation();
            _studentService = studentService;
            _stringLocalizer = stringLocalizer;
        }
        public void ApplyValidationRules()
        {
            RuleFor(s => s.Name).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage("Name Must be Not Null")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKey.MaxLengthis100]);

            RuleFor(s => s.Address).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage("{PropertyValue} Name Must be Not Null")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKey.MaxLengthis100]);
        }
        public void CustomValidation()
        {
            RuleFor(x => x.Name)
                .MustAsync(async (Key, CancellationToken) => !await _studentService.IsNameExist(Key))
                .WithMessage("Name Is Exist");
        }
    }
}
