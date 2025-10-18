using IdentityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using TadbirKish.DataReception.Domain.Entities;

namespace TadbirKish.DataReception.Infrastructure.Persistence.Configurations
{
    public class RequestCoverageConfiguration : IEntityTypeConfiguration<RequestCoverage>
    {
        public void Configure(EntityTypeBuilder<RequestCoverage> builder)
        {
            builder.ToTable("RequestCoverages", "Data");

            builder.HasKey(requestCoverage => requestCoverage.Id);
            builder.Property(requestCoverage => requestCoverage.Id).ValueGeneratedOnAdd();

            builder.Property(requestCoverage => requestCoverage.Name).IsRequired();
            builder.HasIndex(requestCoverage => requestCoverage.Name).IsUnique();
                      

            builder.Property(requestCoverage => requestCoverage.Created).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
            builder.Property(requestCoverage => requestCoverage.LastModified).HasDefaultValueSql("getdate()").ValueGeneratedOnUpdate();
                       
        }
    }
}
