using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Core.UseCases.Steps.Dto;

namespace ProjectManagement.Core.UseCases.Steps.ViewModels
{
    public class StepVm
    {
        public IList<StepDto> StepList { get; set; }
        public int Count { get; set; }
    }
}
