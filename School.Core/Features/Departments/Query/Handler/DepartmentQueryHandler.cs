using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Departments.Query.Models;
using School.Core.Features.Departments.Query.Results;
using School.Core.Resources;
using School.Core.Wrappers;
using School.Data.Entities;
using School.Service.Abstracts;

namespace School.Core.Features.Departments.Query.Handler
{
    public class DepartmentQueryHandler : ResponseHandler, IRequestHandler<GetDepartmentListQuery, Response<List<GetDepartmentListResponse>>>
        , IRequestHandler<GetDepartmentSinglequery, Response<GetDepartmentSingleResponse>>
        , IRequestHandler<GetDepartmentPaginatedListQuery, PaginatedResult<GetDepartmentPAG>>

    //, IRequestHandler<GetDepartmentPaginatedListQuery, PaginatedResult<GetDepartmentPaginatedResponse>>
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

        /* public async Task<PaginatedResult<GetDepartmentPaginatedResponse>> Handle(GetDepartmentPaginatedListQuery request, CancellationToken cancellationToken)
         {
             Expression<Func<Department, GetDepartmentPaginatedResponse>> expression = e => new GetDepartmentPaginatedResponse(e.DID, e.Localize(e.DNameEn, e.DNameAr), e.Instructor.Localize(e.Instructor.ENameEn, e.Instructor.ENameAr), e.Instructors.Select(i => new InstructorDtos
             {
                 InstructorName = i.Localize(i.ENameEn, i.ENameAr)

             }).ToList(), e.Students.Select(s => new StudentDtos
             {
                 StudentName = s.Localize(s.NameEn, s.NameAr),
                 subjectDtos = s.StudentSubject.Select(ss => new SubjectDtos
                 {
                     SubjectID = ss.SubID,
                     Name = ss.Subject.Localize(ss.Subject.SubjectNameEn, ss.Subject.SubjectNameAr)
                 }).ToList()
             }).ToList(), e.departmentsubjects.Select(ds => new SubjectDtos
             {
                 SubjectID = ds.SubID,
                 Name = ds.Subject.Localize(ds.Subject.SubjectNameEn, ds.Subject.SubjectNameAr)
             }).ToList());

             var FilterQuery = _departmentService.FilterDepartmentPaginatedQuerable(request.Search, request.OrderBy);
             var PaginatedList = await FilterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
             return PaginatedList;
         }*/

        public async Task<PaginatedResult<GetDepartmentPAG>> Handle(GetDepartmentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Department, GetDepartmentPAG>> expression = e => new GetDepartmentPAG(e.DID, e.Localize(e.DNameEn, e.DNameAr), e.Instructor != null ? e.Instructor.Localize(e.Instructor.ENameEn, e.Instructor.ENameAr) : "No Manger");
            var FilterQuery = _departmentService.FilterDepartmentPaginatedQuerable(request.Search);
            var PaginatedList = await FilterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return PaginatedList;
        }
    }
}
