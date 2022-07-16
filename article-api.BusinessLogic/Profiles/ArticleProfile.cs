using article_api.BusinessLogic.Dtos.CreateArticle;
using article_api.Domain.Models;
using AutoMapper;

namespace article_api.BusinessLogic.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<CreateArticleRequest, Article>();
            CreateMap<Article, CreateArticleResponse>();
        }
    }
}
