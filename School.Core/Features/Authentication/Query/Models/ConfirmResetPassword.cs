using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Authentication.Query.Models
{
    public class ConfirmResetPassword : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
