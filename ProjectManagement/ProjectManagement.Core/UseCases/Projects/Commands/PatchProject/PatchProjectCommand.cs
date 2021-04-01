using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProjectManagement.Core.UseCases.Projects.Dto;

namespace ProjectManagement.Core.UseCases.Projects.Commands.PatchProject
{
    public class PatchProjectCommand : IRequest<PatchProjectCommandResponse>
    {
        public int ProjectId { get; set; }
        public JsonPatchDocument<ProjectDto> PatchDocument { get; set; }
        public ModelStateDictionary ModelStateDictionary { get; set; }
    }
}
