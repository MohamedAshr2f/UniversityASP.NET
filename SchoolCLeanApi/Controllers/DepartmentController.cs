using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Core.Features.Departments.Command.Models;
using School.Core.Features.Departments.Query.Models;
using School.Data.AppMetaData;
using SchoolCLeanApi.Bases;

namespace SchoolCLeanApi.Controllers
{
    [ApiController]
    [Authorize]
    public class DepartmentController : AppController
    {

        [HttpGet(Router.DepartmentRouting.List)]
        public async Task<IActionResult> GetDepartmentList()
        {
            var response = await Mediator.Send(new GetDepartmentListQuery());
            return NewResult(response);
        }
        [HttpGet(Router.DepartmentRouting.GetByID)]
        public async Task<IActionResult> GetDepartmenById([FromRoute] int id)
        {
            var response = await Mediator.Send(new GetDepartmentSinglequery(id));
            return NewResult(response);
        }
        [HttpPost(Router.DepartmentRouting.Create)]
        public async Task<IActionResult> AddDepartment([FromBody] AddDepartmentCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet(Router.DepartmentRouting.Pagination)]
        public async Task<IActionResult> GetDepartmentPaginationList([FromQuery] GetDepartmentPaginatedListQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }
    }
}
