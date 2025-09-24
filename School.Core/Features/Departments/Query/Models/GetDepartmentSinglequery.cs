using MediatR;
using School.Core.Bases;
using School.Core.Features.Departments.Query.Results;

namespace School.Core.Features.Departments.Query.Models
{
    public class GetDepartmentSinglequery : IRequest<Response<GetDepartmentSingleResponse>>
    {
        public int Id { get; set; }
        public GetDepartmentSinglequery(int id)
        {
            Id = id;
        }
    }
}
