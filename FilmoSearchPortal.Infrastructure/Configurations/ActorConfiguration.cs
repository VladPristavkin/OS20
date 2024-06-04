using FilmoSearchPortal.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmoSearchPortal.Infrastructure.Configurations
{
    internal sealed class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.ToTable(nameof(Actor));

            builder.HasKey(ac => ac.Id);
            builder.Property(ac => ac.Id).HasColumnName("ActorId").IsRequired();
            builder.HasIndex(ac => ac.Id).IsUnique();

            builder.Property(ac => ac.Name).IsRequired();

            builder.HasData(
               new Actor { Id = 1, Name = "Robert Downey Jr.", Biography = "Biography of Robert Downey Jr." },
               new Actor { Id = 2, Name = "Scarlett Johansson", Biography = "Biography of Scarlett Johansson" },
               new Actor { Id = 3, Name = "Chris Hemsworth", Biography = "Biography of Chris Hemsworth" }
           );
        }
    }
}
