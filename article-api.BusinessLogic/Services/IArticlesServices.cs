using article_api.BusinessLogic.Dtos;
using System;
using System.Threading.Tasks;

namespace article_api.BusinessLogic.Services
{
    public interface IArticlesServices
    {
        Task<ArticleDto> CreateArticle(CreateArticleRequest articleRequest);

        ArticleDto GetArticleById(Guid id);

        Task<bool> UpdateArticleById(UpdateArticleRequest updateArticleRequest);

        Task<bool> DeleteArticleById(Guid id);
    }
}
