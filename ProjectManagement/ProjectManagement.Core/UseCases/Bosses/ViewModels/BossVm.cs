using ProjectManagement.Core.UseCases.Bosses.Dto;
using System.Collections.Generic;

namespace ProjectManagement.Core.UseCases.Bosses.ViewModels
{
    public class BossVm
    {
        public IList<BossDto> Bosses { get; set; }
        public int Count { get; set; }
    }
}
