using APMLibrary.Bll.Common.Behaviors;
using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Bll.Services.Implements;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll
{
    public static class DependencyInjection : object
    {
        public async static Task<IServiceCollection> AddBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            services.AddAutoMapper(options =>
            {
                options.AddProfile(new AssemblyProfile(typeof(DependencyInjection).Assembly));
                options.AddProfile(new AssemblyProfile(Assembly.GetEntryAssembly()!));
            });
            services.AddMediatR(options =>
            {
                options.AddOpenBehavior(typeof(LoggingBehavior<,>), ServiceLifetime.Transient)
                    .AddOpenBehavior(typeof(ValidationBehavior<,>), ServiceLifetime.Transient);

                options.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
            return services.AddTextFileService().AddDatabaseBackup();
        }
    }
}
