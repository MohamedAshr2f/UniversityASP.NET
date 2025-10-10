using Microsoft.AspNetCore.Mvc;
using School.Core.Features.Email.Command.Models;
using School.Data.AppMetaData;
using SchoolCLeanApi.Bases;

namespace SchoolCLeanApi.Controllers
{

    [ApiController]
    public class EmailController : AppController
    {
        [HttpPost(Router.EmailsRoute.SendEmail)]
        public async Task<IActionResult> SendEmail([FromQuery] SendEmailCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }
}
