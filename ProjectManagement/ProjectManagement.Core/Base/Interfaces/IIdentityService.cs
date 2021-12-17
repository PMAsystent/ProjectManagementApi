using ProjectManagement.Core.Base.Model;
using System.Threading.Tasks;

namespace ProjectManagement.Core.Base.Interfaces
{
    public interface IIdentityService
    {
        Task<(Result Result, string UserName, string Email, string Id)> RegisterUserAsync(string email, string userName, string password);
        Task<(JWTAuthorizationResult Result, string UserName, string Email)> LoginUserAsync(string email, string password);
        Task<Result> ChangeEmailAsync(string userName, string email, string newEmail);
        Task<Result> ChangePasswordAsync(string userName, string email, string oldPassword, string newPassword);
        Task<Result> ResetPasswordAsync(string userId, string email, string newPassword);
        Task<Result> ConfirmEmailAsync();
        Task<Result> DeleteUserAsync(string userId, string email, string password);
        Task<(Result Result, string UserName, string Email)> CheckTokenAsync(string token);
        Task<bool> CheckIfUserWithEmailExists(string email);
        Task<string> GetUserNameAsync(string userId);
    }
}