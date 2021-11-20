using MediatR.Behaviors.Authorization;
using ProjectManagement.Core.Authorization;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.Projects.Commands.ArchiveOrUnArchiveProject
{
    public class ArchiveOrUnArchiveProjectAuthorizer : AbstractRequestAuthorizer<ArchiveOrUnArchiveProjectCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public ArchiveOrUnArchiveProjectAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override void BuildPolicy(ArchiveOrUnArchiveProjectCommand request)
        {
            UseRequirement(new SuperMemberRequirement()
            {
                ProjectId = request.ProjectId,
                UserId = _currentUserService.UserId
            });
        }
    }
}