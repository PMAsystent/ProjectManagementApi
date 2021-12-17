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
        private readonly AppSettings _settings;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            IOptions<AppSettings> settings)
        {
            _userManager = userManager;
            _settings = settings.Value;
        }

        public async Task<(Result Result, string UserName, string Email, string Id)> RegisterUserAsync(string email, string userName, string password)
        {
            //Create User 
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = email,
            };

            //Check if user already exist
            var userCheckEmail = _userManager.Users.SingleOrDefault(u => u.Email == email);
            var userCheckName = _userManager.Users.SingleOrDefault(u => u.UserName == userName);

            if (userCheckEmail != null || userCheckName != null)
            {
                return (Result.Failure(new List<string> { "User already exist" }), "", "", "");
            }

            //Create user
            var result = await _userManager.CreateAsync(user, password);

            //Check if user was corectly created
            string userNameResponse = "";
            string emailResponse = "";
            string idResponse = "";
            if (result.Succeeded)
            {
                userNameResponse = userName;
                emailResponse = email;
                var userCreated = await _userManager.FindByEmailAsync(email);
                idResponse = userCreated.Id;
            }

            //Return proper response
            return (result.ToApplicationResult(), userNameResponse, emailResponse, idResponse);
        }

        public async Task<(JWTAuthorizationResult Result, string UserName, string Email)> LoginUserAsync(string email, string password)
        {
            //Check if user exists
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return (JWTAuthorizationResult.Failure(new string[] { "Email not found" }), "", "");

            //Sign in User
            var signResult = await _userManager.CheckPasswordAsync(user, password);

            //Create token response
            if (signResult)
            {
                var apiresult = CreateToken(user);
                await _userManager.SetAuthenticationTokenAsync(user, user.Email, "JWT", apiresult.Token);
                return (apiresult, user.UserName, user.Email);
            }
            else
            {
                return (JWTAuthorizationResult.Failure(new string[] { "Wrong password" }), "", "");
            }
        }

        private JWTAuthorizationResult CreateToken(ApplicationUser user)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.AuthKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var apiresult = JWTAuthorizationResult.Success(new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: GetTokenClaims(user),
                notBefore: DateTime.Now,
                expires: DateTime.Now.Add(TimeSpan.FromSeconds(_settings.Expire)),
                signingCredentials: signingCredentials
            )));

            return apiresult;
        }
        private IEnumerable<Claim> GetTokenClaims(ApplicationUser user)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
            };
        }

        public Task<Result> ChangeEmailAsync(string userName, string email, string newEmail)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> ChangePasswordAsync(string userId, string email, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && user.Id == userId)
            {
                var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
                return result.ToApplicationResult();
            }
            return (Result.Failure(new List<string> { "Wrong user" }));
        }

        public async Task<Result> ResetPasswordAsync(string userId, string email, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null && user.Id == userId)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
                return result.ToApplicationResult();
            }
            else if (user == null)
            {
                return (Result.Failure(new List<string> { "User not found" }));
            }
            else
            {
                return (Result.Failure(new List<string> { "Wrong user id" }));
            }
        }

        public Task<Result> ConfirmEmailAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Result> DeleteUserAsync(string userId, string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var signResult = await _userManager.CheckPasswordAsync(user, password);

            if (user != null && user.Id == userId && signResult)
            {
                var result = await _userManager.DeleteAsync(user);

                return result.ToApplicationResult();
            }
            else if (user == null)
            {
                return (Result.Failure(new List<string> { "User not found" }));
            }
            else if (user.Id == userId)
            {
                return (Result.Failure(new List<string> { "Wrong user id" }));
            }
            else
            {
                return (Result.Failure(new List<string> { "Wrong password" }));
            }
        }
        public async Task<(Result Result, string UserName, string Email)> CheckTokenAsync(string token)
        {
            var allUsers = await _userManager.Users.ToListAsync();
            for (int i = 0; i < allUsers.Count(); i++)
            {
                var userToken = await _userManager.GetAuthenticationTokenAsync(allUsers[i], allUsers[i].Email, "JWT");
                if (token == userToken)
                {
                    return (Result.Success(), allUsers[i].UserName, allUsers[i].Email);
                }
            }
            return (Result.Failure(new List<string> { "User not found" }), "", "");
        }

        public Task<bool> CheckIfUserWithEmailExists(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }
    }
}
