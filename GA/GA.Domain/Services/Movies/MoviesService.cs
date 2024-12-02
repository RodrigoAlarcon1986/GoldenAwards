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
            var movies = dataLines
                            .SelectMany(line =>
                            {
                                var movieLine = line.Split(';');
                                return GetProducers(movieLine[3])
                                    .Select(producer => new Movie
                                    {
                                        Year = int.Parse(movieLine[0]),
                                        Title = movieLine[1],
                                        Studios = movieLine[2],
                                        Producers = producer.Trim(),
                                        IsWinner = movieLine[4] == "yes"
                                    });
                            })
                            .ToList();

            return await SaveMoviesAsync(movies, cancellationToken);
        }

        private static string[] GetProducers(string producers)
        {
            return producers.Replace(" and ", ", ").Trim().Split(',');
        }
    }
}
