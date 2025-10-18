using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TadbirKish.DataReception.Domain.Entities;

namespace TadbirKish.DataReception.Infrastructure.Persistence.Configurations
{
    public class RequestCoverageDetailsConfiguration : IEntityTypeConfiguration<RequestCoverageDetails>
    {
        public void Configure(EntityTypeBuilder<RequestCoverageDetails> builder)
        {
            builder.ToTable("RequestCoverageDetails", "Data");

            builder.HasKey(requestCoverageDetail => requestCoverageDetail.Id);
            builder.Property(requestCoverageDetail => requestCoverageDetail.Id).ValueGeneratedOnAdd();

            builder.Property(requestCoverageDetail => requestCoverageDetail.RequestCoverageId).IsRequired();
            builder.Property(requestCoverageDetail => requestCoverageDetail.CoverageId).IsRequired();
            builder.HasIndex(requestCoverageDetail => new { requestCoverageDetail.RequestCoverageId, requestCoverageDetail.CoverageId }).IsUnique();

            builder.Property(requestCoverageDetail => requestCoverageDetail.GrossPremium).IsRequired();
            builder.Property(requestCoverageDetail => requestCoverageDetail.NetPremium).IsRequired();

            builder.HasOne(e => e.Coverage)
               .WithMany(e => e.AllRequestCoverageDetails)
               .HasForeignKey(e => e.CoverageId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.RequestCoverage)
               .WithMany(e => e.AllRequestCoverageDetails)
               .HasForeignKey(e => e.RequestCoverageId)
               .OnDelete(DeleteBehavior.Restrict);


            builder.Property(requestCoverageDetail => requestCoverageDetail.Created).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
            builder.Property(requestCoverageDetail => requestCoverageDetail.LastModified).HasDefaultValueSql("getdate()").ValueGeneratedOnUpdate();

        }
    }
}
