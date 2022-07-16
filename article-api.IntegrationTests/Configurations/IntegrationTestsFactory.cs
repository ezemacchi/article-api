using article_api.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;

namespace article_api.IntegrationTests.Configurations
{
    public class IntegrationTestsFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public IConfigurationRoot Configuration { get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .ConfigureAppConfiguration(configurationBuilder =>
                {
                    configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
                    configurationBuilder.AddJsonFile("appsettings.json");
                })
                .ConfigureServices(services =>
                {
                    //Remove instantiated database from Startup.cs
                    var originalDb = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                    services.Remove(originalDb);

                    //Add InMemoryDb for testing
                    services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMemoryTestingDb"));
                });
        }
    }
}
