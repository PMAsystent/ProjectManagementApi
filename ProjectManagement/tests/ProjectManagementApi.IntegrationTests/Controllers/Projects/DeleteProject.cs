using System;
using System.Threading.Tasks;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace ProjectManagementApi.IntegrationTests.Controllers.Projects
{
    [Collection("Sequential")]
    public class DeleteProject : IntegrationTest
    {
        public DeleteProject(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory, output)
        {
        }

        [Fact]
        public async Task DeleteProjectSuccessful()
        {
            var response = await Client.DeleteAsync($"/api/MyProjects/{Utilities.Project1.Id}");
            response.EnsureSuccessStatusCode();
        }
        
        // [Fact]
        // public async Task DeleteProjectWhenUnauthorized()
        // {
        //     throw new NotImplementedException();
        // }
    }
}