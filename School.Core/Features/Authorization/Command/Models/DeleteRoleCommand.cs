using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Authorization.Command.Models
{
    public class DeleteRoleCommand : IRequest<Response<string>>
    {
        public int ID { get; set; }
        public DeleteRoleCommand(int id)
        {
            ID = id;
        }
    }
}
