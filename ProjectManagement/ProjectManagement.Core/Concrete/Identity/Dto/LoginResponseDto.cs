using ProjectManagement.Core.UseCases.Users.Queries.Dto;
using System.Collections.Generic;

namespace ProjectManagement.Core.Concrete.Identity.Dto
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
