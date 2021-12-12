using Domain.Entities;
using ProjectManagement.Core.Base.Mappings;
using System;
using System.Collections.Generic;

namespace ProjectManagement.Core.UseCases.Projects.Dto
{
    public class DetailedProjectDto : IMapFrom<Project>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int ProgressPercentage { get; set; }
        public DateTime Created { get; set; }
        public ICollection<ProjectStepDto> ProjectSteps { get; set; }
        public ICollection<ProjectTaskDto> ProjectTasks { get; set; }
        public ICollection<ProjectAssignedUserDto> ProjectAssignedUsers { get; set; }
        public CurrentUserInfoInProject CurrentUserInfoInProject { get; set; }

        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Project, DetailedProjectDto>()
                .ForMember(
                    dest => dest.ProjectSteps,
                    opt => opt.MapFrom(
                        src => src.Steps))
                .ReverseMap();
        }
    }
}