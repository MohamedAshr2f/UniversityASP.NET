using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authorization.Command.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Authorization.Command.Handler
{
    public class RoleCommandHandler : ResponseHandler, IRequestHandler<AddRoleCommand, Response<string>>
        , IRequestHandler<EditRoleCommand, Response<string>>
       , IRequestHandler<DeleteRoleCommand, Response<string>>
        , IRequestHandler<UpdateUserRolesCommand, Response<string>>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public RoleCommandHandler(IAuthorizationService authorizationService, IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _authorizationService = authorizationService;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.AddRoleAsync(request.RoleName);

            if (result == "Success") return Success("");
            return BadRequest<string>(_stringLocalizer[SharedResourcesKey.AddFailed]);
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.EditRoleAsync(request);
            if (result == "notFound") return NotFound<string>();
            else if (result == "Success") return Success((string)_stringLocalizer[SharedResourcesKey.Updated]);
            else
                return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.DeleteRoleAsync(request.ID);
            if (result == "NotFound") return NotFound<string>();
            else if (result == "Used") return BadRequest<string>(_stringLocalizer[SharedResourcesKey.RoleIsUsed]);
            else if (result == "Success") return Success((string)_stringLocalizer[SharedResourcesKey.Deleted]);
            else
                return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {

            var result = await _authorizationService.UpdateUserRoles(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(_stringLocalizer[SharedResourcesKey.UserIsNotFound]);
                case "FailedToRemoveOldRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKey.FailedToRemoveOldRoles]);
                case "FailedToAddNewRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKey.FailedToAddNewRoles]);
                case "FailedToUpdateUserRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKey.FailedToUpdateUserRoles]);
            }
            return Success<string>(_stringLocalizer[SharedResourcesKey.Success]);
        }
    }
}
