namespace ProjectManagement.Core.UseCases.Projects.Dto
{
    public class ProjectAssignedUserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string MemberType { get; set; }
        public string ProjectRole { get; set; }
    }
}