using article_api.BusinessLogic.Profiles;
using article_api.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace article_api.BusinessLogic
{
    public static class ServicesDependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ArticleProfile));
            services.AddScoped<IArticlesServices, ArticlesServices>();
        }
    }
}
