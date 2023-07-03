using APMLibrary.Dal.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Dal
{
    public static class DependencyInjection : object
    {
        public async static Task<IServiceCollection> AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextFactory<LibraryDbContext>(options =>
            {
                options.UseNpgsql(configuration["DATABASE_STRING"]);
            });
            services.Configure<FileLoggerOptions>(configuration.GetSection("FileLoggerOptions"));

            var serviceProvider = services.BuildServiceProvider();
            var dbcontextFactory = serviceProvider.GetRequiredService<IDbContextFactory<LibraryDbContext>>();

            using (var dbcontext = await dbcontextFactory.CreateDbContextAsync())
            {
                await dbcontext.Database.MigrateAsync();
            }
            return services;
        }
    }
}
