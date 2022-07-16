using article_api.BusinessLogic.Dtos;
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

            return CreatedAtAction(nameof(GetArticleById), new { id= articleResponse.Id}, articleResponse);
        }

        [HttpGet("{id}")]
        public IActionResult GetArticleById(Guid id)
        {
            var articleResponse = _articlesService.GetArticleById(id);

            if (articleResponse != null) return Ok(articleResponse);

            return NotFound("Article not found");
        }
    }
}
