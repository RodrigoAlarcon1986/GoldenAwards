using Ga.Infrastructure.Data.Mappings;
using GA.Domain.Models.MoviesAggregation;
using Microsoft.EntityFrameworkCore;

namespace Ga.Infrastructure.Data.Context
{
    public class GaContext : BaseDbContext<GaContext>
    {
        public GaContext(DbContextOptions<GaContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IDbMapping).Assembly);
        }
    }
}
