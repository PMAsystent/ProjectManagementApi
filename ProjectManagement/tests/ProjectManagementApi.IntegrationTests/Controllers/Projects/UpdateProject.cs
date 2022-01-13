using System;
using System.Threading.Tasks;
using FluentAssertions;
using ProjectManagement.Core.UseCases.Projects.Commands.UpdateProject;
using ProjectManagement.Core.UseCases.Projects.Dto;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace ProjectManagementApi.IntegrationTests.Controllers.Projects
{
    [Collection("Sequential")]
    public class UpdateProject : IntegrationTest
    {
        public UpdateProject(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory,
            output)
        {
        }


        [Fact]
        public async Task UpdateProjectSuccessful()
        {
            var newName = "new name";
            var newDescription = "new description";
            var newDate = DateTime.UtcNow.AddDays(20);

            var request = new UpdateProjectCommand()
            {
                Id = Utilities.Project1.Id,
                Name = newName,
                Description = newDescription,
                DueDate = newDate,
            };

            var response = await Client.PutAsync("/api/MyProjects", Utilities.GetRequestContent(request));
            response.EnsureSuccessStatusCode();

            var getResponse = await Client.GetAsync($"/api/MyProjects/{Utilities.Project1.Id}");
            var projectAfterUpdate = await Utilities.GetResponseContent<DetailedProjectDto>(getResponse);
            projectAfterUpdate.Name.Should().Be(newName);
            projectAfterUpdate.Description.Should().Be(newDescription);
            projectAfterUpdate.DueDate.Should().Be(newDate);
        }

        // [Fact]
        // public async Task UpdateProjectWhenUnauthorized()
        // {
        //     throw new NotImplementedException();
        // }
    }
}