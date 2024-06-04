using FilmoSearchPortal.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmoSearchPortal.Infrastructure.Configurations
{
    internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable(nameof(Review));

            builder.HasKey(rv => rv.Id);
            builder.Property(rv => rv.Id).HasColumnName("ReviewId").IsRequired();
            builder.HasIndex(rv => rv.Id).IsUnique();

            builder.Property(rv => rv.Comment).IsRequired();

            builder.Property(vac => vac.Date).IsRequired();
            builder.Property(vac => vac.Date).HasConversion(v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            builder.HasOne(rv => rv.User)
                .WithMany(us => us.Reviews)
                .HasForeignKey(rv => rv.UserId);
        }
    }
}
