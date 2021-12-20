using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.Concrete.Identity.Commands;
using ProjectManagement.Core.Concrete.Identity.Dto;
using System;
using System.Threading.Tasks;

namespace ProjectManagementApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ApiControllerBase
    {
        private readonly ICurrentUserService _currentUserService;

        public AuthController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        [HttpPost("RegisterUser")]
        public async Task<RegisterResponseDto> RegisterUser(RegisterUserBaseCommand command)
        {
            var requestUrl = $"{Request.Scheme}://{Request.Host.Value}/";

            return await Mediator.Send(new RegisterUserCommand(command, requestUrl));
        }

        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult<bool>> ConfirmEmail(string userId, string token)
        {
            return await Mediator.Send(new ConfirmEmailCommand { UserId = userId, Token = token.Replace(" ", "+") });
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<LoginResponseDto>> LoginUser(LoginUserCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.Token != "")
                return result;
            return new UnauthorizedResult();
        }

        [HttpPost("LogoutUser")]
        [Authorize]
        public async Task<ActionResult<bool>> LogoutUser(LogoutUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("SendResetPasswordEmail")]
        [Authorize]
        public async Task<ActionResult<bool>> SendResetPasswordEmail(SendResetPasswordEmailCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("ResetPassword")]
        [Authorize]
        public async Task<ActionResult<bool>> ResetPassword(ResetPasswordCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<bool> ChangePassword(ChangePasswordCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("SendChangeEmailAddressEmail")]
        [Authorize]
        public async Task<ActionResult<bool>> SendChangeEmailAddressEmail(SendChangeEmailAddressEmailCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("ChangeEmail")]
        [Authorize]
        public async Task<ActionResult<bool>> ChangeEmail(ChangeEmailCommand command)
        {
            var test = _currentUserService.UserId;
            return await Mediator.Send(command);
        }

        [HttpPost("GetCurrentUserByToken")]
        [Authorize]
        public async Task<ActionResult<CheckTokenResponseDto>> GetCurrentUserByToken(CheckTokenCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
