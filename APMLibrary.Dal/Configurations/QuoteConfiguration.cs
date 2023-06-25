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
    internal sealed class QuoteConfiguration : IEntityTypeConfiguration<Quote>
    {
        public void Configure(EntityTypeBuilder<Quote> builder)
        {
            builder.HasKey(item => item.Id);
            builder.HasIndex(item => item.Id).IsUnique();
            builder.Property(item => item.Text).HasColumnType("text");

            builder.HasOne(item => item.Reader).WithMany(item => item.Quotes)
                .HasForeignKey(item => item.ReaderId).HasPrincipalKey(item => item.Id);
            builder.HasOne(item => item.Publication).WithMany(item => item.Quotes)
                .HasForeignKey(item => item.PublicationId).HasPrincipalKey(item => item.Id);
        }
    }
}
