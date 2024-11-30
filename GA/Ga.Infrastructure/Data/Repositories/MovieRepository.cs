using Ga.Infrastructure.Data.Context;
using GA.Domain.Infrastructure.Data.Repositories;
using GA.Domain.Models.MoviesAggregation;

namespace Ga.Infrastructure.Data.Repositories
{
    public class MovieRepository : Repository<Movie, Guid, GaContext>, IMovieRepository
    {
        public MovieRepository(GaContext context) : base(context)
        {
        }
    }
}
