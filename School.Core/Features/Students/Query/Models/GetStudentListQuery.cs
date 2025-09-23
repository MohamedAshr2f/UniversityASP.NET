using MediatR;
using School.Core.Features.Students.Query.Results;

namespace School.Core.Features.Students.Query.Models
{
    public class GetStudentListQuery : IRequest<List<GetStudentListResponse>>
    {

    }
}
