using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Core.Features.Departments.Query.Models;

namespace SchoolCLeanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("/Department/List")]
        public async Task<IActionResult> GetDepartmentList()
        {
            var response = await _mediator.Send(new GetDepartmentListQuery());
            return Ok(response);
        }
    }
}
