using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Users.Command.Models
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {
        public int ID { get; set; }
        public DeleteUserCommand(int id)
        {
            ID = id;
        }
    }
}
