using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Instructors.Query.Models
{
    public class GetSummationSalaryOfInstructorQuery : IRequest<Response<decimal>>
    {
    }
}
