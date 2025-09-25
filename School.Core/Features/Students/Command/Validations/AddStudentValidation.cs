using FluentValidation;
using School.Core.Features.Students.Command.Models;
using School.Service.Abstracts;

namespace School.Core.Features.Students.Command.Validations
{
    public class AddStudentValidation : AbstractValidator<AddStudentCommand>
    {
        private readonly IStudentService _studentService;

        public AddStudentValidation(IStudentService studentService)
        {
            ApplyValidationRules();
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
                .MustAsync(async (Key, CancellationToken) => !await _studentService.IsNameExist(Key))
                .WithMessage("Name Is Exist");
        }
    }
}
