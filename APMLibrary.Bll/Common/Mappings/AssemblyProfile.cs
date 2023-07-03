using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Common.Mappings
{
    public partial class AssemblyProfile : Profile
    {
        protected readonly ILogger<AssemblyProfile> _logger = default!;
        public AssemblyProfile(Assembly assembly) : base()
        {
            this._logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<AssemblyProfile>();
            this.ConfigureMappingsFromAssembly(assembly);
        }
        protected virtual void ConfigureMappingsFromAssembly(Assembly assembly)
        {
            var assemblyTypes = assembly.GetExportedTypes();
            var mappingTypes = assemblyTypes.Where(item => item.GetInterfaces()
                .Any(item => item.IsGenericType && item.GetGenericTypeDefinition() == typeof(IMappingWith<>)));

            foreach (var item in mappingTypes.ToImmutableList())
            {
                this._logger.LogInformation($"Mapping type: {item.FullName}");

                var typeInstance = Activator.CreateInstance(item);
                item.GetMethod("ConfigureMapping")?.Invoke(typeInstance, new[] { this } );
            }
        }
    }
}
