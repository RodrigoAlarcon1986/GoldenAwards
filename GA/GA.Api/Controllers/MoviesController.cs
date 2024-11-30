using GA.Application.Queries.Movie.MinMaxMovieWinners;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GA.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ISender sender;

        public MoviesController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpGet(nameof(GetMinMaxWinners))]
        public async Task<IActionResult> GetMinMaxWinners(CancellationToken cancellationToken)
        {
            var minMaxWinners = await sender.Send(new MinMaxMovieWinnersQuery(), cancellationToken);

            return Ok(minMaxWinners);
        }
    }
}
