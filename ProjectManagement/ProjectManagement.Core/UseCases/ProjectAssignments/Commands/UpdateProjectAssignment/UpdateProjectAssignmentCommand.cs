using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.ProjectAssignments.Commands.UpdateProjectAssignment
{
    public class UpdateProjectAssignmentCommand : IRequest, IMapFrom<ProjectAssignment>
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string MemberType { get; set; }
        public string ProjectRole { get; set; }   
        
        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<UpdateProjectAssignmentCommand, ProjectAssignment>();
        }
    }
}