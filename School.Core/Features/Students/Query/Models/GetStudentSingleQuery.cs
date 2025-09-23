using MediatR;
using School.Core.Bases;
using School.Core.Features.Students.Query.Results;

namespace School.Core.Features.Students.Query.Models
{
    public class GetStudentSingleQuery : IRequest<Response<GetStudentSingleResponse>>
    {
        public int Id { get; set; }
        public GetStudentSingleQuery(int id)
        {
            Id = id;
        }
    }
}
