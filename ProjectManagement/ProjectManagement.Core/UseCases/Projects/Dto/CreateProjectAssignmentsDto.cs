namespace ProjectManagement.Core.UseCases.Projects.Dto
{
    public class CreateProjectAssignmentsDto
    {
        public int UserId { get; set; }
        public string ProjectRole { get; set; }
        public string MemberType { get; set; }
    }
}