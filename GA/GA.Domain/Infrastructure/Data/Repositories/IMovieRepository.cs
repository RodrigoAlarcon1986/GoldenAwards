using GA.Domain.Models.MoviesAggregation;

namespace GA.Domain.Infrastructure.Data.Repositories
{
    public interface IMovieRepository : IRepository<Movie, Guid>
    {
    }
}
