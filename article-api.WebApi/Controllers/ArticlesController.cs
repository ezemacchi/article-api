using article_api.WebApi.Dtos.CreateArticle;
using Microsoft.AspNetCore.Mvc;
using System;

namespace article_api.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateArticle(CreateArticleRequest article)
        {
            var articleResponse = new CreateArticleResponse { Id = Guid.NewGuid(), Text = article.Text, Title = article.Title };

            return CreatedAtAction(nameof(GetArticle), new { id= articleResponse.Id}, articleResponse);
        }

        [HttpGet("{id}")]
        public IActionResult GetArticle(Guid id)
        {
            return Ok();
        }
    }
}
