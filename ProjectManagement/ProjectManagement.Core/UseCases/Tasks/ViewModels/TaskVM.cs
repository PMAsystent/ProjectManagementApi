using System.Collections.Generic;
using ProjectManagement.Core.UseCases.Tasks.Dto;

namespace ProjectManagement.Core.UseCases.Tasks.ViewModels
{
    public class TaskVM
    {
        public IList<TaskDto> TaskList { get; set; }
        public int Count { get; set; }
    }
}
