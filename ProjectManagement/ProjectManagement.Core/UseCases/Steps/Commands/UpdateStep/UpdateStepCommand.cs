using System;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.Steps.Commands.UpdateStep
{
    public class UpdateStepCommand : IRequest<UpdateStepCommandResponse>, IMapFrom<Step>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<UpdateStepCommand, Step>();
        }
    }
}
