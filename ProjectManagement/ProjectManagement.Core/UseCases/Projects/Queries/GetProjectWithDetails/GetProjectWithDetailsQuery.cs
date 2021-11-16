using MediatR;
using ProjectManagement.Core.UseCases.Projects.Dto;

namespace ProjectManagement.Core.UseCases.Projects.Queries.GetProjectWithDetails
{
    public class GetProjectWithDetailsQuery : IRequest<DetailedProjectDto>
    {
        public int ProjectId { get; set; }
    }
}
