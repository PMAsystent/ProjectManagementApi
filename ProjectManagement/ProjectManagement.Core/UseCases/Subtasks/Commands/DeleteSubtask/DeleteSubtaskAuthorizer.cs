using MediatR.Behaviors.Authorization;
using ProjectManagement.Core.Authorization;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.Subtasks.Commands.DeleteSubtask
{
    public class DeleteSubtaskAuthorizer : AbstractRequestAuthorizer<DeleteSubtaskCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public DeleteSubtaskAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        
        public override void BuildPolicy(DeleteSubtaskCommand request)
        {
            UseRequirement(new AssignedToProjectRequirement
            {
                SubtaskId = request.SubtaskId,
                UserId = _currentUserService.UserId
            });
        }
    }
}