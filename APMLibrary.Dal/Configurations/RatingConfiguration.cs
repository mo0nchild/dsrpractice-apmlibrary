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
    internal sealed class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(item => item.Id);
            builder.HasIndex(item => item.Id).IsUnique();
            builder.Property(item => item.Comment).HasColumnType("text");
            builder.ToTable(item => item.HasCheckConstraint("Value_CheckConstraint", "\"Value\" BETWEEN 0 AND 5"));

            builder.HasOne(item => item.Reader).WithMany(item => item.Ratings)
                .HasForeignKey(item => item.ReaderId).HasPrincipalKey(item => item.Id);
            builder.HasOne(item => item.Publication).WithMany(item => item.Ratings)
                .HasForeignKey(item => item.PublicationId).HasPrincipalKey(item => item.Id);
        }
    }
}
