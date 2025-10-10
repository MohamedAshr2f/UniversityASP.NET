
using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Email.Command.Models
{
    public class SendEmailCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
