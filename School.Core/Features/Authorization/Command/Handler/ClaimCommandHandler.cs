using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authorization.Command.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Authorization.Command.Handler
{
    public class ClaimsCommandHandler : ResponseHandler,
          IRequestHandler<UpdateUserClaimsCommand, Response<string>>
    {
        #region Fileds
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;

        #endregion
        #region Constructors
        public ClaimsCommandHandler(IStringLocalizer<SharedResource> stringLocalizer,
                                    IAuthorizationService authorizationService) : base(stringLocalizer)
        {
            _authorizationService = authorizationService;
            _stringLocalizer = stringLocalizer;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserClaims(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(_stringLocalizer[SharedResourcesKey.UserIsNotFound]);
                case "FailedToRemoveOldClaims": return BadRequest<string>(_stringLocalizer[SharedResourcesKey.FailedToRemoveOldClaims]);
                case "FailedToAddNewClaims": return BadRequest<string>(_stringLocalizer[SharedResourcesKey.FailedToAddNewClaims]);
                case "FailedToUpdateClaims": return BadRequest<string>(_stringLocalizer[SharedResourcesKey.FailedToUpdateClaims]);
            }
            return Success<string>(_stringLocalizer[SharedResourcesKey.Success]);
        }
        #endregion
    }
}
