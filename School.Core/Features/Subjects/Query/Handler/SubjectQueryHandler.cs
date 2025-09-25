using AutoMapper;
using MediatR;
using School.Core.Bases;
using School.Core.Features.Subjects.Query.Models;
using School.Core.Features.Subjects.Query.Results;
using School.Service.Abstracts;

namespace School.Core.Features.Subjects.Query.Handler
{
    public class SubjectQueryHandler : ResponseHandler, IRequestHandler<GetSubjectsListQuery, Response<List<GetSubjectsListResponse>>>
    {
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;

        public SubjectQueryHandler(ISubjectService subjectService, IMapper mapper)
        {
            _subjectService = subjectService;
            _mapper = mapper;
        }


        public async Task<Response<List<GetSubjectsListResponse>>> Handle(GetSubjectsListQuery request, CancellationToken cancellationToken)
        {
            var subjectlist = await _subjectService.GetSubjectListAsync();
            var subjectmapper = _mapper.Map<List<GetSubjectsListResponse>>(subjectlist);
            return Success(subjectmapper);

        }
    }
}
