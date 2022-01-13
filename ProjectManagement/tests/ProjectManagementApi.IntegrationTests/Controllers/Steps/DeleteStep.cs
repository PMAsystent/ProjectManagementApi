using System;
using System.Threading.Tasks;
using ProjectManagementApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace ProjectManagementApi.IntegrationTests.Controllers.Steps
{
    [Collection("Sequential")]
    public class DeleteStep : IntegrationTest
    {
        public DeleteStep(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory, output)
        {
        }

        [Fact]
        public async Task DeleteStepSuccessful()
        {
            var response = await Client.DeleteAsync($"/api/Step/{Utilities.P1Step1.Id}");
            response.EnsureSuccessStatusCode();
        }

        // [Fact]
        // public async Task DeleteStepWhenUnauthorized()
        // {
        //     throw new NotImplementedException();
        // }
    }
}