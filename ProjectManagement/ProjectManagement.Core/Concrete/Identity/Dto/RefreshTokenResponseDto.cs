using System.Collections.Generic;

namespace ProjectManagement.Core.Concrete.Identity.Dto
{
    public class RefreshTokenResponseDto
    {
        public string Token { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
