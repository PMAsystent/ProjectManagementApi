using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using ProjectManagement.Core.UseCases.Projects.Dto;

namespace ProjectManagement.Core.UseCases.Projects.Commands.PatchProject
{
    public class PatchProjectCommand : IRequest<int>
    {
        public int ProjectId { get; set; }
        public JsonPatchDocument<ProjectDto> PatchDocument { get; set; }
    }
}
