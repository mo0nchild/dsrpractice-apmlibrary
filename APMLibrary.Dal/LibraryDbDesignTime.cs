using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace APMLibrary.Dal
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
        public LibraryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseNpgsql(this._configuration.GetConnectionString("DefaultConnection"));

            return new LibraryDbContext(optionsBuilder.Options);
        }
    }
}
