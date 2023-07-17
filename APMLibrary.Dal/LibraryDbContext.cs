using APMLibrary.Dal.Common;
using APMLibrary.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace APMLibrary.Dal
{
    public partial class LibraryDbContext : DbContext
    {
        protected readonly IOptionsMonitor<FileLoggerOptions> loggerOptions = default!;
        protected static ILoggerFactory? _loggerFactory = default!;

        public LibraryDbContext(DbContextOptions options, IOptionsMonitor<FileLoggerOptions> config)
            : base(options) => this.LoggerInitial(this.loggerOptions = config);
        protected LibraryDbContext() : base() { }

        public virtual DbSet<Authorization> Authorizations { get; set; } = default!;
        public virtual DbSet<Category> Categories { get; set; } = default!;

        public virtual DbSet<Genre> Genres { get; set; } = default!;
        public virtual DbSet<Language> Languages { get; set; } = default!;
        public virtual DbSet<Profile> Profiles { get; set; } = default!;
        public virtual DbSet<Publication> Publications { get; set; } = default!;

        public virtual DbSet<PublicationType> PublicationTypes { get; set; } = default!;
        public virtual DbSet<Quote> Quotes { get; set; } = default!;
        public virtual DbSet<Rating> Ratings { get; set; } = default!;

        protected virtual void LoggerInitial(IOptionsMonitor<FileLoggerOptions> config)
        {
            if (LibraryDbContext._loggerFactory != null) return;
            LibraryDbContext._loggerFactory = LoggerFactory.Create((ILoggingBuilder builder) =>
            {
                builder.AddConsole(options => options.UseUtcTimestamp = true);
                builder.AddProvider(new FileLoggerProvider(config));
            });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(LibraryDbContext._loggerFactory);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
