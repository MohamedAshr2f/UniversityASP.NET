using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Users.Query.Models;
using School.Core.Features.Users.Query.Results;
using School.Core.Resources;
using School.Core.Wrappers;
using School.Data.Entities.Identity;

namespace School.Core.Features.Users.Query.Handler
{
    public class UserQueryHandler : ResponseHandler, IRequestHandler<GetUserPaginatedList, PaginatedResult<GetUserPaginatedResponse>>
        , IRequestHandler<GetUserSingle, Response<GetUserSingleResponse>>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public UserQueryHandler(IMapper mapper, UserManager<User> userManager, IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _mapper = mapper;
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<PaginatedResult<GetUserPaginatedResponse>> Handle(GetUserPaginatedList request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            var paginatedlist = await _mapper.ProjectTo<GetUserPaginatedResponse>(users).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedlist;
        }

        public async Task<Response<GetUserSingleResponse>> Handle(GetUserSingle request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.ID.ToString());
            if (user == null)
            {
                return NotFound<GetUserSingleResponse>(_stringLocalizer[SharedResourcesKey.NotFound]);
            }
            var usermapping = _mapper.Map<GetUserSingleResponse>(user);
            return Success(usermapping);
        }
    }
}
