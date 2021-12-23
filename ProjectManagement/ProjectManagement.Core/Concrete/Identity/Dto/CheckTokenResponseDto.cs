using ProjectManagement.Core.UseCases.Users.Queries.Dto;
using System.Collections.Generic;

namespace ProjectManagement.Core.Concrete.Identity.Dto
{
    public class CheckTokenResponseDto
    {
        public UserDto User { get; set; }

        public List<string> Errors { get; set; }
    }
}
