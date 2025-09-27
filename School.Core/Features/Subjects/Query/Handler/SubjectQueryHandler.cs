using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Subjects.Query.Models;
using School.Core.Features.Subjects.Query.Results;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Subjects.Query.Handler
{
    public class SubjectQueryHandler : ResponseHandler, IRequestHandler<GetSubjectsListQuery, Response<List<GetSubjectsListResponse>>>
        , IRequestHandler<GetSubjectSingleQuery, Response<GetSubjectSingleResponse>>
    {
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public SubjectQueryHandler(ISubjectService subjectService, IMapper mapper, IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _subjectService = subjectService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
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
