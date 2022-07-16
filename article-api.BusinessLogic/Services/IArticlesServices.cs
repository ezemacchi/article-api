using article_api.BusinessLogic.Dtos.CreateArticle;
using System.Threading.Tasks;

namespace article_api.BusinessLogic.Services
{
    public interface IArticlesServices
    {
        Task<CreateArticleResponse> CreateArticle(CreateArticleRequest articleRequest);
    }
}
