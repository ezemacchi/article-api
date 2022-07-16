using article_api.BusinessLogic.Dtos.CreateArticle;
using article_api.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace article_api.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesServices _articlesService;

        public ArticlesController(IArticlesServices articlesService)
        {
            _articlesService = articlesService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle(CreateArticleRequest article)
        {
            var articleResponse = await _articlesService.CreateArticle(article);

            return CreatedAtAction(nameof(GetArticle), new { id= articleResponse.Id}, articleResponse);
        }

        [HttpGet("{id}")]
        public IActionResult GetArticle(Guid id)
        {
            return Ok();
        }
    }
}
