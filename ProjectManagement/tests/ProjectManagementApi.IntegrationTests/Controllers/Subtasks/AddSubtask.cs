using System.Threading.Tasks;
using ProjectManagement.Core.UseCases.Subtasks.Commands.CreateSubtask;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace ProjectManagementApi.IntegrationTests.Controllers.Subtasks
{
    [Collection("Sequential")]
    public class AddSubtask:IntegrationTest
    {
        public AddSubtask(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory, output)
        {
        }

        [Fact]
        public async Task AddSubtaskSuccessful()
        {
            var request = new CreateSubtaskCommand()
            {
                Name = "new subtask",
                TaskId = Utilities.P1S1Task1.Id
            };
            
            var response = await Client.PostAsync("api/Subtasks",Utilities.GetRequestContent(request));
            response.EnsureSuccessStatusCode();
        }
    }
}