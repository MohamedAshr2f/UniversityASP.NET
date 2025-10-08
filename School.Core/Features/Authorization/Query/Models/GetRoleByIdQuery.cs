using MediatR;
using School.Core.Bases;
using School.Core.Features.Authorization.Query.Results;

namespace School.Core.Features.Authorization.Query.Models
{
    public class GetRoleByIdQuery : IRequest<Response<GetRoleByIdResponse>>
    {
        public int Id { get; set; }
        public GetRoleByIdQuery(int id)
        {
            Id = id;
        }
    }
}
