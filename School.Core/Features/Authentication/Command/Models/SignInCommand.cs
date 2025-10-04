using MediatR;
using School.Core.Bases;
using School.Data.Results;

namespace School.Core.Features.Authentication.Command.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthResult>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
