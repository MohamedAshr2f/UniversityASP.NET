using MediatR;
using School.Core.Bases;
using School.Core.Features.Authorization.Query.Results;

namespace School.Core.Features.Authorization.Query.Models
{
    public class GetRolesListQuery : IRequest<Response<List<GetRoleListResponse>>>
    {

    }
}
