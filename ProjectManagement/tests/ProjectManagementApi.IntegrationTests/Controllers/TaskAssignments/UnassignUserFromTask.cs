using System;
using System.Net.Http;
using System.Threading.Tasks;
using ProjectManagement.Core.UseCases.TaskAssignments.Commands.AssignUserToTask;
using ProjectManagement.Core.UseCases.TaskAssignments.Commands.UnassignUserFromTask;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace ProjectManagementApi.IntegrationTests.Controllers.TaskAssignments
{
    [Collection("Sequential")]
    public class UnassignUserFromTask : IntegrationTest
    {
        public UnassignUserFromTask(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory, output)
        {
        }

        [Fact]
        public async Task UnassignUserFromTaskSuccessful()
        {
            var content = new UnassignUserFromTaskCommand()
            {
                TaskId = Utilities.P1S1Task1.Id,
                UserId = Utilities.User1.Id
            };
            
            var request = new HttpRequestMessage
            {
                Content = Utilities.GetRequestContent(content),
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"/api/TaskAssignment", UriKind.Relative)
            };
            
            var response = await Client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}