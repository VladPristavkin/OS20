using FilmoSearchPortal.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmoSearchPortal.Infrastructure.Configurations
{
    internal sealed class DirectorConfiguration : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
        {
            builder.ToTable(nameof(Director));

            builder.HasKey(dr => dr.Id);
            builder.Property(dr => dr.Id).HasColumnName("DirectorId").IsRequired();
            builder.HasIndex(dr => dr.Id).IsUnique();

            builder.Property(dr => dr.Name).IsRequired();

            builder.HasData(
             new Director { Id = 1, Name = "Steven Spielberg", Biography = "Biography of Steven Spielberg" },
             new Director { Id = 2, Name = "Christopher Nolan", Biography = "Biography of Christopher Nolan" }
         );
        }
    }
}
