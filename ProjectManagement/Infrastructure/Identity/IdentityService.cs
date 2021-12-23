using Infrastructure.Identity.Helpers;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.Base.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthSettings _authSettings;
        private readonly EmailProviderSettings _emailSettings;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            IOptions<AuthSettings> settingsAuth,
            IOptions<EmailProviderSettings> settingsEmail)
        {
            _userManager = userManager;
            _authSettings = settingsAuth.Value;
            _emailSettings = settingsEmail.Value;
        }

        public async Task<(Result Result, string UserName, string Email, string Id)> RegisterUserAsync(string email, string userName, string password, string apiUrl)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = email,
            };

            var userCheckEmail = _userManager.Users.SingleOrDefault(u => u.Email == email);
            var userCheckName = _userManager.Users.SingleOrDefault(u => u.UserName == userName);

            if (userCheckEmail != null || userCheckName != null)
            {
                return (Result.Failure(new List<string> { "User already exist" }), "", "", "");
            }

            var result = await _userManager.CreateAsync(user, password);

            string userNameResponse = "";
            string emailResponse = "";
            string idResponse = "";

            if (result.Succeeded)
            {
                userNameResponse = userName;
                emailResponse = email;
                var userCreated = await _userManager.FindByEmailAsync(email);
                idResponse = userCreated.Id;
                var emailResult = await SendAccountConfirmationEmail(userCreated, apiUrl, email);
                return (result.ToApplicationResult(), userNameResponse, emailResponse, idResponse);
               
                /*if(!emailResult.Succeeded)
                {
                    await _userManager.DeleteAsync(userCreated);
                    return (emailResult, "", "", "");
                }*/
            }

            return (result.ToApplicationResult(), userNameResponse, emailResponse, idResponse);
        }

        public async Task<Result> SendAccountConfirmationEmail(ApplicationUser user, string apiUrl, string email)
        {
            try
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = apiUrl + $"api/Auth/ConfirmEmail?userId={user.Id}&token={token}";


                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Email Test", _emailSettings.SenderEmail));

                message.To.Add(new MailboxAddress("User name", email));

                message.Subject = "Confirm link: ";

                message.Body = new TextPart("plain")
                {
                    Text = "<html><body> " + confirmationLink + " </body></html>",
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(_emailSettings.SenderServer, 587, false);
                    client.Authenticate(_emailSettings.SenderEmail, "1234azer&");
                    client.Send(message);
                    client.Disconnect(true);
                }



                /*MailMessage message = new MailMessage
                {
                    From = new MailAddress(_emailSettings.SenderEmail),
                    Subject = "Account confirmation",
                    Body = "<html><body> " + confirmationLink + " </body></html>",
                    IsBodyHtml = true
                };

                message.To.Add(new MailAddress(email));

                var smtpClient = new SmtpClient(_emailSettings.SenderServer)
                {
                    Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.Password),
                    EnableSsl = true,
                };

                await smtpClient.SendMailAsync(message);*/

                return Result.Success();
            }catch(Exception e)
            {
                return Result.Failure(new List<string> { e.Message });
            }
        }
        public async Task<Result> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                return result.ToApplicationResult();
            }
            else if (user == null)
            {
                return (Result.Failure(new List<string> { "User not found" }));
            }
            else
            {
                return (Result.Failure(new List<string> { "Wrong confirmation  data" }));
            }
        }

        public async Task<(JWTAuthorizationResult Result, string UserName, string Email)> LoginUserAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return (JWTAuthorizationResult.Failure(new string[] { "Email not found" }), "", "");

            /*if (!user.EmailConfirmed)
                return (JWTAuthorizationResult.Failure(new string[] { "Email not confirmed" }), "", "");*/

            var signResult = await _userManager.CheckPasswordAsync(user, password);

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
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.AuthKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var apiresult = JWTAuthorizationResult.Success(new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                issuer: _authSettings.Issuer,
                audience: _authSettings.Audience,
                claims: GetTokenClaims(user),
                notBefore: DateTime.Now,
                expires: DateTime.Now.Add(TimeSpan.FromSeconds(_authSettings.Expire)),
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

        public async Task<Result> SendResetPasswordEmail(string userId, string apiUrl, string email)
        {
            try
            {
                var userById = await _userManager.FindByIdAsync(userId);
                var userByEmail = await _userManager.FindByEmailAsync(email);

                if (userByEmail == null || userById == null)
                {
                    return (Result.Failure(new List<string> { "User not found" }));
                }
                else if (userById.Id != userByEmail.Id)
                {
                    return (Result.Failure(new List<string> { "User does not match token" }));
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(userById);
                var resetPasswordLink = apiUrl + $"api/Auth/token={token}";
















            /*MailMessage message = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail),
                Subject = "Password reset link: ",
                Body = "<html><body> " + resetPasswordLink + " </body></html>",
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(email));

            var smtpClient = new SmtpClient(_emailSettings.SenderServer)
            {
                Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.Password),
                EnableSsl = true,
            };

            await smtpClient.SendMailAsync(message);*/

            return (Result.Success());
            }
            catch(Exception e)
            {
                return (Result.Failure(new List<string> { e.Message }));
            }
        }

        public async Task<Result> ResetPasswordAsync(string userId, string email, string newPassword, string token)
        {
            var userById = await _userManager.FindByIdAsync(userId);
            var userByEmail = await _userManager.FindByEmailAsync(email);

            if (userByEmail == null || userById == null)
            {
                return (Result.Failure(new List<string> { "User not found" }));
            }
            else if (userById.Id != userByEmail.Id)
            {
                return (Result.Failure(new List<string> { "User does not match token" }));
            }
            else if (userByEmail != null && userById!=null && userById.Id == userByEmail.Id)
            {
                var result = await _userManager.ResetPasswordAsync(userByEmail, token, newPassword);
                return result.ToApplicationResult();
            }
            else
            {
                return (Result.Failure(new List<string> { "Wrong data" }));
            }
        }

        public async Task<Result> ChangePasswordAsync(string userId, string email, string oldPassword, string newPassword)
        {
            var userById = await _userManager.FindByIdAsync(userId);
            var userByEmail = await _userManager.FindByEmailAsync(email);

            if (userByEmail == null || userById == null)
            {
                return (Result.Failure(new List<string> { "User not found" }));
            }
            else if (userById.Id != userByEmail.Id)
            {
                return (Result.Failure(new List<string> { "User does not match token" }));
            }
            else if (userByEmail != null && userById != null && userById.Id == userByEmail.Id)
            {
                var result = await _userManager.ChangePasswordAsync(userByEmail, oldPassword, newPassword);
                return result.ToApplicationResult();
            }
            else
            {
                return (Result.Failure(new List<string> { "Wrong data" }));
            }
        }

        public async Task<Result> SendChangeEmailAddressEmail(string userId, string apiUrl, string email, string newEmail)
        {
            try
            {
                var userById = await _userManager.FindByIdAsync(userId);
                var userByEmail = await _userManager.FindByEmailAsync(email);

                if (userByEmail == null || userById == null)
                {
                    return (Result.Failure(new List<string> { "User not found" }));
                }
                else if (userById.Id != userByEmail.Id)
                {
                    return (Result.Failure(new List<string> { "User does not match token" }));
                }

                var token = await _userManager.GenerateChangeEmailTokenAsync(userById, newEmail);
                var changeEmailLink = apiUrl + $"api/Auth/{token}";

                /*MailMessage message = new MailMessage
                {
                    From = new MailAddress(_emailSettings.SenderEmail),
                    Subject = "Change email link: ",
                    Body = "<html><body> " + changeEmailLink + " </body></html>",
                    IsBodyHtml = true
                };

                message.To.Add(new MailAddress(email));

                var smtpClient = new SmtpClient(_emailSettings.SenderServer)
                {
                    Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.Password),
                    EnableSsl = true,
                };

                await smtpClient.SendMailAsync(message);*/

                return (Result.Success());
            }
            catch (Exception e)
            {
                return (Result.Failure(new List<string> { e.Message }));
            }
        }

        public async Task<Result> ChangeEmailAsync(string userId, string email, string newEmail, string token)
        {
            var userById = await _userManager.FindByIdAsync(userId);
            var userByEmail = await _userManager.FindByEmailAsync(email);

            if (userByEmail == null || userById == null)
            {
                return (Result.Failure(new List<string> { "User not found" }));
            }
            else if (userById.Id != userByEmail.Id)
            {
                return (Result.Failure(new List<string> { "User does not match token" }));
            }
            else if (userByEmail != null && userById != null && userById.Id == userByEmail.Id)
            {
                var result = await _userManager.ChangeEmailAsync(userByEmail, newEmail, token);
                return result.ToApplicationResult();
            }
            else
            {
                return (Result.Failure(new List<string> { "Wrong data" }));
            }
        }

        public async Task<Result> DeleteUserAsync(string userId, string email, string password)
        {
            var userById = await _userManager.FindByIdAsync(userId);
            var userByEmail = await _userManager.FindByEmailAsync(email);

            if (userByEmail == null || userById == null)
            {
                return (Result.Failure(new List<string> { "User not found" }));
            }
            else if (userById.Id != userByEmail.Id)
            {
                return (Result.Failure(new List<string> { "User does not match token" }));
            }
            else if (userByEmail != null && userById != null && userById.Id == userByEmail.Id)
            {
                var result = await _userManager.DeleteAsync(userByEmail);
                return result.ToApplicationResult();
            }
            else
            {
                return (Result.Failure(new List<string> { "Wrong data" }));
            }
        }

        public async Task<bool> CheckIfUserWithEmailExists(string email) =>
            await _userManager.Users.SingleOrDefaultAsync(u => u.Email == email) != null;

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<Result> LogoutUserAsync(string userId, string email)
        {
            var userById = await _userManager.FindByIdAsync(userId);
            var userByEmail = await _userManager.FindByEmailAsync(email);

            if (userByEmail == null || userById == null)
            {
                return (Result.Failure(new List<string> { "User not found" }));
            }
            else if (userById.Id != userByEmail.Id)
            {
                return (Result.Failure(new List<string> { "User does not match token" }));
            }
            else if (userByEmail != null && userById != null && userById.Id == userByEmail.Id)
            {
                var result = await _userManager.SetAuthenticationTokenAsync(userByEmail, userByEmail.Email, "JWT", _authSettings.LogoutToken);
                return result.ToApplicationResult();
            }
            else
            {
                return (Result.Failure(new List<string> { "Wrong data" }));
            }
        }

        public async Task<bool> CheckLogoutAsync(string userId)
        {
            if(userId==null)
            {
                return true;
            }
            var userById = await _userManager.FindByIdAsync(userId);
            var userToken = await _userManager.GetAuthenticationTokenAsync(userById, userById.Email, "JWT");
            if(userToken==_authSettings.LogoutToken)
            {
                return false;
            }
            return true;
        }

        public async Task<(Result Result, string UserName, string Email)> CheckTokenAsync(string token)
        {
            var allUsers = await _userManager.Users.ToListAsync();
            for (int i = 0; i < allUsers.Count; i++)
            {
                var userToken = await _userManager.GetAuthenticationTokenAsync(allUsers[i], allUsers[i].Email, "JWT");
                if (token == userToken)
                {
                    return (Result.Success(), allUsers[i].UserName, allUsers[i].Email);
                }
            }

            return (Result.Failure(new List<string> { "User not found" }), "", "");
        }

        public async Task<JWTAuthorizationResult> RefreshToken(string userId, string email)
        {
            var userById = await _userManager.FindByIdAsync(userId);
            var userByEmail = await _userManager.FindByEmailAsync(email);

            if (userByEmail == null || userById == null)
            {
                return (JWTAuthorizationResult.Failure(new List<string> { "User not found" }));
            }
            else if (userById.Id != userByEmail.Id)
            {
                return (JWTAuthorizationResult.Failure(new List<string> { "User does not match token" }));
            }
            else if (userByEmail != null && userById != null && userById.Id == userByEmail.Id)
            {
                var apiresult = CreateToken(userByEmail);
                await _userManager.SetAuthenticationTokenAsync(userByEmail, userByEmail.Email, "JWT", apiresult.Token);
                return apiresult;
            }
            else
            {
                return (JWTAuthorizationResult.Failure(new List<string> { "Wrong data" }));
            }
        }
    }
}
