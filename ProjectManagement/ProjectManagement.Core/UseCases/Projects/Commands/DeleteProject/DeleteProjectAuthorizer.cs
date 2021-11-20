using MediatR.Behaviors.Authorization;
using ProjectManagement.Core.Authorization;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Projects.Commands.DeleteProject;

namespace ProjectManagement.Core.UseCases.Projects.Commands.DeleteProject
{
    public class DeleteProjectAuthorizer : AbstractRequestAuthorizer<DeleteProjectCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public DeleteProjectAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        
        public override void BuildPolicy(DeleteProjectCommand request)
        {
            UseRequirement(new ProjectAuthorRequirement
            {
                ProjectId = request.ProjectId,
                UserId = _currentUserService.UserId
            });
        }
    }
}
