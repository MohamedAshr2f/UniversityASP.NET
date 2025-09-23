using MediatR;
using School.Core.Bases;
using School.Core.Features.Departments.Query.Results;

namespace School.Core.Features.Departments.Query.Models
{
    public class GetDepartmentListQuery : IRequest<Response<List<GetDepartmentListResponse>>>
    {

    }
}
