using FilmoSearchPortal.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmoSearchPortal.Infrastructure.Configurations
{
    internal sealed class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable(nameof(Genre));

            builder.HasKey(gr => gr.Id);
            builder.Property(gr => gr.Id).HasColumnName("GenreId").IsRequired();
            builder.HasIndex(gr => gr.Id).IsUnique();

            builder.Property(gr=>gr.Name).IsRequired();

            builder.HasData(
               new Genre { Id = 1, Name = "Action", Description = "Action movies" },
               new Genre { Id = 2, Name = "Sci-Fi", Description = "Science Fiction movies" }
           );
        }
    }
}
