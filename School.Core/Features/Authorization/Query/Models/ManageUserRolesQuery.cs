using MediatR;
using School.Core.Bases;
using SchoolProject.Data.Results;

namespace School.Core.Features.Authorization.Query.Models
{
    public class ManageUserRolesQuery : IRequest<Response<ManageUserRolesResult>>
    {
        public int UserId { get; set; }
        public ManageUserRolesQuery(int id)
        {
            UserId = id;
        }

    }
}
