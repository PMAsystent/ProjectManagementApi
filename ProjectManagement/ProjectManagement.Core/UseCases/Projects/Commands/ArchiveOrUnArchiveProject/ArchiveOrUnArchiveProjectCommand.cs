using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.Projects.Commands.ArchiveOrUnArchiveProject
{
    public class ArchiveOrUnArchiveProjectCommand : IRequest, IMapFrom<Project>
    {
        public bool IsActive { get; set; }
        public int ProjectId { get; set; }
        
        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<ArchiveOrUnArchiveProjectCommand, Project>();
        }
    }
}