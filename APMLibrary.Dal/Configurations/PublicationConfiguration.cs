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
    internal sealed class PublicationConfiguration : IEntityTypeConfiguration<Publication>
    {
        public void Configure(EntityTypeBuilder<Publication> builder)
        {
            builder.HasKey(item => item.Id);
            builder.HasIndex(item => item.Id).IsUnique();

            builder.Property(item => item.Title).HasMaxLength(100);
            builder.Property(item => item.Description).HasColumnType("text");
            builder.Property(item => item.AuthorName).HasMaxLength(50);

            builder.HasOne(item => item.Language).WithMany(item => item.Publications)
                .HasForeignKey(item => item.LanguageId).HasPrincipalKey(item => item.Id);

            builder.HasOne(item => item.Publisher).WithMany(item => item.Publications)
                .HasForeignKey(item => item.PublisherId).HasPrincipalKey(item => item.Id);

            builder.HasOne(item => item.PublicationType).WithMany(item => item.Publications)
                .HasForeignKey(item => item.PublicationTypeId).HasPrincipalKey(item => item.Id);
        }
    }
}
