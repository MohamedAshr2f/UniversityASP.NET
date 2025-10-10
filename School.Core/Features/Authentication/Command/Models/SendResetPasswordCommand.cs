using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Authentication.Command.Models
{
    public class SendResetPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
}
