using article_api.IntegrationTests.Configurations;
using article_api.WebApi;
using article_api.WebApi.Dtos.CreateArticle;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace article_api.IntegrationTests
{
    public class ArticleTests : IClassFixture<IntegrationTestsFactory<Startup>>
    {
        private readonly IntegrationTestsFactory<Startup> _factory;

        private const string Url = "/api/Articles";

        public ArticleTests(IntegrationTestsFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Create_New_Article_Success()
        {
            var httpClient = _factory.CreateClient();
            var article = new CreateArticleRequest { Title = "Some article", Text = "Hello World!" };

            var responseMessage = await httpClient.PostAsJsonAsync(Url, article);
            var response = await responseMessage.Content.ReadFromJsonAsync<CreateArticleResponse>();

            Assert.Equal(HttpStatusCode.Created, responseMessage.StatusCode);
            Assert.Equal($"{Url}/{response.Id}", responseMessage.Headers.Location.PathAndQuery);
            Assert.Equal(article.Title, response.Title);
            Assert.Equal(article.Text, response.Text);
        }

        [Fact]
        public async Task Create_New_Article_BadRequest()
        {
            var httpClient = _factory.CreateClient();
            var article = new CreateArticleRequest { Text = "Hello World!" };

            var responseMessage = await httpClient.PostAsJsonAsync(Url, article);

            Assert.Null(article.Title);
            Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);
        }
    }
}