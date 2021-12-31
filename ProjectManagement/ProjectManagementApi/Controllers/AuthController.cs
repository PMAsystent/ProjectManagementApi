using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.Concrete.Identity.Commands;
using ProjectManagement.Core.Concrete.Identity.Dto;
using System.Threading.Tasks;

namespace ProjectManagementApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ApiControllerBase
    {
        [HttpPost("RegisterUser")]
        public async Task<RegisterResponseDto> RegisterUser(RegisterUserCommand command)
        {
            //var requestUrl = $"{Request.Scheme}://{Request.Host.Value}/";

            return await Mediator.Send(command);
        }

        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult<bool>> ConfirmEmail(string userId, string token)
        {
            return await Mediator.Send(new ConfirmEmailCommand { UserId = userId, Token = token.Replace(" ", "+") });
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<LoginResponseDto>> LoginUser(LoginUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("LogoutUser")]
        [Authorize]
        public async Task<ActionResult<bool>> LogoutUser(LogoutUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("SendResetPasswordEmail")]
        [Authorize]
        public async Task<ActionResult<SendResetPasswordEmailDto>> SendResetPasswordEmail(SendResetPasswordEmailCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("ResetPassword")]
        [Authorize]
        public async Task<ActionResult<ResetPasswordDto>> ResetPassword(ResetPasswordCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<ChangePasswordDto> ChangePassword(ChangePasswordCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("SendChangeEmailAddressEmail")]
        [Authorize]
        public async Task<ActionResult<SendChangeAddressEmailDto>> SendChangeEmailAddressEmail(SendChangeEmailAddressEmailCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("ChangeEmail")]
        [Authorize]
        public async Task<ActionResult<ChangeEmailDto>> ChangeEmail(ChangeEmailCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("GetCurrentUserByToken")]
        [Authorize]
        public async Task<ActionResult<CheckTokenResponseDto>> GetCurrentUserByToken(CheckTokenCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("RefreshToken")]
        [Authorize]
        public async Task<ActionResult<RefreshTokenResponseDto>> RefreshToken(RefreshTokenCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
