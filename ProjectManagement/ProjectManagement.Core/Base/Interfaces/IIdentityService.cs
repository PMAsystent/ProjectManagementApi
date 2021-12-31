using ProjectManagement.Core.Base.Model;
using System.Threading.Tasks;

namespace ProjectManagement.Core.Base.Interfaces
{
    public interface IIdentityService
    {
        Task<(Result Result, string UserName, string Email, string Id)> RegisterUserAsync(string email, string userName, string password);
        Task<(JWTAuthorizationResult Result, string UserName, string Email)> LoginUserAsync(string email, string password);
        Task<Result> ChangeEmailAsync(string userId, string email, string newEmail, string token);
        Task<Result> ChangePasswordAsync(string userId, string email, string oldPassword, string newPassword);
        Task<Result> ResetPasswordAsync(string userId, string email, string newPassword, string token);
        Task<Result> LogoutUserAsync(string userId, string email);
        Task<Result> ConfirmEmailAsync(string userId, string token);
        Task<Result> DeleteUserAsync(string userId, string email, string password);
        Task<(Result Result, string UserName, string Email)> CheckTokenAsync(string token);
        Task<bool> CheckIfUserWithEmailExists(string email);
        Task<string> GetUserNameAsync(string userId);
        Task<Result> SendResetPasswordEmail(string userId, string email);
        Task<Result> SendChangeEmailAddressEmail(string userId, string email, string newEmail);
        Task<bool> CheckLogoutAsync(string userId);
        Task<JWTAuthorizationResult> RefreshToken(string userId, string email);
    }
}