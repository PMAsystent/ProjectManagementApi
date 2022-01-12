using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ProjectManagement.Core.UseCases.Projects.ViewModels;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace ProjectManagementApi.IntegrationTests.Controllers.Projects
{
    [Collection("Sequential")]
    public class GetMyProjects : IntegrationTest
    {
        public GetMyProjects(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory,
            output)
        {
        }

        [Fact]
        public async Task GetMyProjectsSuccessful()
        {
            var response = await Client.GetAsync($"/api/MyProjects");
            response.EnsureSuccessStatusCode();

            var projectsListVm = await Utilities.GetResponseContent<MyProjectsListVm>(response);
            projectsListVm.Count.Should().Be(1);
        }

        [Fact]
        public async Task GetMyProjectsAndCheckStepAndProjectPercentage()
        {
            var correctPercentage = 50;

            var response = await Client.GetAsync($"/api/MyProjects");
            response.EnsureSuccessStatusCode();

            var projectsListVm = await Utilities.GetResponseContent<MyProjectsListVm>(response);

            var project1 = projectsListVm.ProjectsList.SingleOrDefault(p => p.Id == Utilities.Project1.Id);
            project1.Should().NotBeNull();

            if (project1 != null)
            {
                project1.ProgressPercentage.Should().Be(correctPercentage);

                var step1 = project1.Steps.SingleOrDefault(s => s.Id == Utilities.P1Step1.Id);
                step1.Should().NotBeNull();

                if (step1 != null)
                {
                    step1.ProgressPercentage.Should().Be(correctPercentage);
                }
            }
        }
        
        [Fact]
        public async Task GetMyProjectsAndCheckActiveTasksCount()
        {
            var response = await Client.GetAsync($"/api/MyProjects");
            response.EnsureSuccessStatusCode();

            var projectsListVm = await Utilities.GetResponseContent<MyProjectsListVm>(response);

            var project1 = projectsListVm.ProjectsList.SingleOrDefault(p => p.Id == Utilities.Project1.Id);
            project1.Should().NotBeNull();

            if (project1 != null)
            {
                project1.ActiveTasksCount.Should().Be(1);
            }
        }
    }
}