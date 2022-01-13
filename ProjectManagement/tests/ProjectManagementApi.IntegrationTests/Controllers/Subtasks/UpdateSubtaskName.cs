using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ProjectManagement.Core.UseCases.Tasks.Dto;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace ProjectManagementApi.IntegrationTests.Controllers.Subtasks
{
    [Collection("Sequential")]
    public class UpdateSubtaskName : IntegrationTest
    {
        public UpdateSubtaskName(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory,
            output)
        {
        }

        [Fact]
        public async Task UpdateSubtaskNameSuccessful()
        {
            var newValue = "new subtask name";
            var response = await Client.PutAsync(
                $"api/Subtasks/updateName/{Utilities.P1S1T1Subtask1.Id}",
                Utilities.GetRequestContent(newValue));
            response.EnsureSuccessStatusCode();

            var getResponse = await Client.GetAsync($"api/Tasks/{Utilities.P1S1Task1.Id}");
            getResponse.EnsureSuccessStatusCode();
            var task = await Utilities.GetResponseContent<DetailedTaskDto>(getResponse);
            
            var subtask = task.Subtasks.SingleOrDefault(s => s.Id == Utilities.P1S1T1Subtask1.Id);
            if (subtask != null)
            {
                subtask.Name.Should().Be(newValue);
            }
        }
    }
}