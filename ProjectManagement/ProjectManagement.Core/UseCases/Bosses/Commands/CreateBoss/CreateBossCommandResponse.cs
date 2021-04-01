using FluentValidation.Results;
using ProjectManagement.Core.Base.Responses;
using ProjectManagement.Core.UseCases.Bosses.Dto;

namespace ProjectManagement.Core.UseCases.Bosses.Commands.CreateBoss
{
    public class CreateBossCommandResponse : BaseResponse
    {
        public DetailedBossDto DetailedBossDto { get; set; }

        public CreateBossCommandResponse() : base() { }
        public CreateBossCommandResponse(ValidationResult validationResult) : base(validationResult) { }
        public CreateBossCommandResponse(string message) : base(message) { }
        public CreateBossCommandResponse(string message, bool success) : base(message, success) { }
        public CreateBossCommandResponse(DetailedBossDto detailedCustomerDto) => DetailedBossDto = detailedCustomerDto;
    }
}
