using System;
using System.Threading.Tasks;
using FluentAssertions;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace ProjectManagementApi.IntegrationTests.Controllers.Users
{
    [Collection("Sequential")]
    public class CheckIfUserWithEmailExists : IntegrationTest
    {
        public CheckIfUserWithEmailExists(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) :
            base(factory, output)
        {
        }

        [Fact]
        public async Task TestWithExistingEmail()
        {
            if (Utilities.User1 == null)
            {
                throw new Exception("Seed failed.");
            }

            var mailOfExistingUser = Utilities.User1.Email;

            var response = await Client.GetAsync($"/api/Users/ifExists/{mailOfExistingUser}");
            response.EnsureSuccessStatusCode();

            var exist = await Utilities.GetResponseContent<bool>(response);
            exist.Should().BeTrue();
        }

        [Fact]
        public async Task TestWithNotExistingEmail()
        {
            var mailOfNotExistingUser = "xyz@mail.com";

            var response = await Client.GetAsync($"/api/Users/ifExists/{mailOfNotExistingUser}");
            response.EnsureSuccessStatusCode();

            var exist = await Utilities.GetResponseContent<bool>(response);
            exist.Should().BeFalse();
        }
    }
}