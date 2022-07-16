using article_api.BusinessLogic.Dtos;
using article_api.BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
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

        [HttpPost(Name = "Create a new article")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateArticle(CreateArticleRequest article)
        {
            var articleResponse = await _articlesService.CreateArticle(article);

            return CreatedAtAction(nameof(GetArticleById), new { id = articleResponse.Id }, articleResponse);
        }

        [HttpGet("{id}", Name= "Get an article with a given identifier")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetArticleById(Guid id)
        {
            var articleResponse = _articlesService.GetArticleById(id);

            if (articleResponse != null) return Ok(articleResponse);

            return NotFound("Article not found");
        }

        [HttpPut("{id}", Name= "Update an article by updating its identifier to the given one")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateArticleById([FromRoute] Guid id, [FromBody]UpdateArticleRequest updateArticleRequest)
        {
            updateArticleRequest.Id = id;

            var isUpdated = await _articlesService.UpdateArticleById(updateArticleRequest);

            if (isUpdated) return Ok();

            return NotFound("Article not found");
        }

        [HttpDelete("{id}", Name= "Delete an article with a given identifier")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteArticleById(Guid id)
        {
            var isDeleted = await _articlesService.DeleteArticleById(id);

            if (isDeleted) return Ok();

            return NotFound("Article not found");
        }
    }
}
