using System;
using System.Net.Http;
using System.Threading.Tasks;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace ProjectManagementApi.IntegrationTests.Controllers.Projects
{
    [Collection("Sequential")]
    public class ArchiveOrUnArchiveProject : IntegrationTest
    {
        public ArchiveOrUnArchiveProject(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory, output)
        {
        }

        [Fact]
        public async Task ArchiveOrUnArchiveProjectSuccessful()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Patch,
                RequestUri = new Uri($"/api/MyProjects/{Utilities.Project1.Id}/archive/{true}", UriKind.Relative)
            };
            
            var response = await Client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
        
        // [Fact]
        // public async Task ArchiveOrUnArchiveProjectWhenUnauthorized()
        // {
        //     throw new NotImplementedException();
        // }
    }
}