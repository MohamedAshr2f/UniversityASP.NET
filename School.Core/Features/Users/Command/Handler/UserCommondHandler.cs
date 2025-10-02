using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Users.Command.Models;
using School.Core.Resources;
using School.Data.Entities.Identity;

namespace School.Core.Features.Users.Command.Handler
{
    public class UserCommondHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>
        , IRequestHandler<EditUserCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly UserManager<User> _userManager;

        public UserCommondHandler(IMapper mapper, UserManager<User> userManager, IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;

        }

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //if email exist or not 
            var useremail = await _userManager.FindByEmailAsync(request.Email);
            if (useremail != null)
            {
                return BadRequest<string>(_stringLocalizer[SharedResourcesKey.EmailISExist]);
            }
            //if username is Exist
            var username = await _userManager.FindByNameAsync(request.UserName);
            if (username != null)
            {
                return BadRequest<string>(_stringLocalizer[SharedResourcesKey.UserNameIsExist]);
            }
            //maping 
            var identityuser = _mapper.Map<User>(request);
            //adduser
            var createResult = await _userManager.CreateAsync(identityuser, request.Password);
            if (!createResult.Succeeded)
            {
                return BadRequest<string>(createResult.Errors.FirstOrDefault().Description);
            }
            return Created("");
        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.ID.ToString());
            if (user == null)
            {
                return BadRequest<string>();
            }
            var usermapping = _mapper.Map(request, user);

            var result = await _userManager.UpdateAsync(usermapping);
            if (!result.Succeeded)
            {
                return BadRequest<string>(result.Errors.FirstOrDefault().Description);
            }
            return Updated("");
        }
    }
}

