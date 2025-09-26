using AutoMapper;
using MediatR;
using School.Core.Bases;
using School.Core.Features.Subjects.Query.Models;
using School.Core.Features.Subjects.Query.Results;
using School.Service.Abstracts;

namespace School.Core.Features.Subjects.Query.Handler
{
    public class SubjectQueryHandler : ResponseHandler, IRequestHandler<GetSubjectsListQuery, Response<List<GetSubjectsListResponse>>>
        , IRequestHandler<GetSubjectSingleQuery, Response<GetSubjectSingleResponse>>
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

        public async Task<Response<GetSubjectSingleResponse>> Handle(GetSubjectSingleQuery request, CancellationToken cancellationToken)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(request.ID);
            var subjectmapping = _mapper.Map<GetSubjectSingleResponse>(subject);
            return Success(subjectmapping);
        }
    }
}
