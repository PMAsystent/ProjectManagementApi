using System;
using System.Collections.Generic;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.Steps.Commands.CreateStep
{
    public class CreateStepCommand : IRequest<CreateStepCommandResponse>, IMapFrom<Step>
    {
        public string Name { get; set; }
        public int ProjectId { get; set; }

        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<CreateStepCommand, Step>();
        }
    }
}