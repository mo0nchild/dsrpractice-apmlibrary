using APMLibrary.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Dal.Configurations
{
    internal sealed class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.HasKey(item => item.Id);
            builder.HasIndex(item => item.Id).IsUnique();
            builder.HasIndex(item => item.Email).IsUnique();

            builder.Property(item => item.Name).HasMaxLength(50);
            builder.Property(item => item.Surname).HasMaxLength(50);

            builder.Property(item => item.Email).HasMaxLength(100);
            builder.Property(item => item.Name).HasMaxLength(50);

            builder.HasMany(item => item.Bookmarks).WithMany(item => item.Readers)
                .UsingEntity("Bookmarks");
        }
    }
}
