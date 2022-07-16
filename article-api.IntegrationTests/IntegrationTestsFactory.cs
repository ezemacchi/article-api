using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace article_api.IntegrationTests
{
    public class IntegrationTestsFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public IConfigurationRoot Configuration{ get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .ConfigureAppConfiguration(configurationBuilder =>
                {
                    configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
                    configurationBuilder.AddJsonFile("appsettings.json");
                });           
        }
    }
}
