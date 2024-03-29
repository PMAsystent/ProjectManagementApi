﻿using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<SendResetPasswordEmailDto>> SendResetPasswordEmail(SendResetPasswordEmailCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("ResetPassword")]
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
        public async Task<ActionResult<SendChangeAddressEmailDto>> SendChangeEmailAddressEmail(SendChangeEmailAddressEmailCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("ChangeEmail")]
        public async Task<ActionResult<ChangeEmailDto>> ChangeEmail(ChangeEmailCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("GetCurrentUserByToken")]
        [Authorize]
        public async Task<ActionResult<CheckTokenResponseDto>> GetCurrentUserByToken(string token)
        {
            return await Mediator.Send(new CheckTokenCommand() { Token = token});
        }

        [HttpPost("RefreshToken")]
        [Authorize]
        public async Task<ActionResult<RefreshTokenResponseDto>> RefreshToken(RefreshTokenCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
