using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Authentication.Query.Models
{
    public class AuthorizeUserQuery : IRequest<Response<string>>
    {
        public string AccessToken { get; set; }
    }
}
