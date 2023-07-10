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
    internal sealed class AuthorizationConfiguration : IEntityTypeConfiguration<Authorization>
    {
        public void Configure(EntityTypeBuilder<Authorization> builder)
        {
            builder.HasKey(item => item.Id);
            builder.HasIndex(item => item.Id).IsUnique();
            builder.HasIndex(item => item.Login).IsUnique();

            builder.Property(item => item.Login).HasMaxLength(50);
            builder.Property(item => item.Password).HasMaxLength(100);

            builder.HasOne(item => item.Profile).WithOne(item => item.Authorization)
                .HasForeignKey((Authorization item) => item.ProfileId)
                .HasPrincipalKey((Profile item) => item.Id);
        }
    }
}
