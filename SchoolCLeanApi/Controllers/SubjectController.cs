using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Core.Features.Subjects.Query.Models;
using School.Data.AppMetaData;
using SchoolCLeanApi.Bases;

namespace SchoolCLeanApi.Controllers
{
    [ApiController]
    [Authorize]
    public class SubjectController : AppController
    {
        [HttpGet(Router.SubjectRouting.List)]
        public async Task<IActionResult> GetSubjectsList()
        {
            var response = await Mediator.Send(new GetSubjectsListQuery());
            return NewResult(response);
        }
        [HttpGet(Router.SubjectRouting.GetByID)]
        public async Task<IActionResult> GetSubjectsById([FromRoute] int id)
        {
            var response = await Mediator.Send(new GetSubjectSingleQuery(id));
            return NewResult(response);
        }
    }
}
