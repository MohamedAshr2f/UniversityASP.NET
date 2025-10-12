using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Instructors.Query.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Instructors.Query.Handler
{
    public class InstructorQueryHandler : ResponseHandler,
                   IRequestHandler<GetSummationSalaryOfInstructorQuery, Response<decimal>>
    {
        private readonly IInstructorService _instructorService;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public InstructorQueryHandler(IInstructorService instructorService, IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _instructorService = instructorService;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<Response<decimal>> Handle(GetSummationSalaryOfInstructorQuery request, CancellationToken cancellationToken)
        {
            var result = await _instructorService.GetSalarySummationOfInstructor();
            return Success(result);
        }
    }
}
