using MediatR;
using School.Core.Bases;
using School.Data.Results;

namespace School.Core.Features.Authentication.Command.Models
{
    public class RefreshTokenCommand : IRequest<Response<JwtAuthResult>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
