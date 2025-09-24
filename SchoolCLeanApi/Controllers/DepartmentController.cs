using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Core.Features.Departments.Query.Models;
using School.Data.AppMetaData;

namespace SchoolCLeanApi.Controllers
{
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet(Router.DepartmentRouting.List)]
        public async Task<IActionResult> GetDepartmentList()
        {
            var response = await _mediator.Send(new GetDepartmentListQuery());
            return Ok(response);
        }
        [HttpGet(Router.DepartmentRouting.GetByID)]
        public async Task<IActionResult> GetDepartmenById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetDepartmentSinglequery(id));
            return Ok(response);
        }
    }
}
