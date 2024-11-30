using GA.Domain.Infrastructure.Data.Repositories;
using GA.Domain.Models.MoviesAggregation;

namespace GA.Domain.Services.Movies
{
    public interface IMoviesService
    {
        Task<bool> SaveMoviesAsync(IEnumerable<Movie> movies, CancellationToken cancellationToken);
        Task<bool> SaveMoviesAsync(string pathToCsv, CancellationToken cancellationToken);
    }

    public class MoviesService : IMoviesService
    {
        private readonly IMovieRepository movieRepository;

        public MoviesService(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task<bool> SaveMoviesAsync(IEnumerable<Movie> movies, CancellationToken cancellationToken)
        {
            await movieRepository.InserirAsync(movies, cancellationToken);

            var executed = await movieRepository.UnitOfWork.CommitAsync(cancellationToken);

            return executed > 0;
        }

        public async Task<bool> SaveMoviesAsync(string pathToCsv, CancellationToken cancellationToken)
        {
            var dataLines = (await File.ReadAllLinesAsync(pathToCsv, cancellationToken)).Skip(1);
            var movies = new List<Movie>();

            foreach (var line in dataLines)
            {
                var movieLine = line.Split(";");
                var movie = new Movie
                {
                    Year = int.Parse(movieLine[0]),
                    Title = movieLine[1],
                    Studios = movieLine[2],
                    Producers = movieLine[3],
                    IsWinner = movieLine[4] == "yes"
                };

                movies.Add(movie);
            }

            return await SaveMoviesAsync(movies, cancellationToken);
        }
    }
}
