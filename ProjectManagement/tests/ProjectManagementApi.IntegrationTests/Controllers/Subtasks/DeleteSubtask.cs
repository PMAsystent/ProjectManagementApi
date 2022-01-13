using System.Threading.Tasks;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace ProjectManagementApi.IntegrationTests.Controllers.Subtasks
{
    [Collection("Sequential")]
    public class DeleteSubtask : IntegrationTest
    {
        public DeleteSubtask(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory, output)
        {
        }

        [Fact]
        public async Task DeleteSubtaskSuccessful()
        {
            var response = await Client.DeleteAsync($"api/Subtasks/{Utilities.P1S1T1Subtask1.Id}");
            response.EnsureSuccessStatusCode();
        }
    }
}