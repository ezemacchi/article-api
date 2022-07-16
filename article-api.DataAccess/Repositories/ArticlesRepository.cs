using article_api.Common.Exceptions;
using article_api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

        public async Task<bool> Delete(Guid id)
        {
            var article = _appDbContext.Articles.FirstOrDefault(art => art.Id == id);

            if (article == null) return false;

            _appDbContext.Articles.Remove(article);
            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public Article Get(Guid id)
        {
            var article = _appDbContext.Articles
                .AsNoTracking()
                .FirstOrDefault(article => article.Id == id);

            return article;
        }

        public async Task<bool> Update(Article articleToUpdate)
        {
            var article = _appDbContext.Articles.FirstOrDefault(art => art.Id == articleToUpdate.Id);

            if (article == null) return false;

            article.Text = articleToUpdate.Text;
            article.Title = articleToUpdate.Title;

            _appDbContext.Update(article);
            await _appDbContext.SaveChangesAsync();

            return true;
        }
    }
}
