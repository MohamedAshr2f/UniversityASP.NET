using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Core.Features.Users.Command.Models;
using School.Core.Features.Users.Query.Models;
using School.Data.AppMetaData;
using SchoolCLeanApi.Bases;

namespace SchoolCLeanApi.Controllers
{

    [ApiController]
    [Authorize]
    public class UserController : AppController
    {
        [HttpPost(Router.UserRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet(Router.UserRouting.Pagination)]
        public async Task<IActionResult> GetUserList([FromQuery] GetUserPaginatedList query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }
        [HttpGet(Router.UserRouting.GetByID)]
        public async Task<IActionResult> GetUserList([FromRoute] int id)
        {
            var response = await Mediator.Send(new GetUserSingle(id));
            return Ok(response);
        }
        [HttpPut(Router.UserRouting.Edit)]
        public async Task<IActionResult> EditUser([FromBody] EditUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpDelete(Router.UserRouting.Delete)]
        public async Task<IActionResult> DeleteUser([FromQuery] int id)
        {
            var response = await Mediator.Send(new DeleteUserCommand(id));
            return NewResult(response);
        }
        [HttpPut(Router.UserRouting.ChangePassword)]
        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangePasswordCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }
}
