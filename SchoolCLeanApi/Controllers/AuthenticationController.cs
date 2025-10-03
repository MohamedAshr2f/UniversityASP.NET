using Microsoft.AspNetCore.Mvc;
using School.Core.Features.Authentication.Command.Models;
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
    }
}
