using System.Threading.Tasks;
using FluentAssertions;
using ProjectManagement.Core.UseCases.TaskAssignments.ViewModels;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace ProjectManagementApi.IntegrationTests.Controllers.TaskAssignments
{
    [Collection("Sequential")]
    public class GetUnassignedUsers : IntegrationTest
    {
        public GetUnassignedUsers(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory, output)
        {   
        }

        [Fact]
        public async Task GetUnassignedUsersSuccessful()
        {
            var response = await Client.GetAsync($"api/TaskAssignment/unassignedUsers/{Utilities.P1S1Task1.Id}");
            response.EnsureSuccessStatusCode();

            var users = await Utilities.GetResponseContent<UnassignedUsersVm>(response);
            users.UnassignedUser.Count.Should().Be(1);
        }
    }
}