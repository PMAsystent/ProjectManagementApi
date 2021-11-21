using MediatR.Behaviors.Authorization;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.Tasks.Commands.DeleteTask
{
    public class DeleteTaskAuthorizer:AbstractRequestAuthorizer<DeleteTaskCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public DeleteTaskAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        public override void BuildPolicy(DeleteTaskCommand request)
        {
            throw new System.NotImplementedException();
        }
    }
}