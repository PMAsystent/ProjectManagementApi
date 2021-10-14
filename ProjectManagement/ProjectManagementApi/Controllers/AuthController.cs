using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.Base.Model;
using ProjectManagement.Core.Concrete.Identity.Commands;
using ProjectManagement.Core.Concrete.Identity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPost("LoginUser")]
        public async Task<ActionResult<JWTAuthorizationResult>> LoginUser(LoginUserCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.Succeeded)
                return new OkObjectResult(result.Token);
            return new UnauthorizedResult();
        }

        [HttpPost("LogoutUser")]
        [Authorize]
        public async Task<ActionResult<bool>> LogoutUser(LogoutUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("ConfirmEmail")]
        [Authorize]
        public async Task<ActionResult<bool>> ConfirmEmail(ConfirmEmailCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("ChangeEmail")]
        [Authorize]
        public async Task<ActionResult<bool>> ChangeEmail(ChangeEmailCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("ChangePassword")]
        [Authorize]
        public async Task<ActionResult<bool>> ChangePassword(ChangePasswordCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("ResetPassword")]
        [Authorize]
        public async Task<ActionResult<bool>> ResetPassword(ResetPasswordCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("GetCurrentUserByToken")]
        public async Task<ActionResult<CheckTokenResponseDto>> GetCurrentUserByToken(CheckTokenCommand command)
        {
            var test = HttpContext.User.Identity.IsAuthenticated;
            return await Mediator.Send(command);
        }
    }
}
