using AutoMapper;
using MediatR;
using School.Core.Bases;
using School.Core.Features.Students.Query.Models;
using School.Core.Features.Students.Query.Results;
using School.Service.Abstracts;

namespace School.Core.Features.Students.Query.Handler
{
    public class StudentQueryHandler : ResponseHandler, IRequestHandler<GetStudentListQuery, Response<List<GetStudentListResponse>>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentQueryHandler(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var studentlist = await _studentService.GetStudentsListAsync();
            var studentlistmapper = _mapper.Map<List<GetStudentListResponse>>(studentlist);
            return Success(studentlistmapper);
        }
    }
}
