using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProjectManagement.Core.UseCases.Tasks.Dto;

namespace ProjectManagement.Core.UseCases.Tasks.Commands.PatchTask
{
    public class PatchTaskCommand : IRequest<PatchTaskCommandResponse>
    {
        public int TaskId { get; set; }
        public JsonPatchDocument<TaskDto> PatchDocument { get; set; }
        public ModelStateDictionary ModelStateDictionary { get; set; }
    }
}
