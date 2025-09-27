using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Students.Query.Models;
using School.Core.Features.Students.Query.Results;
using School.Core.Resources;
using School.Core.Wrappers;
using School.Data.Entities;
using School.Service.Abstracts;

namespace School.Core.Features.Students.Query.Handler
{
    public class StudentQueryHandler : ResponseHandler, IRequestHandler<GetStudentListQuery, Response<List<GetStudentListResponse>>>
        , IRequestHandler<GetStudentSingleQuery, Response<GetStudentSingleResponse>>
        , IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentPaginatedListResponse>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public StudentQueryHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var studentlist = await _studentService.GetStudentsListAsync();
            var studentlistmapper = _mapper.Map<List<GetStudentListResponse>>(studentlist);
            return Success(studentlistmapper);
        }

        public async Task<Response<GetStudentSingleResponse>> Handle(GetStudentSingleQuery request, CancellationToken cancellationToken)
        {
            var stud = await _studentService.GetStudentByIdAsync(request.Id);
            if (stud == null)
            {

                return NotFound<GetStudentSingleResponse>();
            }

            var studmapping = _mapper.Map<GetStudentSingleResponse>(stud);
            return Success(studmapping);

        }

        public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, GetStudentPaginatedListResponse>> expression = e => new GetStudentPaginatedListResponse(e.StudID, e.Name, e.Address, e.Department.DName, e.StudentSubjects.Select(ss => new Subject
            {
                SubID = ss.Subjects.SubID,
                SubjectName = ss.Subjects.SubjectName,
                Period = ss.Subjects.Period
            }).ToList());
            var FilterQuery = _studentService.FilterStudentPaginatedQuerable(request.Search, request.OrderBy);
            var PaginatedList = await FilterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return PaginatedList;


        }
    }
}
