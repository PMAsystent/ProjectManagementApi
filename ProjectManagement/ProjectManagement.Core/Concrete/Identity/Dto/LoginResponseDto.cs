using ProjectManagement.Core.UseCases.Users.Queries.Dto;

namespace ProjectManagement.Core.Concrete.Identity.Dto
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
    }
}
