using FilmoSearchPortal.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmoSearchPortal.Infrastructure.Configurations
{
    internal sealed class FilmConfiguration : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.ToTable(nameof(Film));

            builder.HasKey(fl => fl.Id);
            builder.Property(fl => fl.Id).HasColumnName("FilmId").IsRequired();
            builder.HasIndex(fl => fl.Id).IsUnique();

            builder.Property(fl => fl.Title).IsRequired();

            builder.Property(fl => fl.ReleaseYear).IsRequired();

            builder.Property(fl => fl.Duration).IsRequired();

            builder.Property(fl => fl.Rating).IsRequired();

            builder.HasOne(fl => fl.Director)
                .WithMany(dr => dr.Films)
                .HasForeignKey(fl => fl.DirectorId);

            builder.HasMany(fl => fl.Actors)
                .WithMany(ac => ac.Films)
                .UsingEntity(
                ac => ac.HasOne(typeof(Actor)).WithMany().HasForeignKey("ActorId"),
                fl => fl.HasOne(typeof(Film)).WithMany().HasForeignKey("FilmId"))
                .HasData(
                    new { FilmId = 1, ActorId = 1 },
                    new { FilmId = 1, ActorId = 2 },
                    new { FilmId = 2, ActorId = 3 });

            builder.HasMany(fl => fl.Genres)
              .WithMany(gr => gr.Films)
              .UsingEntity(
              gr => gr.HasOne(typeof(Genre)).WithMany().HasForeignKey("GenreId"),
              fl => fl.HasOne(typeof(Film)).WithMany().HasForeignKey("FilmId"))
              .HasData(
                  new { FilmId = 1, GenreId = 1 },
                  new { FilmId = 1, GenreId = 2 },
                  new { FilmId = 2, GenreId = 2 });

            builder.HasMany(fl => fl.Reviews)
                .WithOne(rv => rv.Film)
                .HasForeignKey(rv => rv.FilmId);

            builder.HasData(
               new Film { Id = 1, Title = "Avengers: Endgame", Description = "Description of Avengers: Endgame", ReleaseYear = 2019, Duration = 181, Rating = 8.4f, DirectorId = 1 },
               new Film { Id = 2, Title = "Inception", Description = "Description of Inception", ReleaseYear = 2010, Duration = 148, Rating = 8.8f, DirectorId = 2 }
           );
        }
    }
}
