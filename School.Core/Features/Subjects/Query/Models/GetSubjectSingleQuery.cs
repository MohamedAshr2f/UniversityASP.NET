using MediatR;
using School.Core.Bases;
using School.Core.Features.Subjects.Query.Results;

namespace School.Core.Features.Subjects.Query.Models
{
    public class GetSubjectSingleQuery : IRequest<Response<GetSubjectSingleResponse>>
    {
        public int ID { get; set; }
        public GetSubjectSingleQuery(int id)
        {
            ID = id;
        }
    }
}
