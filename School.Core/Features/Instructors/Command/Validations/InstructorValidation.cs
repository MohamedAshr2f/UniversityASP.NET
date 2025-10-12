using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Instructors.Command.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Instructors.Command.Validations
{
    public class InstructorValidation : AbstractValidator<AddInstructorCommand>
    {


        #region Fields
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IDepartmentService _departmentService;
        private readonly IInstructorService _instructorService;
        private readonly IFileService _fileService;
        #endregion

        #region Constructors
        public InstructorValidation(IFileService fileService, IStringLocalizer<SharedResource> localizer,
                                      IDepartmentService departmentService,
                                      IInstructorService instructorService)
        {
            _fileService = fileService;
            _localizer = localizer;
            _instructorService = instructorService;
            _departmentService = departmentService;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();

        }
        #endregion

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.NameAr)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);

            RuleFor(x => x.NameEn)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);

            RuleFor(x => x.DID)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);
        }

        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.NameAr)
                .MustAsync(async (Key, CancellationToken) => !await _instructorService.IsNameArExist(Key))
                .WithMessage(_localizer[SharedResourcesKey.IsExist]);
            RuleFor(x => x.NameEn)
               .MustAsync(async (Key, CancellationToken) => !await _instructorService.IsNameEnExist(Key))
               .WithMessage(_localizer[SharedResourcesKey.IsExist]);

            RuleFor(x => x.DID)
           .MustAsync(async (Key, CancellationToken) => await _departmentService.IsDepartmentIdExist(Key))
           .WithMessage(_localizer[SharedResourcesKey.IsNotExist]);

            RuleFor(x => x.Image)
                .MustAsync(async (file, CancellationToken) => await _fileService.IsValidImageFileMaxLength(file))
                .WithMessage(_localizer[SharedResourcesKey.InvaildImageSize]);

            RuleFor(x => x.Image)
               .MustAsync(async (file, CancellationToken) => await _fileService.IsValidImageFileExtension(file))
               .WithMessage(_localizer[SharedResourcesKey.InvaildImageExtension]);
        }

        #endregion
    }
}
