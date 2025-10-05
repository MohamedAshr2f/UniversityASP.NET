using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authentication.Command.Models;
using School.Core.Resources;
using School.Data.Entities.Identity;
using School.Data.Results;
using School.Service.Abstracts;

namespace School.Core.Features.Authentication.Command.Handler
{
    public class AuthenticationCommandHandler : ResponseHandler, IRequestHandler<SignInCommand, Response<JwtAuthResult>>
        , IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IAuthenticationService _authenticationService;


        public AuthenticationCommandHandler(IAuthenticationService authenticationService, SignInManager<User> signInManager, UserManager<User> userManager, IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
            _authenticationService = authenticationService;
        }
        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //Check if user is exist or not
            var user = await _userManager.FindByNameAsync(request.UserName);
            //Return The UserName Not Found
            if (user == null)
            {
                return NotFound<JwtAuthResult>();
            }
            //try To Sign in 
            // var result=await _userManager.CheckPasswordAsync(user, request.Password);
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            //if Failed Return Passord is wrong
            if (!result.Succeeded)
            {
                return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResourcesKey.passwordNOtcorrect]);
            }

            //Generate Token
            var accesstoken = await _authenticationService.GetJWTToken(user);
            //return Token 
            return Success(accesstoken);

        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.GetRefreshToken(request.AccessToken, request.RefreshToken);
            return Success(result);
        }
    }
}
