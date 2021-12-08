using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;
using Xunit.Abstractions;

namespace ProjectManagementApi.IntegrationTests.Common
{
    public class IntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        protected readonly CustomWebApplicationFactory<Startup> Factory;
        protected readonly ITestOutputHelper _output;
        protected readonly HttpClient Client;

        public IntegrationTest(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output)
        {
            Factory = factory;
            _output = output;
            Client = Factory.CreateClient();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("IntegrationTest");

        }
    }
}