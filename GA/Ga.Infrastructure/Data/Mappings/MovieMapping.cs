using GA.Domain.Models.MoviesAggregation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ga.Infrastructure.Data.Mappings
{
    internal class MovieMapping : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(x => x.Year).HasColumnType("smallint").IsRequired();
            builder.Property(x => x.Title).HasColumnType("varchar(200)").IsRequired();
            builder.Property(x => x.Studios).HasColumnType("varchar(200)").IsRequired();
            builder.Property(x => x.Producers).HasColumnType("varchar(200)");

            builder.ToTable(nameof(Movie));
        }
    }
}
