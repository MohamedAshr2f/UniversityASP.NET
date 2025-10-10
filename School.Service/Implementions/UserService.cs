using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using School.Data.Entities.Identity;
using School.Infrastructure.ApplicationContext;
using School.Service.Abstracts;

namespace School.Service.Implementions
{
    public class UserService : IUserService
    {
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly IUrlHelper _urlHelper;
        private readonly Context _context;

        public UserService(IEmailService emailService, IHttpContextAccessor httpContextAccessor
                           , UserManager<User> userManager
                           , IUrlHelper urlHelper
                           , Context context)
        {
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _urlHelper = urlHelper;
            _context = context;
        }
        public async Task<string> AddUserAsync(User user, string password)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                //if email exist or not 
                var useremail = await _userManager.FindByEmailAsync(user.Email);
                if (useremail != null)
                {
                    return "EmailISExist";
                }
                //if username is Exist
                var username = await _userManager.FindByNameAsync(user.UserName);
                if (username != null)
                {
                    return "UserNameIsExist";
                }
                //adduser
                var createResult = await _userManager.CreateAsync(user, password);
                if (!createResult.Succeeded)
                {
                    return string.Join(",", createResult.Errors.Select(x => x.Description).ToList());
                }
                //ADD Role To USer
                await _userManager.AddToRoleAsync(user, "User");

                //Send Confirm Email
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var resquestAccessor = _httpContextAccessor.HttpContext.Request;
                var returnUrl = resquestAccessor.Scheme + "://" + resquestAccessor.Host + _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code });
                //message or body
                var message = $"To Confirm Email Click Link: <a href='{returnUrl}'>Link Of Confirmation</a>";
                //$"/Api/V1/Authentication/ConfirmEmail?userId={user.Id}&code={code}";

                await _emailService.SendEmailAsync(user.Email, message, "Confirmation Email");
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }

        }
    }
}
