using System.Collections.Generic;

namespace ProjectManagement.Core.Concrete.Identity.Dto
{
    public class RegisterResponseDto
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public List<string> Errors { get; set; }
    }
}
