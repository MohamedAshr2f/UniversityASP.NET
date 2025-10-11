using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Core.AuthFilters;
using School.Core.Features.Students.Command.Models;
using School.Core.Features.Students.Query.Models;
using School.Data.AppMetaData;
using SchoolCLeanApi.Bases;

namespace SchoolCLeanApi.Controllers
{

    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class StudentController : AppController
    {

        [HttpGet(Router.StudentRouting.List)]
        [Authorize(Roles = "User")]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> GetStudentsList()
        {
            var resposnse = await Mediator.Send(new GetStudentListQuery());
            return NewResult(resposnse);
        }
        [HttpGet(Router.StudentRouting.GetByID)]
        public async Task<IActionResult> GetStudentById([FromRoute] int id)
        {
            var response = await Mediator.Send(new GetStudentSingleQuery(id));
            return NewResult(response);
        }
        [Authorize(Policy = "CreateStudent")]
        [HttpPost(Router.StudentRouting.Create)]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut(Router.StudentRouting.Edit)]
        public async Task<IActionResult> EditStudent([FromBody] EditStudentCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [Authorize(Policy = "DeleteStudent")]
        [HttpDelete(Router.StudentRouting.Delete)]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            var response = await Mediator.Send(new DeleteStudentCommand(id));
            return NewResult(response);
        }
        [HttpGet(Router.StudentRouting.Pagination)]
        public async Task<IActionResult> GetStudentsPaginationList([FromQuery] GetStudentPaginatedListQuery query)
        {
            var resposnse = await Mediator.Send(query);
            return Ok(resposnse);
        }
    }
}
