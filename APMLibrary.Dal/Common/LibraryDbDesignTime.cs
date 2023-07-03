using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using static APMLibrary.Dal.Common.LibraryDbDesignTime;

namespace APMLibrary.Dal.Common
{
    internal class LibraryDbDesignTime : IDesignTimeDbContextFactory<LibraryDbContext>
    {
        protected readonly IConfiguration _configuration = default!;
        public LibraryDbDesignTime() : base()
        {
            var configurationBuilder = new ConfigurationBuilder();
            using (var fileStream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), FileMode.Open))
            {
                this._configuration = configurationBuilder.AddJsonStream(fileStream).Build();
            }
        }
        public class DesignOptionsMonitor<TConfig> : IOptionsMonitor<TConfig> where TConfig : class, new()
        {
            public TConfig CurrentValue { get; private set; } = default!;
            public DesignOptionsMonitor(TConfig currentValue) => this.CurrentValue = currentValue;

            public TConfig Get(string? name) => this.CurrentValue;
            public IDisposable OnChange(Action<TConfig, string> listener) => throw new NotImplementedException();
        }
        public LibraryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseNpgsql(this._configuration.GetConnectionString("DefaultConnection"));

            return new LibraryDbContext(optionsBuilder.Options, 
                new DesignOptionsMonitor<FileLoggerOptions>(new FileLoggerOptions()));
        }
    }
}
