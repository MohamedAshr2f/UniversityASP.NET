using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Users.Command.Models
{
    public class EditUserCommand : IRequest<Response<string>>
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
