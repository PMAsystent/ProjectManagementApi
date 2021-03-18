using MediatR;

namespace ProjectManagement.Core.UseCases.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommand : IRequest
    {
        public int ProjectId { get; set; }
    }
}
