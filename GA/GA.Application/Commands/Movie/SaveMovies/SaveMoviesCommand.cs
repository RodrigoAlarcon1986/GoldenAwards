using GA.Domain.Dtos.Movies;
using MediatR;

namespace GA.Application.Commands.Movie.SaveMovies
{
    public class SaveMoviesCommand : IRequest<bool>
    {
        public string PathToCsvFile { get; set; } = string.Empty;
    }
}
