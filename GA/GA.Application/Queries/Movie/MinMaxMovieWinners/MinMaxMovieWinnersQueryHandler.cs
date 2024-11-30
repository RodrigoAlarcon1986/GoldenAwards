using Ga.Infrastructure.Data.Context;
using GA.Domain.Dtos.Movies;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GA.Application.Queries.Movie.MinMaxMovieWinners
{
    public class MinMaxMovieWinnersQueryHandler : IRequestHandler<MinMaxMovieWinnersQuery, MinMaxWinnersDto>
    {
        private readonly GaContext gaContext;

        public MinMaxMovieWinnersQueryHandler(GaContext gaContext)
        {
            this.gaContext = gaContext;
        }

        private static WinnerDto GetMinWinIntervalForProducer(string producer, IEnumerable<int> years)
        {
            var orderedYears = years.OrderBy(x => x).ToList();
            var minInterval = int.MaxValue;
            var previousWin = 0;
            var followingWin = 0;

            for (int i = 0; i < orderedYears.Count - 1; i++)
            {
                var interval = orderedYears[i + 1] - orderedYears[i];
                if (interval < minInterval)
                {
                    minInterval = interval;
                    previousWin = orderedYears[i];
                    followingWin = orderedYears[i + 1];
                }
            }

            return new WinnerDto(producer, minInterval, previousWin, followingWin);
        }

        private static WinnerDto GetMaxWinIntervalForProducer(string producer, IEnumerable<int> years)
        {
            var orderedYears = years.OrderBy(x => x).ToList();
            var maxInterval = int.MinValue;
            var previousWin = 0;
            var followingWin = 0;

            for (int i = 0; i < orderedYears.Count - 1; i++)
            {
                var interval = orderedYears[i + 1] - orderedYears[i];
                if (interval > maxInterval)
                {
                    maxInterval = interval;
                    previousWin = orderedYears[i];
                    followingWin = orderedYears[i + 1];
                }
            }

            return new WinnerDto(producer, maxInterval, previousWin, followingWin);
        }

        public async Task<MinMaxWinnersDto> Handle(MinMaxMovieWinnersQuery request, CancellationToken cancellationToken)
        {
            var winners = await gaContext.Movies
                                .Where(movie => movie.IsWinner.HasValue && movie.IsWinner.Value)
                                .GroupBy(movie => movie.Producers)
                                .Where(movie => movie.Select(x => x.Year).Count() > 1)
                                .Select(g => new
                                {
                                    MinWin = GetMinWinIntervalForProducer(g.Key, g.Select(x => x.Year)),
                                    MaxWin = GetMaxWinIntervalForProducer(g.Key, g.Select(x => x.Year))
                                })
                                .ToListAsync(cancellationToken);

            var minDate = winners.Min(x => x.MinWin.Interval);
            var maxDate = winners.Max(x => x.MaxWin.Interval);

            var minWinners = winners.Where(x => x.MinWin.Interval == minDate).Select(x => x.MinWin).ToList();
            var maxWinners = winners.Where(x => x.MaxWin.Interval == maxDate).Select(x => x.MaxWin).ToList();

            return new MinMaxWinnersDto(minWinners, maxWinners);
        }
    }
}
