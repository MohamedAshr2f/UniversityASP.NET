using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authorization.Query.Models;
using School.Core.Resources;
using School.Data.Entities.Identity;
using School.Service.Abstracts;
using SchoolProject.Data.Results;

namespace School.Core.Features.Authorization.Query.Handler
{
    public class ClaimsQueryHandler : ResponseHandler,
        IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaimsResult>>
    {
        #region Fileds
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<User> _userManager;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion
        #region Constructors
        public ClaimsQueryHandler(IStringLocalizer<SharedResource> stringLocalizer,
                                  IAuthorizationService authorizationService,
                                  UserManager<User> userManager) : base(stringLocalizer)
        {
            _authorizationService = authorizationService;
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<ManageUserClaimsResult>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return NotFound<ManageUserClaimsResult>(_stringLocalizer[SharedResourcesKey.UserIsNotFound]);
            var result = await _authorizationService.ManageUserClaimData(user);
            return Success(result);
        }
        #endregion
    }
}
