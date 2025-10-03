using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Users.Command.Models
{
    public class ChangePasswordCommand : IRequest<Response<string>>
    {
        public int ID { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
