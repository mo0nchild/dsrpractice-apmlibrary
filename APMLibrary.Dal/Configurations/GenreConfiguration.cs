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
    internal sealed class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(item => item.Id);
            builder.HasIndex(item => item.Id).IsUnique();

            builder.Property(item => item.Name).HasMaxLength(50);

            builder.HasOne(item => item.Category).WithMany(item => item.Genres)
                .HasForeignKey(item => item.CategoryId).HasPrincipalKey(item => item.Id);
        }
    }
}
