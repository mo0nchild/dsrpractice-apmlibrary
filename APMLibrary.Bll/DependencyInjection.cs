﻿using APMLibrary.Bll.Common.Mappings;
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
                options.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
            return services;
        }
    }
}
