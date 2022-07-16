using article_api.BusinessLogic.Dtos;
using article_api.DataAccess.Repositories;
using article_api.Domain.Models;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace article_api.BusinessLogic.Services
{
    public class ArticlesServices : IArticlesServices
    {
        private readonly IArticlesRepository _articlesRepository;
        private readonly IMapper _mapper;

        public ArticlesServices(IArticlesRepository articlesRepository, IMapper mapper)
        {
            _articlesRepository = articlesRepository;
            _mapper = mapper;
        }

        public async Task<ArticleDto> CreateArticle(CreateArticleRequest articleRequest)
        {
            var article = _mapper.Map<Article>(articleRequest);

            article.Id = await _articlesRepository.Create(article);

            return _mapper.Map<ArticleDto>(article);
        }

        public ArticleDto GetArticleById(Guid id)
        {
            var article = _articlesRepository.Get(id);

            var response = article == null ? null : _mapper.Map<ArticleDto>(article);

            return response;
        }

        public async Task<bool> UpdateArticleById(UpdateArticleRequest updateArticleRequest)
        {
            var article = _mapper.Map<Article>(updateArticleRequest);

            return await _articlesRepository.Update(article);
        }
    }
}
