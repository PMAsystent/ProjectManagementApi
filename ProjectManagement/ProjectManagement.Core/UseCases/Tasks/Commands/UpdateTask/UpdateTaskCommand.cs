using System;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommand : IRequest<Unit>, IMapFrom<Task>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string TaskStatus { get; set; }
        public DateTime DueDate { get; set; }
        public int StepId { get; set; }
        
        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<UpdateTaskCommand, Task>();
        }
    }
}
