using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Departments.Command.Models;
using School.Core.Resources;
using School.Data.Entities;
using School.Service.Abstracts;

namespace School.Core.Features.Departments.Command.Handler
{
    public class DepartmentCommandHandler : ResponseHandler, IRequestHandler<AddDepartmentCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public DepartmentCommandHandler(IMapper mapper, IDepartmentService departmentService, IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _mapper = mapper;
            _departmentService = departmentService;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
        {
            var deptmapping = _mapper.Map<Department>(request);
            var dept = await _departmentService.AddDepartmentAsync(deptmapping);
            if (dept == "successful")
            {
                return Created(dept);
            }
            return BadRequest<string>();
        }
    }
}
