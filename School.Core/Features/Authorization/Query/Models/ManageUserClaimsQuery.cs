using MediatR;
using School.Core.Bases;
using SchoolProject.Data.Results;

namespace School.Core.Features.Authorization.Query.Models
{
    public class ManageUserClaimsQuery : IRequest<Response<ManageUserClaimsResult>>
    {
        public int UserId { get; set; }
    }
}
