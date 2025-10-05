using MediatR;

using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authentication.Query.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Authentication.Query.Handler
{
    public class AuthenticationQueryHandler : ResponseHandler, IRequestHandler<AuthorizeUserQuery, Response<string>>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;


        public AuthenticationQueryHandler(IAuthenticationService authenticationService, IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _authenticationService = authenticationService;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.AccessToken);
            if (result == "NotExpired")
                return Success(result);
            return Unauthorized<string>(_stringLocalizer[SharedResourcesKey.TokenIsExpired]);
        }
    }
}
