using FluentValidation;
using School.Core.Features.Students.Command.Models;
using School.Service.Abstracts;

namespace School.Core.Features.Students.Command.Validations
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {
        private readonly IStudentService _studentService;

        public EditStudentValidator(IStudentService studentService)
        {
            ApplyValidationRules();
            CustomValidation();
            _studentService = studentService;
        }
        public void ApplyValidationRules()
        {
            RuleFor(s => s.Name).NotEmpty().WithMessage("Name Is Empty")
                .NotNull().WithMessage("Name Must be Not Null")
                .MaximumLength(50).WithMessage("MaxLength is 50");

            RuleFor(s => s.Address).NotEmpty().WithMessage("{PropertyName} Is Empty")
               .NotNull().WithMessage("{PropertyValue} Name Must be Not Null")
               .MaximumLength(50).WithMessage("{PropertyName} MaxLength is 50");
        }
        public void CustomValidation()
        {
            RuleFor(x => x.Name)
                .MustAsync(async (model, Key, CancellationToken) => !await _studentService.IsNameExistExcludeSelf(Key, model.Id))
                .WithMessage("Name Is Exist");
        }
    }
}
