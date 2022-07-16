﻿using article_api.BusinessLogic.Dtos.CreateArticle;
using article_api.DataAccess.Repositories;
using article_api.Domain.Models;
using AutoMapper;
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

        public async Task<CreateArticleResponse> CreateArticle(CreateArticleRequest articleRequest)
        {
            var article = _mapper.Map<Article>(articleRequest);

            article.Id = await _articlesRepository.Create(article);

            return _mapper.Map<CreateArticleResponse>(article);
        }
    }
}