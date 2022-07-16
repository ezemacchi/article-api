using article_api.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace article_api.DataAccess
{
    public static class RepositoriesDependencyInjection
    {
        public static void AddRepositories(this IServiceCollection services, string connectionString){
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));
            services.AddScoped<IArticlesRepository, ArticlesRepository>();
        }
    }
}
