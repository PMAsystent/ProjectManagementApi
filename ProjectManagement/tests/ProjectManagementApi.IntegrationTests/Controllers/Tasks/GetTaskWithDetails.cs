using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using ProjectManagement.Core.UseCases.Tasks.Dto;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;
using TaskEntity = Domain.Entities.Task;

namespace ProjectManagementApi.IntegrationTests.Controllers.Tasks
{
    [Collection("Sequential")]
    public class GetTaskWithDetails : IntegrationTest
    {
        public GetTaskWithDetails(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory, output)
        {
        }

        [Fact]
        public async Task GetTaskWithDetailsSuccessful()
        {
            var response = await Client.GetAsync($"/api/Tasks/{Utilities.P1S1Task1.Id}");
            response.EnsureSuccessStatusCode();

            var task = await Utilities.GetResponseContent<TaskEntity>(response);
            task.Name.Should().Be(Utilities.P1S1Task1.Name);
        }
        
        [Fact]
        public async Task GetTaskWithDetailsUnsuccessful()
        {
            var response = await Client.GetAsync($"/api/Tasks/{9999}");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        
        [Fact]
        public async Task GetTaskWithDetailsAndValidateProgressPercentageForTask1()
        {
            var response = await Client.GetAsync($"/api/Tasks/{Utilities.P1S1Task1.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            
            var task = await Utilities.GetResponseContent<DetailedTaskDto>(response);
            task.ProgressPercentage.Should().Be(50);
        }
        
        [Fact]
        public async Task GetTaskWithDetailsAndValidateProgressPercentageForTask2()
        {
            var response = await Client.GetAsync($"/api/Tasks/{Utilities.P1S1Task2.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            
            var task = await Utilities.GetResponseContent<DetailedTaskDto>(response);
            task.ProgressPercentage.Should().Be(50);
        }
        
        [Fact]
        public async Task GetTaskWithDetailsAndValidateProgressPercentageForTask3()
        {
            var response = await Client.GetAsync($"/api/Tasks/{Utilities.P1S1Task3.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            
            var task = await Utilities.GetResponseContent<DetailedTaskDto>(response);
            task.ProgressPercentage.Should().Be(100);
        }
    }
}