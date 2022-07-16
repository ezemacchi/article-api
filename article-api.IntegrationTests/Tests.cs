using article_api.WebApi;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace article_api.IntegrationTests
{
    public class Tests : IClassFixture<IntegrationTestsFactory<Startup>>
    {
        private readonly IntegrationTestsFactory<Startup> _factory;

        public Tests(IntegrationTestsFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Test_Configuration()
        {
            var httpClient = _factory.CreateClient();

            var response = await httpClient.GetAsync("/WeatherForecast");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}