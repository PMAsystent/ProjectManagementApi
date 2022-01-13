using System;
using System.Threading.Tasks;
using ProjectManagement.Core.UseCases.TaskAssignments.Commands.AssignUserToTask;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace ProjectManagementApi.IntegrationTests.Controllers.TaskAssignments
{
    [Collection("Sequential")]
    public class AssignUserToTask : IntegrationTest
    {
        public AssignUserToTask(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory,
            output)
        {
        }

        [Fact]
        public async Task AssignUserToTaskSuccessful()
        {
            var request = new AssignUserToTaskCommand()
            {
                TaskId = Utilities.P1S1Task1.Id,
                UserId = Utilities.User3.Id
            };
            var response = await Client.PostAsync("api/TaskAssignment", Utilities.GetRequestContent(request));                                                      
            response.EnsureSuccessStatusCode();
        }
        
        // [Fact]
        // public async Task AssignUserToTaskWhoIsNotInProject()
        // {
        //     throw new NotImplementedException();
        // }
    }
}