using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Instructors.Command.Models;
using School.Core.Resources;
using School.Data.Entities;
using School.Service.Abstracts;

namespace School.Core.Features.Instructors.Command.Handler
{
    public class InstructorCommandHandler : ResponseHandler, IRequestHandler<AddInstructorCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IInstructorService _instructorService;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public InstructorCommandHandler(IMapper mapper,
            IInstructorService instructorService, IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _mapper = mapper;
            _instructorService = instructorService;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = _mapper.Map<Instructor>(request);
            var result = await _instructorService.AddInstructorAsync(instructor, request.Image);
            switch (result)
            {
                case "FailedToUploadImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKey.FailedToUploadImage]);
                case "FailedInAdd": return BadRequest<string>(_stringLocalizer[SharedResourcesKey.AddFailed]);
            }
            return Success("");
        }
    }
}
