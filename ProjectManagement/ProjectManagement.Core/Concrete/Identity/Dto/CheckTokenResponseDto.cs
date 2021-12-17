using ProjectManagement.Core.UseCases.Users.Queries.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Core.Concrete.Identity.Dto
{
    public class CheckTokenResponseDto
    {
        public UserDto User { get; set; }

        public List<string> Errors { get; set; }
    }
}
