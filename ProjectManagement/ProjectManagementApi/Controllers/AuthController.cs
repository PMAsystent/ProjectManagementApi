using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.Base.Model;
using ProjectManagement.Core.Concrete.Identity.Commands;
using System.Threading.Tasks;

namespace ProjectManagementApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ApiControllerBase
    {
        [HttpPost("RegisterUser")]
        public async Task<ActionResult<bool>> RegisterUser(RegisterUserCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// Logins users, and returns JWT token on successfull auth
        /// </summary>
        /// <param name="command"></param>
        /// <returns>
        /// 200, JWT token - if successful
        /// 401 - if email, password pair not found
        /// </returns>
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
    }
}
