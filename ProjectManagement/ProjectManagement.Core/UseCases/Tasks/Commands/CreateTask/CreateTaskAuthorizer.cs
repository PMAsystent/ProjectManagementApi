using FluentValidation;
using MediatR.Behaviors.Authorization;
using ProjectManagement.Core.Authorization;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.Tasks.Commands.CreateTask
{
    public class CreateTaskAuthorizer : AbstractRequestAuthorizer<CreateTaskCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public CreateTaskAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override void BuildPolicy(CreateTaskCommand request)
        {
            UseRequirement(new AssignedToProjectRequirement()
            {
                StepId = request.StepId,
                UserId = _currentUserService.UserId
            });
        }
    }
}