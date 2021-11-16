using System.Collections.Generic;
using ProjectManagement.Core.UseCases.Users.Queries.Dto;

namespace ProjectManagement.Core.UseCases.Users.ViewsModels
{
    public class UserVm
    {
        public IList<UserDto> Users { get; set; }
        
        public int Count { get; set; }
    }
}
