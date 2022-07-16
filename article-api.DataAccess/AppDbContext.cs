using article_api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace article_api.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        DbSet<Article> Articles { get; set; }
    }
}
