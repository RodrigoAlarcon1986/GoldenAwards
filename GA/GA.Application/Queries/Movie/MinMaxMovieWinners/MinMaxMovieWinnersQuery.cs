using GA.Domain.Dtos.Movies;
using MediatR;

namespace GA.Application.Queries.Movie.MinMaxMovieWinners
{
    public class MinMaxMovieWinnersQuery : IRequest<MinMaxWinnersDto>
    {
    }
}
