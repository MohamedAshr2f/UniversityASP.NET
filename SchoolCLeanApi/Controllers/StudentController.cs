using Microsoft.AspNetCore.Mvc;
using School.Core.Features.Students.Query.Models;
using School.Data.AppMetaData;
using SchoolCLeanApi.Bases;

namespace SchoolCLeanApi.Controllers
{

    [ApiController]
    public class StudentController : AppController
    {

        [HttpGet(Router.StudentRouting.List)]
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
    }
}
