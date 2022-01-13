using System.Threading.Tasks;
using FluentAssertions;
using ProjectManagement.Core.UseCases.Users.ViewsModels;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace ProjectManagementApi.IntegrationTests.Controllers.Users
{
    [Collection("Sequential")]
    public class FindUsers : IntegrationTest
    {
        public FindUsers(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory, output)
        {
        }

        [Fact]
        public async Task FindUserInSystemByEmailPartSuccessful()
        {
            var term = "integration@";
            var response = await Client.GetAsync($"api/Users/findUsers?term={term}");
            response.EnsureSuccessStatusCode();

            var users = await Utilities.GetResponseContent<UserVm>(response);
            users.Count.Should().Be(1);
        }
        
        [Fact]
        public async Task FindUserInSystemByUsernamePartSuccessful()
        {
            var term = ".tests";
            var response = await Client.GetAsync($"api/Users/findUsers?term={term}");
            response.EnsureSuccessStatusCode();

            var users = await Utilities.GetResponseContent<UserVm>(response);
            users.Count.Should().Be(1);
        }
        
        [Fact]
        public async Task FindNotExistingUserInSystem()
        {
            var term = "wojtek";
            var response = await Client.GetAsync($"api/Users/findUsers?term={term}");
            response.EnsureSuccessStatusCode();

            var users = await Utilities.GetResponseContent<UserVm>(response);
            users.Count.Should().Be(0);
        }
        
        [Fact]
        public async Task FindUserInProjectByEmailPartSuccessful()
        {
            var term = "integration@";
            var response = await Client.GetAsync($"api/Users/findUsers?term={term}&projectId={Utilities.Project1.Id}");
            response.EnsureSuccessStatusCode();   
            
            var users = await Utilities.GetResponseContent<UserVm>(response);
            users.Count.Should().Be(1);
        }
        
        [Fact]
        public async Task FindUserInProjectByUserNameSuccessful()
        {
            var term = "integration";
            var response = await Client.GetAsync($"api/Users/findUsers?term={term}&projectId={Utilities.Project1.Id}");
            response.EnsureSuccessStatusCode();   
            
            var users = await Utilities.GetResponseContent<UserVm>(response);
            users.Count.Should().Be(1);
        }
        
        [Fact]
        public async Task FindNotExistingUserInProjectByMailPart()
        {
            var term = "user2@";
            var response = await Client.GetAsync($"api/Users/findUsers?term={term}&projectId={Utilities.Project1.Id}");
            response.EnsureSuccessStatusCode();   
            
            var users = await Utilities.GetResponseContent<UserVm>(response);
            users.Count.Should().Be(0);
        }
        
        [Fact]
        public async Task FindNotExistingUserInProjectByUserName()
        {
            var term = "user2";
            var response = await Client.GetAsync($"api/Users/findUsers?term={term}&projectId={Utilities.Project1.Id}");
            response.EnsureSuccessStatusCode();   
            
            var users = await Utilities.GetResponseContent<UserVm>(response);
            users.Count.Should().Be(0);
        }
        
    }
}