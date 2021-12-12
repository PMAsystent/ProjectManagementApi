using System;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using ProjectManagement.Core.Base.Mappings;
using ProjectManagement.Core.UseCases.Tasks.Dto;

namespace ProjectManagement.Core.UseCases.Tasks.Commands.CreateTask
{
    public class CreateTaskCommand : IRequest<DetailedTaskDto>, IMapFrom<Task>
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string TaskStatus { get; set; }
        public DateTime DueDate { get; set; }
        public int StepId { get; set; }
        
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<CreateTaskCommand, Task>();
        }
    }
}
