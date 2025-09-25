
using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Students.Command.Models
{
    public class DeleteStudentCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteStudentCommand(int id)
        {
            Id = id;
        }
    }
}
