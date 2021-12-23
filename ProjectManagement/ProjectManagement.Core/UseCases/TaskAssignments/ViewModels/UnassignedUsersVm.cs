using System.Collections.Generic;
using ProjectManagement.Core.UseCases.TaskAssignments.Dto;

namespace ProjectManagement.Core.UseCases.TaskAssignments.ViewModels
{
    public class UnassignedUsersVm
    {
        public ICollection<UnassignedUserDto> UnassignedUser { get; set; }
        public int Count { get; set; }
    }
}
