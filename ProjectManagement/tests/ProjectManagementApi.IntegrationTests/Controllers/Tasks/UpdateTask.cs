using System;
using System.Threading.Tasks;
using Domain.Enums;
using ProjectManagement.Core.UseCases.Tasks.Commands.UpdateTask;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace ProjectManagementApi.IntegrationTests.Controllers.Tasks
{
    [Collection("Sequential")]
    public class UpdateTask : IntegrationTest
    {
        public UpdateTask(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory, output)
        {
        }
        
        [Fact]
        public async Task UpdateTaskSuccessful()
        {
            var request = new UpdateTaskCommand()
            {
                Id = Utilities.P1S1Task1.Id,
                Name = "updated task",
                Description = "test description",
                DueDate = DateTime.UtcNow,
                Priority = TaskPriority.High.ToString(),
                StepId = Utilities.P1Step1.Id
            };

            var response = await Client.PutAsync("api/Tasks", Utilities.GetRequestContent(request));
            response.EnsureSuccessStatusCode();
        }
    }
}