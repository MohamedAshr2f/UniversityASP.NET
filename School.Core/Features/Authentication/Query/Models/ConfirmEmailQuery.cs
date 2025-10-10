using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Authentication.Query.Models
{
    public class ConfirmEmailQuery : IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public string Code { get; set; }
    }
}
