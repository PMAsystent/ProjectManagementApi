using System;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using ProjectManagement.Core.Base.Mappings;
using ProjectManagement.Core.UseCases.Tasks.Dto;

namespace ProjectManagement.Core.UseCases.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommand : IRequest<UpdateTaskCommandResponse>, IMapFrom<Task>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime TargetDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public bool IsActive { get; set; }
       
        public int StepId { get; set; }
        
        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<UpdateTaskCommand, Task>();
        }
    }
}
