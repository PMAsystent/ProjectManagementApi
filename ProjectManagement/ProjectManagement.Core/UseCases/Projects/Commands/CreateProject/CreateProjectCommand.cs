using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Mappings;
using System;
using System.Collections.Generic;

namespace ProjectManagement.Core.UseCases.Projects.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest, IMapFrom<Project>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public ICollection<string> AssignedEmails { get; set; }
        public string CurrentUserId { get; set; }

        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<CreateProjectCommand, Project>();
        }
    }
}