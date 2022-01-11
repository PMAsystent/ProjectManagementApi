using Domain.Entities;
using FluentAssertions;
using ProjectManagement.Core.UseCases.Steps.Commands.CreateStep;
using ProjectManagement.Core.UseCases.Steps.Dto;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;
using Task = System.Threading.Tasks.Task;

namespace ProjectManagementApi.IntegrationTests.Controllers.Steps
{
    [Collection("Sequential")]
    public class AddStep : IntegrationTest
    {
        public AddStep(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory, output)
        {
        }

        [Fact]
        public async Task AddStepSuccessful()
        {
            var name = "test step";
            
            var request = new CreateStepCommand()
            {
                Name = name,
                ProjectId = Utilities.Project1.Id
            };
            
            var response = await Client.PostAsync("/api/Step", Utilities.GetRequestContent(request));
            response.EnsureSuccessStatusCode();

            var responseContent = await Utilities.GetResponseContent<StepDto>(response);
            responseContent.Name.Should().Be(name);
            responseContent.Id.Should().BeGreaterThan(0);
        }
    }
}