using System.Collections.Generic;

namespace ProjectManagement.Core.Concrete.Identity.Dto
{
    public class SendChangeAddressEmailDto
    {
        public bool IsSented { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
