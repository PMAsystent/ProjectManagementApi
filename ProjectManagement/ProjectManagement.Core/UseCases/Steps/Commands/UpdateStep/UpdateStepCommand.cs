using System;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Mappings;
using ProjectManagement.Core.UseCases.Steps.Commands.CreateStep;

namespace ProjectManagement.Core.UseCases.Steps.Commands.UpdateStep
{
    public class UpdateStepCommand : IRequest<UpdateStepCommandResponse>, IMapFrom<Step>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime TargetDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public int ProjectId { get; set; }

        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<UpdateStepCommand, Step>();
        }
    }
}
