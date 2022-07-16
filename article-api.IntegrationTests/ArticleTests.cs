using article_api.BusinessLogic.Dtos.CreateArticle;
using article_api.DataAccess;
using article_api.Domain.Models;
using article_api.IntegrationTests.Configurations;
using article_api.WebApi;
using Microsoft.Extensions.DependencyInjection;
using System;
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
            var articleRequest = new CreateArticleRequest { Title = "Some article", Text = "Hello World!" };

            var responseMessage = await httpClient.PostAsJsonAsync(Url, articleRequest);
            var response = await responseMessage.Content.ReadFromJsonAsync<CreateArticleResponse>();

            var context = GetDbContext();
            var article = context.Articles.Find(response.Id);
            context.Dispose();

            Assert.Equal(article.Id, response.Id);
            Assert.Equal(HttpStatusCode.Created, responseMessage.StatusCode);
            Assert.Equal($"{Url}/{response.Id}", responseMessage.Headers.Location.PathAndQuery);
            Assert.Equal(articleRequest.Title, response.Title);
            Assert.Equal(articleRequest.Text, response.Text);
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

        [Fact]
        public async Task Get_Article_By_Id_Sucess()
        {
            var httpClient = _factory.CreateClient();
            var context = GetDbContext();

            var id = Guid.NewGuid();
            var article = new Article { Id = id, Text = "Test text", Title = "Test title" };
            context.Articles.Add(article);

            var responseMessage = await httpClient.GetAsync($"Url/{article.Id}");
            var response = await responseMessage.Content.ReadFromJsonAsync<GetArticleResponse>();

            Assert.Equal(article.Id, response.Id);
            Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
        }

        private AppDbContext GetDbContext()
        {
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            var scope = scopeFactory.CreateScope();
            return scope.ServiceProvider.GetService<AppDbContext>();
        }
    }
}