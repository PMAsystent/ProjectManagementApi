using System.Collections.Generic;
using ProjectManagement.Core.UseCases.Steps.Dto;

namespace ProjectManagement.Core.UseCases.Steps.ViewModels
{
    public class StepVm
    {
        public IList<StepDto> StepList { get; set; }
        public int Count { get; set; }
    }
}
