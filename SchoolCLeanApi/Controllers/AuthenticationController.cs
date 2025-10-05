using Microsoft.AspNetCore.Mvc;
using School.Core.Features.Authentication.Command.Models;
using School.Core.Features.Authentication.Query.Models;
using School.Data.AppMetaData;
using SchoolCLeanApi.Bases;

namespace SchoolCLeanApi.Controllers
{

    [ApiController]
    public class AuthenticationController : AppController
    {
        [HttpPost(Router.Authentications.SignIn)]
        public async Task<IActionResult> SignIn([FromForm] SignInCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet(Router.Authentications.ValidateToken)]
        public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery query)
        {
            var response = await Mediator.Send(query);
            return NewResult(response);
        }
    }
}
