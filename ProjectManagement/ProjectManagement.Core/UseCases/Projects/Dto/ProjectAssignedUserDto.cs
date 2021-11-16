namespace ProjectManagement.Core.UseCases.Projects.Dto
{
    public class ProjectAssignedUserDto
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }

        public string MemberType { get; set; }
    }
}