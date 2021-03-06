using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Mappings;
using System;

namespace ProjectManagement.Core.UseCases.Projects.Commands.CreateProject
{
    public class CreatePostCommand : IRequest<CreatePostCommandResponse>, IMapFrom<Project>
    {
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime TargetDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public int CustomerId { get; set; }

        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<CreatePostCommand, Project>();
        }
    }
}
