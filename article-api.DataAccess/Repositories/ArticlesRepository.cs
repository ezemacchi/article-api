using article_api.Common.Exceptions;
using article_api.Domain.Models;
using System;
using System.Threading.Tasks;

namespace article_api.DataAccess.Repositories
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly AppDbContext _appDbContext;

        public ArticlesRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Guid> Create(Article article)
        {
            article.Id = Guid.NewGuid();

            if(article == null) throw new ArgumentNullException(nameof(article));

            if (string.IsNullOrEmpty(article.Title))
            {
                throw new BusinessException("Cannot add an article without title");
            }

            _appDbContext.Articles.Add(article);
            await _appDbContext.SaveChangesAsync();

            return article.Id;
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Article> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Article articleToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
