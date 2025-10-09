using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authorization.Query.Models;
using School.Core.Features.Authorization.Query.Results;
using School.Core.Resources;
using School.Data.Entities.Identity;
using School.Service.Abstracts;
using SchoolProject.Data.Results;

namespace School.Core.Features.Authorization.Query.Handler
{
    public class RoleQueryHandler : ResponseHandler, IRequestHandler<GetRolesListQuery, Response<List<GetRoleListResponse>>>
        , IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResponse>>
        , IRequestHandler<ManageUserRolesQuery, Response<ManageUserRolesResult>>
    {
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly UserManager<User> _userManager;

        public RoleQueryHandler(UserManager<User> userManager, IMapper mapper, IAuthorizationService authorizationService, IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _userManager = userManager;
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

        public async Task<Response<ManageUserRolesResult>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                return NotFound<ManageUserRolesResult>(_stringLocalizer[SharedResourcesKey.UserIsNotFound]);
            }
            var result = await _authorizationService.ManageUserRolesData(user);
            return Success(result);

        }
    }
}
