using MediatR.Behaviors.Authorization;
using ProjectManagement.Core.Authorization;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.Subtasks.Commands.UpdateSubtaskName
{
    public class UpdateSubtaskNameAuthorizer : AbstractRequestAuthorizer<UpdateSubtaskNameCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public UpdateSubtaskNameAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        
        public override void BuildPolicy(UpdateSubtaskNameCommand request)
        {
            UseRequirement(new AssignedToProjectRequirement
            {
                SubtaskId = request.SubtaskId,
                UserId = _currentUserService.UserId
            });
        }
    }
}