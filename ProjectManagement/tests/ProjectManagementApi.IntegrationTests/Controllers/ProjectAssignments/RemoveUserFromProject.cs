using System;
using System.Net.Http;
using System.Threading.Tasks;
using ProjectManagement.Core.UseCases.ProjectAssignments.Commands.RemoveUserFromProject;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace ProjectManagementApi.IntegrationTests.Controllers.ProjectAssignments
{
    [Collection("Sequential")]
    public class RemoveUserFromProject : IntegrationTest
    {
        public RemoveUserFromProject(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory, output)
        {
        }

        [Fact]
        public async Task RemoveUserFromProjectSuccessful()
        {
            var removeUserFromProjectCommand = new RemoveUserFromProjectCommand()
            {
                UserId = Utilities.User3.Id,
                ProjectId = Utilities.Project1.Id
            };
            
            var request = new HttpRequestMessage
            {
                Content = Utilities.GetRequestContent(removeUserFromProjectCommand),
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"/api/ProjectAssignments", UriKind.Relative)
            };
            
            var response = await Client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
        
        // [Fact]
        // public async Task RemoveUserFromProjectWhenUnauthorized()
        // {
        //     throw new NotImplementedException();
        // }
    }
}