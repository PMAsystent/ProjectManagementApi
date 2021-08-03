using Infrastructure.Identity.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.Base.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly AppSettings _settings;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService,
            IOptions<AppSettings> settings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _settings = settings.Value;
        }
        public async Task<bool> AuthorizeAsync(string userName, string policyName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == userName);
            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
            var result = await _authorizationService.AuthorizeAsync(principal, policyName);
            return result.Succeeded;
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return Result.Success();
        }
        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<bool> IsInRoleAsync(string userName, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == userName);
            return await _userManager.IsInRoleAsync(user, role);
        }


        public async Task<(Result Result, string UserId)> RegisterUserAsync(string email, string userName, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = email,
            };

            var result = await _userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        /// <summary>
        /// Checks user credentials agains Identity DB, and constructs JWT token if successful
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<JWTAuthorizationResult> LoginUserAsync(string email, string password)
        {
            SignInResult result = new SignInResult();
            if (_signInManager != null)
            {
                var test = _userManager.Users.Count();
                var user = _userManager.Users.SingleOrDefault(u => u.Email == email);
                if (user == null)
                    return JWTAuthorizationResult.Failure(new string[] { "Email not found" });

                result = await _signInManager.PasswordSignInAsync(user, password, true, false);
                await AuthorizeAsync(user.UserName, "CanPurge"); //?? is it needed?


                if (result.Succeeded == true)
                {
                    //TODO: take key from keystore
                    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.AuthKey));
                    var expiration = _settings.Expire; //[s]
                    return JWTAuthorizationResult.Success(new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                        claims: GetClaimTokens(user.Id),
                        notBefore: DateTime.Now,
                        expires: DateTime.Now.Add(TimeSpan.FromSeconds(expiration)),
                        signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                    )));
                }
                else
                {
                    return JWTAuthorizationResult.Failure(new string[] { "Wrong password" });
                }
            }

            throw new Exception("signInManager cannot be null");
        }

        private IEnumerable<Claim> GetClaimTokens(string userID)
        {
            //For info about JWT claims
            //go to: https://auth0.com/docs/tokens/json-web-tokens/json-web-token-claims
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userID), //Subject
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()), //Issued at
            };
        }


        public async Task LogoutUserAsync(string userName)
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Result> ConfirmEmailAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Result> ChangePasswordAsync(string userName, string email, string oldPassword, string newPassword)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == email);
            var test = _userManager.Users.Count();
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            return result.ToApplicationResult();
        }

        public async Task<Result> ResetPasswordAsync(string userName, string email, string newPassword)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result.ToApplicationResult();
        }

        public async Task<Result> ChangeEmailAsync(string userName, string email, string newEmail)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == email);
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
            var result = await _userManager.ChangeEmailAsync(user, newEmail, token);
            return result.ToApplicationResult();
        }
    }
}
