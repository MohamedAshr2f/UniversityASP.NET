using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Core.Features.Students.Query.Models;
using School.Data.AppMetaData;

namespace SchoolCLeanApi.Controllers
{

    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet(Router.StudentRouting.List)]
        public async Task<IActionResult> GetStudentsList()
        {
            var resposnse = await _mediator.Send(new GetStudentListQuery());
            return Ok(resposnse);
        }
        [HttpGet(Router.StudentRouting.GetByID)]
        public async Task<IActionResult> GetStudentById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetStudentSingleQuery(id));
            return Ok(response);
        }
    }
}
