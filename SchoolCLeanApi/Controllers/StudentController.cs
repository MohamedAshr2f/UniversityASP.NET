using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Core.Features.Students.Query.Models;

namespace SchoolCLeanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("/Student/List")]
        public async Task<IActionResult> GetStudentsList()
        {
            var resposnse = await _mediator.Send(new GetStudentListQuery());
            return Ok(resposnse);
        }
    }
}
