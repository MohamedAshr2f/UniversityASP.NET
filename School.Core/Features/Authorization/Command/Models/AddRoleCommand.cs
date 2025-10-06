using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Authorization.Command.Models
{
    public class AddRoleCommand : IRequest<Response<string>>
    {
        public string RoleName { get; set; }
    }
}
