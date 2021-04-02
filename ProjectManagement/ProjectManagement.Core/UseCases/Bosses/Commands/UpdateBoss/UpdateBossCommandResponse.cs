using FluentValidation.Results;
using ProjectManagement.Core.Base.Responses;
using ProjectManagement.Core.UseCases.Bosses.Dto;

namespace ProjectManagement.Core.UseCases.Bosses.Commands.UpdateBoss
{
    public class UpdateBossCommandResponse : BaseResponse
    {
        public DetailedBossDto DetailedBossDto { get; set; }

        public UpdateBossCommandResponse() : base() { }
        public UpdateBossCommandResponse(ValidationResult validationResult) : base(validationResult) { }
        public UpdateBossCommandResponse(string message) : base(message) { }
        public UpdateBossCommandResponse(string message, bool success) : base(message, success) { }
        public UpdateBossCommandResponse(DetailedBossDto detailedCustomerDto) => DetailedBossDto = detailedCustomerDto;
    }
}
