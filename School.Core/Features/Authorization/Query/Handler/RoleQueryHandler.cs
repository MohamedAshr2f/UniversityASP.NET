using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authorization.Query.Models;
using School.Core.Features.Authorization.Query.Results;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Authorization.Query.Handler
{
    public class RoleQueryHandler : ResponseHandler, IRequestHandler<GetRolesListQuery, Response<List<GetRoleListResponse>>>
        , IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public RoleQueryHandler(IMapper mapper, IAuthorizationService authorizationService, IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _mapper = mapper;
            _authorizationService = authorizationService;
            _stringLocalizer = stringLocalizer;
        }



        public async Task<Response<GetRoleByIdResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.GetRoleById(request.Id);
            if (role == null) return NotFound<GetRoleByIdResponse>(_stringLocalizer[SharedResourcesKey.RoleNotExist]);
            var result = _mapper.Map<GetRoleByIdResponse>(role);
            return Success(result);
        }

        public async Task<Response<List<GetRoleListResponse>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.GetRolesList();

            var result = _mapper.Map<List<GetRoleListResponse>>(roles);
            return Success(result);
        }
    }
}
