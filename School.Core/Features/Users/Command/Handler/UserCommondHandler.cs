using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Users.Command.Models;
using School.Core.Resources;
using School.Data.Entities.Identity;
using School.Service.Abstracts;

namespace School.Core.Features.Users.Command.Handler
{
    public class UserCommondHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>
        , IRequestHandler<EditUserCommand, Response<string>>
        , IRequestHandler<DeleteUserCommand, Response<string>>
        , IRequestHandler<ChangePasswordCommand, Response<string>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly UserManager<User> _userManager;

        public UserCommondHandler(IHttpContextAccessor httpContextAccessor, IUserService userService, IMapper mapper, UserManager<User> userManager, IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;

        }

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {

            //maping 
            var identityuser = _mapper.Map<User>(request);
            var createResult = await _userService.AddUserAsync(identityuser, request.Password);
            switch (createResult)
            {
                case "EmailIsExist": return BadRequest<string>(_stringLocalizer[SharedResourcesKey.EmailISExist]);
                case "UserNameIsExist": return BadRequest<string>(_stringLocalizer[SharedResourcesKey.UserNameIsExist]);
                case "ErrorInCreateUser": return BadRequest<string>(_stringLocalizer[SharedResourcesKey.FaildToAddUser]);
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourcesKey.TryToRegisterAgain]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(createResult);
            }
        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.ID.ToString());
            if (user == null)
            {
                return BadRequest<string>();
            }
            var usermapping = _mapper.Map(request, user);
            //if username is Exist
            var userByUserName = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == usermapping.UserName && x.Id != usermapping.Id);
            //username is Exist
            if (userByUserName != null) return BadRequest<string>(_stringLocalizer[SharedResourcesKey.UserNameIsExist]);

            var result = await _userManager.UpdateAsync(usermapping);
            if (!result.Succeeded)
            {
                return BadRequest<string>(result.Errors.FirstOrDefault().Description);
            }
            return Updated("");
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.ID.ToString());
            if (user == null)
            {
                return NotFound<string>();
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Deleted<string>($"User Deleted {request.ID}");
            }
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.ID.ToString());
            if (user == null)
            {
                return NotFound<string>();
            }
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest<string>(result.Errors.FirstOrDefault().Description);
            }
            return Updated((string)_stringLocalizer[SharedResourcesKey.passwordchanged]);
        }
    }
}

