using System.Threading.Tasks;
using FluentAssertions;
using ProjectManagement.Core.UseCases.Steps.Commands.UpdateStep;
using ProjectManagement.Core.UseCases.Steps.Dto;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace ProjectManagementApi.IntegrationTests.Controllers.Steps
{
    [Collection("Sequential")]
    public class UpdateStep : IntegrationTest
    {
        public UpdateStep(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory, output)
        {
        }

        [Fact]
        public async Task UpdateStepSuccessful()
        {
            var newName = "new name";

            var request = new UpdateStepCommand()
            {
                Id = Utilities.P1Step1.Id,
                Name = newName
            };
            
            var response = await Client.PutAsync("/api/Step", Utilities.GetRequestContent(request));
            response.EnsureSuccessStatusCode();

            var responseContent = await Utilities.GetResponseContent<StepDto>(response);
            responseContent.Name.Should().Be(newName);
        }
    }
}