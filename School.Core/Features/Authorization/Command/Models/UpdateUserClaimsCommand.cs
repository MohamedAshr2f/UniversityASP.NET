using MediatR;
using School.Core.Bases;
using School.Data.Dtos;

namespace School.Core.Features.Authorization.Command.Models
{
    public class UpdateUserClaimsCommand : UpdateUserClaimsRequest, IRequest<Response<string>>
    {
    }
}
