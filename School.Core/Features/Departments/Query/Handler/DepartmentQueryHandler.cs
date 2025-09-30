using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Departments.Query.Models;
using School.Core.Features.Departments.Query.Results;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Departments.Query.Handler
{
    public class DepartmentQueryHandler : ResponseHandler, IRequestHandler<GetDepartmentListQuery, Response<List<GetDepartmentListResponse>>>
        , IRequestHandler<GetDepartmentSinglequery, Response<GetDepartmentSingleResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public DepartmentQueryHandler(IMapper mapper, IDepartmentService departmentService, IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _mapper = mapper;
            _departmentService = departmentService;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<List<GetDepartmentListResponse>>> Handle(GetDepartmentListQuery request, CancellationToken cancellationToken)
        {
            var DepartmentList = await _departmentService.GetDepartmentListAsync();
            var DepartmentListMapping = _mapper.Map<List<GetDepartmentListResponse>>(DepartmentList);
            return Success(DepartmentListMapping);
        }

        public async Task<Response<GetDepartmentSingleResponse>> Handle(GetDepartmentSinglequery request, CancellationToken cancellationToken)
        {
            var Dept = await _departmentService.GetDepartmentByIdAsync(request.Id);
            if (Dept == null)
            {
                return NotFound<GetDepartmentSingleResponse>();
            }
            var DeptMapping = _mapper.Map<GetDepartmentSingleResponse>(Dept);
            return Success(DeptMapping);
        }
    }
}
