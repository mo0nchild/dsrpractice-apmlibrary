using APMLibraryDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace APMLibrary.Dal
{
    public partial class LibraryDbContext : DbContext
    {
        public virtual DbSet<Author> Authors { get; set; } = default!;
        public virtual DbSet<Authorization> Authorizations { get; set; } = default!;
        public virtual DbSet<BookCover> BookCovers { get; set; } = default!;
        public virtual DbSet<Category> Categories { get; set; } = default!;

        public virtual DbSet<Genre> Genres { get; set; } = default!;
        public virtual DbSet<Language> Languages { get; set; } = default!;
        public virtual DbSet<Profile> Profiles { get; set; } = default!;
        public virtual DbSet<Publication> Publications { get; set; } = default!;

        public virtual DbSet<PublicationType> PublicationTypes { get; set; } = default!;
        public virtual DbSet<Quote> Quotes { get; set; } = default!;
        public virtual DbSet<Rating> Ratings { get; set; } = default!;
        public virtual DbSet<Reader> Readers { get; set; } = default!;

        public LibraryDbContext(DbContextOptions options) : base(options) { }
        protected LibraryDbContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
