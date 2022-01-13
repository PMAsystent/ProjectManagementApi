using System;
using System.Threading.Tasks;
using FluentAssertions;
using ProjectManagement.Core.UseCases.Projects.Dto;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace ProjectManagementApi.IntegrationTests.Controllers.Projects
{
    [Collection("Sequential")]
    public class GetProjectWithDetails : IntegrationTest
    {
        public GetProjectWithDetails(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory, output)
        {
        }

        [Fact]
        public async Task GetProjectWithDetailsSuccessful()
        {
            var response = await Client.GetAsync($"/api/MyProjects/{Utilities.Project1.Id}");
            response.EnsureSuccessStatusCode();

            var project = await Utilities.GetResponseContent<DetailedProjectDto>(response);
            project.Should().NotBeNull();
        }

        // [Fact]
        // public async Task GetProjectWithDetailsWhenUnauthorized()
        // {
        //     throw new NotImplementedException();
        // }
    }
}