using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Core.Concrete.Identity.Dto
{
    public class ChangePasswordDto
    {
        public bool IsChanged { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
