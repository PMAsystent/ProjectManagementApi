using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Mappings;
using System;
using ProjectManagement.Core.UseCases.Projects.Dto;

namespace ProjectManagement.Core.UseCases.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommand : IRequest, IMapFrom<Project>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<UpdateProjectCommand, Project>();
        }
    }
}