using System.Collections.Generic;

namespace ProjectManagement.Core.Concrete.Identity.Dto
{
    public class ChangeEmailDto
    {
        public bool IsChanged { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
