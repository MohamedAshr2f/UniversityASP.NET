using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Email.Command.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Email.Command.Handler
{
    public class EmailCommandHandlerL : ResponseHandler, IRequestHandler<SendEmailCommand, Response<string>>
    {
        private readonly IEmailService _emailService;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public EmailCommandHandlerL(IEmailService emailService, IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _emailService = emailService;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await _emailService.SendEmailAsync(request.Email, request.Message, null);
            if (response == "Success")
                return Success<string>("");
            return BadRequest<string>(_stringLocalizer[SharedResourcesKey.SendEmailFailed]);
        }
    }
}
