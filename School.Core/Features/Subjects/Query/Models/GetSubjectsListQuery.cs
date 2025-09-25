using MediatR;
using School.Core.Bases;
using School.Core.Features.Subjects.Query.Results;

namespace School.Core.Features.Subjects.Query.Models
{
    public class GetSubjectsListQuery : IRequest<Response<List<GetSubjectsListResponse>>>
    {

    }
}
