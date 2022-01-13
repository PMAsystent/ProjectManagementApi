using System.Threading.Tasks;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace ProjectManagementApi.IntegrationTests.Controllers.Tasks
{
    [Collection("Sequential")]
    public class DeleteTask : IntegrationTest
    {
        public DeleteTask(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory, output)
        {
        }
        
        [Fact]
        public async Task DeleteTaskSuccessful()
        {
            var response = await Client.DeleteAsync($"api/Tasks/{Utilities.P1S1Task1.Id}");
            response.EnsureSuccessStatusCode();
        }
    }
}