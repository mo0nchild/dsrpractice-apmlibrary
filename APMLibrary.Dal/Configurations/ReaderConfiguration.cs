using APMLibraryDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Dal.Configurations
{
    internal sealed class ReaderConfiguration : IEntityTypeConfiguration<Reader>
    {
        public void Configure(EntityTypeBuilder<Reader> builder)
        {
            builder.HasKey(item => item.Id);
            builder.HasIndex(item => item.Id).IsUnique();

            builder.HasOne(item => item.Profile).WithMany(item => item.Readers)
                .HasForeignKey(item => item.ProfileId).HasPrincipalKey(item => item.Id);
            builder.HasMany(item => item.Publications).WithMany(item => item.Readers)
                .UsingEntity("Bookmarks");
        }
    }
}
