using System.Collections.Generic;

namespace ProjectManagement.Core.Concrete.Identity.Dto
{
    public class ResetPasswordDto
    {
        public bool IsReseted { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
