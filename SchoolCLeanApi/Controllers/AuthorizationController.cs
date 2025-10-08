using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Core.Features.Authorization.Command.Models;
using School.Core.Features.Authorization.Query.Models;
using School.Data.AppMetaData;
using SchoolCLeanApi.Bases;
using Swashbuckle.AspNetCore.Annotations;

namespace SchoolCLeanApi.Controllers
{

    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuthorizationController : AppController
    {
        [HttpPost(Router.AuthorizationRouting.Create)]
        public async Task<IActionResult> Create([FromForm] AddRoleCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpPut(Router.AuthorizationRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditRoleCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpDelete(Router.AuthorizationRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await Mediator.Send(new DeleteRoleCommand(id));
            return NewResult(response);
        }
        [HttpGet(Router.AuthorizationRouting.RoleList)]
        public async Task<IActionResult> GetRoleList()
        {
            var response = await Mediator.Send(new GetRolesListQuery());
            return NewResult(response);
        }
        [SwaggerOperation(Summary = "idالصلاحية عن طريق ال", OperationId = "RoleById")]
        [HttpGet(Router.AuthorizationRouting.GetRoleById)]
        public async Task<IActionResult> GetRoleById([FromRoute] int id)
        {
            var response = await Mediator.Send(new GetRoleByIdQuery(id));
            return NewResult(response);
        }

    }
}
