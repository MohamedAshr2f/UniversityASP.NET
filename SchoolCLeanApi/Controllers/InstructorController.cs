using Microsoft.AspNetCore.Mvc;
using School.Core.Features.Instructors.Command.Models;
using School.Core.Features.Instructors.Query.Models;
using School.Data.AppMetaData;
using SchoolCLeanApi.Bases;

namespace SchoolCLeanApi.Controllers
{

    [ApiController]
    public class InstructorController : AppController
    {
        [HttpGet(Router.InstructorRouting.GetSalarySummationOfInstructor)]
        public async Task<IActionResult> GetSalary()
        {
            var response = await Mediator.Send(new GetSummationSalaryOfInstructorQuery());
            return NewResult(response);
        }
        [HttpPost(Router.InstructorRouting.AddInstructor)]
        public async Task<IActionResult> AddInstructor([FromForm] AddInstructorCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }
    }
}
