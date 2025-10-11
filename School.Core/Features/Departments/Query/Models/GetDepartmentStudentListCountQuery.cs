using MediatR;
using School.Core.Bases;
using School.Core.Features.Departments.Query.Results;

namespace School.Core.Features.Departments.Query.Models
{
    public class GetDepartmentStudentListCountQuery : IRequest<Response<List<GetDepartmentStudentListCountResponse>>>
    {

    }
}
