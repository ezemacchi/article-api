using article_api.BusinessLogic.Dtos;
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
            var response = await responseMessage.Content.ReadFromJsonAsync<ArticleDto>();

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
            //TODO check ErrorMessage
        }

        [Fact]
        public async Task Get_Article_By_Id_Sucess()
        {
            var httpClient = _factory.CreateClient();
            var context = GetDbContext();

            var id = Guid.Parse("7d34d51a-c3ad-4e92-8fd1-c69777026f43");
            var article = new Article { Id = id, Text = "Test text", Title = "Test title" };
            context.Articles.Add(article);
            context.SaveChanges();
            context.Dispose();

            var responseMessage = await httpClient.GetAsync($"{Url}/{article.Id}");
            var response = await responseMessage.Content.ReadFromJsonAsync<ArticleDto>();

            Assert.Equal(article.Id, response.Id);
            Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
        }

        [Fact]
        public async Task Get_Article_By_Id_NotFound()
        {
            var httpClient = _factory.CreateClient();
            var context = GetDbContext();

            var id = Guid.Parse("7d34d51a-c3ad-4e92-8fd1-c69877026f43");
            var article = new Article { Id = id, Text = "Test text", Title = "Test title" };
            context.Articles.Add(article);
            context.SaveChanges();
            context.Dispose();

            var wrongId = Guid.Parse("7d5c7649-1fef-42ad-888f-350a12afc56a");
            var responseMessage = await httpClient.GetAsync($"{Url}/{wrongId}");
            var response = await responseMessage.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);
            Assert.Equal("Article not found", response);
        }

        [Fact]
        public async Task Update_Article_By_Id_Success()
        {
            var httpClient = _factory.CreateClient();
            var context = GetDbContext();

            var id = Guid.Parse("7d34d51a-c3ad-4e92-8fd1-c69777026f44");
            var article = new Article { Id = id, Text = "Test text", Title = "Test title" };
            context.Articles.Add(article);
            context.SaveChanges();

            var updateArticleRequest = new UpdateArticleRequest { Title = "new test title", Text = "new test text" };

            var responseMessage = await httpClient.PutAsJsonAsync($"{Url}/{id}", updateArticleRequest);

            //Have an error without this line, the entity wasn't changed after updating, probably the way of using the scope is the real problem
            context.Entry(article).Reload();

            var updatedArticle = context.Articles.Find(id);
            context.Dispose();

            Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
            Assert.Equal(updateArticleRequest.Title, updatedArticle.Title);
            Assert.Equal(updateArticleRequest.Text, updatedArticle.Text);
        }

        [Fact]
        public async Task Update_Article_By_Id_NotFound()
        {
            var httpClient = _factory.CreateClient();
            var context = GetDbContext();

            var id = Guid.Parse("7d34d51a-c3ad-4e92-8fd1-c69777026f45");
            var article = new Article { Id = id, Text = "Test text", Title = "Test title" };
            context.Articles.Add(article);
            context.SaveChanges();
            context.Dispose();

            var updateArticleRequest = new UpdateArticleRequest { Title = "new test title", Text = "new test text" };

            var wrongId = Guid.Parse("7d5c7649-1fef-42ad-888f-350a12afc56b");
            var responseMessage = await httpClient.PutAsJsonAsync($"{Url}/{wrongId}", updateArticleRequest);
            var response = await responseMessage.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);
            Assert.Equal("Article not found", response);
        }

        [Fact]
        public async Task Update_Article_By_Id_BadRequest()
        {
            var httpClient = _factory.CreateClient();

            var updateArticleRequest = new UpdateArticleRequest { Text = "new test text" };

            var id = Guid.Parse("7d5c7649-1fef-42ad-888f-350a12afc56c");
            var responseMessage = await httpClient.PutAsJsonAsync($"{Url}/{id}", updateArticleRequest);

            Assert.Null(updateArticleRequest.Title);
            Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);
            //TODO check ErrorMessage
        }

        [Fact]
        public async Task Delete_Article_By_Id_Success()
        {
            var httpClient = _factory.CreateClient();
            var context = GetDbContext();

            var id = Guid.Parse("7d34d51a-c3ad-4e92-8fd1-c69777026f53");
            var article = new Article { Id = id, Text = "Test text", Title = "Test title" };
            context.Articles.Add(article);
            context.SaveChanges();

            var responseMessage = await httpClient.DeleteAsync($"{Url}/{id}");

            //Have an error without this line, the entity wasn't changed after updating, probably the way of using the scope is the real problem
            context.Entry(article).Reload();

            var result = context.Articles.Find(id);
            context.Dispose();

            Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
            Assert.Null(result);
        }

        [Fact]
        public async Task Delete_Article_By_Id_NotFound()
        {
            var httpClient = _factory.CreateClient();
            var context = GetDbContext();

            var id = Guid.Parse("7d34d51a-c3ad-4e92-8fd1-c69777026f93");
            var article = new Article { Id = id, Text = "Test text", Title = "Test title" };
            context.Articles.Add(article);
            context.SaveChanges();

            var wrongId = Guid.Parse("7d5c7649-1fef-42ad-888f-351a12afc56a");

            var responseMessage = await httpClient.DeleteAsync($"{Url}/{wrongId}");
            var response = await responseMessage.Content.ReadAsStringAsync();

            var result = context.Articles.Find(id);
            context.Dispose();

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);
            Assert.Equal("Article not found", response);
        }

        private AppDbContext GetDbContext()
        {
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            var scope = scopeFactory.CreateScope();
            return scope.ServiceProvider.GetService<AppDbContext>();
        }
    }
}