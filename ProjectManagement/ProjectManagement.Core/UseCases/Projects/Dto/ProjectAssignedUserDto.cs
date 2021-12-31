using Newtonsoft.Json;

namespace ProjectManagement.Core.UseCases.Projects.Dto
{
    public class ProjectAssignedUserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public string MemberType { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public string ProjectRole { get; set; }
    }
}