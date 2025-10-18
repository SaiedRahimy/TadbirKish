using IdentityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using TadbirKish.DataReception.Domain.Entities;

namespace TadbirKish.DataReception.Infrastructure.Persistence.Configurations
{
    public class CoverageConfiguration : IEntityTypeConfiguration<Coverage>
    {
        public void Configure(EntityTypeBuilder<Coverage> builder)
        {
            builder.ToTable("Coverages", "Data");

            builder.HasKey(coverage => coverage.Id);
            builder.Property(coverage => coverage.Id).ValueGeneratedOnAdd();

            builder.Property(coverage => coverage.Name).IsRequired();
            builder.HasIndex(coverage => coverage.Name).IsUnique();

            builder.Property(coverage => coverage.Min).IsRequired();
            builder.Property(coverage => coverage.Max).IsRequired();
            builder.Property(coverage => coverage.Coefficient).IsRequired();

            builder.Property(coverage => coverage.Created).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
            builder.Property(coverage => coverage.LastModified).HasDefaultValueSql("getdate()").ValueGeneratedOnUpdate();

            builder.HasData(new List<Coverage>()
            {
                new Coverage()
                {
                    Id=1,
                    Name = "جراحی",
                    Min = 5000,
                    Max = 500000000,
                    Coefficient = 0.0052,
                },
                new Coverage()
                {
                    Id=2,
                    Name = "دندانپزشکی",
                    Min = 4000,
                    Max = 400000000,
                    Coefficient = 0.0042,
                },
                new Coverage()
                {
                    Id=3,
                    Name = "بستری",
                    Min = 2000,
                    Max = 200000000,
                    Coefficient = 0.005,
                }
            });
        }
    }
}
