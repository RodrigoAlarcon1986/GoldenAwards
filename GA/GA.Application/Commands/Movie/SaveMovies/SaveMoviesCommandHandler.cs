using GA.Domain.Services.Movies;
using MediatR;

namespace GA.Application.Commands.Movie.SaveMovies
{
    public class SaveMoviesCommandHandler : IRequestHandler<SaveMoviesCommand, bool>
    {
        private readonly IMoviesService moviesService;

        public SaveMoviesCommandHandler(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        public async Task<bool> Handle(SaveMoviesCommand request, CancellationToken cancellationToken)
        {
            return await moviesService.SaveMoviesAsync(request.PathToCsvFile, cancellationToken);
        }
    }
}
